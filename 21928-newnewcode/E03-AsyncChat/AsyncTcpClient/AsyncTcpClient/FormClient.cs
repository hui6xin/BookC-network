using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;
namespace AsyncTcpClient
{
    public partial class FormClient : Form
    {
        //是否正常退出
        private bool isExit = false;
        private TcpClient client;
        private BinaryReader br;
        private BinaryWriter bw;
        //BackgroundWorker connectWork = new BackgroundWorker();
        public FormClient()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            Random r = new Random((int)DateTime.Now.Ticks);
            textBoxUserName.Text = "user" + r.Next(100, 999);
            listBoxOnline.HorizontalScrollbar = true;
            //connectWork.DoWork += new DoWorkEventHandler(connectWork_DoWork);
            //connectWork.RunWorkerCompleted += 
            //    new RunWorkerCompletedEventHandler(connectWork_RunWorkerCompleted);
            //ConnectToServer();
        }
        /// <summary>异步方式与服务器进行连接</summary>
        void ConnectToServer()
        {
            client = new TcpClient();
            //此处仅为方便本机调试，实际使用时要将Dns.GetHostName()改为服务器域名
            IAsyncResult result = client.BeginConnect(Dns.GetHostName(), 51888, null, null);
            while (result.IsCompleted == false)
            {
                Thread.Sleep(100);
                AddStatus(".");
            }
            try
            {
                client.EndConnect(result);
                AddStatus("连接成功");
                //获取网络流
                NetworkStream networkStream = client.GetStream();
                //将网络流作为二进制读写对象
                br = new BinaryReader(networkStream);
                bw = new BinaryWriter(networkStream);
                AsyncSendMessage("Login," + textBoxUserName.Text);
                Thread threadReceive = new Thread(new ThreadStart(ReceiveData));
                threadReceive.IsBackground = true;
                threadReceive.Start();
            }
            catch (Exception ex)
            {
                AddStatus("连接失败:" + ex.Message);
                buttonConnect.Enabled = true;
            }
        }

        /// <summary>
        /// 【连接服务器】按钮的Click事件
        /// </summary>
        private void buttonConnect_Click(object sender, EventArgs e)
        {
            buttonConnect.Enabled = false;
            AddStatus("开始连接.");
            ConnectToServer();
        }

        /// <summary>处理接收的服务器端数据</summary>
        private void ReceiveData()
        {
            string receiveString = null;
            while (isExit == false)
            {
                ReceiveMessageDelegate d = new ReceiveMessageDelegate(ReceiveMessage);
                IAsyncResult result = d.BeginInvoke(out receiveString, null, null);
                //使用轮询方式来判断异步操作是否完成
                while (result.IsCompleted == false)
                {
                    if (isExit)
                    {
                        break;
                    }
                    Thread.Sleep(250);
                }
                //获取Begin方法的返回值和所有输入/输出参数
                d.EndInvoke(out receiveString, result);
                if(receiveString==null)
                {
                    if (isExit == false)
                    {
                        MessageBox.Show("与服务器失去联系。");
                    }
                    break;
                }
                string[] splitString = receiveString.Split(',');
                string command = splitString[0].ToLower();
                switch (command)
                {
                    case "login":  //格式：login,用户名
                        AddOnline(splitString[1]);
                        break;
                    case "logout":  //格式：logout,用户名
                        RemoveUserName(splitString[1]);
                        break;
                    case "talk":  //格式：talk,用户名,对话信息
                        AddTalkMessage(splitString[1] + "：\r\n");
                        AddTalkMessage(receiveString.Substring(
                            splitString[0].Length + splitString[1].Length + 2));
                        break;
                }
            }
            Application.Exit();
        }
        /// <summary>发送信息状态的数据结构</summary>
        private struct SendMessageStates
        {
            public SendMessageDelegate d;
            public IAsyncResult result;
        }

