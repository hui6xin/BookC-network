using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;

namespace AsyncUdpChatExample
{
    class UdpState
    {
        private UdpClient myudpClient;

        public UdpClient MyudpClient
        {
            get { return myudpClient; }
            set { myudpClient = value; }
        }
        private IPEndPoint ipEndPoint;

        public IPEndPoint IpEndPoint
        {
            get { return ipEndPoint; }
            set { ipEndPoint = value; }
        }


    }
}
