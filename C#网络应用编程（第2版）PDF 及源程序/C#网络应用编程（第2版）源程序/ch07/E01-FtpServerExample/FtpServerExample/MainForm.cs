using System;
using System.Collections.Generic;
using System.Windows.Forms;
//新添加的命名空间
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using System.Globalization;
namespace FtpServerExample
{
    public partial class MainForm : Form
    {      
        TcpListener myTcpListener;
        Dictionary<string, string> users;  //保存用户名和密码      
        public MainForm()
        {
            InitializeComponent();
            //为简单起见，此处假设已经有MyTestName用户,密码12345
            users = new Dictionary<string, string>();
            users.Add("mytestName", "12345");
            //设置默认主目录
            textBox1.Text = "e:/ls/";
        }
        /// <summary>单击【启动FTP服务】触发的事件</summary>
        private void buttonStart_Click(object sender, EventArgs e)
        {
            this.listBoxStatus.Items.Add("FTP服务已启动");
            Thread t = new Thread(ListenClientConnect);
            t.IsBackground = true;
            t.Start();
            buttonStart.Enabled = false;
            buttonStop.Enabled = true;
        }
        /// <summary>监听端口，处理客户端连接</summary>
        private void ListenClientConnect()
        {
            myTcpListener = new TcpListener(IPAddress.Any, 21);
            myTcpListener.Start();
            while (true)
            {
                try
                {
                    TcpClient client = myTcpListener.AcceptTcpClient();
                    AddInfo(string.Format("{0}和本机({1})建立FTP连接", client.Client.RemoteEndPoint, myTcpListener.LocalEndpoint));
                    User user = new User();
                    user.commandSession = new UserSession(client);
                    user.workDir = textBox1.Text;
                    Thread t = new Thread(UserProcessing);
                    t.IsBackground = true;
                    t.Start(user);
                }
                catch
                {
                    break;
                }
            }
        }
        /// <summary>
        /// 处理USER命令，但不进行用户名验证
        /// </summary>
        /// <param name="user">客户端信息</param>
        /// <param name="command">命令</param>
        /// <param name="param">参数</param>
        private void CommandUser(User user, string command, string param)
        {
            string sendString = string.Empty;
            if (command == "USER")
            {
                sendString = "331 USER command OK, password required.";
                user.userName = param;
                user.LoginOK = 1;  //1表示已接收到用户名，等待接收密码
            }
            else
            {
                sendString = "501 USER command syntax error.";
            }
            ReplyCommandToUser(user, sendString);
        }  
        /// <summary>
        /// 处理密码命令，验证用户名和密码
        /// </summary>
        /// <param name="user">客户端信息</param>
        /// <param name="command">命令</param>
        /// <param name="param">参数</param>
        private void CommandPassword(User user, string command, string param)
        {
            string sendString = string.Empty;
            if (command == "PASS")
            {
                string password = null;
                if (users.TryGetValue(user.userName, out password))
                {
                    if (password == param)
                    {
                        sendString = "230 User logged in success";
                        user.LoginOK = 2;  //2表示登录成功
                    }
                    else
                        sendString = "530 Password incorrect.";
                }
                else
                {
                    sendString = "530 User name or password incorrect.";
                }
            }
            else
            {
                sendString = "501 PASS command Syntax error.";
            }
            ReplyCommandToUser(user, sendString);
            //用户当前工作目录
            user.CurrentDir = user.workDir;
        }
        /// <summary>
        /// 处理CWD命令，改变工作目录
        /// </summary>
        /// <param name="user">客户端信息</param>
        /// <param name="temp">目录路径部分信息</param>
        private void CommandCWD(User user, string temp)
        {     
            string sendString = string.Empty;
            try
            {
                string dir = user.workDir.TrimEnd('/') + temp;
                //是当前目录的子目录，且不包含父目录名称
                if (Directory.Exists(dir))
                {
                    user.CurrentDir = dir;
                    sendString = "250 Directory changed to '" + dir + "' successfully";
                }
                else
                {
                    sendString = "550 Directory '" + dir + "' does not exist";
                }
            }
            catch
            {
                sendString = "502 Directory changed unsuccessfully";
            }
            ReplyCommandToUser(user, sendString);
        }
        /// <summary>
        /// 增加尾缀
        /// </summary>
        /// <param name="s">要增加的尾缀</param>
        /// <returns></returns>
        private String AddEnd(String s)
        {
            if (!s.EndsWith("/"))
                s += "/";
            return s;
        }
        /// <summary>
        /// 处理PWD命令，显示工作目录
        /// </summary>
        /// <param name="user">客户端信息</param>
        private void CommandPWD(User user)
        {
            string sendString = string.Empty;
            sendString = "257 '" + user.CurrentDir + "' is the current directory";
            ReplyCommandToUser(user, sendString);
        }
        /// <summary>
        /// 处理PASV命令，设置数据传输模式
        /// </summary>
        /// <param name="user">客户端信息</param>
        private void CommandPASV(User user)
        {
            string sendString = string.Empty;
            IPAddress localIP = Dns.GetHostEntry("").AddressList[0];
            //被动模式
            Random random = new Random();
            int randNum1, randNum2, port;
            while (true)
            {
                randNum1 = random.Next(5, 200);
                randNum2 = random.Next(0, 200);
                port = (randNum1 << 8) | randNum2;
                try
                {
                    user.dataListener = new TcpListener(localIP, port);
                    AddInfo("被动模式--" + localIP.ToString() + ":" + port);
                }
                catch
                {
                    continue;
                }
                user.isPassive = true;
                string tmp = localIP.ToString().Replace('.', ',');
                sendString = "227 Entering Passive Mode (" + tmp + "," + randNum1 + "," + randNum2 + ")";
                ReplyCommandToUser(user, sendString);
                user.dataListener.Start();
                break;
            }
        }
        /// <summary>
        /// 处理PORT命令,使用主动模式进行传输，获取客户端发过来的数据连接ip及端口信息
        /// </summary>
        /// <param name="user">客户端信息</param>
        /// <param name="portString">端口信息</param>
        private void CommandPORT(User user, string portString)
        {
            string sendString = string.Empty;
            String[] tmp = portString.Split(',');
            String ipString = "" + tmp[0] + "." + tmp[1] + "." + tmp[2] + "." + tmp[3];
            int portNum = (int.Parse(tmp[4]) << 8) | int.Parse(tmp[5]);
            user.remoteEndPoint = new IPEndPoint(IPAddress.Parse(ipString), portNum);
            sendString = "200 PORT command successful.";
            ReplyCommandToUser(user, sendString);
        }
        /// <summary>
        /// 处理LIST命令，向客户端发送当前或指定工作目录下的所有文件名和子目录名
        /// </summary>
        /// <param name="user">客户端信息</param>
        /// <param name="parameter">指定工作目录</param>
        private void CommandLIST(User user, string parameter)
        {
            string sendString = string.Empty;
            DateTimeFormatInfo m = new CultureInfo("en-US", true).DateTimeFormat;
            //得到目录列表
            string[] dir = Directory.GetDirectories(user.CurrentDir);
            if (string.IsNullOrEmpty(parameter) == false)
            {
                if (Directory.Exists(user.CurrentDir + parameter))
                {
                    dir = Directory.GetDirectories(user.CurrentDir + parameter);
                }
                else
                {
                    string s = user.CurrentDir.TrimEnd('/');
                    user.CurrentDir = s.Substring(0, s.LastIndexOf("/") + 1);
                }
            }
            for (int i = 0; i < dir.Length; i++)
            {
                string folderName = Path.GetFileName(dir[i]);
                DirectoryInfo d = new DirectoryInfo(dir[i]);
                //为了能用CuteFTP客户端测试本程序，按下面的格式输出目录列表
                sendString += @"dwr-\t" + Dns.GetHostName() + "\t" +
                    m.GetAbbreviatedMonthName(d.CreationTime.Month) +
                    d.CreationTime.ToString(" dd yyyy") + "\t" +
                    folderName + Environment.NewLine;
            }
            //得到文件列表
            string[] files = Directory.GetFiles(user.CurrentDir);
            if (string.IsNullOrEmpty(parameter) == false)
            {
                if (Directory.Exists(user.CurrentDir + parameter + "/"))
                {
                    files = Directory.GetFiles(user.CurrentDir + parameter + "/");
                }
            }
            for (int i = 0; i < files.Length; i++)
            {
                FileInfo f = new FileInfo(files[i]);
                string fileName = Path.GetFileName(files[i]);
                //为了能用CuteFTP客户端测试本程序，按下面的格式输出文件列表
                sendString += "-wr-\t" + Dns.GetHostName() + "\t" + f.Length +
                    " " + m.GetAbbreviatedMonthName(f.CreationTime.Month) +
                    f.CreationTime.ToString(" dd yyyy") + "\t" +
                    fileName + Environment.NewLine;
            }
            bool isBinary = user.isBinary;
            user.isBinary = false;
            ReplyCommandToUser(user, "150 Opening ASCII mode data connection");
            InitDataSession(user);
            SendByUserSession(user, sendString);
            ReplyCommandToUser(user, "226 Transfer complete.");
            user.isBinary = isBinary;
        }
        /// <summary>
        /// 处理RETR命令，提供下载功能，将用户请求的文件发送给用户
        /// </summary>
        /// <param name="user">客户端信息</param>
        /// <param name="fileName">用户请求的文件信息</param>
        private void CommandRETR(User user, string fileName)
        {
            string sendString = "";
            //下载的文件全名
            string path = user.CurrentDir + fileName;
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            // 发送150到用户，意思为服务器文件状态良好
            if (user.isBinary)
            {
                sendString = "150 Opening BINARY mode data connection for  download";
            }
            else
            {
                sendString = "150 Opening ASCII mode data connection for download";
            }
            ReplyCommandToUser(user, sendString);
            InitDataSession(user);
            SendFileByUserSession(user, fs);
            ReplyCommandToUser(user, "226 Transfer complete.");
        }
        /// <summary>
        /// 处理STOR命令，提供上传功能，接收用户上传的文件
        /// </summary>
        /// <param name="user">客户端信息</param>
        /// <param name="fileName">用户上传的文件信息</param>
        private void CommandSTOR(User user, string fileName)
        {
            string sendString = "";
            //上传的文件全名
            string path = user.CurrentDir + fileName;
            FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write);
            // 发送150到用户，意思为服务器状态良好
            if (user.isBinary)
            {
                sendString = "150 Opening BINARY mode data connection for upload";
            }
            else
            {
                sendString = "150 Opening ASCII mode data connection for upload";
            }
            ReplyCommandToUser(user, sendString);
            InitDataSession(user);
            ReadFileByUserSession(user, fs);
            ReplyCommandToUser(user, "226 Transfer complete.");
        } 
        /// <summary>
        /// 处理TYPE命令，设置数据传输方式
        /// </summary>
        /// <param name="user">客户端信息</param>
        /// <param name="param">数据传输方式</param>
        private void CommandTYPE(User user, string param)
        {
            string sendString = "";
            if (param == "I")
            {
                //二进制方式
                user.isBinary = true;
                sendString = "200 Type set to I(Binary)";
            }
            else
            {
                //ASCII方式
                user.isBinary = false;
                sendString = "200 Type set to A(ASCII)";
            }
            ReplyCommandToUser(user, sendString);
        }
        /// <summary>
        /// 处理客户端用户请求
        /// </summary>
        /// <param name="obj">指定的客户端用户</param>
        private void UserProcessing(object obj)
        {
            User user = (User)obj;
            string sendString = "220 FTP Server v1.0";
            string oldFileName = "";
            ReplyCommandToUser(user, sendString);
            while (true)
            {
                string receiveString = null;
                try
                {
                    receiveString = user.commandSession.sr.ReadLine();
                }
                catch (Exception ex)
                {
                    if (user.commandSession.client.Connected == false)
                    {
                        AddInfo("客户端断开连接");
                    }
                    else
                    {
                        AddInfo("接收命令失败:" + ex.Message);
                    }
                    break;
                }
                if (receiveString == null)
                {
                    AddInfo("接收字符串为null，结束线程");
                    break;
                }
                AddInfo(string.Format("来自[{0}]--{1}", user.commandSession.client.Client.RemoteEndPoint, receiveString));
                //分解客户端发来的控制信息中的命令及参数
                string command = receiveString;
                string param = string.Empty;
                int index = receiveString.IndexOf(' ');
                if (index != -1)
                {
                    command = receiveString.Substring(0, index).ToUpper();
                    param = receiveString.Substring(command.Length).Trim();
                }
                //处理不需登录即可响应的命令（此处仅处理QUIT）
                if (command == "QUIT")
                {
                    //关闭TCP连接并释放与其关联的所有资源
                    user.commandSession.Close();
                    return;
                }
                else
                {
                    switch (user.LoginOK)
                    {
                        //等待用户输入用户名
                        case 0:
                            CommandUser(user, command, param);
                            break;
                        //等待用户输入密码
                        case 1:
                            CommandPassword(user, command, param);
                            break;
                        //用户名和密码验证正确时登录
                        case 2:
                            {
                                switch (command)
                                {
                                    case "CWD":
                                        CommandCWD(user, param);
                                        break;
                                    case "PWD":
                                        CommandPWD(user);
                                        break;
                                    case "PASV":
                                        CommandPASV(user);
                                        break;
                                    case "PORT":
                                        CommandPORT(user, param);
                                        break;
                                    case "LIST":
                                    case "NLST":
                                        CommandLIST(user, param);
                                        break;
                                    //处理下载文件命令
                                    case "RETR":
                                        CommandRETR(user, param);
                                        break;
                                    //处理上传文件命令
                                    case "STOR":
                                        CommandSTOR(user, param);
                                        break;                                  
                                    case "TYPE":
                                        CommandTYPE(user, param);
                                        break;
                                    default:
                                        sendString = "502 command is not implemented.";
                                        ReplyCommandToUser(user, sendString);
                                        break;
                                }
                            }
                            break;
                    }
                }
            }
        }
        /// <summary>
        /// 初始化数据连接
        /// </summary>
        /// <param name="user">指定数据连接所属客户端用户</param>
        private void InitDataSession(User user)
        {
            TcpClient client = null;
            if (user.isPassive)
            {
                AddInfo("采用被动模式返回LIST命令结果");
                client = user.dataListener.AcceptTcpClient();
            }
            else
            {
                AddInfo("采用主动模式向用户发送LIST结果");
                client = new TcpClient();
                client.Connect(user.remoteEndPoint);
            }
            user.dataSession = new UserSession(client);
        }
        /// <summary>
        /// 使用数据连接发送字符串数据
        /// </summary>
        /// <param name="user">指定客户端用户</param>
        /// <param name="sendString">指定要发送的字符串数据</param>
        private void SendByUserSession(User user, string sendString)
        {
            AddInfo("开始向用户发送：" + sendString);
            try
            {
                user.dataSession.sw.WriteLine(sendString);
                AddInfo("发送完毕");
            }
            finally
            {
                user.dataSession.Close();
            }
        }
        /// <summary>
        /// 使用数据连接接收文件流
        /// </summary>
        /// <param name="user">指定客户端用户</param>
        /// <param name="sendString">指定要接收的文件流</param>
        private void ReadFileByUserSession(User user, FileStream fs)
        {
            AddInfo("开始接收");
            try
            {
                if (user.isBinary)
                {
                    byte[] bytes = new byte[1024];
                    BinaryWriter bw = new BinaryWriter(fs);
                    int count = user.dataSession.br.Read(bytes, 0, bytes.Length);
                    while (count > 0)
                    {
                        bw.Write(bytes, 0, count);
                        bw.Flush();
                        count = user.dataSession.br.Read(bytes, 0, bytes.Length);
                    }
                }
                else
                {
                    StreamWriter sw = new StreamWriter(fs);
                    while (user.dataSession.sr.Peek() > -1)
                    {
                        sw.WriteLine(user.dataSession.sr.ReadLine());
                        sw.Flush();
                    }
                }
                AddInfo("接收完毕");
            }
            finally
            {
                user.dataSession.Close();
                fs.Close();
            }
        }
        /// <summary>
        /// 使用数据连接发送文件流
        /// </summary>
        /// <param name="user">指定客户端用户</param>
        /// <param name="sendString">指定要发送的文件流</param>
        private void SendFileByUserSession(User user, FileStream fs)
        {
            AddInfo("开始发送文件流");
            try
            {
                if (user.isBinary)
                {
                    byte[] bytes = new byte[1024];
                    BinaryReader br = new BinaryReader(fs);
                    int count = br.Read(bytes, 0, bytes.Length);
                    while (count > 0)
                    {
                        user.dataSession.bw.Write(bytes, 0, count);
                        user.dataSession.bw.Flush();
                        count = br.Read(bytes, 0, bytes.Length);
                    }
                }
                else
                {
                    StreamReader sr = new StreamReader(fs);
                    while (sr.Peek() > -1)
                    {
                        user.dataSession.sw.WriteLine(sr.ReadLine());
                    }
                }
                AddInfo("发送完毕");
            }
            finally
            {
                user.dataSession.Close();
                fs.Close();
            }
        }
        /// <summary>向客户端用户发送响应码信息</summary>
        /// <param name="user">指定客户端用户</param>
        /// <param name="str">响应码信息</param>
        private void ReplyCommandToUser(User user, string str)
        {
            try
            {
                user.commandSession.sw.WriteLine(str);
                AddInfo(string.Format("向{0}发送：{1}", user.commandSession.client.Client.RemoteEndPoint, str));
            }
            catch
            {
                AddInfo(string.Format("向{0}发送信息失败", user.commandSession.client.Client.RemoteEndPoint));
            }
        }
        /// <summary>单击【停止FTP服务】时发生</summary>
        private void buttonStop_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>向listBoxStatus中添加状态信息</summary>
        private delegate void AddInfoDelegate(string str);
        private void AddInfo(string str)
        {
            if (listBoxStatus.InvokeRequired == true)
            {
                AddInfoDelegate d = new AddInfoDelegate(AddInfo);
                this.Invoke(d, str);
            }
            else
            {
                listBoxStatus.Items.Add(str);
                listBoxStatus.SelectedIndex = listBoxStatus.Items.Count - 1;
                listBoxStatus.ClearSelected();
            }
        }
    }
}