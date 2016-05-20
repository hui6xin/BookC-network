using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using System.Security.Cryptography;
namespace EncryptTcpServer
{
    public partial class MainForm : Form
    {
        //连接的用户
        System.Collections.Generic.List<User> userList = new List<User>();
        private delegate void SetListBoxCallback(string str);
        private SetListBoxCallback setListBoxCallback;
        private delegate void SetComboBoxCallback(User user);
        private SetComboBoxCallback setComboBoxCallback;
        //使用的本机IP地址
        IPAddress localAddress;
        //监听端口
        private int port = 51888;
        private TcpListener myListener;
        public MainForm()
        {
            InitializeComponent();
            listBoxStatus.HorizontalScrollbar = true;
            setListBoxCallback = new SetListBoxCallback(SetListBox);
            setComboBoxCallback = new SetComboBoxCallback(AddComboBoxitem);
            IPAddress[] addrIP = Dns.GetHostAddresses(Dns.GetHostName());
            localAddress = addrIP[0];
            buttonStop.Enabled = false;
        }
        /// <summary>
        /// 【开始监听】按钮的Click事件
        /// </summary>     
        private void buttonStart_Click(object sender, EventArgs e)
        {
            myListener = new TcpListener(localAddress, port);
            myListener.Start();
            SetListBox(string.Format("开始在{0}:{1}监听客户连接", localAddress, port));
            //创建一个线程监听客户端连接请求
            ThreadStart ts = new ThreadStart(ListenClientConnect);
            Thread myThread = new Thread(ts);
            myThread.Start();
            buttonStart.Enabled = false;
            buttonStop.Enabled = true;
        }
        /// <summary>
        /// 接收客户端连接的线程
        /// </summary>
        private void ListenClientConnect()
        {
            while (true)
            {
                TcpClient newClient = null;
                try
                {
                    //等待用户进入
                    newClient = myListener.AcceptTcpClient();
                }
                catch
                {
                    //当单击“停止监听”或者退出此窗体时AcceptTcpClient()会产生异常
                    //因此可以利用此异常退出循环
                    break;
                }
                //每接受一个客户端连接,就创建一个对应的线程循环接收该客户端发来的信息
                ParameterizedThreadStart pts = new ParameterizedThreadStart(ReceiveData);
                Thread threadReceive = new Thread(pts);
                User user = new User(newClient);
                threadReceive.Start(user);
                userList.Add(user);
                AddComboBoxitem(user);
                SetListBox(string.Format("[{0}]进入", newClient.Client.RemoteEndPoint));
                SetListBox(string.Format("当前连接用户数：{0}", userList.Count));
            }
        }
        /// <summary>
        /// 接收、处理客户端信息的线程，每客户1个线程
        /// </summary>
        /// <param name="obj">客户端信息</param>
        private void ReceiveData(object obj)
        {
            User user = (User)obj;
            TcpClient client = user.client;
            //是否正常退出接收线程
            bool normalExit = false;
            //用于控制是否退出循环
            bool exitWhile = false;
            while (exitWhile == false)
            {
                //保存接收的命令字符串
                string receiveString = null;
                //解析命令用
                //每条命令均带有一个参数，值为true或者false，表示是否有紧跟的字节数组
                string[] splitString = null;
                byte[] receiveBytes = null;
                try
                {
                    //从网络流中读出命令字符串
                    //此方法会自动判断字符串长度前缀，并根据长度前缀读出字符串
                    receiveString = user.br.ReadString();
                    splitString = receiveString.Split(',');
                    if (splitString[1] == "true")
                    {
                        //先从网络流中读出32位的长度前缀
                        int bytesLength = user.br.ReadInt32();
                        //然后读出指定长度的内容保存到字节数组中
                        receiveBytes = user.br.ReadBytes(bytesLength);
                    }
                }
                catch
                {
                    //底层套接字不存在时会出现异常
                    SetListBox("接收数据失败");
                }
                if (receiveString == null)
                {
                    if (normalExit == false)
                    {
                        //如果停止了监听，Connected为false
                        if (client.Connected == true)
                        {
                            SetListBox(string.Format(
                                "与[{0}]失去联系，已终止接收该用户信息", client.Client.RemoteEndPoint));
                        }
                    }
                    break;
                }
                SetListBox(string.Format("来自[{0}]：{1}", user.client.Client.RemoteEndPoint, receiveString));
                if (receiveBytes != null)
                {
                    SetListBox(string.Format("来自[{0}]：{1}",
                        user.client.Client.RemoteEndPoint, Encoding.Default.GetString(receiveBytes)));
                }
                switch (splitString[0])
                {
                    case "rsaPublicKey":
                        //使用传递过来的公钥重新初始化该客户端对应的RSACryptoServiceProvider对象
                        //然后就可以使用这个对象加密对称加密的私钥了
                        user.rsa.FromXmlString(Encoding.Default.GetString(receiveBytes));
                        //加密对称加密的私钥
                        try
                        {
                            //使用RSA算法加密对称加密算法的私钥Key
                            byte[] encryptedKey = user.rsa.Encrypt(user.tdes.Key, false);
                            SendToClient(user, "tdesKey,true", encryptedKey);
                            //加密IV
                            byte[] encryptedIV = user.rsa.Encrypt(user.tdes.IV, false);
                            SendToClient(user, "tdesIV,true", encryptedIV);
                        }
                        catch (Exception err)
                        {
                            MessageBox.Show(err.Message);
                        }
                        break;
                    case "Logout":
                        //格式：Logout
                        SetListBox(string.Format("[{0}]退出", user.client.Client.RemoteEndPoint));
                        normalExit = true;
                        exitWhile = true;
                        break;
                    case "Talk":
                        //解密
                        string talkString = DecryptText(receiveBytes, user.tdes.Key, user.tdes.IV);
                        if (talkString != null)
                        {
                            SetListBox(string.Format("[{0}]说：{1}", client.Client.RemoteEndPoint, talkString));
                        }
                        break;
                    default:
                        SetListBox("什么意思啊：" + receiveString);
                        break;
                }
            }
            userList.Remove(user);
            client.Close();
            SetListBox(string.Format("当前连接用户数：{0}", userList.Count));
        }
        /// <summary>
        /// 使用对称加密加密字符串
        /// </summary>
        /// <param name="str">要加密的内容</param>
        /// <param name="Key">密钥</param>
        /// <param name="IV">初始化向量</param>
        /// <returns>加密后的结果</returns>
        private byte[] EncryptText(string str, byte[] Key, byte[] IV)
        {
            //创建一个内存流
            MemoryStream memoryStream = new MemoryStream();
            //使用传递的私钥和IV创建加密流
            CryptoStream cryptoStream = new CryptoStream(memoryStream,
                new TripleDESCryptoServiceProvider().CreateEncryptor(Key, IV),
                CryptoStreamMode.Write);
            //将传递的字符串转换为字节数组
            byte[] toEncrypt = Encoding.UTF8.GetBytes(str);
            try
            {
                //将字节数组写入加密流,并清除缓冲区
                cryptoStream.Write(toEncrypt, 0, toEncrypt.Length);
                cryptoStream.FlushFinalBlock();
                //得到加密后的字节数组
                byte[] encryptedBytes = memoryStream.ToArray();
                return encryptedBytes;
            }
            catch (Exception err)
            {
                SetListBox("加密出错：" + err.Message);
                return null;
            }
            finally
            {
                cryptoStream.Close();
                memoryStream.Close();
            }
        }
        /// <summary>
        /// 使用对称加密算法解密接收的字符串
        /// </summary>
        /// <param name="dataBytes">被解密的内容</param>
        /// <param name="Key">密钥</param>
        /// <param name="IV">初始化向量</param>
        /// <returns>解密后的结果</returns>
        private string DecryptText(byte[] dataBytes, byte[] Key, byte[] IV)
        {
            //根据加密后的字节数组创建一个内存流
            MemoryStream memoryStream = new MemoryStream(dataBytes);
            //使用传递的私钥、IV和内存流创建解密流
            CryptoStream cryptoStream = new CryptoStream(memoryStream,
                new TripleDESCryptoServiceProvider().CreateDecryptor(Key, IV),
                CryptoStreamMode.Read);
            //创建一个字节数组保存解密后的数据
            byte[] decryptBytes = new byte[dataBytes.Length];
            try
            {
                //从解密流中将解密后的数据读到字节数组中
                cryptoStream.Read(decryptBytes, 0, decryptBytes.Length);
                //得到解密后的字符串
                string decryptedString = Encoding.UTF8.GetString(decryptBytes);
                return decryptedString;
            }
            catch (Exception err)
            {
                SetListBox("解密出错：" + err.Message);
                return null;
            }
            finally
            {
                cryptoStream.Close();
                memoryStream.Close();
            }
        }
        /// <summary>
        /// 发送信息到客户端
        /// </summary>
        /// <param name="user">指定客户端</param>
        /// <param name="command">命令</param>
        /// <param name="bytes">加密后信息</param>
        private void SendToClient(User user, string command, byte[] bytes)
        {
            //每条命令均带有一个参数，值为true或者false，表示是否有紧跟的字节数组
            string[] splitCommand = command.Split(',');
            try
            {
                //先将命令字符串写入网络流，此方法会自动附加字符串长度前缀
                user.bw.Write(command);
                SetListBox(string.Format("向[{0}]发送：{1}", user.client.Client.RemoteEndPoint, command));
                if (splitCommand[1] == "true")
                {
                    //先将字节数组的长度（32位整数）写入网络流
                    user.bw.Write(bytes.Length);
                    //然后将字节数组写入网络流
                    user.bw.Write(bytes);
                    user.bw.Flush();
                    SetListBox(string.Format("向[{0}]发送：{1}",
                        user.client.Client.RemoteEndPoint, Encoding.UTF8.GetString(bytes)));
                    if (splitCommand[0] == "Talk")
                    {
                        SetListBox("加密前内容：" + textBoxSend.Text);
                    }
                }
            }
            catch
            {
                SetListBox(string.Format("向[{0}]发送信息失败", user.client.Client.RemoteEndPoint));
            }
        }
        /// <summary>
        /// 向comboBoxReceiver中添加客户端选项
        /// </summary>
        /// <param name="user">客户端信息</param>
        private void AddComboBoxitem(User user)
        {
            if (comboBoxReceiver.InvokeRequired == true)
            {
                this.Invoke(setComboBoxCallback, user);
            }
            else
            {
                comboBoxReceiver.Items.Add(user.client.Client.RemoteEndPoint);
            }
        }
        /// <summary>
        /// 向listBoxStatus中写入状态信息
        /// </summary>
        /// <param name="str">要显示的信息</param>
        private void SetListBox(string str)
        {
            if (listBoxStatus.InvokeRequired == true)
            {
                this.Invoke(setListBoxCallback, str);
            }
            else
            {
                listBoxStatus.Items.Add(str);
                listBoxStatus.SelectedIndex = listBoxStatus.Items.Count - 1;
                listBoxStatus.ClearSelected();
            }
        }
        /// <summary>
        /// 单击停止监听按钮触发的事件
        /// </summary>
        private void buttonStop_Click(object sender, EventArgs e)
        {
            SetListBox(string.Format("目前连接用户数：{0}", userList.Count));
            SetListBox("开始停止服务，并依次使用户退出!");
            for (int i = 0; i < userList.Count; i++)
            {
                comboBoxReceiver.Items.Remove(userList[i].client.Client.RemoteEndPoint);
                userList[i].bw.Close();
                userList[i].br.Close();
                userList[i].client.Close();
            }
            //通过停止监听让myListener.AcceptTcpClient()产生异常退出监听线程
            myListener.Stop();
            buttonStart.Enabled = true;
            buttonStop.Enabled = false;
        }
        /// <summary>
        /// 单击【发送】按钮的Click事件
        /// </summary>
        private void buttonSend_Click(object sender, EventArgs e)
        {
            int index = comboBoxReceiver.SelectedIndex;
            if (index == -1)
            {
                MessageBox.Show("请先选择接收方，然后再单击［发送］");
            }
            else
            {
                User user = (User)userList[index];
                //加密textBoxSend.Text的内容
                byte[] encryptedBytes = EncryptText(textBoxSend.Text, user.tdes.Key, user.tdes.IV);
                if (encryptedBytes != null)
                {
                    SendToClient(user, "Talk,true", encryptedBytes);
                    textBoxSend.Clear();
                }
            }
        }
        private void FormServer_FormClosing(object sender, FormClosingEventArgs e)
        {
            //未单击开始监听就直接退出时，myListener为null
            if (myListener != null)
            {
                buttonStop_Click(null, null);
            }
        }
    }
}
