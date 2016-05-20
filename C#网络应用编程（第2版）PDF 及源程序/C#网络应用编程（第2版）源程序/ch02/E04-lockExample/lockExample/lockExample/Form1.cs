using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
namespace lockExample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            Thread[] threads = new Thread[10];
            Account acc = new Account(1000, this);
            for (int i = 0; i < 10; i++)
            {
                Thread t = new Thread(acc.DoTransactions);
                t.Name = "线程" + i;
                threads[i] = t;
            }
            for (int i = 0; i < 10; i++)
            {
                threads[i].Start();
            }
        }
        delegate void AddListBoxItemDelegate(string str);
        public void AddListBoxItem(string str)
        {
            if (listBox1.InvokeRequired)
            {
                AddListBoxItemDelegate d = AddListBoxItem;
                listBox1.Invoke(d, str);
            }
            else
            {
                listBox1.Items.Add(str);
            }
        }
    }
}