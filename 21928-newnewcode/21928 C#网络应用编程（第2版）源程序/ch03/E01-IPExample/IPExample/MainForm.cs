using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Net;

namespace IPExample
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 获取本机IP信息
        /// </summary>
        private void buttonLocalIP_Click(object sender, EventArgs e)
        {
            listBoxLocalInfo.Items.Clear();
            string name = Dns.GetHostName();
            listBoxLocalInfo.Items.Add("本机主机名：" + name);
            IPHostEntry me = Dns.GetHostEntry(name);
            listBoxLocalInfo.Items.Add("本机所有IP地址：");
            foreach (IPAddress ip in me.AddressList)
            {
                listBoxLocalInfo.Items.Add(ip);
            }
            IPAddress localip = IPAddress.Parse("127.0.0.1");
            IPEndPoint iep = new IPEndPoint(localip, 80);
            listBoxLocalInfo.Items.Add("IP端点: " + iep.ToString());
            listBoxLocalInfo.Items.Add("IP端口: " + iep.Port);
            listBoxLocalInfo.Items.Add("IP地址: " + iep.Address);
            listBoxLocalInfo.Items.Add("IP地址族: " + iep.AddressFamily);
            listBoxLocalInfo.Items.Add("可分配端口最大值: " + IPEndPoint.MaxPort);
            listBoxLocalInfo.Items.Add("可分配端口最小值: " + IPEndPoint.MinPort);
        }
        /// <summary>
        /// 获取远程主机信息
        /// </summary>
        private void buttonRemoteIP_Click(object sender, EventArgs e)
        {
            
            this.listBoxRemoteInfo.Items.Clear();
            IPHostEntry remoteHost = Dns.GetHostEntry(this.textBoxRmoteIP.Text);
            IPAddress[] remoteIP = remoteHost.AddressList;
            IPEndPoint iep;
            foreach (IPAddress ip in remoteIP)
            {
                iep = new IPEndPoint(ip, 80);
                listBoxRemoteInfo.Items.Add(iep);
            }
        }
    }
}
