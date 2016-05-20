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
        /// 指示是否正常退出接收线程
        /// </summary>
        public bool normalExit = false;
        /// <summary>
        /// 与该用户通信的TCPClient对象
        /// </summary>
        public TcpClient client;
        /// <summary>
        /// 与该用户通信用的StreamReader对象
        /// </summary>
        public StreamReader sr;
        /// <summary>
        /// 与该用户通信用的StreamWriter对象
        /// </summary>
        public StreamWriter sw;
        /// <summary>
        /// 与该用户通信用的NetworkStream对象
        /// </summary>
        public NetworkStream networkStream;

        public User(TcpClient client)
        {
            this.client = client;
            networkStream = client.GetStream();
            sr = new StreamReader(networkStream, System.Text.Encoding.UTF8);
            sw = new StreamWriter(networkStream, System.Text.Encoding.UTF8);
            sw.AutoFlush = true;
            //说明：由于此例子除了发送字符串(用sr、sw即可)就是发送字节数组(用networkStream即可)，因此不需要再创建BinaryReader和BinaryReader对象
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
