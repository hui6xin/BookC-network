﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
//添加的命名空间引用
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace SyncChatServer
{
    public partial class MainForm : Form
    {
        /// <summary>保存连接的所有用户</summary>
        private List<User> userList = new List<User>();
        /// <summary>使用的本机IP地址</summary>
        IPAddress localAddress;
        /// <summary>监听端口</summary>
        private const int port = 51888;
        private TcpListener myListener;
        /// <summary>是否正常退出所有接收线程</summary>
        bool isNormalExit = false;
        public MainForm()
        {
            InitializeComponent();
            listBoxStatus.HorizontalScrollbar = true;
            IPAddress[] addrIP = Dns.GetHostAddresses(Dns.GetHostName());
            localAddress = addrIP[0];
            buttonStop.Enabled = false;
        }
        /// <summary>【开始监听】按钮的Click事件</summary>
        private void buttonStart_Click(object sender, EventArgs e)
        {
            myListener = new TcpListener(localAddress, port);
            myListener.Start();
            AddItemToListBox(string.Format("开始在{0}:{1}监听客户连接", localAddress, port));
            //创建一个线程监听客户端连接请求
            Thread myThread = new Thread(ListenClientConnect);
            myThread.Start();
            buttonStart.Enabled = false;
            buttonStop.Enabled = true;
        }
        /// <summary>接收客户端连接</summary>
        private void ListenClientConnect()
        {
            TcpClient newClient = null;
            while (true)
            {
                try
                {
                    newClient = myListener.AcceptTcpClient();
                }
                catch
                {
                    //当单击“停止监听”或者退出此窗体时AcceptTcpClient()会产生异常
                    //因此可以利用此异常退出循环
                    break;
                }
                //每接受一个客户端连接,就创建一个对应的线程循环接收该客户端发来的信息
                User user = new User(newClient);
                Thread threadReceive = new Thread(ReceiveData);
                threadReceive.Start(user);
                userList.Add(user);
                AddItemToListBox(string.Format("[{0}]进入", newClient.Client.RemoteEndPoint));
                AddItemToListBox(string.Format("当前连接用户数：{0}", userList.Count));
            }
        }
       /// <summary>
       /// 处理接收的客户端数据
       /// </summary>
       /// <param name="userState">客户端信息</param>
        private void ReceiveData(object userState)
        {
            User user = (User)userState;
            TcpClient client = user.client;
            while (isNormalExit == false)
            {
                string receiveString = null;
                try
                {
                    //从网络流中读出字符串，此方法会自动判断字符串长度前缀，并根据长度前缀读出字符串
                    receiveString = user.br.ReadString();
                }
                catch
                {
                    if (isNormalExit == false)
                    {
                        AddItemToListBox(string.Format("与[{0}]失去联系，已终止接收该用户信息", client.Client.RemoteEndPoint));
                        RemoveUser(user);
                    }
                    break;
                }
                AddItemToListBox(string.Format("来自[{0}]：{1}", user.client.Client.RemoteEndPoint, receiveString));
                string[] splitString = receiveString.Split(',');
                switch (splitString[0])
                {
                    case "Login":
                        user.userName = splitString[1];
                        SendToAllClient(user, receiveString);
                        break;
                    case "Logout":
                        SendToAllClient(user, receiveString);
                        RemoveUser(user);
                        return;
                    case "Talk":
                        string talkString = receiveString.Substring(splitString[0].Length + splitString[1].Length + 2);
                        AddItemToListBox(string.Format("{0}对{1}说：{2}",
                            user.userName, splitString[1], talkString));
                        SendToClient(user, "talk," + user.userName + "," + talkString);
                        foreach (User target in userList)
                        {
                            if (target.userName == splitString[1] && user.userName != splitString[1])
                            {
                                SendToClient(target, "talk," + user.userName + "," + talkString);
                                break;
                            }
                        }
                        break;
                    default:
                        AddItemToListBox("什么意思啊：" + receiveString);
                        break;
                }

            }
        }
        /// <summary>
        /// 发送message给user
        /// </summary>
        /// <param name="user">指定发给哪个用户</param>
        /// <param name="message">信息内容</param>
        private void SendToClient(User user, string message)
        {
            try
            {
                //将字符串写入网络流，此方法会自动附加字符串长度前缀
                user.bw.Write(message);
                user.bw.Flush();
                AddItemToListBox(string.Format("向[{0}]发送：{1}",
                    user.userName, message));
            }
            catch
            {
                AddItemToListBox(string.Format("向[{0}]发送信息失败",
                    user.userName));
            }
        }
        /// <summary>发送信息给所有客户</summary>
        /// <param name="user">指定发给哪个用户</param>
        /// <param name="message">信息内容</param>
        private void SendToAllClient(User user, string message)
        {
            string command = message.Split(',')[0].ToLower();
            if (command == "login")
            {
                for (int i = 0; i < userList.Count; i++)
                {
                    SendToClient(userList[i], message);
                    if (userList[i].userName != user.userName)
                    {
                        SendToClient(user, "login," + userList[i].userName);
                    }
                }
            }
            else if(command=="logout")
            {
                for (int i = 0; i < userList.Count; i++)
                {
                    if (userList[i].userName != user.userName)
                    {
                        SendToClient(userList[i], message);
                    }
                }
            }
        }
        /// <summary>移除用户</summary>
        /// <param name="user">指定要删除的用户</param>
        private void RemoveUser(User user)
        {
            userList.Remove(user);
            user.Close();
            AddItemToListBox(string.Format("当前连接用户数：{0}", userList.Count));
        }
        private delegate void AddItemToListBoxDelegate(string str);
        /// <summary>在ListBox中追加状态信息</summary>
        /// <param name="str">要追加的信息</param>
        private void AddItemToListBox(string str)
        {
            if (listBoxStatus.InvokeRequired)
            {
                AddItemToListBoxDelegate d = AddItemToListBox;
                listBoxStatus.Invoke(d, str);
            }
            else
            {
                listBoxStatus.Items.Add(str);
                listBoxStatus.SelectedIndex = listBoxStatus.Items.Count - 1;
                listBoxStatus.ClearSelected();
            }
        }
        /// <summary>【停止监听】按钮的Click事件</summary>
        private void buttonStop_Click(object sender, EventArgs e)
        {
            AddItemToListBox("开始停止服务，并依次使用户退出!");
            isNormalExit = true;
            for (int i = userList.Count - 1; i >= 0; i--)
            {
                RemoveUser(userList[i]);
            }
            //通过停止监听让myListener.AcceptTcpClient()产生异常退出监听线程
            myListener.Stop();
            buttonStart.Enabled = true;
            buttonStop.Enabled = false;
        }
        /// <summary>关闭窗口时触发的事件</summary>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (myListener != null)
            {
                //引发buttonStop的Click事件
                buttonStop.PerformClick();
            }
        }
    }
}