using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;
using System.IO;

namespace WindowsForms5
{
    public partial class SyncChatClient : Form
    {
        private bool isExit = false;
        private TcpClient client;
        private BinaryReader br;
        private BinaryWriter bw;
       
       
        public SyncChatClient()
        {
            InitializeComponent();
            Random r = new Random((int)DateTime.Now.Ticks);
            textBox1.Text = "user" + r.Next(100, 999);
            listBox2.HorizontalScrollbar = true;
        }

        private void SendMessage(string message)
        {
            try
            {
                bw.Write(message);
                bw.Flush();
            }
            catch
            {
                AddTalkMessage("send fail!!!!!!!!!!!!!");
            }
        }
        private delegate void AddOnlineDelegate(string message);
        private void AddOnline(string message)
        {
            if (listBox2.InvokeRequired)
            {
                AddOnlineDelegate d = new AddOnlineDelegate(AddOnline);
                listBox2.Invoke(d, new object[] { message });
            }
            else
            {
                listBox2.Items.Add(message);
                listBox2.SelectedIndex = listBox2.Items.Count - 1;
                listBox2.ClearSelected();
            }
        }
        private delegate void RemoveUserNamedelegate(string message);
        private void RemoveUserName(string message)
        {
            if (listBox2.InvokeRequired)
            {
                RemoveUserNamedelegate d = new RemoveUserNamedelegate(AddOnline);
                listBox2.Invoke(d, new object[] { message });
            }
            else
            {
                listBox2.Items.Remove(message);
                listBox2.SelectedIndex = listBox2.Items.Count - 1;
                listBox2.ClearSelected();
            }
        }
        
        private delegate void MessageDelegate(string message);
        private void AddTalkMessage(string message)
        {
            if (richTextBox1.InvokeRequired)
            {
                MessageDelegate d = new MessageDelegate(AddTalkMessage);
                richTextBox1.Invoke(d, new object[] { message });

            }
            else
            {
                richTextBox1.AppendText(message + Environment.NewLine);
                richTextBox1.ScrollToCaret();
            }
        }
        private void ReceiveData()
        {
            string receviestring = null;
            while (isExit == false)
            {
                try
                {
                    receviestring = br.ReadString();
                }
                catch
                {
                    if (isExit == false)
                    {
                        MessageBox.Show("diconnect");
                    }
                    break;
                }
                string[] splitstring = receviestring.Split(',');
                string command = splitstring[0].ToLower();
                switch (command)
                {
                    case "login":
                        AddOnline(splitstring[1]);
                        break;
                    case "logout":
                        RemoveUserName(splitstring[1]);
                        break;
                    case "talk":
                        AddTalkMessage(splitstring[1] + ":\r\n");
                        AddTalkMessage(receviestring.Substring(splitstring[0].Length + splitstring[1].Length + 2));
                        break;
                    default:
                        AddTalkMessage("unkown");
                        break;

                }
            }
            
        }
        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            try
            {
                client = new TcpClient(Dns.GetHostName(), 51888);
                Console.Write("connect success");
            }
            catch
            {
                Console.Write("connect fial");
                button1.Enabled = true;
            }
            NetworkStream networkstream = client.GetStream();

            br = new BinaryReader(networkstream);
            bw = new BinaryWriter(networkstream);
            SendMessage("Login," + textBox1.Text);
            Thread threadreceive = new Thread(new ThreadStart(ReceiveData));
            threadreceive.IsBackground = true;
            threadreceive.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox2.SelectedIndex != -1)
            {
                SendMessage("Talk," + listBox2.SelectedItem + "," + textBox2.Text + "\r\n");
                textBox2.Clear();
            }
            else
            {
                MessageBox.Show("choose one first");
            }
        }

        private void SyncChatClient_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (client != null) ;
            {
                SendMessage("Logout," + textBox1.Text);
                isExit = true;
                br.Close();
                bw.Close();
                client.Close();
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                button2.PerformClick();
            }
        }
    }
}
