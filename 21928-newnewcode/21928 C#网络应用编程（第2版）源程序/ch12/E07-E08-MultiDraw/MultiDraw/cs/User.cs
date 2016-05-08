using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.IO;

namespace MultiDraw
{
    public class User
    {
        /// <summary>
        /// ָʾ�Ƿ������˳������߳�
        /// </summary>
        public bool normalExit = false;
        /// <summary>
        /// ����û�ͨ�ŵ�TCPClient����
        /// </summary>
        public TcpClient client;
        /// <summary>
        /// ����û�ͨ���õ�StreamReader����
        /// </summary>
        public StreamReader sr;
        /// <summary>
        /// ����û�ͨ���õ�StreamWriter����
        /// </summary>
        public StreamWriter sw;
        /// <summary>
        /// ����û�ͨ���õ�NetworkStream����
        /// </summary>
        public NetworkStream networkStream;

        public User(TcpClient client)
        {
            this.client = client;
            networkStream = client.GetStream();
            sr = new StreamReader(networkStream, System.Text.Encoding.UTF8);
            sw = new StreamWriter(networkStream, System.Text.Encoding.UTF8);
            sw.AutoFlush = true;
            //˵�������ڴ����ӳ��˷����ַ���(��sr��sw����)���Ƿ����ֽ�����(��networkStream����)����˲���Ҫ�ٴ���BinaryReader��BinaryReader����
        }

        public void CloseUser()
        {
            sr.Close();
            sw.Close();
            networkStream.Close();
            client.Close();
        }
    }
}
