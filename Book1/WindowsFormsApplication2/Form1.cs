using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            

            sss1.dt = dt1;
            sss1.i = 0;
            sss1.label = label1;


            sss2.dt = dt2;
            sss2.i = 0;
            sss2.label = label2;


            sss3.dt = dt3;
            sss3.i = 0;
            sss3.label = label3;


            sss4.dt = dt4;
            sss4.i = 0;
            sss4.label = label4;

            
        }
        sss sss1 = new sss();
        sss sss2 = new sss();
        sss sss3 = new sss();
        sss sss4 = new sss();
        private DateTime dt1 = DateTime.Now;
        private DateTime dt2 = DateTime.Now;
        private DateTime dt3 = DateTime.Now;
        private DateTime dt4 = DateTime.Now;
        private bool b1 = true;
        private bool b2 = true;
        private bool b3= true;
        private bool b4 = true;
        private void thread(object obj)
        {
            sss lb = (sss)obj;

            DateTime dt = DateTime.Now;
            while (true)
            {
                if ((DateTime.Now - lb.dt).TotalSeconds > lb.i)
                {
                    lb.i = (DateTime.Now - lb.dt).TotalSeconds;
                    AddMessage(lb.i.ToString(), lb.label);
                }
                lb.dt = DateTime.Now;
                if (B_1() && B_2() && B_3() && B_4())
                {
                    while ((DateTime.Now - dt).TotalMilliseconds < 500)
                    {
                        Thread.Sleep(10);
                    }
                    dt = DateTime.Now;
                }
            }
        }
        private delegate void AddMessageDelegate(string message,Label lbtemp);
        public void AddMessage(string message, Label lbtemp)
        {
            if (lbtemp.InvokeRequired)
            {
                AddMessageDelegate d = AddMessage;
                lbtemp.Invoke(d, message, lbtemp);
            }
            else
            {
                lbtemp.Text = message;
            }

        }
        public struct sss
        {
            public DateTime dt;
            public double i ;
            public Label label;
        }
        public  bool B_1()
        {
            return b1;
        }
        public  bool B_2()
        {
            return b2;
        }
        public  bool B_3()
        {
            return b3;
        }
        public  bool B_4()
        {
            return b4;
        }
        private bool bstart = false;
        private void button1_Click(object sender, EventArgs e)
        {
            sss1.i = 0;
            sss1.label.Text = "0";
            sss2.i = 0;
            sss2.label.Text = "0";
            sss3.i = 0;
            sss3.label.Text = "0";
            sss4.i = 0;
            sss4.label.Text = "0";
            if (!bstart)
            {
                Thread th1 = new Thread(new ParameterizedThreadStart(thread));
                th1.IsBackground = true;
                Thread th2 = new Thread(new ParameterizedThreadStart(thread));
                th2.IsBackground = true;
                Thread th3 = new Thread(new ParameterizedThreadStart(thread));
                th3.IsBackground = true;
                Thread th4 = new Thread(new ParameterizedThreadStart(thread));
                th4.IsBackground = true;
                th1.Start(sss1);
                th2.Start(sss2);
                th3.Start(sss3);
                th4.Start(sss4);
                bstart = true;
            }
        }
    }
}
