using System.Text;
using System.Net.Sockets;
using System.IO;

namespace FtpServerExample
{
    public class UserSession
    {
        private NetworkStream networkStream;
        public readonly StreamReader sr;
        public readonly StreamWriter sw;
        public readonly TcpClient client;
        public readonly BinaryReader br;
        public readonly BinaryWriter bw;
        public UserSession(TcpClient client)
        {
            this.client = client;
            networkStream = client.GetStream();
            sr = new StreamReader(networkStream, Encoding.Default);
            sw = new StreamWriter(networkStream, Encoding.Default);
            sw.AutoFlush = true;
            br = new BinaryReader(networkStream,Encoding.Default);
            bw = new BinaryWriter(networkStream,Encoding.Default);
        }
        public void Close()
        {
            client.Client.Shutdown(SocketShutdown.Both);
            client.Client.Close();
            client.Close();
        }
    }
}
