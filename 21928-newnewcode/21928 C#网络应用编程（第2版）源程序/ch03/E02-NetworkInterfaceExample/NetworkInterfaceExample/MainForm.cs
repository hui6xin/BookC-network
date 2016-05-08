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


namespace NetworkInterfaceExample
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            listBoxAdpterInfo.Anchor = AnchorStyles.Left |
                AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            ShowAdapterInfo();
        }
        /// <summary>
        /// 显示网卡信息
        /// </summary>
        private void ShowAdapterInfo()
        {
            NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces();
            listBoxAdpterInfo.Items.Add("适配器个数：" + adapters.Length);
            int index = 0;
            foreach (NetworkInterface adapter in adapters)
            {
                index++;
                //显示网络适配器描述信息、名称、型号、速度、MAC地址
                listBoxAdpterInfo.Items.Add("-----------------第" + index + "个适配器信息-------------------");
                listBoxAdpterInfo.Items.Add("描述信息：{0}" + adapter.Description);
                listBoxAdpterInfo.Items.Add("名称：{0}" + adapter.Name);
                listBoxAdpterInfo.Items.Add("类型：{0}" + adapter.NetworkInterfaceType);
                listBoxAdpterInfo.Items.Add("速度：{0}" + adapter.Speed);
                listBoxAdpterInfo.Items.Add("MAC地址：{0}" + adapter.GetPhysicalAddress());

                //获取IPInterfaceProperties实例
                IPInterfaceProperties adapterProperties = adapter.GetIPProperties();
                //获取并显示DNS服务器IP地址信息
                IPAddressCollection dnsServers = adapterProperties.DnsAddresses;
                if (dnsServers.Count > 0)
                {
                    foreach (IPAddress dns in dnsServers)
                    {
                        listBoxAdpterInfo.Items.Add("DNS服务器IP地址：{0} " + dns + "\n");
                    }
                }
                else
                {
                    listBoxAdpterInfo.Items.Add("DNS服务器IP地址：{0} " + "\n");
                }
            }
        }

    }
}
