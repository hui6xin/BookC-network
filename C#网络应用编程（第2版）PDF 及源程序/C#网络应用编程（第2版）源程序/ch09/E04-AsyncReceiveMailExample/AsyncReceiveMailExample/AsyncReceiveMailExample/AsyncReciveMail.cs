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
using System.IO;
using System.Threading;

namespace AsyncReceiveMailExample
{
    public partial class AsyncReciveMail : Form
    {
        private TcpClient tcpClient;
        private NetworkStream networkStream;
        private EventWaitHandle waitHandle;

        public AsyncReciveMail()
        {
            InitializeComponent();
            textBoxPOP3Server.Text = "pop3.126.com";
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            listBoxStatus.Items.Clear();
            tcpClient = new TcpClient();
            //开始异步请求
            AddListBoxItem(listBoxStatus, "开始与pop3服务器连接");
            tcpClient.BeginConnect(textBoxPOP3Server.Text, 110, new AsyncCallback(ConnectCallBack), null);
        }
        //异步连接请求
        private void ConnectCallBack(IAsyncResult ar)
        {
            try
            {
                tcpClient.EndConnect(ar);
                AddListBoxItem(listBoxStatus, "连接成功");
            }
            catch (Exception err)
            {
                AddListBoxItem(listBoxStatus, "与服务器连接失败：" + err.Message);
                return;
            }
            networkStream = tcpClient.GetStream();
            waitHandle = new EventWaitHandle(false, EventResetMode.AutoReset);
            ReadMailInfo();
        }

        private void ReadMailInfo()
        {
            //读取服务器回送的连接信息
            ResponseData data = new ResponseData();
            GetResponse(data);
            if (CheckResponse(data.responseString) == false) return;
            //向服务器发送用户名，请求确认
            SendToServer("USER " + textBoxUser.Text+"\r\n");
            GetResponse(data);
            if (CheckResponse(data.responseString) == false) return;
            //向服务器发送密码，请求确认
            SendToServer("PASS " + textBoxPassword.Text+"\r\n");
            GetResponse(data);
            if (CheckResponse(data.responseString) == false) return;
            //向服务器发送LIST命令，请求获取邮件总数和总字节数
            SendToServer("LIST \r\n");
            GetResponse(data);
            if (CheckResponse(data.responseString) == false) return;
            string[] splitString = data.responseString.Split(new string[] { "\r\n" }, StringSplitOptions.None);
            //从字符串中取子串获取邮件总数
            int count;
            string [] strCount = splitString[0].Split(' ');
            if (strCount.Length == 3) //返回形如 +ok 1 1521 这种格式
                count = int.Parse(strCount[1]);
            else //返回形如 +OK
                count = splitString.Length - 2;
            if (splitString.Length < count) return;
            //判断邮箱中是否有邮件
            if (count > 0)
            {
                ClearListBoxItem(listBoxOperation);
                SetGroupBoxText(groupBoxOperation,"信箱中共有 " + count + " 封邮件");
                string str ;
                //向邮件列表框中添加邮件
                for (int i = 1; i <= count; i++)
                {
                    if (splitString[i] == "") continue;
                    string[] s = splitString[i].Split(' ');
                    if (s.Length == 2) 
                        str = string.Format("第{0}封：{1}字节", s[0], s[1]);
                    else 
                        str = string.Format("第{0}封",s[0]);
                    AddListBoxItem(listBoxOperation, str);
                }
            }
            else
            {
                SetGroupBoxText(groupBoxOperation,"信箱中没有邮件");
            }
            SetButtonState(buttonConnect,false);
            SetButtonState(buttonDisconnect,true);
        }

        private void SendToServer(string str)
        {
            AddListBoxItem(listBoxStatus, "发送：" + str);
            byte[] bytes = Encoding.ASCII.GetBytes(str);
            networkStream.BeginWrite(bytes, 0, bytes.Length, new AsyncCallback(WriteCallBack), null);
            waitHandle.WaitOne();
        }

