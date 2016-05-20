using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.Serialization;
using System.Runtime.InteropServices;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Media;

namespace Chymistry.Client
{
    public partial class chat_frm : Form
    {

        #region 私有字段

        /// <summary>
        /// 当前用户名
        /// </summary>
        private string _username = null;

        /// <summary>
        /// 数据缓冲区大小
        /// </summary>
        private int _maxPacket = 2048;

        /// <summary>
        /// 用于接受消息的线程
        /// </summary>
        private Thread _receiveThread = null;

        /// <summary>
        /// 用于接受和发送的网络流，从登录窗体得到
        /// </summary>
        private NetworkStream _nws = null;

        /// <summary>
        /// 服务器套接字的字符串形式，从登录窗体得到
        /// </summary>
        private string _svrskt = null;

        /// <summary>
        /// 播放消息提示的播放器
        /// </summary>
        private SoundPlayer _sp1 = new SoundPlayer(Properties.Resources.msg);
        private SoundPlayer _sp2 = new SoundPlayer(Properties.Resources.nudge);
        /// <summary>
        /// 指示是否最小化到托盘
        /// </summary>
        private bool _hideFlag = false;

        #endregion


        #region 聊天窗体构造函数

        /// <summary>
        /// 构造函数，得到登录窗体的一些信息
        /// </summary>
        /// <param name="userName">当前用户名</param>
        /// <param name="nws">接受和发送消息的网络流</param>
        /// <param name="svrskt">服务器套接字的字符串形式</param>
        public chat_frm(string userName, NetworkStream nws, string svrskt)
        {
            InitializeComponent();
            _username = userName;
            _nws = nws;
            _svrskt = svrskt;
        }

        #endregion


        #region 聊天窗体的私有方法

        /// <summary>
        /// 保存聊天记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void save_btn_Click(object sender, EventArgs e)
        {
            DialogResult ret;
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "文本文件(*.txt)|*.txt";
            sfd.AddExtension = true;
            if ((ret = sfd.ShowDialog()) == DialogResult.OK)
            {
                chatrcd_rtb.SaveFile(sfd.FileName, RichTextBoxStreamType.PlainText);
            }
        }

        /// <summary>
        /// 清除聊天记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clear_btn_Click(object sender, EventArgs e)
        {
            DialogResult ret;
            ret = MessageBox.Show("确定清除吗？清除后不可恢复。", 
                                  "提示",
                                  MessageBoxButtons.OKCancel,
                                  MessageBoxIcon.Question, 
                                  MessageBoxDefaultButton.Button2);

            if (ret == DialogResult.OK)
            {
                chatrcd_rtb.Clear();
            }
        }

        /// <summary>
        /// 向服务器发送离线请求，结束接受消息线程，清理资源并关闭父窗体和自身窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void close_btn_Click(object sender, EventArgs e)
        {
            DialogResult ret;
            ret = MessageBox.Show("确定与服务器断开连接吗？", 
                                  "退出",
                                  MessageBoxButtons.OKCancel,
                                  MessageBoxIcon.Question,
                                  MessageBoxDefaultButton.Button2);

            if (ret == DialogResult.OK)
            {
                //向服务器发送离线请求
                _nws.Write(new byte[] { 0, 1 }, 0, 2);
                //结束接受消息的线程
                if (_receiveThread != null)
                {
                    _receiveThread.Abort();
                }
                //关闭网络流
                _nws.Close();
                //关闭父窗口及自身
                this.Owner.Close();
                this.Close();
            }
        }

        /// <summary>
        /// 提取命令
        /// 格式为两个一位整数拼接成的字符串。
        /// 第一位为0表示客户机向服务器发送的命令，为1表示服务器向客户机发送的命令。
        /// 第二位表示命令的含义，具体如下：
        /// "11"-服务器要求客户机更新在线列表
        /// "12"-服务器要求客户机做闪屏振动
        /// default-接受用户消息或者系统消息的正文
        /// </summary>
        /// <param name="s">要解析的包含命令的byte数组，只提取前两个字节</param>
        /// <returns>拼接成的命令</returns>
        private string DecodingBytes(byte[] s)
        {
            return string.Concat(s[0].ToString(), s[1].ToString());
        }

