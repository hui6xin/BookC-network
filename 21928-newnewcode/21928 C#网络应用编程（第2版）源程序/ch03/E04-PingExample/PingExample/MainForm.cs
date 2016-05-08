using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Net.NetworkInformation;
namespace PingExample
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            this.textBoxRemoteIp.Text = "127.0.0.1";
        }

        private void buttonPing_Click(object sender, EventArgs e)
        {
            this.listBoxResult.Items.Clear();
            //远程服务器IP
            string ipString = this.textBoxRemoteIp.Text.ToString().Trim();
            //构造Ping实例
            Ping pingSender = new Ping();
            //Ping选项设置
            PingOptions options = new PingOptions();
            options.DontFragment = true;
            //测试数据
            string data = "test data abcabc";
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            //设置超时时间
            int timeout = 120;
            //调用同步send方法发送数据，将返回结果保存至PingReply实例
            PingReply reply = pingSender.Send(ipString, timeout, buffer, options);
            if (reply.Status == IPStatus.Success)
            {
                listBoxResult.Items.Add("答复的主机地址: " + reply.Address.ToString());
                listBoxResult.Items.Add("往返时间: " + reply.RoundtripTime);
                listBoxResult.Items.Add("生存时间（TTL）: " + reply.Options.Ttl);
                listBoxResult.Items.Add("是否控制数据包的分段: " + reply.Options.DontFragment);
                listBoxResult.Items.Add("缓冲区大小: " + reply.Buffer.Length);
            }
            else
            {
                listBoxResult.Items.Add(reply.Status.ToString());
            }
        }
    }
}
