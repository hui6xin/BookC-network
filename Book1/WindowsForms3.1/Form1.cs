using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Drawing;
using System.Windows.Forms;
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
using System.Net;
namespace WindowsForms3._1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            listBox1.Items.Clear();
            string name = this.textBox1.Text;//Dns.GetHostName();
            listBox1.Items.Add("name:"+name);
            IPHostEntry me = Dns.GetHostEntry(name);
            listBox1.Items.Add("all ip");
            foreach (IPAddress ip in me.AddressList)
            {
                listBox1.Items.Add(ip);
            }
            IPAddress localip=IPAddress.Parse("127.0.0.1");
            IPEndPoint iep = new IPEndPoint(localip, 80);
            listBox1.Items.Add("ip 端点"+iep.ToString());
            listBox1.Items.Add("ip 端口"+iep.Port);
            listBox1.Items.Add("ip 地址"+iep.Address);
            listBox1.Items.Add("ip 地址族"+iep.AddressFamily);
            listBox1.Items.Add("可分配端口的最大值"+IPEndPoint.MaxPort);
            listBox1.Items.Add("可分配店口最小值"+IPEndPoint.MinPort);
        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            listBox2.Items.Clear();
            IPHostEntry remotehost = Dns.GetHostEntry(this.textBox1.Text);
            IPAddress[] remoteip = remotehost.AddressList;
            IPEndPoint iep;
            foreach (IPAddress ip in remoteip)
            {
                iep = new IPEndPoint(ip, 80);
                listBox2.Items.Add(iep);
            }
        }
    }
    
        
}