        /// <summary>
        /// 接受消息的线程执行体
        /// </summary>
        private void ReceiveMsg()
        {
            while (true)
            {
                byte[] packet = new byte[_maxPacket];
                _nws.Read(packet, 0, packet.Length);
                string _cmd = DecodingBytes(packet);

                switch (_cmd)
                {
                    /// "11"-服务器要求客户机更新在线列表
                    /// "12"-服务器要求客户机做闪屏振动
                    /// default-接受用户消息或者系统消息的正文
                    case "11":
                        {
                            byte[] onlineBuff = new byte[_maxPacket];
                            int byteCnt = _nws.Read(onlineBuff, 0, onlineBuff.Length);
                            IFormatter format = new BinaryFormatter();
                            MemoryStream stream = new MemoryStream();
                            stream.Write(onlineBuff, 0, byteCnt);
                            stream.Position = 0;
                            StringCollection onlineList = (StringCollection)format.Deserialize(stream);
                            this.ClearListBox();
                            foreach (string onliner in onlineList)
                            {
                                if (!onliner.Equals(_username))
                                {
                                    this.ListBoxItemAdd(onliner);
                                }
                            }
                            break;
                        }
                    case "12":
                        {
                            Nudge();
                            break;
                        }
                    default:
                        {
                            string displaytxt = Encoding.Unicode.GetString(packet);
                            AppendText(displaytxt);
                            _sp1.Play();
                            break;
                        }
                }
            }
        }

 /////////////////////////////////////////////////////////////////////////////////////////////
       public delegate void MyDelegate_textBox(string s);
        public delegate void MyDelegate_listBox();
        public delegate void MyDelegate_Form(Point p);
        public delegate void MyDelegate_listBoxAdd(string s);
        MyDelegate_textBox d;
        MyDelegate_listBox dd;
        MyDelegate_Form ddd;
        MyDelegate_listBoxAdd dddd;
        private void AppendText(string s)
        {
            if (this.chatrcd_rtb.InvokeRequired)
            {
                this.Invoke(d, s);
            }
            else
            {
                this.chatrcd_rtb.AppendText(s);
            }
        }

        private void ClearListBox()
        {
            if (this.online_cb.InvokeRequired)
            {
                this.Invoke(dd);
            }
            else
            {
                this.online_cb.Items.Clear();
            }
        }

        private void ChangePosition(Point p)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(ddd, p);
            }
            else
            {
                this.Location = p;
            }
        }

        private void ListBoxItemAdd(string s)
        {
            if (this.online_cb.InvokeRequired)
            {
                this.Invoke(dddd, s);
            }
            else
            {
                this.online_cb.Items.Add(s);
            }
        }
