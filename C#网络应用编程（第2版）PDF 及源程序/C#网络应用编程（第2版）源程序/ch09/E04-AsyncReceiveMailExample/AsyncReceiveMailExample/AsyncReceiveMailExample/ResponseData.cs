using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.IO;

namespace AsyncReceiveMailExample
{
    public class ResponseData
    {
        public string responseString;
        public byte[] bytes=new byte[8192];
    }
}
