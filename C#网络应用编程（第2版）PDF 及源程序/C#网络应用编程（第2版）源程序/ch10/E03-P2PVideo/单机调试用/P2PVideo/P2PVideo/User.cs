using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.IO;
using System.Net;
namespace P2PVideo
{
    class User
    {
        public static int port = 0;
        public NetworkStream stream;
        public TcpClient remoteClient;
        public BinaryReader br;
        public BinaryWriter bw;

        public User(TcpClient client)
        {
            remoteClient = client;
            stream = client.GetStream();
            br = new BinaryReader(stream);
            bw = new BinaryWriter(stream);
        }
    }
}
