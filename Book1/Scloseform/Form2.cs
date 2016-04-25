using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Net.Sockets;
using System.Net;

namespace Scloseform
{
    public partial class Form2 : Form
    {
        private static Label labelone = new Label();
        private static IntPtr handlethis = new IntPtr();
        private static Label labeltwo;
        private static Label labelthree = new Label();
        Thread threadreciv;
        public Form2()
        {
            InitializeComponent();
            init();
            threadreciv = new Thread(threadclient);
            threadreciv.IsBackground = true;
            threadreciv.Start();
        }

        private void init()
        {
            //labelone = new Label();
            labelone.Left = this.Left;
            labelone.Top = 15;
            labelone.Text = "i'm only one";
            this.Controls.Add(labelone);

            labeltwo = new Label();
            labeltwo.Left = this.Left;
            labeltwo.Top = 35;
            labeltwo.Text = "i'm two";
            this.Controls.Add(labeltwo);

            labelthree.Top = 100;
            labelthree.Text = "i'm three";
            this.Controls.Add(labelthree);

            handlethis = this.Handle;
        }

        private static void threadclient()
        {
            return;
            IntPtr handlehandle = handlethis;

            Socket newclient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Console.Write("please input the server ip:");

            //string ipadd = Console.ReadLine();
            string ipadd = "127.0.0.1";
            Console.WriteLine();
            Console.Write("please input the server port:");
            //int port = Convert.ToInt32(Console.ReadLine());
            int port = 9050;

            IPEndPoint ie = new IPEndPoint(IPAddress.Parse(ipadd), port);//服务器的IP和端口

            try
            {
                newclient.Connect(ie);
            }
            catch (SocketException e)
            {
                Console.WriteLine("unable to connect to server");
                Console.WriteLine(e.ToString());
                return;
            }
            byte[] data = new byte[1024];


            int recv;//= newclient.Receive(data);
            string stringdata;//= Encoding.ASCII.GetString(data, 0, recv);
            //Console.WriteLine("recive1 " + stringdata);
            
            while (true)
            {
                //string input = "123";// Console.ReadLine();
                //if (input == "close")
                //{
                //    Classpub.PostMessage(handlethis, Classpub.WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
                //    break;
                //}
                //newclient.Send(Encoding.ASCII.GetBytes(input));
                data = new byte[1024];
                recv = newclient.Receive(data);
                stringdata = Encoding.ASCII.GetString(data, 0, recv);
                Console.Write(stringdata);
                if (stringdata.IndexOf("close") >= 0)
                {
                    Classpub.PostMessage(handlehandle, Classpub.WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
                    break;
                }
                //Console.WriteLine("recive2 " + stringdata );

            }
            Console.WriteLine("disconnect from sercer");
            newclient.Shutdown(SocketShutdown.Both);
            newclient.Close();
        }

        
        protected override void WndProc(ref Message m)
        {
            
            switch (m.Msg)
            {
                case 0x10:
                    //MessageBox.Show("aaaaa");
                    base.DefWndProc(ref m);
                    if (threadreciv.IsAlive)
                        threadreciv.Abort();
                    this.Dispose();
                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
        }
        protected override void DefWndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case 0x10:
                    MessageBox.Show("xxxxx");
                    break;
                default:
                base.DefWndProc(ref m);
                    break;
            }
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (threadreciv.IsAlive)
                threadreciv.Abort();
            this.Dispose();
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (threadreciv.IsAlive)
                threadreciv.Abort();
            this.Dispose();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            labeltwo.Text = handlethis.ToString()+"   ";
        }
    }
}