        private void WriteCallBack(IAsyncResult ar)
        {
            string str = (string)ar.AsyncState;
            try
            {
                networkStream.EndWrite(ar);
                AddListBoxItem(listBoxStatus, "发送成功");
            }
            catch (Exception err)
            {
                AddListBoxItem(listBoxStatus, "发送失败：" + err.Message);
            }
            waitHandle.Set();
        }

        private void GetResponse(ResponseData data)
        {
            networkStream.BeginRead(data.bytes, 0, data.bytes.Length, new AsyncCallback(ReadCallBack), data);
            waitHandle.WaitOne();
        }
        private void ReadCallBack(IAsyncResult ar)
        {
            ResponseData data = (ResponseData)ar.AsyncState;
            int count = networkStream.EndRead(ar);
            string str = Encoding.ASCII.GetString(data.bytes, 0, count);
            if (str == null)
            {
                AddListBoxItem(listBoxStatus, "收到：null");
            }
            else
            {
                string[] strSplit = str.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < strSplit.Length ; i++)
                {
                    if (i == 0)//第一行显示"收到"
                        AddListBoxItem(listBoxStatus, "收到：" + strSplit[i]);
                    else
                        AddListBoxItem(listBoxStatus, strSplit[i]);
                }
                if (str.StartsWith("-ERR"))
                {
                    str = null;
                }
            }
            data.responseString = str;
            waitHandle.Set();
        }

        private bool CheckResponse(string str)
        {
            if (str == null)
            {
                return false;
            }
            else
            {
                if (str.StartsWith("+OK"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        private void buttonDisconnect_Click(object sender, EventArgs e)
        {
            StreamWriter sw = new StreamWriter(networkStream, Encoding.ASCII);
            sw.WriteLine("QUIT");
            tcpClient.Close();
            waitHandle.Close();
            networkStream.Close();
            listBoxOperation.Items.Clear();
            listBoxStatus.Items.Clear();
            groupBoxOperation.Text = "邮件信息";
            buttonConnect.Enabled = true;
            buttonDisconnect.Enabled = false;
        }

        private delegate void AddListBoxItemDelegate(ListBox listbox, string str);
        private void AddListBoxItem(ListBox listbox, string str)
        {
            if (listbox.InvokeRequired)
            {
                AddListBoxItemDelegate d = new AddListBoxItemDelegate(AddListBoxItem);
                listbox.Invoke(d, new object[] { listbox, str });
            }
            else
            {
                listbox.Items.Add(str);
                listbox.SelectedIndex = listbox.Items.Count - 1;
                listbox.ClearSelected();
            }
        }

        private delegate void ClearListBoxItemDelegate(ListBox listbox);
        private void ClearListBoxItem(ListBox listbox)
        {
            if (listbox.InvokeRequired)
            {
                ClearListBoxItemDelegate d = new ClearListBoxItemDelegate(ClearListBoxItem);
                listbox.Invoke(d, listbox);
            }
            else
            {
                listbox.Items.Clear();
            }
        }

        private delegate void SetButtonStateDelegate(Button button, bool enabled);
        private void SetButtonState(Button button, bool enabled)
        {
            if (button.InvokeRequired)
            {
                SetButtonStateDelegate d = new SetButtonStateDelegate(SetButtonState);
                button.Invoke(d, new object[] { button, enabled });
            }
            else
            {
                button.Enabled = enabled;
            }
        }

        private delegate void SetGroupBoxTextDelegate(GroupBox groupbox, string str);
        private void SetGroupBoxText(GroupBox groupbox, string str)
        {
            if (groupbox.InvokeRequired)
            {
                SetGroupBoxTextDelegate d = new SetGroupBoxTextDelegate(SetGroupBoxText);
                groupbox.Invoke(d, new object[] { groupbox, str });
            }
            else
            {
                groupbox.Text = str;
            }
        }

    }
}