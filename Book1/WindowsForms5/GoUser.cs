using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading;
using System.IO;

namespace WindowsForms5
{
    class GoUser
    {
        public TcpClient client { get; private set; }
        public StreamReader sr { get; private set; }
        public StreamWriter sw { get; private set; }
        public string userName { get; set; }
        public GoUser(TcpClient client)
        {
            this.client = client;
            this.userName = "";
            NetworkStream netstream = client.GetStream();
            sr = new StreamReader(netstream, System.Text.Encoding.UTF8);
            sw = new StreamWriter(netstream, System.Text.Encoding.UTF8);
        }
    }
}
