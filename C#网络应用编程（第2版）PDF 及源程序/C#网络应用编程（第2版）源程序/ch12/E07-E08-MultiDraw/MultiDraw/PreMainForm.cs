using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Threading;
using System.Diagnostics;
using System.Net.Sockets;
using System.IO;

namespace MultiDraw
{
    public partial class PreMainForm : Form
    {
        public PreMainForm()
        {
            InitializeComponent();
            toolStripLabel1.Text = "";
            IPAddress[] addrIP = Dns.GetHostAddresses(Dns.GetHostName());
            int n = addrIP.Length - 1;
            textBoxLocal.Text = addrIP[n].ToString();
            textBoxServer.Text = addrIP[n].ToString();
            groupBox1.Enabled = false;
            radioButtonServer.Checked = true;
            CC.userState = UserState.Server;
            CC.isNeedRunMainForm = false;
        }

        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            groupBox1.Enabled = !radioButtonServer.Checked;
            if (radioButtonServer.Checked)
            {
                CC.userState = UserState.Server;
            }
            else
            {
                CC.userState = UserState.Client;
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {

            buttonOK.Enabled = false;
            if (radioButtonServer.Checked)//本机为主机
            {
                CC.myServer = new MyServer();
                //创建新客户端，用于自己与本机通信
                CC.me = new MyClient();
                textBoxLocal.Text = CC.me.LocalIPString;
                textBoxServer.Text = textBoxLocal.Text;
                IPEndPoint ipe = new IPEndPoint(IPAddress.Parse(textBoxLocal.Text), MyServer.port);
                CC.me.client = new TcpClient(AddressFamily.InterNetwork);
                CC.me.client.Connect(ipe);
                CC.me.CreateNetStream();
                CC.me.InitReceiveThread();
                CC.isNeedRunMainForm = true;
                this.Close();
            }
            else
            {
                IPAddress ip;
                if (IPAddress.TryParse(textBoxServer.Text, out ip) == false)
                {
                    toolStripLabel1.Text = "主机IP地址格式不正确，请重新输入！";
                    buttonOK.Enabled = true;
                    return;
                }
                else
                {
                    toolStripLabel1.Text = "正在连接主机，请稍等……";
                    backgroundWorker1.RunWorkerAsync();
                    while (backgroundWorker1.IsBusy)
                    {
                        if (backgroundWorker1.CancellationPending == false)
                        {
                            Application.DoEvents();
                        }
                    }
                }
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            CC.isNeedRunMainForm = false;
            if (CC.userState == UserState.Server)
            {
                if (CC.myServer != null)
                {
                    if (CC.myServer.myListener != null)
                    {
                        CC.myServer.myListener.Stop();
                    }
                }
            }
            else
            {
                if (backgroundWorker1.IsBusy)
                {
                    backgroundWorker1.CancelAsync();
                }
            }
            this.Close();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            Debug.Print("开始与主机连接");
            IPAddress ip = IPAddress.Parse(textBoxServer.Text);
            CC.me = new MyClient();
            IPEndPoint ipe = new IPEndPoint(ip, MyServer.port);
            CC.me.client = new TcpClient(AddressFamily.InterNetwork);
            try
            {
                CC.me.client.Connect(ipe);
            }
            catch
            {
                toolStripLabel1.Text = "与主机连接失败，请查看“" + textBoxServer.Text + "”是否为主机并处于制作状态！";
                e.Cancel = true;
                return;
            }
            CC.me.CreateNetStream();
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == false)
            {
                if (e.Error == null)
                {
                    Debug.Print("本机与主机连接成功，开始启动InitReceiveThread");
                    CC.me.InitReceiveThread();
                    CC.isNeedRunMainForm = true;
                    this.Close();
                }
            }
            else
            {
                CC.isNeedRunMainForm = false;
                buttonOK.Enabled = true;
            }
        }
    }
}