        /// <summary>异步向服务器端发送数据</summary>
        private void AsyncSendMessage(string message)
        {
            SendMessageDelegate d = new SendMessageDelegate(SendMessage);
            IAsyncResult result = d.BeginInvoke(message, null, null);
            while (result.IsCompleted == false)
            {
                if (isExit)
                {
                    return;
                }
                Thread.Sleep(50);
            }
            SendMessageStates states = new SendMessageStates();
            states.d = d;
            states.result = result;
            Thread t = new Thread(FinishAsyncSendMessage);
            t.IsBackground = true;
            t.Start(states);
        }
        /// <summary>处理接收的服务器端数据</summary>
        private void FinishAsyncSendMessage(object obj)
        {
            SendMessageStates states = (SendMessageStates)obj;
            states.d.EndInvoke(states.result);
        }
        delegate void SendMessageDelegate(string message);
        /// <summary>向服务器端发送数据</summary>
        private void SendMessage(string message)
        {
            try
            {
                bw.Write(message);
                bw.Flush();
            }
            catch
            {
                AddStatus("发送失败!");
            }
        }
        /// <summary>【发送】按钮的Click事件</summary>
        private void buttonSend_Click(object sender, EventArgs e)
        {
            if (listBoxOnline.SelectedIndex != -1)
            {
                AsyncSendMessage("Talk," + listBoxOnline.SelectedItem + "," + textBoxSend.Text+"\r\n");
                textBoxSend.Clear();
            }
            else
            {
                MessageBox.Show("请先在[当前在线]中选择一个对话者");
            }
        }

        delegate void ReceiveMessageDelegate(out string receiveMessage);
        /// <summary>读取服务器发过来的信息</summary>
        private void ReceiveMessage(out string receiveMessage)
        {
            receiveMessage = null;
            try
            {
                receiveMessage = br.ReadString();
            }
            catch(Exception ex)
            {
                AddStatus(ex.Message);
            }
        }
        private delegate void AddTalkMessageDelegate(string message);
        /// <summary>向richTextBoxTalkInfo中添加聊天记录</summary>
        private void AddTalkMessage(string message)
        {
            if (richTextBoxTalkInfo.InvokeRequired)
            {
                AddTalkMessageDelegate d = new AddTalkMessageDelegate(AddTalkMessage);
                richTextBoxTalkInfo.Invoke(d, new object[] { message });
            }
            else
            {
                richTextBoxTalkInfo.AppendText(message);
                richTextBoxTalkInfo.ScrollToCaret();
            }
        }
        private delegate void AddStatusDelegate(string message);
        /// <summary>向richTextBoxStatus中添加状态信息</summary>
        private void AddStatus(string message)
        {
            if (richTextBoxStatus.InvokeRequired)
            {
                AddStatusDelegate d = new AddStatusDelegate(AddStatus);
                richTextBoxStatus.Invoke(d, new object[] { message });
            }
            else
            {
                richTextBoxStatus.AppendText(message);
            }
        }
        private delegate void AddOnlineDelegate(string message);
        /// <summary>向listBoxOnline添加在线用户</summary>
        private void AddOnline(string message)
        {
            if (listBoxOnline.InvokeRequired)
            {
                AddOnlineDelegate d = new AddOnlineDelegate(AddOnline);
                listBoxOnline.Invoke(d, new object[] { message });
            }
            else
            {
                listBoxOnline.Items.Add(message);
                listBoxOnline.SelectedIndex = listBoxOnline.Items.Count - 1;
                listBoxOnline.ClearSelected();
            }
        }
        private delegate void RemoveUserNameDelegate(string userName);
        /// <summary>从listBoxOnline删除离线用户</summary>
        private void RemoveUserName(string userName)
        {
            if (listBoxOnline.InvokeRequired)
            {
                RemoveUserNameDelegate d = RemoveUserName;
                listBoxOnline.Invoke(d, userName);
            }
            else
            {
                listBoxOnline.Items.Remove(userName);
                listBoxOnline.SelectedIndex = listBoxOnline.Items.Count - 1;
                listBoxOnline.ClearSelected();
            }
        }
        /// <summary>关闭窗口时触发的事件</summary>
        private void FormClient_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (client != null)
            {
                isExit = true;
                if (client.Client.Connected)
                {
                    AsyncSendMessage("Logout," + textBoxUserName.Text);
                    br.Close();
                    bw.Close();
                }
                client.Close();
            }
        }
    }
}
