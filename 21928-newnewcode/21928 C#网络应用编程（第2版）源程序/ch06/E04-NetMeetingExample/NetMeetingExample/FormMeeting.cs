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

namespace NetMeetingExample
{
    public partial class FormMeeting : Form
    {
        private enum ListBoxOperation { AddItem, RemoveItem };
        private delegate void SetListBoxItemCallback(
            ListBox listbox, string text, ListBoxOperation operation);
        SetListBoxItemCallback listBoxCallback;
        //使用的IP地址
        private IPAddress broderCastIp = IPAddress.Parse("224.100.0.1");
        //使用的接收端口号
        private int port = 8001;
        private UdpClient udpClient;
        public FormMeeting()
        {
            InitializeComponent();
            listBoxCallback = new SetListBoxItemCallback(SetListBoxItem);
        }
        private void SetListBoxItem(ListBox listbox, string text, ListBoxOperation operation)
        {
            if (listbox.InvokeRequired == true)
            {
                this.Invoke(listBoxCallback, listbox, text, operation);
            }
            else
            {
                if (operation == ListBoxOperation.AddItem)
                {
                    if (listbox == listBoxAddress)
                    {
                        if (listbox.Items.Contains(text) == false)
                        {
                            listbox.Items.Add(text);
                        }
                    }
                    else
                    {
                        listbox.Items.Add(text);
                    }
                    listbox.SelectedIndex = listbox.Items.Count - 1;
                    listbox.ClearSelected();
                }
                else if (operation == ListBoxOperation.RemoveItem)
                {
                    listbox.Items.Remove(text);
                }
            }
        }
        private void SendMessage(IPAddress ip, string sendString)
        {
            UdpClient myUdpClient = new UdpClient();
            //允许发送和接收广播数据报
           // myUdpClient.EnableBroadcast = true;
            //必须使用组播地址范围内的地址
            IPEndPoint iep = new IPEndPoint(ip, port);
            //将发送内容转换为字节数组
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(sendString);
            try
            {
                //向子网发送信息
                myUdpClient.Send(bytes, bytes.Length, iep);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "发送失败");
            }
            finally
            {
                myUdpClient.Close();
            }
        }
        private void FormMeeting_Load(object sender, EventArgs e)
        {
            listBoxMessage.HorizontalScrollbar = true;
            buttonLogin.Enabled = true;
            buttonLogout.Enabled = false;
            groupBoxRoom.Enabled = false;
        }
        /// <summary>
        /// 接收线程
        /// </summary>
        private void ReceiveMessage()
        {
            udpClient = new UdpClient(port);
            //必须使用组播地址范围内的地址
            udpClient.JoinMulticastGroup(broderCastIp);
            udpClient.Ttl = 50;
            IPEndPoint remote = null;
            while (true)
            {
                try
                {
                    //关闭udpClient时此句会产生异常
                    byte[] bytes = udpClient.Receive(ref remote);
                    string str = Encoding.UTF8.GetString(bytes, 0, bytes.Length);
                    string[] splitString = str.Split(',');
                    int s = splitString[0].Length;
                    switch (splitString[0])
                    {
                        case "Login":  //进入会议室 
                            SetListBoxItem(listBoxMessage,
                                string.Format("[{0}]进入。", remote.Address), ListBoxOperation.AddItem);
                            SetListBoxItem(listBoxAddress,
                            remote.Address.ToString(), ListBoxOperation.AddItem);
                            string userListString = "List," + remote.Address.ToString();
                            for (int i = 0; i < listBoxAddress.Items.Count; i++)
                            {
                                userListString += "," + listBoxAddress.Items[i].ToString();
                            }
                            SendMessage(remote.Address, userListString);
                            break;
                        case "List": //参加会议人员名单
                            for (int i = 1; i < splitString.Length; i++)
                            {
                                SetListBoxItem(listBoxAddress,
                                    splitString[i], ListBoxOperation.AddItem);
                            }
                            break;
                        case "Message":  //发言内容
                            SetListBoxItem(listBoxMessage,
                                string.Format("[{0}]说：{1}", remote.Address, str.Substring(8)),
                                ListBoxOperation.AddItem);
                            break;
                        case "Logout": //退出会议室
                            SetListBoxItem(listBoxMessage,
                                string.Format("[{0}]退出。", remote.Address),
                                ListBoxOperation.AddItem);
                            SetListBoxItem(listBoxAddress,
                                remote.Address.ToString(), ListBoxOperation.RemoveItem);
                            break;
                    }
                }
                catch
                {
                    //退出循环，结束线程
                    break;
                }
            }
        }


        private void textBoxMessage_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                if (textBoxMessage.Text.Trim().Length > 0)
                {
                    SendMessage(broderCastIp, "Message," + textBoxMessage.Text);
                    textBoxMessage.Text = "";
                }
            }
        }
        //窗体已关闭并指定关闭原因前触发的事件
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (buttonLogout.Enabled == true)
            {
                MessageBox.Show("请先离开会议室，然后再退出！", "",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //不关闭窗体
                e.Cancel = true;
            }
        }
        //单击进入会议室按钮触发的事件
        private void buttonLogin_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            Thread myThread = new Thread(ReceiveMessage);
            myThread.Start();
            //等待接收线程准备完毕
            Thread.Sleep(1000);
            SendMessage(broderCastIp, "Login");
            buttonLogin.Enabled = false;
            buttonLogout.Enabled = true;
            groupBoxRoom.Enabled = true;
            Cursor.Current = Cursors.Default;
        }
        //单击退出会议室按钮触发的事件
        private void buttonLogout_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            SendMessage(broderCastIp, "Logout");
            udpClient.DropMulticastGroup(this.broderCastIp);
            //等待接收线程处理完毕
            Thread.Sleep(1000);
            //结束接收线程
            udpClient.Close();
            buttonLogin.Enabled = true;
            buttonLogout.Enabled = false;
            groupBoxRoom.Enabled = false;
            Cursor.Current = Cursors.Default;
        }
    }
}