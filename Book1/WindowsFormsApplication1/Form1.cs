using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        private Thread thread1111;
        int istep = 0;
        int[] nums;
        public int Istep
        {
            get { return istep; }
            set 
            { 
                istep = value;
                if (istep > 50)
                {
                    mut1.WaitOne();
                    if (this.InvokeRequired)
                    {
                        BeginInvoke(new EventHandler(button2_Click),null);
                    }
                    else
                        button2_Click(null, null);
                    //richTextBox1.AppendText("-----" + Istep.ToString() + "----- " + "\n");
                    mut1.ReleaseMutex();
                }
            }
        }
        TransInfo trasinfolast = new TransInfo();
        Mutex mut1 = new Mutex();
        public Form1()
        {
            InitializeComponent();
            thread1111 = new Thread(threaddoing);
            thread1111.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                int i = int.Parse(textBox1.Text);
                Byte[] bytei = BitConverter.GetBytes(i);
                byte b1 = bytei[0];
                byte b2 = bytei[1];
            }

            byte b3 = 0xff;
            string address = "\n";
            byte[] bb = System.Text.Encoding.Default.GetBytes(address);
            address += b3.ToString("00");



            byte[] byte1 = new byte[] { 1, 0x0a, 2, 3, 0, 0, 0, 0xff, 0xFF, 10 };
            List<byte> byte2 = new List<byte>();
            byte2.AddRange(byte1);

            List<byte> byte3 = new List<byte>();
            //byte2.CopyTo(0, byte3, 1, 5);
            Random rd = new Random();
            Random ro = new Random(Guid.NewGuid().GetHashCode());
            for (int index = 0; index < 100; index++)
            {
                int ix = ro.Next(1, 100);
                //richTextBox1.AppendText(ix.ToString() + " ");
            }

            char c = 'a';
            string s = c.ToString();

            string ss = "6ff66aa6cc6dd601";
            int iindex = -1;
            int iindexold = -1;
            do
            {
                iindexold = iindex + 1;
                iindex = ss.IndexOf("6", iindex + 1);
                if (iindex != -1)
                {
                    s += DateTime.Now.ToString("HH:mm:ss:fff") + "  " + ss.Substring(iindexold, iindex - iindexold + 1);
                }
                else if (ss.Length - 1 > iindexold)
                {
                    s += DateTime.Now.ToString("HH:mm:ss:fff") + "  " + ss.Substring(iindexold, ss.Length - iindexold);
                }
            }
            while (iindex != -1);

            string[] keys = ss.Split('6');
            foreach (string key in keys)
            {
                if (!string.IsNullOrEmpty(key))
                {
                    int code = Convert.ToInt32(key, 16);//将16进制字符串转换成其ASCII码（实际是Unicode码）
                    //char c = (char)code;//取得这个Unicode码表示的char（强制转换就行）
                    //richTextBox2.AppendText(c.ToString());
                    byte bbb = Convert.ToByte(key, 16);
                }
            }
            byte[] byteArray = System.Text.Encoding.Default.GetBytes(ss);

            int iii = this.serialPort1.ReadTimeout;

            get5();
           
            //for (int i = 0; i < itotalcount; i++)
            //{
            //    get5check(ilistred, iblue, ref icount);
            //}
            Thread threadcount = new Thread(threadstart);
            icount = itotalcount = 0;
            threadcount.Start();
            //while (icount == 0)
            //{
            //    m_mutex.WaitOne();
            //    label1.Text = itotalcount.ToString();
            //    m_mutex.ReleaseMutex();
            //    //get5check(ilistred, iblue, ref icount, ref itotalcount);
            //}
            //richTextBox1.AppendText(" " + icount.ToString() + "  " + itotalcount.ToString());
        }
        private int icount = 0;
        private Mutex m_mutex = new Mutex(false);
        private int itotalcount = 0;
        private int[] ilistred = new int[] { 8, 9, 16, 23, 24, 30 };
        private int iblue = 05;
        private void threadstart()
        {
            while (icount == 0)
            {
                get5check(ilistred, iblue, ref icount, ref itotalcount);
            }
            
        }
        private void get5()
        {
            piao[] pp = new piao[5];
            Random rp = new Random(Guid.NewGuid().GetHashCode());
            for (int i = 0; i < pp.Length; i++)
            {
                pp[i].red = new List<int>();
                while (pp[i].red.Count < 6)
                {
                    rp = new Random(Guid.NewGuid().GetHashCode());
                    int itep = rp.Next(1, 34);
                    bool bexsist = false;
                    for (int ii = 0; ii < pp[i].red.Count; ii++)
                    {
                        if (pp[i].red[ii] == itep)
                        {
                            bexsist = true;
                            break;
                        }
                    }
                    if (!bexsist)
                        pp[i].red.Add(itep);
                }
                rp = new Random(Guid.NewGuid().GetHashCode());
                pp[i].blue = rp.Next(1, 17);
            }
            for (int i = 0; i < pp.Length; i++)
            {
                pp[i].red.Sort();
                for (int ii = 0; ii < pp[i].red.Count; ii++)
                {
                    richTextBox1.AppendText(pp[i].red[ii].ToString("00") + " ");
                }
                richTextBox1.AppendText(" | "+pp[i].blue.ToString("00") + " ");
                richTextBox1.AppendText("\n");
            }
        }
        private void get5check(int[] list,int blue,ref int count,ref int itotal)
        {
            piao[] pp = new piao[5];
            Random rp = new Random(Guid.NewGuid().GetHashCode());
            for (int i = 0; i < pp.Length; i++)
            {
                pp[i].red = new List<int>();
                while (pp[i].red.Count < 6)
                {
                    rp = new Random(Guid.NewGuid().GetHashCode());
                    int itep = rp.Next(1, 34);
                    bool bexsist = false;
                    for (int ii = 0; ii < pp[i].red.Count; ii++)
                    {
                        if (pp[i].red[ii] == itep)
                        {                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   
                            bexsist = true;
                            break;
                        }
                    }
                    if (!bexsist)
                        pp[i].red.Add(itep);
                }
                rp = new Random(Guid.NewGuid().GetHashCode());
                pp[i].blue = rp.Next(1, 17);
                pp[i].red.Sort();
                if (pp[i].red[0] == list[0] && pp[i].red[1] == list[1] && pp[i].red[2] == list[2] && pp[i].red[3] == list[3] && pp[i].red[4] == list[4] && pp[i].red[5] == list[5] && pp[i].blue==blue)
                {
                    m_mutex.WaitOne();
                    count++;
                    m_mutex.ReleaseMutex();
                }
                m_mutex.WaitOne();
                itotal++;
                m_mutex.ReleaseMutex();
            }
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            m_mutex.WaitOne();
            label2.Text = itotalcount.ToString();
            m_mutex.ReleaseMutex();
        }
        private void threaddoing()
        {
            while (true)
            {
                Thread.Sleep(1);
                if (Istep >= int.MaxValue)
                    Istep = 0;
                else
                    Istep++;
                TransInfo trasinforead = new TransInfo();
                Istep = 36;//linshi
                trasinforead.order = 0x03;
                Istep = 37;//linshi
                trasinforead.ReaderID = "123";
                Istep = 38;//linshi
                trasinforead.trytime = 0;
                Istep = 39;//linshi
                try
                {
                    trasinfolast = trasinforead;
                    Istep = 40;//linshi
                }
                catch
                {
                    Istep = 120;//linshi
                    Istep = 0;//linshi
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            mut1.WaitOne();
            richTextBox1.AppendText("-----"+Istep.ToString()+"----- "+"\n");
            mut1.ReleaseMutex();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
        }
        public ListNode RemoveElements(ListNode head, int val)
        {

            ListNode listhead = head;
            while (listhead != null && listhead.val == val)
            {
                if (listhead.next != null)
                    listhead = listhead.next;
                else
                    listhead = null;
            }

            ListNode listtemp = head;
            ListNode listlast = head;
            while (listtemp.next != null)
            {
                if (listtemp.val == val)
                {
                    if (listtemp.next != null)
                    {
                        listtemp = listtemp.next;
                        listlast = listtemp;
                    }
                }
                else
                {
                    listlast = listtemp;
                    listtemp = listtemp.next;

                }

            }
            return listhead;
        }
    }
    public struct piao
    {
        public List<int> red;
        public int blue;
    }
    public struct TransInfo
    {
        public byte order;

        public string ReaderID;

        public int value1;
        public int value2;
        public List<int> valuelist;
        public int trytime;
        public DateTime dtsend;
        public int value3;
        /// <summary>
        /// 20个值
        /// </summary>
        public List<int> list;

        /// <summary>
        /// 寄存器地址
        /// </summary>
        public int address;
        /// <summary>
        /// 寄存器起始地址
        /// </summary>
        public int addressstart;
    }
     public class ListNode 
     {
          public int val;
          public ListNode next;
          public ListNode(int x) { val = x; }
     }
}
