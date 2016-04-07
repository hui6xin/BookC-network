using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.NetworkInformation;
using System.Net;

namespace WindowsForms3._2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            listBox1.Anchor = AnchorStyles.Left |
                AnchorStyles.Right |
                AnchorStyles.Top |
                AnchorStyles.Bottom;

            //3.22
            IPGlobalProperties properties = IPGlobalProperties.GetIPGlobalProperties();
            IPGlobalStatistics ipstat = properties.GetIPv4GlobalStatistics();
            listBox2.Items.Add("本机所在的域：。。。。" + properties.DomainName);
            listBox2.Items.Add("接收数据包数：。。。。" + ipstat.ReceivedPackets);
            listBox2.Items.Add("转发数据包数：。。。。" + ipstat.ReceivedPacketsForwarded);
            listBox2.Items.Add("传送数据包数：。。。。" + ipstat.ReceivedPacketsDelivered);
            listBox2.Items.Add("丢弃数据包数：。。。。" + ipstat.ReceivedPacketsDiscarded);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ShowAdapterInfo();
        }

        private void ShowAdapterInfo()
        {
            NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces();
            listBox1.Items.Add("适配器个数: " + adapters.Length);
            int index = 0;
            foreach (NetworkInterface adapter in adapters)
            {
                index++;
                //显示网络适配器描述的信息。名称。类型。速度。mac
                listBox1.Items.Add("描述信息：{0}"+adapter.Description);
                listBox1.Items.Add("名称：{0}"+adapter.Name);
                listBox1.Items.Add("类型:{0}"+adapter.NetworkInterfaceType);
                listBox1.Items.Add("速度：{0}"+adapter.Speed);
                listBox1.Items.Add("Mac：{0}"+adapter.GetPhysicalAddress());
                //获取ipinterfaceproperties实例
                IPInterfaceProperties adapterproperties = adapter.GetIPProperties();
                //获取dns ip
                IPAddressCollection dnsservers = adapterproperties.DnsAddresses;
                if (dnsservers.Count > 0)
                {
                    foreach (IPAddress dns in dnsservers)
                    {
                        listBox1.Items.Add("DNS：{0}" + dns + "\n");
                    }
                }
                else
                {
                    listBox1.Items.Add("DNS：{0}" + "空" + "\n");
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox3.Items.Clear();
            //ip
            string ipstring = textBox1.Text.ToString().Trim();
            //ping
            Ping pingsender = new Ping();
            //ping setting
            PingOptions options = new PingOptions();
            options.DontFragment = true;
            //test data
            string data = "test data abcdefg";
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            //time out set
            int timeout = 120;
            // send and return pingreply
            PingReply replay = pingsender.Send(ipstring, timeout, buffer, options);
            if (replay.Status == IPStatus.Success)
            {
                listBox3.Items.Add("replay ip;"+replay.Address.ToString());
                listBox3.Items.Add("RoundtripTime:" + replay.RoundtripTime);
                listBox3.Items.Add("Ttl:" + replay.Options.Ttl);
                listBox3.Items.Add("DontFragment数据包是否分段" + replay.Options.DontFragment);
                listBox3.Items.Add("缓冲区大小"+replay.Buffer.Length);
            }
            else
            {
                listBox3.Items.Add(replay.Status.ToString());
            }
        }
    }
}
