using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

//添加的命名空间引用
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace AsyncUdpChatExample
{
    public partial class FormAsyncChat : Form
    {
        delegate void AddListBoxItemCallback(string text);
        AddListBoxItemCallback listBoxCallback;
        //使用的接收端口号
        private int port = 8001;
        //接收信息用UdpClient实例
        UdpClient receiveClient;
        IPEndPoint iep;
        String sendMessage;
        public FormAsyncChat()
        {
            InitializeComponent();
            listBoxCallback = new AddListBoxItemCallback(AddListBoxItem);
        }
        private void AddListBoxItem(string text)
        {
            //如果listBoxReceive被不同的线程访问则通过委托处理；
            if (listBoxReceive.InvokeRequired)
            {
                this.Invoke(listBoxCallback, text);
            }
            else
            {
                listBoxReceive.Items.Add(text);
                listBoxReceive.SelectedIndex = listBoxReceive.Items.Count - 1;
                listBoxReceive.ClearSelected();
            }
        }
        /// <summary>
        /// 在后台运行的接收线程
        /// </summary>
        private void ReceiveData()
        {
            //在本机指定的端口接收
            UdpState udpState = new UdpState();
            udpState.IpEndPoint = null;
            udpState.MyudpClient = receiveClient;
            //接收从远程主机发送过来的信息；
            IAsyncResult ar =  udpState.MyudpClient.BeginReceive(ReceiveUdpClientCallback, udpState);
            ar.AsyncWaitHandle.WaitOne();
            Console.Write("线程结束");
        }
        /// <summary>
        /// 发送数据到远程主机
        /// </summary>
        private void sendData()
        {
            UdpClient sendUdpClient = new UdpClient();
            IPAddress remoteIP;
            if (IPAddress.TryParse(textBoxRemoteIP.Text, out remoteIP) == false)
            {
                MessageBox.Show("远程IP格式不正确");
                return;
            }
            iep = new IPEndPoint(remoteIP, port);
            sendMessage = textBoxSend.Text;
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(sendMessage);
            try
            {
                //异步发送数据
                IAsyncResult ar=
                sendUdpClient.BeginSend(bytes, bytes.Length, iep, SendCallback, sendUdpClient);
                
                textBoxSend.Clear();
                textBoxSend.Focus();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "发送失败");
            }
        }
        /// <summary>
        /// 接收信息回调方法
        /// </summary>
        /// <param name="ar"></param>
        void ReceiveUdpClientCallback(IAsyncResult ar)
        {
            try
            {
                UdpClient u = (UdpClient)((UdpState)(ar.AsyncState)).MyudpClient;
                IPEndPoint remote = (IPEndPoint)((UdpState)(ar.AsyncState)).IpEndPoint;
                Byte[] receiveBytes = u.EndReceive(ar, ref remote);
                string str = Encoding.UTF8.GetString(receiveBytes, 0, receiveBytes.Length);
                AddItem(listBoxReceive, string.Format("来自{0}：{1}", remote, str));
                ReceiveData();
            }
            catch (Exception ex)
            {
                AddItem(listBoxReceive, string.Format("错误信息{0}", ex.ToString()));
            }
        }
        /// <summary>
        /// 初始化窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormChat_Load(object sender, EventArgs e)
        {
            //设置listBox样式
            listBoxReceive.HorizontalScrollbar = true;
            listBoxReceive.Dock = DockStyle.Fill;
            //获取本机第一个可用IP地址
            IPAddress myIP = (IPAddress)Dns.GetHostAddresses(Dns.GetHostName()).GetValue(1);
            //为了在同一台机器调试，此IP也作为默认远程IP
            textBoxRemoteIP.Text = myIP.ToString();
            receiveClient = new UdpClient(port);
            //创建一个线程接收远程主机发来的信息
            Thread myThread = new Thread(ReceiveData);
            //将线程设为后台运行
            myThread.IsBackground = true;
            myThread.Start();
            textBoxSend.Focus();
        }
        /// <summary>
        /// 单击发送按钮触发的事件
        /// </summary>
        private void buttonSend_Click(object sender, EventArgs e)
        {
            sendData();
        }
        /// <summary>
        /// 在textBoxSend中按下并释放Enter键后触发的事件
        /// </summary>
        private void textBoxData_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
                sendData();
        }
        /// <summary>
        /// 关闭窗体，释放资源
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormChat_FormClosing(object sender, FormClosingEventArgs e)
        {
            receiveClient.Close();
        }
        /// </summary>
        /// <param name="ar">IAsyncResult接口</param>
        public void SendCallback(IAsyncResult ar)
        {
            UdpClient udpClient = (UdpClient)ar.AsyncState;
            udpClient.EndSend(ar);
            String message = string.Format("向{0}发送：{1}", iep.ToString(), sendMessage);
            AddItem(listBoxStatus, message);
            udpClient.Close();
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