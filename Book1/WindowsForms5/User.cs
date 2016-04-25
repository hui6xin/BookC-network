using System.IO;
using System.Net.Sockets;

namespace WindowsForms5
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
            NetworkStream networkstream = client.GetStream();
            br = new BinaryReader(networkstream);
            bw = new BinaryWriter(networkstream);
        }
        public void close()
        {
            br.Close();
            bw.Close();
            client.Close();
        }

    }
}
