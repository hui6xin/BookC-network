using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
namespace ThreadExample
{
    public partial class Form1 : Form
    {
        Thread thread1, thread2;
        Class1 class1;

        public Form1()
        {
            InitializeComponent();
            class1 = new Class1(this);
            buttonStart.Click += new EventHandler(buttonStart_Click);
            buttonStop.Click += new EventHandler(buttonStop_Click);
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            class1.shouldStop = false;
            thread1 = new Thread(class1.Method1);
            thread1.IsBackground = true;
            thread2 = new Thread(class1.Method2);
            thread2.IsBackground = true;
            thread1.Start("a method start\n");
            thread2.Start();

        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            class1.shouldStop = true;
            thread1.Join(0);
            thread2.Join(0);
        }

        private delegate void AddMessageDelegate(string message);
        public void AddMessage(string message)
        {
            if (richTextBox1.InvokeRequired)
            {
                AddMessageDelegate d = AddMessage;
                richTextBox1.Invoke(d, message);
            }
            else
            {
                richTextBox1.AppendText(message);
            }
        }
    }
}
