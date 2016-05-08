using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;

namespace test1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

     

        private void buttonDns_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                //解析主机名
                IPHostEntry IPinfo = Dns.GetHostEntry(textBox1.Text);
                //清空列表框
                listBox1.Items.Clear();
                listBox2.Items.Clear();
                //显示IP地址
                foreach (IPAddress IP in IPinfo.AddressList)
                {
                    listBox1.Items.Add(IP.ToString());
                }
                //显示别名
                foreach (string alias in IPinfo.Aliases)
                {
                    listBox2.Items.Add(alias);
                }
                //显示主机名
                textBox2.Text = IPinfo.HostName;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
    }
}
