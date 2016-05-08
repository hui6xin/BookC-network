using System.Net.Sockets;
using System.IO;
namespace AsyncTcpServer
{
    class User
    {
        public TcpClient client { get; private set; }
        public BinaryReader br { get; private set; }
        public BinaryWriter bw { get; private set; }
        public string userName { get; set; }
        public User(TcpClient client)
        {
            this.client = client;
            NetworkStream networkStream = client.GetStream();
            br = new BinaryReader(networkStream);
            bw = new BinaryWriter(networkStream);
        }
        public void Close()
        {
            br.Close();
            bw.Close();
            client.Close();
        }
    }
}
