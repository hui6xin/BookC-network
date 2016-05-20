using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Net.Sockets;
using System.IO;
namespace EncryptTcpServer
{
    class User
    {
        public TcpClient client;
        public BinaryReader br;
        public BinaryWriter bw;
        //对称加密
        public TripleDESCryptoServiceProvider tdes;
        //不对称加密
        public RSACryptoServiceProvider rsa;
        public User(TcpClient client)
        {
            this.client = client;
            NetworkStream networkStream = client.GetStream();
            br = new BinaryReader(networkStream, Encoding.UTF8);
            bw = new BinaryWriter(networkStream, Encoding.UTF8);
            tdes = new TripleDESCryptoServiceProvider();
            rsa = new RSACryptoServiceProvider();
        }
    }
}
