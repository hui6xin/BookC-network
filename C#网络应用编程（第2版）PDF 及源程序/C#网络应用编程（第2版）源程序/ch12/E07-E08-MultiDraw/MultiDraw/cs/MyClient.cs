using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.IO;
using System.Net;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using System.Drawing;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace MultiDraw
{
    //封装附机信息
    public class MyClient
    {
        private string localIPString = "";
        /// <summary>获取本机IP地址</summary>
        public string LocalIPString
        {
            get { return localIPString; }
        }
        public TcpClient client;
        public StreamWriter sw;
        public StreamReader sr;
        public NetworkStream networkStream;
        /// <summary>是否正常退出接收线程</summary>
        public bool normalExit = false;
        public MyClient()
        {
            IPAddress[] addrIP = Dns.GetHostAddresses(Dns.GetHostName());
            localIPString = addrIP[addrIP.Length-1].ToString();
        }
        public void CreateNetStream()
        {
            networkStream = client.GetStream();
            sr = new StreamReader(networkStream, System.Text.Encoding.UTF8);
            sw = new StreamWriter(networkStream, System.Text.Encoding.UTF8);
            sw.AutoFlush = true;
        }
        /// <summary>创建线程接收服务器数据</summary>
        public void InitReceiveThread()
        {
            Thread threadReceive = new Thread(new ThreadStart(ReceiveData));
            threadReceive.Start();
        }
        private bool finishGetNewID = false;
        public bool FinishGetNewID
        {
            get { return finishGetNewID; }
        }
        private void ReceiveData()
        {
            while (normalExit == false)
            {
                string receiveString = null;
                try
                {
                    receiveString = sr.ReadLine();
                }
                catch
                {
                    Debug.Print("MyClient:接收receiveString数据失败");
                    break;
                }
                if (receiveString == null)
                {
                    if (normalExit == false)
                    {
                        MessageBox.Show("与主机失去联系，无法继续制作！");
                    }
                    break;
                }
                Debug.Print("附机收到：{0}", receiveString);
                string[] splitString = receiveString.Split(',');
                switch (splitString[0])
                {
                    case "ID":
                        CC.ID = int.Parse(splitString[1]);
                        finishGetNewID = true;
                        break;
                    case "ServerExit":
                        if (CC.userState == UserState.Client)
                        {
                            MessageBox.Show("主机正常退出制作，由于本机为附机，无法继续！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        normalExit = true;
                        break;
                    case "WelcomeLogin":
                        {
                            //格式：WelcomeLogin，字节数
                            byte[] bytes = ReceiveBytesFromServer(int.Parse(splitString[1]));
                            try
                            {
                                MemoryStream stream = new MemoryStream(bytes);
                                IFormatter formatter = new BinaryFormatter();
                                CC.palette.graphics = (GraphicsList)formatter.Deserialize(stream);
                                stream.Close();
                                CC.myService.RefreshPalette();
                            }
                            catch (Exception err)
                            {
                                Debug.Print("WelcomeLogin反序列化失败，原因： {0}", err);
                                break;
                            }
                        }
                        break;
                    case "Logout":
                        {
                            if (splitString[1] == CC.me.client.Client.LocalEndPoint.ToString())
                            {
                                normalExit = true;
                                break;
                            }
                            if (splitString[1] == CC.me.client.Client.RemoteEndPoint.ToString())
                            {
                                MessageBox.Show("\n主机正常退出制作，由于本机为附机，无法继续！\n\n请保存文件，立即退出", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            else
                            {
                                Debug.Print(splitString[1] + " 退出制作（非主机），本机可以继续制作！", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        break;
                    case "DrawMyImage":
                        {
                            //格式：DrawMyImage，序列化后的字节数
                            int count = int.Parse(splitString[1]); 
                            byte[] bytes = ReceiveBytesFromServer(count);
                            try
                            {
                                MemoryStream stream = new MemoryStream(bytes);
                                IFormatter formatter = new BinaryFormatter();
                                GraphicsList myGraphicsList = (GraphicsList)formatter.Deserialize(stream);
                                stream.Close();
                                DrawMyImage w = (DrawMyImage)myGraphicsList[0];
                                w.Selected = false;
                                CC.palette.graphics.Add(w);
                                myGraphicsList.Clear();
                                CC.myService.RefreshPalette();
                            }
                            catch (Exception err)
                            {
                                Debug.Print("反序列化图片失败，原因： {0}", err);
                            }
                        }
                        break;
                    case "DeleteObjects":
                        {
                            string[] str = splitString[1].Split('@');
                            for (int i = 0; i < str.Length; i++)
                            {
                                CC.palette.graphics.Remove(int.Parse(str[i]));
                            }
                            CC.myService.RefreshPalette();
                        }
                        break;
                    default:
                        CC.myService.DataProcessing(receiveString);
                        break;
                }
            }
        }
        /// <summary>发送字符串</summary>
        public void SendToServer(string str)
        {
            if (str == "GetID")
            {
                finishGetNewID = false;
            }
            try
            {
                sw.WriteLine(str);
                sw.Flush();
            }
            catch (Exception err)
            {
                Debug.Print("向服务器发送数据失败：{0}", err);
            }
         }
        /// <summary>发送字节数据</summary>
        public void SendToServer(byte[] bytes)
        {
            try
            {
                int size = client.SendBufferSize;
                int dataleft = bytes.Length;
                int start = 0;
                while (dataleft > 0)
                {
                    if (dataleft < size)
                    {
                        size = dataleft;
                    }
                    networkStream.Write(bytes, start, size);
                    start += size;
                    dataleft -= size;
                }
                Debug.Print("发送字节{0}成功", bytes.Length);
            }
            catch (Exception err)
            {
                Debug.Print("向服务器发送数据失败：{0}", err);
            }
        }

        /// <summary>接收字节数组并处理接收的内容。count：字节个数</summary>
        public byte[] ReceiveBytesFromServer(int count)
        {
            byte[] bytes = new byte[count];
            int size = client.ReceiveBufferSize;
            int dataleft = bytes.Length;
            int start = 0;
            try
            {
                while (dataleft > 0)
                {
                    if (dataleft < size)
                    {
                        size = dataleft;
                    }
                    int recv = networkStream.Read(bytes, start, size);
                    start += recv;
                    dataleft -= recv;
                }
            }
            catch (Exception err)
            {
                Debug.Print("从服务器读取数据失败：{0}", err);
                return null;
            }
            Debug.Print("附机收到{0}字节的数据", bytes.Length);
            return bytes;
        }

    }
}
