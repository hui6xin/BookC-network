using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Net.NetworkInformation;

namespace IPGlobalStatics
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            IPGlobalProperties properties = IPGlobalProperties.GetIPGlobalProperties();
            IPGlobalStatistics ipstat = properties.GetIPv4GlobalStatistics();
            listBoxResult.Items.Add("本机所在域 ...... : " + properties.DomainName);
            listBoxResult.Items.Add("接收数据包数 .... : " + ipstat.ReceivedPackets);
            listBoxResult.Items.Add("转发数据包数 .... : " + ipstat.ReceivedPacketsForwarded);
            listBoxResult.Items.Add("传送数据包数 .... : " + ipstat.ReceivedPacketsDelivered);
            listBoxResult.Items.Add("丢弃数据包数 .... : " + ipstat.ReceivedPacketsDiscarded);
        }
    }
}
