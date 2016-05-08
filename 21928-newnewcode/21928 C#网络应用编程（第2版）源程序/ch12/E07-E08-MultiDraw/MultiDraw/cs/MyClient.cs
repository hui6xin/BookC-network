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
    //��װ������Ϣ
    public class MyClient
    {
        private string localIPString = "";
        /// <summary>��ȡ����IP��ַ</summary>
        public string LocalIPString
        {
            get { return localIPString; }
        }
        public TcpClient client;
        public StreamWriter sw;
        public StreamReader sr;
        public NetworkStream networkStream;
        /// <summary>�Ƿ������˳������߳�</summary>
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
        /// <summary>�����߳̽��շ���������</summary>
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
                    Debug.Print("MyClient:����receiveString����ʧ��");
                    break;
                }
                if (receiveString == null)
                {
                    if (normalExit == false)
                    {
                        MessageBox.Show("������ʧȥ��ϵ���޷�����������");
                    }
                    break;
                }
                Debug.Print("�����յ���{0}", receiveString);
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
                            MessageBox.Show("���������˳����������ڱ���Ϊ�������޷�������", "����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        normalExit = true;
                        break;
                    case "WelcomeLogin":
                        {
                            //��ʽ��WelcomeLogin���ֽ���
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
                                Debug.Print("WelcomeLogin�����л�ʧ�ܣ�ԭ�� {0}", err);
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
                                MessageBox.Show("\n���������˳����������ڱ���Ϊ�������޷�������\n\n�뱣���ļ��������˳�", "����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            else
                            {
                                Debug.Print(splitString[1] + " �˳������������������������Լ���������", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        break;
                    case "DrawMyImage":
                        {
                            //��ʽ��DrawMyImage�����л�����ֽ���
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
                                Debug.Print("�����л�ͼƬʧ�ܣ�ԭ�� {0}", err);
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
        /// <summary>�����ַ���</summary>
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
                Debug.Print("���������������ʧ�ܣ�{0}", err);
            }
         }
        /// <summary>�����ֽ�����</summary>
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
                Debug.Print("�����ֽ�{0}�ɹ�", bytes.Length);
            }
            catch (Exception err)
            {
                Debug.Print("���������������ʧ�ܣ�{0}", err);
            }
        }

        /// <summary>�����ֽ����鲢������յ����ݡ�count���ֽڸ���</summary>
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
                Debug.Print("�ӷ�������ȡ����ʧ�ܣ�{0}", err);
                return null;
            }
            Debug.Print("�����յ�{0}�ֽڵ�����", bytes.Length);
            return bytes;
        }

    }
}
