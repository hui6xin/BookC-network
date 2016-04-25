using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;

namespace WindowsForms2._5
{
    public partial class Form1 : Form
    {
        //[DllImport("Btdrt.dll", SetLastError = true)]
        //public static extern int BthReadLocalAddr(byte[] pba);  
        public Form1()
        {
            InitializeComponent();
            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.WorkerSupportsCancellation = true;
            button3.Enabled = false;
           
        }
        private ListNode l1 = new ListNode(1);
        private ListNode l2 = new ListNode(2);
        private ListNode l3 = new ListNode(3);
        private ListNode l4 = new ListNode(4);
        private ListNode l5 = new ListNode(5); 
        private IList<int> ill=new List<int>();
        private void button1_Click(object sender, EventArgs e)
        {
            //int ix = 0;
            //byte[] pba=new byte[5];
            //ix = BthReadLocalAddr(pba);
            
            l1.next = l2;
            l2.next = l3;
            l3.next = l4;
            l4.next = l5;
            SwapPairs(l1);

            GrayCode(3);
            //ill = new List<int>();
            
        }
        public IList<int> GrayCode(int n)
        {
            double i = Math.Pow(2, n);
            IList<int> ill = new List<int>();
            for (int index = 0; index < i; index++)
            {
                ill.Add(index);
            }
            return ill;
        }
        public ListNode SwapPairs(ListNode head)
        {
            if (head != null && head.next != null)
            {
                ListNode temp = head.next.next;
                ListNode newhead = head.next;
                head.next.next = head;
                if (temp != null)
                {
                    temp=SwapPairs(temp);
                }
                head.next = temp;
                return newhead;
            }
            else
                return head;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "begin random";
            button2.Enabled = false;
            button3.Enabled = true;

            backgroundWorker1.RunWorkerAsync();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            //不要直接使用 backgroundWorker1，当多个backgroundWorker会出现耦合
            BackgroundWorker worker = sender as BackgroundWorker;
            //下面是相当于线程处理的内容//注意：不要操作界面控件
            Random r = new Random();
            int numCount = 0;
            while (worker.CancellationPending == false)
            {
                int num = r.Next(10000);
                if (num % 5 == 0)
                {
                    numCount++;
                    worker.ReportProgress(0, num);
                    Thread.Sleep(1000);
                }
            }
            e.Result = numCount;
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            int num = (int)e.UserState;
            richTextBox1.Text += num + " ";
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                richTextBox1.Text += "\n\n finish,have" + e.Result + "can %5";
            }
            else
            {
                richTextBox1.Text += "\n error" + e.Error;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            backgroundWorker1.CancelAsync();
            button2.Enabled = true;
            button3.Enabled = false;
        }
        
    }
    public class ListNode 
    {
        public int val;
        public ListNode next;
        public ListNode(int x) 
        { 
            val = x; 
        }
        public ListNode()
        {
            this.val = 1;
        }
    }
}
