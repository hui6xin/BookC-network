using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading;
using System.IO;
using System.Runtime.InteropServices;

namespace WindowsForms4
{
    public partial class Form1 : Form
    {

        private static byte[] clientresult = new byte[1024];
        private static byte[] serverresult = new byte[1024];
        IPAddress clientip=IPAddress.Parse("127.0.0.1");
        private static int myport = 8889;
        static Socket serversocket;
        Socket clientsocket;

        IPAddress serverip = IPAddress.Parse("127.0.0.1");

        FileStream fs;
        public Form1()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            clientsocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                clientsocket.Connect(new IPEndPoint(clientip,8889));
                listBox2.Items.Add("connet");
                //Console.Write("connet");
            }
            catch
            {
                listBox2.Items.Add("connet fall");
                //Console.Write("connet fall");
                return;
            }
            // receive data
            int receiveLength = clientsocket.Receive(clientresult);
            listBox2.Items.Add(string.Format( "接受服务器消息：{0}", Encoding.ASCII.GetString(clientresult, 0, receiveLength)) );
            //Console.WriteLine("接受服务器消息：{0}", Encoding.ASCII.GetString(clientresult, 0, receiveLength));
            //send data
            for (int i = 0; i < 5; i++)
            {
                try
                {
                    Application.DoEvents();
                    Thread.Sleep(1000);
                    string sendMessage = "client send message hello " + DateTime.Now;
                    clientsocket.Send(Encoding.ASCII.GetBytes(sendMessage));
                    listBox2.Items.Add(string.Format("send data :{0}", sendMessage));
                    //Console.WriteLine("send data :{0}", sendMessage);
                }
                catch
                {
                    clientsocket.Shutdown(SocketShutdown.Both);
                    clientsocket.Close();
                    return;
                    //break;
                }
            }
            listBox2.Items.Add(string.Format("send end"));
            clientsocket.Shutdown(SocketShutdown.Both);
            clientsocket.Close();
            //Console.WriteLine("send end");
            //Console.ReadLine();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            serversocket=new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);
            serversocket.Bind(new IPEndPoint(serverip, myport));
            serversocket.Listen(10);
            listBox1.Items.Add(string.Format("begin {0} listen:", serversocket.LocalEndPoint.ToString()));
            //Console.WriteLine("begin {0} listen:" + serversocket.LocalEndPoint.ToString());
            //send data
            fm1 = this;
            Thread mythread = new Thread(Listenclientconnect);
            mythread.Start();
            //Console.ReadLine();
        }

        private static void  Listenclientconnect()
        {
            while (true)
            {
                Socket clientsocket = serversocket.Accept();
                clientsocket.Send(Encoding.ASCII.GetBytes("server say hello "+DateTime.Now));
                Thread receivethread=new Thread(receivemessage);
                receivethread.Start(clientsocket);
            }
        }
        private static Form1 fm1;
        public static void receivemessage(object clientsocket)
        {
            Socket myclientsocket = (Socket)clientsocket;
            while (true)
            {
                try
                {
                    int receivnumber = myclientsocket.Receive(serverresult);
                    if (receivnumber > 0)
                    {
                        fm1.listadd(string.Format("server {0} receive{1}",
                            myclientsocket.RemoteEndPoint.ToString(),
                           ASCIIEncoding.ASCII.GetString(serverresult, 0, receivnumber)));
                        //listBox1.Items.Add(string.Format("server {0} receive{1}",
                        //    myclientsocket.RemoteEndPoint.ToString(),
                        //   ASCIIEncoding.ASCII.GetString(serverresult, 0, receivnumber)));
                        Console.WriteLine("server {0} receive{1}",
                            myclientsocket.RemoteEndPoint.ToString(),
                            ASCIIEncoding.ASCII.GetString(serverresult, 0, receivnumber));
                    }
                }
                catch(Exception ex)
                {
                    fm1.listadd(string.Format(ex.Message));
                    //listBox1.Items.Add(string.Format(ex.Message));
                    Console.WriteLine(ex.Message);
                    myclientsocket.Shutdown(SocketShutdown.Both);
                    myclientsocket.Close();
                    break;
                }
            }
        }

        private delegate void Listadddelegate(string str);

        private void listadd(string str)
        {
            if (listBox1.InvokeRequired)
            {
                Listadddelegate l = listadd;
                listBox1.Invoke(l, str);
            }
            else
            {
                listBox1.Items.Add(str);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string filepath = "c:\\filepath.txt";
            try
            {
                fs = new FileStream(filepath, FileMode.OpenOrCreate);
            }
            catch
            {
                richTextBox1.AppendText("file open fail");
                return;
            }
            long left = fs.Length;

            byte[] bytes = new byte[10];

            int maxlength = bytes.Length;

            int start = 0;

            int num = 0;

            while (left > 0) 
            {
                fs.Position = start;
                num = 0;
                if (left < maxlength)
                {
                    num = fs.Read(bytes, 0, Convert.ToInt32(left));
                }
                else
                {
                    num = fs.Read(bytes, 0, maxlength);
                }
                if (num == 0)
                {
                    break;
                }
                start += num;
            }
            richTextBox1.AppendText("   end of file \n");
            fs.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            fs = null;
            string filepath = "c:\\filepath.txt";
            Encoding encoder = Encoding.UTF8;
            byte[] bytes = encoder.GetBytes("Hello World ! \n\r");
            try
            {
                fs = File.OpenWrite(filepath);

                fs.Position = fs.Length; //5000;//

                fs.Write(bytes, 0, bytes.Length);
            }
            catch (Exception ex)
            {
                richTextBox1.AppendText(ex.ToString() + "\n");
            }
            finally
            {
                fs.Close();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MemoryStream mem = new MemoryStream();
            listBox3.Items.Add(string.Format("orginal memory:{0}",mem.Capacity));
            listBox3.Items.Add(string.Format("used memory:{0}", mem.Length));

            UnicodeEncoding encoder = new UnicodeEncoding();
            byte[] bytes = encoder.GetBytes("新增数据");
            for (int i = 1; i < 5; i++)
            {
                listBox3.Items.Add(string.Format("add data:{0}",i));
                mem.Write(bytes, 0, bytes.Length);
            }
            listBox3.Items.Add(string.Format("all memory:{0}", mem.Capacity));
            listBox3.Items.Add(string.Format("used memory:{0}", mem.Length));
        }

        private void button6_Click(object sender, EventArgs e)
        {
            FileStream fs;
            fs = new FileStream("c:\\Binfile.dat", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            BinaryWriter bw = new BinaryWriter(fs);

            double adouble = 1234.67;
            int aint = 34567;
            char[] achararray = {'A','B','C','a','b','c'};

            bw.Write(adouble);
            bw.Write(aint);
            bw.Write(achararray);
            int length = Convert.ToInt32(bw.BaseStream.Length);
            fs.Close();
            bw.Close();

            fs = new FileStream("c:\\Binfile.dat", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            BinaryReader br = new BinaryReader(fs);
            richTextBox2.AppendText(br.ReadDouble().ToString()+" \n");
            richTextBox2.AppendText(br.ReadInt32().ToString() + " \n");
            char[] data = br.ReadChars(length);
            for (int i = 0; i < data.Length; i++)
            {
                richTextBox2.AppendText(string.Format("{0,7:x}",data[i]) + " \n");
            }
            fs.Close();
            br.Close();
        }

    }
}
