using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Scloseform
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            init();
            Thread threadserver = new Thread(Form1.threadserver);
            threadserver.IsBackground = true;
            threadserver.Start();
        }

        private void init()
        {
            int le=235;
            byte[] byte2 = new byte[le];
            //textBox1.AppendText(int.MaxValue.ToString("F") + "   " + double.MaxValue.ToString("F") + "   " + long.MaxValue.ToString("F") + "    " + Double.MaxValue.ToString("F"));
            IPHostEntry hostInfo = Dns.GetHostByName(@"www.baidu.com");//通过网址获取ip
            //IPHostEntry hostInfo = Dns.GetHostEntry(@"www.baidu.com");
            textBox1.AppendText(hostInfo.AddressList[0].ToString());


        }

        private static void threadserver()
        {
            IPEndPoint ipep = new IPEndPoint(IPAddress.Any, 9050);//本地ip和端口
            Socket newsockt = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);//新建socket
            newsockt.Bind(ipep);
            newsockt.Listen(10);//最大连接数
            Console.WriteLine("waiting for a client");


            while (true)
            {
                Socket client = newsockt.Accept();//当有可用的客户端连接尝试时执行，并返回一个新的socket,用于与客户端之间的通信
                //线程等待 等待连接
                Thread threadsend = new Thread(Form1.sendclose);
                threadsend.Start(client);
            }
            newsockt.Close();
        }

        private static void sendclose(object socket1)
        {
            Socket client = (Socket)socket1;
            IPEndPoint clientip = (IPEndPoint)client.RemoteEndPoint;
            Console.WriteLine("connect with client:" + clientip.Address + " at port:" + clientip.Port);
            int recv;//用于表示客户端发送的信息长度
            byte[] data = new byte[1024];//用于缓存客户端所发送的信息,通过socket传递的信息必须为字节数组
            DateTime dt = DateTime.Now;
            while (true)
            {
                ////用死循环来不断的从客户端获取信息
                //data = new byte[1024];
                //recv = client.Receive(data);
                //Console.WriteLine("recv=" + recv);
                //if (recv == 0)//当信息长度为0，说明客户端连接断开
                //    break;
                //Console.WriteLine(Encoding.ASCII.GetString(data, 0, recv));
                if ((DateTime.Now - dt).TotalSeconds > 1)
                {
                    Array.Clear(data, 0, data.Length);
                    data = Encoding.ASCII.GetBytes("c1lose");
                    //client.Send(data, recv, SocketFlags.None);
                    try
                    {
                        client.Send(data);
                    }
                    catch
                    {
                        break;
                    }
                }
            }
            Console.WriteLine("Disconnected from" + clientip.Address);
            client.Close();
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 formc1 = new Form2();
            formc1.Text = "chuangkou1";
            formc1.StartPosition = FormStartPosition.WindowsDefaultLocation;
            formc1.Show();
            this.BringToFront();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 formc2 = new Form2();
            formc2.Text = "chuangkou2";
            formc2.StartPosition = FormStartPosition.WindowsDefaultLocation;
            formc2.Show();
            this.BringToFront();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form2 formc3 = new Form2();
            formc3.Text = "chuangkou3";
            formc3.StartPosition = FormStartPosition.WindowsDefaultLocation;
            formc3.Show();
            this.BringToFront();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form2 formc4 = new Form2();
            formc4.Text = "chuangkou4";
            formc4.StartPosition = FormStartPosition.WindowsDefaultLocation;
            formc4.Show();
            this.BringToFront();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            IntPtr handle1 = Classpub.FindWindow(null, "chuangkou1");
            if (handle1 != IntPtr.Zero)
            {
                Classpub.PostMessage(handle1, Classpub.WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
                //MessageBox.Show("chuangkou1", "MessageBox1111");
                //MessageBox.Show("postmessage 这里是跟随运行的窗口", "窗口");
            }
        }
        private void button6_Click(object sender, EventArgs e)
        {
            IntPtr handle1 = Classpub.FindWindow(null, "chuangkou2");
            if (handle1 != IntPtr.Zero)
            {
                Classpub.SendMessage(handle1, Classpub.WM_CLOSE, 0, 0);
                //MessageBox.Show("chuangkou2", "MessageBox1111");
                //MessageBox.Show("sendmessage 这里是跟随运行的窗口", "窗口");
            }
        }
        public int indexxx=1;
        public DateTime dt1 = DateTime.Now;
        private void timer2_Tick(object sender, EventArgs e)
        {
            //IntPtr handle1 = Classpub.FindWindow(null, "发起会话");
            //if (handle1 != IntPtr.Zero)
            //{
            //    Classpub.PostMessage(handle1, Classpub.WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
            //    //MessageBox.Show("找到发起会话");
            //}

            //while (indexxx < 10)
            //{
            //    Form2 fff = new Form2();
            //    fff.Text = indexxx.ToString() + "     " + (DateTime.Now - dt1).TotalMilliseconds.ToString("F");
            //    fff.StartPosition = FormStartPosition.WindowsDefaultLocation;
            //    fff.Show();
            //    indexxx++;
            //    dt1 = DateTime.Now;
            //}
            //pictureBox1.Load(Application.StartupPath + "\\" + indexxx + ".jpg");
            if (richTextBox1.Lines.Length > 100)
                richTextBox1.Clear();
            pictureBox1.Load(Application.StartupPath + "\\" + indexxx + ".png");
            
            pictureBox1.Refresh();
            richTextBox1.AppendText((DateTime.Now - dt1).TotalMilliseconds.ToString() + "\n");
            //richTextBox1.Select(richTextBox1.TextLength, 0);
            richTextBox1.SelectionStart = richTextBox1.TextLength;
            richTextBox1.ScrollToCaret();
            dt1 = DateTime.Now;
            indexxx++;
            if (indexxx > 6)
                indexxx = 1;

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            IntPtr handle1 = Classpub.FindWindow(null, "MessageBox1111");
            if (handle1 != IntPtr.Zero)
            {
                Classpub.PostMessage(handle1, Classpub.WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
                //MessageBox.Show("MessageBox");
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {

            //for (int index = 0; index < 999; index++)
            //{
            //    Form2 fff = new Form2();
            //    fff.Text = index.ToString();
            //    fff.StartPosition = FormStartPosition.WindowsDefaultLocation;
            //    fff.Show();
            //}
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            timer2.Interval = 2 * trackBar1.Value;
            label1.Text = timer2.Interval.ToString();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
                timer2.Enabled = true;
            else
            {
                timer2.Enabled = false;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
                timer2.Enabled = true;
            else
            {
                timer2.Enabled = false;
            }
        }

       
    }
}
