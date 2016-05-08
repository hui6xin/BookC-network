using System;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace UdpChatExample
{
    public partial class FormChat : Form
    {
        /// <summary>接收用</summary>
        private UdpClient receiveUdpClient;
        /// <summary>发送用</summary>
        private UdpClient sendUdpClient;
        /// <summary>和本机绑定的端口号</summary>
        private const int port = 18001;
        /// <summary>本机IP</summary>
        IPAddress ip;
        /// <summary>远程主机IP</summary>
        IPAddress remoteIp;
        public FormChat()
        {
            InitializeComponent();
            //获取本机可用IP地址
            IPAddress[] ips = Dns.GetHostAddresses(Dns.GetHostName());
            ip = ips[ips.Length - 1];
            //为了在同一台机器调试，此IP也作为默认远程IP
            remoteIp = ip;
            textBoxRemoteIP.Text = remoteIp.ToString();
            textBoxSend.Text = "你好！";
        }
        private void FormChat_Load(object sender, EventArgs e)
        {
            //创建一个线程接收远程主机发来的信息
            Thread myThread = new Thread(ReceiveData);
            //将线程设为后台运行
            myThread.IsBackground = true;
            myThread.Start();
            textBoxSend.Focus();
        }
        private void ReceiveData()
        {
            IPEndPoint local = new IPEndPoint(ip, port);
            receiveUdpClient = new UdpClient(local);
            IPEndPoint remote = new IPEndPoint(IPAddress.Any, 0);
            while (true)
            {
                try
                {
                    //关闭udpClient时此句会产生异常
                    byte[] receiveBytes = receiveUdpClient.Receive(ref remote);
                    string receiveMessage = Encoding.Unicode.GetString(
                        receiveBytes, 0, receiveBytes.Length);
                    AddItem(listBoxReceive, string.Format("来自{0}：{1}", remote, receiveMessage));
                }
                catch
                {
                    break;
                }
            }
        }
        private void buttonSend_Click(object sender, EventArgs e)
        {
            Thread t = new Thread(SendMessage);
            t.IsBackground = true;
            t.Start(textBoxSend.Text);
        }
        /// <summary>发送数据到远程主机</summary>
        private void SendMessage(object obj)
        {
            string message = (string)obj;
            sendUdpClient = new UdpClient(0);
            byte[] bytes = System.Text.Encoding.Unicode.GetBytes(message);
            IPEndPoint iep = new IPEndPoint(remoteIp, port);
            try
            {
                sendUdpClient.Send(bytes, bytes.Length, iep);
                AddItem(listBoxStatus, string.Format("向{0}发送：{1}", iep, message));
                ClearTextBox();
            }
            catch (Exception ex)
            {
                AddItem(listBoxStatus, "发送出错:" + ex.Message);
            }
        }
        delegate void AddListBoxItemDelegate(ListBox listbox, string text);
        private void AddItem(ListBox listbox, string text)
        {
            if (listbox.InvokeRequired)
            {
                AddListBoxItemDelegate d = AddItem;
                listbox.Invoke(d, new object[] { listbox, text });
            }
            else
            {
                listbox.Items.Add(text);
                listbox.SelectedIndex = listbox.Items.Count - 1;
                listbox.ClearSelected();
            }
        }
        delegate void ClearTextBoxDelegate();
        private void ClearTextBox()
        {
            if (textBoxSend.InvokeRequired)
            {
                ClearTextBoxDelegate d = ClearTextBox;
                textBoxSend.Invoke(d);
            }
            else
            {
                textBoxSend.Clear();
                textBoxSend.Focus();
            }
        }
    }
}
