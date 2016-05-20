using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Diagnostics;
//服务器程序
namespace SocketServer
{
    class Program
    {
        private static byte[] result = new Byte[1024];
        private static int myprot = 8889;
        static Socket serverSocket;
        static void Main(string[] args)
        {
            //服务器IP地址
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream,ProtocolType.Tcp);
            serverSocket.Bind(new IPEndPoint(ip, myprot));
            serverSocket.Listen(10);
            Console.WriteLine("启动监听{0}成功", serverSocket.LocalEndPoint.ToString());
            //通过clientsocket发送数据
            Thread myThread = new Thread(ListenClientConnect);
            myThread.Start();
            Console.ReadLine();
        }
        /// <summary>
        /// 接收连接
        /// </summary>
        private static void ListenClientConnect()
        {
            while (true)
            {
                Socket clientsocket = serverSocket.Accept();
                clientsocket.Send(Encoding.ASCII.GetBytes("Server Say Hello"));
                Thread receiveThread = new Thread(ReceiveMessage);
                receiveThread.Start(clientsocket);
            }
        }
        /// <summary>
        /// 接收信息
        /// </summary>
        /// <param name="clientSocket">包含客户机信息的套接字</param>
        private static void ReceiveMessage(Object clientSocket)
        {
            Socket myClientSocket = (Socket)clientSocket;
            while (true)
            {
                try
                {
                    //通过clientsocket接收数据
                    int receiveNumber = myClientSocket.Receive(result);
                    Console.WriteLine("接收客户端{0}消息{1}",myClientSocket.RemoteEndPoint.ToString(), Encoding.ASCII.GetString(result, 0, receiveNumber));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    myClientSocket.Shutdown(SocketShutdown.Both);
                    myClientSocket.Close();
                    break;
                }
            }
        }
    }
}
