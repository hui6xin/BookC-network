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
    //��װ������Ϣ
    public class MyServer
    {
        public IPAddress localAddress;//����IP��ַ
        public const int port = 51888;//�����˿�
        public TcpListener myListener;
        private int id = 1;  //��ʾ�����id��ÿ����һ���¶��󣬸�ֵ�����1
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
            //����һ���̼߳����ͻ�����������
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
        /// <summary>���տͻ�������</summary>
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
            //�ر��û������ͷ���ռ�õ�������Դ
            foreach (User myuser in users)
            {
                myuser.CloseUser();
            }
        }
        delegate void MyDelegate();
        MyDelegate myDelegate;        
        /// <summary>���ա�����ͻ�����Ϣ���߳�</summary>
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
                        MessageBox.Show("��" + user.client.Client.RemoteEndPoint + "ʧȥ��ϵ������ֹ���ո��û���Ϣ");
                    }
                    Debug.Print("�˳�����");
                    break;
                }
                Debug.Print("�������յ���{0}", receiveString);
                string[] splitString = receiveString.Split(',');
                switch (splitString[0])
                {
                    case "GetID":
                        SendToOne(user, "ID," + this.GetID());
                        break;
                    case "Login": //���û��ոյ�¼
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
                                MessageBox.Show("Login���л�ʧ�ܣ�ԭ��" + err);
                            }
                        }
                        break;
                    case "Logout"://�û��˳�
                        Debug.Print("{0}�˳�", user.client.Client.RemoteEndPoint);
                        SendToAllUser("Logout," + user.client.Client.RemoteEndPoint);
                        user.normalExit = true;
                        users.Remove(user);
                        myDelegate = CC.myService.mainForm.SetUserState;
                        CC.myService.mainForm.Invoke(myDelegate);
                        break;
                    case "DrawMyImage"://��ʽ��DrawMyImage�����л�����ֽ���
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
                Debug.Print("��������{0}����{1}�ֽڵ�����", user.client.Client.RemoteEndPoint, bytes.Length);
            }
            catch (Exception err)
            {
                Debug.Print("������������Ϣʧ�ܣ�ԭ��{0}", err.Message);
            }
        }
        public void SendToOne(User user, string str)
        {
            try
            {
                user.sw.WriteLine(str);
                user.sw.Flush();
                Debug.Print("��������{0}����{1}", user.client.Client.RemoteEndPoint, str);
            }
            catch(Exception err)
            {
                Debug.Print("������������Ϣʧ�ܣ�ԭ��{0}", err.Message);
            }
        }
        /// <summary>����count���ֽڵ����ݣ������ֽ�����</summary>
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
                Debug.Print("�����������ֽ�{0}�ɹ�", bytes.Length);
                return bytes;
            }
            catch (Exception err)
            {
                Debug.Print(user.client.Client.LocalEndPoint.ToString() + "��ȡ����ʧ�ܣ�ԭ��{0}",err.Message);
                return null;
            }
        }

    }
}