///////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 启动接受消息线程并显示相关信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chat_frm_Load(object sender, EventArgs e)
        {
/////////////////////////////////////////////////////////////////////////////////
            d = new MyDelegate_textBox(AppendText);
            dd = new MyDelegate_listBox(ClearListBox);
            ddd = new MyDelegate_Form(ChangePosition);
            dddd = new MyDelegate_listBoxAdd(ListBoxItemAdd);
            _receiveThread = new Thread(new ThreadStart(ReceiveMsg));
////////////////////////////////////////////////////////////////////////////////
            _receiveThread.Start();
            online_cb.Enabled = false;
            user_lb.Text   = "当前用户：" + _username;
            svrskt_lb.Text = "服务器：" + _svrskt;
            
        }

        /// <summary>
        /// 通过窗体右上角关闭窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chat_frm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
            }
            if (e.CloseReason == CloseReason.FormOwnerClosing)
            {
                return;
            }
            close_btn_Click(sender, e);
        }

        /// <summary>
        /// 发送消息，将接受方用户名和消息正文分开发送，便于服务器端处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void send_btn_Click(object sender, EventArgs e)
        {
            string localTxt = null;
            string sendTxt = null;
            string msg = msg_tb.Text.Trim();
            if (msg == string.Empty)
            {
                MessageBox.Show("不能发送空消息",
                                "提示",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                return;
            }

            //如果是聊天室模式则向服务器发送广播请求
            if (broadcast_rb.Checked)
            {
                localTxt = string.Format("[广播]您在 {0} 对所有人说：\r\n{1}\r\n\r\n", DateTime.Now, msg);
                sendTxt  = string.Format("[广播]{0} 在 {1} 对所有人说：\r\n{2}\r\n\r\n", _username, DateTime.Now, msg);
                //发送广播请求
                _nws.Write(new byte[] { 0, 5 }, 0, 2);
            }
            else
            {
                string _receiver = online_cb.Text;
                if (_receiver == string.Empty)
                {
                    MessageBox.Show("请选择一个接收者！\n如果没有接受者可选，表明当前只有您一个人在线\t",
                                    "发送消息",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                    return;
                }
                localTxt = string.Format("[私聊]您在 {0} 对 {1} 说：\r\n{2}\r\n\r\n", DateTime.Now, _receiver, msg);
                sendTxt  = string.Format("[私聊]{0} 在 {1} 对您说：\r\n{2}\r\n\r\n", _username, DateTime.Now, msg);
                //发送接受者用户名
                _nws.Write(Encoding.Unicode.GetBytes(_receiver), 0, Encoding.Unicode.GetBytes(_receiver).Length);
            }
            _nws.Write(Encoding.Unicode.GetBytes(sendTxt), 0, Encoding.Unicode.GetBytes(sendTxt).Length);

            chatrcd_rtb.AppendText(localTxt);
            msg_tb.Clear();
        }

        /// <summary>
        /// 有新消息来时闪烁任务栏并且保持聊天记录内容滚动到最底端，QQ就是这么玩滴~
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [DllImport("user32.dll")]
        public static extern bool FlashWindow(IntPtr hWnd, bool bInvert);
        private void chatrcd_rtb_TextChanged(object sender, EventArgs e)
        {
            chatrcd_rtb.ScrollToCaret();
            if (this.WindowState == FormWindowState.Minimized)
            {
                FlashWindow(this.Handle, true);
            }
        }

        /// <summary>
        /// 当窗口恢复后取消任务栏的闪烁效果
        /// 当窗口最小化时判断是否要隐藏到系统托盘
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chat_frm_SizeChanged(object sender, EventArgs e)
        {
            switch (this.WindowState)
            {
                case FormWindowState.Normal:
                    FlashWindow(this.Handle, false);
                    break;
                case FormWindowState.Minimized:
                    if (_hideFlag)
                    {
                        notifyIcon1.Visible = true;
                        this.Visible = false;
                    }
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 在线列表下拉框显示之前向服务器发送请求在线列表的命令
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void online_cb_DropDown(object sender, EventArgs e)
        {
            _nws.Write(new byte[] { 0, 2 }, 0, 2);
        }

        /// <summary>
        /// 聊天模式改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void broadcast_rb_CheckedChanged(object sender, EventArgs e)
        {
            if (private_rb.Checked)
            {
                online_cb.Enabled = true;
            }
            else
            {
                online_cb.Enabled = false;
            }
        }

        /// <summary>
        /// 设置最小化到系统托盘的标记值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hide_cb_CheckedChanged(object sender, EventArgs e)
        {
            _hideFlag = hide_cb.Checked;
        }

        /// <summary>
        /// 产生闪屏振动效果
        /// </summary>
        private void Nudge()
        {
            if (notifyIcon1.Visible == true)
            {
                return;
            }
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Normal;
            }
            int i = 0;
            Point _old = this.Location;
            Point _new1 = new Point(_old.X + 2, _old.Y + 2);
            Point _new2 = new Point(_old.X - 2, _old.Y - 2);
            _sp2.Play();
            while (i < 4)
            {
                this.ChangePosition(_new1);
                Thread.Sleep(60);
                this.ChangePosition(_new2);
                Thread.Sleep(60);
                i++;
            }
            this.ChangePosition(_old) ;
        }

        /// <summary>
        /// 发送闪屏振动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nudge_pb_Click(object sender, EventArgs e)
        {
            string displayTxt = null;
            if (private_rb.Checked && online_cb.Text == string.Empty)
            {
                MessageBox.Show("非聊天室模式下必须先选择一个接收者！",
                                "发送闪屏振动",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                return;
            }
            if (private_rb.Checked)
            {
                _nws.Write(new byte[] { 0, 4 }, 0, 2);
                string _receiver = online_cb.Text;
                _nws.Write(Encoding.Unicode.GetBytes(_receiver), 0, Encoding.Unicode.GetBytes(_receiver).Length);
                displayTxt = string.Format("[系统提示]您向 {0} 发送了一个闪屏振动。\r\n\r\n", _receiver);
            }
            else
            {
                _nws.Write(new byte[] { 0, 3 }, 0, 2);
                displayTxt = "[系统提示]您向所有人发送了一个闪屏振动。\r\n\r\n";
            }
            chatrcd_rtb.AppendText(displayTxt);
            Nudge();
        }

        /// <summary>
        /// 以下是系统托盘菜单的处理函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void close_tsmi_Click(object sender, EventArgs e)
        {
            close_btn_Click(sender, e);
        }
        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            comeback_tsmi_Click(sender, e);
        }
        private void comeback_tsmi_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = false;
            this.Visible = true;
            this.WindowState = FormWindowState.Normal;
            this.BringToFront();
        }

        #endregion

    }
}