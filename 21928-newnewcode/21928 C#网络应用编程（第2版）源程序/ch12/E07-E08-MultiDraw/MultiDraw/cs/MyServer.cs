using System;
using System.Collections.Generic;
using System.Net;
using System.Windows.Forms;
using System.Net.Sockets;
using System.IO;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
namespace MultiDraw
{
    //封装主机信息
    public class MyServer
    {
        public IPAddress localAddress;//本机IP地址
        public const int port = 51888;//监听端口
        public TcpListener myListener;
        private int id = 1;  //表示对象的id，每接收一个新对象，该值都会加1
        public int ID
        {
            set{ id = value;}
        }
        public bool exitServerThread = false;
        private List<User> users = new List<User>();
        public List<User> Users
        {
            get { return users; }
        }
        public MyServer()
        {
            IPAddress[] addrIP = Dns.GetHostAddresses(Dns.GetHostName());
            localAddress = addrIP[addrIP.Length-1];
            myListener = new TcpListener(localAddress, port);
            myListener.Start();
            //创建一个线程监听客户端连接请求
            Thread myThread = new Thread(ListenClientConnect);
            myThread.Start();
        }
        private object lockedObj = new object();
        private int GetID()
        {
            lock (lockedObj)
            {
                id++;
            }
            return id;
        }
        /// <summary>接收客户端连接</summary>
        private void ListenClientConnect()
        {
            while (true)
            {
                TcpClient newClient = null;
                try
                {
                    newClient = myListener.AcceptTcpClient();
                }
                catch
                {
                    for (int i = 0; i < users.Count; i++)
                    {
                        users[i].normalExit = true;
                    }
                    break;
                }
                User user = new User(newClient);
                users.Add(user);
                if (users.Count > 1)
                {
                    myDelegate = CC.myService.mainForm.SetUserState;
                    CC.myService.mainForm.Invoke(myDelegate);
                }
                Thread threadReceive = new Thread(ReceiveData);
                threadReceive.Start(user);
            }
            //关闭用户对象，释放其占用的所有资源
            foreach (User myuser in users)
            {
                myuser.CloseUser();
            }
        }
        delegate void MyDelegate();
        MyDelegate myDelegate;        
        /// <summary>接收、处理客户端信息的线程</summary>
        private void ReceiveData(object obj)
        {
            User user = (User)obj;
            while (user.normalExit == false)
            {
                string receiveString = null;
                try
                {
                    receiveString = user.sr.ReadLine();
                }
                catch
                {
                    if (user.normalExit == false)
                    {
                        MessageBox.Show("与" + user.client.Client.RemoteEndPoint + "失去联系，已终止接收该用户信息");
                    }
                    Debug.Print("退出监听");
                    break;
                }
                Debug.Print("服务器收到：{0}", receiveString);
                string[] splitString = receiveString.Split(',');
                switch (splitString[0])
                {
                    case "GetID":
                        SendToOne(user, "ID," + this.GetID());
                        break;
                    case "Login": //该用户刚刚登录
                        {
                            try
                            {
                                MemoryStream stream = new MemoryStream();
                                IFormatter formatter = new BinaryFormatter();
                                formatter.Serialize(stream, CC.palette.graphics);
                                byte[] bytes = stream.GetBuffer();
                                SendToOne(user, "WelcomeLogin," + bytes.Length);
                                SendToOne(user, bytes);
                            }
                            catch (Exception err)
                            {
                                MessageBox.Show("Login序列化失败，原因：" + err);
                            }
                        }
                        break;
                    case "Logout"://用户退出
                        Debug.Print("{0}退出", user.client.Client.RemoteEndPoint);
                        SendToAllUser("Logout," + user.client.Client.RemoteEndPoint);
                        user.normalExit = true;
                        users.Remove(user);
                        myDelegate = CC.myService.mainForm.SetUserState;
                        CC.myService.mainForm.Invoke(myDelegate);
                        break;
                    case "DrawMyImage"://格式：DrawMyImage，序列化后的字节数
                        { 
                            int count = int.Parse(splitString[1]);
                            byte[] bytes = ReceiveBytesFromUser(user, count);
                            SendToAllUser(receiveString);
                            SendToAllUser(bytes);
                        }
                        break;
                    case "DeleteObjects":
                        lock (lockedObj)
                        {
                            id++;
                        }
                        SendToAllUser(receiveString + "," + id.ToString());
                        break;
                    default:
                        SendToAllUser(receiveString);
                        break;
                }
            }
            user.CloseUser();
        }
        public void SendToAllUser(string str)
        {
            foreach (User user in CC.myServer.Users)
            {
                SendToOne(user, str);
            }
        }
        public void SendToAllUser(byte[] bytes)
        {
            foreach (User user in Users)
            {
                SendToOne(user, bytes);
            }
        }
        public void SendToOne(User user, byte[] bytes)
        {
            try
            {
                int size = user.client.SendBufferSize;
                int dataleft = bytes.Length;
                int start = 0;
                while (dataleft > 0)
                {
                    if (dataleft < size)
                    {
                        size = dataleft;
                    }
                    user.networkStream.Write(bytes, start, size);
                    start += size;
                    dataleft -= size;
                }
                Debug.Print("服务器向{0}发送{1}字节的数据", user.client.Client.RemoteEndPoint, bytes.Length);
            }
            catch (Exception err)
            {
                Debug.Print("服务器发送信息失败，原因：{0}", err.Message);
            }
        }
        public void SendToOne(User user, string str)
        {
            try
            {
                user.sw.WriteLine(str);
                user.sw.Flush();
                Debug.Print("服务器向{0}发送{1}", user.client.Client.RemoteEndPoint, str);
            }
            catch(Exception err)
            {
                Debug.Print("服务器发送信息失败，原因：{0}", err.Message);
            }
        }
        /// <summary>接收count个字节的数据，返回字节数组</summary>
        private byte[] ReceiveBytesFromUser(User user, int count)
        {
            byte[] bytes = new byte[count];
            int dataleft = bytes.Length;
            int start = 0;
            int size = user.client.ReceiveBufferSize;
            try
            {
                while (dataleft > 0)
                {
                    if (dataleft < size)
                    {
                        size = dataleft;
                    }
                    int recv = user.networkStream.Read(bytes, start, size);
                    start += recv;
                    dataleft -= recv;
                }
                Debug.Print("服务器接受字节{0}成功", bytes.Length);
                return bytes;
            }
            catch (Exception err)
            {
                Debug.Print(user.client.Client.LocalEndPoint.ToString() + "读取数据失败，原因：{0}",err.Message);
                return null;
            }
        }

    }
}
