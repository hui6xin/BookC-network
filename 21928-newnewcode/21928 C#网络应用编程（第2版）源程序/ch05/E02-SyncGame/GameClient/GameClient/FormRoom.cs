//---------FormRoom.cs-----------//
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;
namespace GameClient
{
    public partial class FormRoom : Form
    {
        private int maxPlayingTables;
        private CheckBox[,] checkBoxGameTables;
        private TcpClient client = null;
        private StreamWriter sw;
        private StreamReader sr;
        private Service service;
        private FormPlaying formPlaying;
        //是否正常退出接收线程
        private bool normalExit = false;
        //命令是否来自服务器
        private bool isReceiveCommand = false;
        //所坐的游戏桌座位号，-1表示未入座,0表示坐到黑方,1表示坐到白方
        private int side = -1;
        public FormRoom()
        {
            InitializeComponent();
        }
        private void FormRoom_Load(object sender, EventArgs e)
        {
            Random r = new Random((int)DateTime.Now.Ticks);
            textBoxName.Text = "Player" + r.Next(1, 100);
            maxPlayingTables = 0;
            textBoxLocal.ReadOnly = true;
            textBoxServer.ReadOnly = true;
        }
        /// <summary>【登录】按钮的Click事件</summary>
        private void buttonConnect_Click(object sender, EventArgs e)
        {
            try
            {
                //仅作本机测试,实际使用时要将Dns.GetHostName()改为服务器域名
                client = new TcpClient(Dns.GetHostName(), 51888);
            }
            catch
            {
                MessageBox.Show("与服务器连接失败", "",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            groupBox1.Visible = true;
            textBoxLocal.Text = client.Client.LocalEndPoint.ToString();
            textBoxServer.Text = client.Client.RemoteEndPoint.ToString();
            buttonConnect.Enabled = false;
            //获取网络流
            NetworkStream netStream = client.GetStream();
            sr = new StreamReader(netStream, System.Text.Encoding.UTF8);
            sw = new StreamWriter(netStream, System.Text.Encoding.UTF8);
            service = new Service(listBox1, sw);
            //登录服务器，获取服务器各桌信息
            //格式：Login,昵称
            service.SendToServer("Login," + textBoxName.Text.Trim());
            Thread threadReceive = new Thread(new ThreadStart(ReceiveData));
            threadReceive.Start();
        }
        /// <summary>处理接收数据</summary>
        private void ReceiveData()
        {
            bool exitWhile = false;
            while (exitWhile == false)
            {
                string receiveString = null;
                try
                {
                    receiveString = sr.ReadLine();
                }
                catch
                {
                    service.AddItemToListBox("接收数据失败");
                }
                if (receiveString == null)
                {
                    if (normalExit == false)
                    {
                        MessageBox.Show("与服务器失去联系，游戏无法继续！");
                    }
                    if (side != -1)
                    {
                        ExitFormPlaying();
                    }
                    side = -1;
                    normalExit = true;
                    //结束线程
                    break;
                }
                service.AddItemToListBox("收到：" + receiveString);
                string[] splitString = receiveString.Split(',');
                string command=splitString[0].ToLower();
                switch (command)
                {
                    case "sorry":
                        MessageBox.Show("连接成功，但游戏室人数已满，无法进入。");
                        exitWhile = true;
                        break;
                    case "tables":
                        //字符串格式：Tables,各桌是否有人的字符串
                        //其中每位表示一个座位，1表示有人，0表示无人
                        string s = splitString[1];
                        //如果maxPlayingTables为0，说明尚未创建checkBoxGameTables
                        if (maxPlayingTables == 0)
                        {
                            //计算所开桌数
                            maxPlayingTables = s.Length / 2;
                            checkBoxGameTables = new CheckBox[maxPlayingTables, 2];
                            isReceiveCommand = true;
                            //将CheckBox对象添加到数组中，以便管理
                            for (int i = 0; i < maxPlayingTables; i++)
                            {
                                AddCheckBoxToPanel(s, i);
                            }
                            isReceiveCommand = false;
                        }
                        else
                        {
                            isReceiveCommand = true;
                            for (int i = 0; i < maxPlayingTables; i++)
                            {
                                for (int j = 0; j < 2; j++)
                                {
                                    if (s[2 * i + j] == '0')
                                    {
                                        UpdateCheckBox(checkBoxGameTables[i, j], false);
                                    }
                                    else
                                    {
                                        UpdateCheckBox(checkBoxGameTables[i, j], true);
                                    }
                                }
                            }
                            isReceiveCommand = false;
                        }
                        break;
                    case "sitdown":
                        //格式：SitDown,座位号,用户名
                        formPlaying.SetTableSideText(splitString[1], splitString[2],
                                string.Format("{0}进入", splitString[2]));
                        break;
                    case "getup":
                        //格式：GetUp,座位号,用户名
                        //自己或者对方离座
                        if (side == int.Parse(splitString[1]))
                        {
                            //自己离座
                            side = -1;
                        }
                        else
                        {
                            //对方离座
                            formPlaying.SetTableSideText(splitString[1], "",
                                string.Format("{0}退出", splitString[2]));
                            formPlaying.Restart("敌人逃跑了，我方胜利了");
                        }
                        break;
                    case "lost":
                        //格式：Lost,座位号,用户名
                        //对家与服务器失去联系
                        formPlaying.SetTableSideText(splitString[1], "",
                            string.Format("[{0}]与服务器失去联系", splitString[2]));
                        formPlaying.Restart("对家与服务器失去联系，游戏无法继续");
                        break;
                    case "talk":
                        //格式：Talk,说话者,对话内容
                        if (formPlaying != null)
                        {
                            //由于说话内容可能包含逗号，所以需要特殊处理
                            formPlaying.ShowTalk(splitString[1],
                                receiveString.Substring(splitString[0].Length +
                                splitString[1].Length + splitString[2].Length + 3));
                        }
                        break;
                    case "message":
                        //格式：Message,内容
                        //服务器自动发送的一般信息（比如进入游戏桌入座等）
                        formPlaying.ShowMessage(splitString[1]);
                        break;
                    case "level":
                        //设置难度级别
                        //格式：Time,桌号,难度级别
                        formPlaying.SetLevel(splitString[2]);
                        break;
                    case "setdot":
                        //产生的棋子位置信息
                        //格式：Setdot,行,列,颜色
                        formPlaying.SetDot(
                            int.Parse(splitString[1]),
                            int.Parse(splitString[2]),
                            (DotColor)int.Parse(splitString[3]));
                        break;
                    case "unsetdot":
                        //消去棋子的信息
                        //格式：UnsetDot,行,列,黑方成绩,白方成绩
                        int x = 20 * (int.Parse(splitString[1]) + 1);
                        int y = 20 * (int.Parse(splitString[2]) + 1);
                        formPlaying.UnsetDot(x, y);
                        formPlaying.SetGradeText(splitString[3], splitString[4]);
                        break;
                    case "win":
                        //格式：Win,相邻棋子的颜色,黑方成绩,白方成绩
                        string winner = "";
                        if ((DotColor)int.Parse(splitString[1]) == DotColor.Black)
                        {
                            winner = "黑方出现相邻点，白方胜利！";
                        }
                        else
                        {
                            winner = "白方出现相邻点，黑方胜利！";
                        }
                        formPlaying.ShowMessage(winner);
                        formPlaying.Restart(winner);
                        break;
                }
            }
            //接收线程结束后，游戏继续进行已经没有意义，所以直接退出程序
            Application.Exit();
        }
        delegate void ExitFormPlayingDelegate();
        /// <summary>退出游戏</summary>
        private void ExitFormPlaying()
        {
            if (formPlaying.InvokeRequired == true)
            {
                ExitFormPlayingDelegate d = new ExitFormPlayingDelegate(ExitFormPlaying);
                this.Invoke(d);
            }
            else
            {
                formPlaying.Close();
            }
        }
        delegate void PanelDelegate(string s, int i);
        /// <summary>添加一个游戏桌</summary>
        /// <param name="s">表示游戏桌的字符串</param>
        /// <param name="i">用于确定第几桌</param>
        private void AddCheckBoxToPanel(string s, int i)
        {
            if (panel1.InvokeRequired == true)
            {
                PanelDelegate d = AddCheckBoxToPanel;
                this.Invoke(d, s, i);
            }
            else
            {
                Label label = new Label();
                label.Location = new Point(10, 15 + i * 30);
                label.Text = string.Format("第{0}桌：", i + 1);
                label.Width = 70;
                this.panel1.Controls.Add(label);
                CreateCheckBox(i, 0, s, "黑方");
                CreateCheckBox(i, 1, s, "白方");
            }
        }
        delegate void CheckBoxDelegate(CheckBox checkbox, bool isChecked);
        /// <summary>修改选择状态</summary>
        /// <param name="checkbox">注定选择的复选框</param>
        /// <param name="isChecked">是否被选择</param>
        private void UpdateCheckBox(CheckBox checkbox, bool isChecked)
        {
            if (checkbox.InvokeRequired == true)
            {
                CheckBoxDelegate d = UpdateCheckBox;
                this.Invoke(d, checkbox, isChecked);
            }
            else
            {
                if (side == -1)
                {
                    checkbox.Enabled = !isChecked;
                }
                else
                {
                    //已经坐到某游戏桌上，不允许再选其他桌
                    checkbox.Enabled = false;
                }
                //注意改变Checked属性会触发checked_Changed事件
                checkbox.Checked = isChecked;
            }
        }
        /// <summary>添加游戏桌座位选项</summary>
        /// <param name="i">指定游戏桌序号</param>
        /// <param name="j">指定座位序号</param>
        /// <param name="s">表示游戏桌的字符串</param>
        /// <param name="text">说明信息</param>
        private void CreateCheckBox(int i, int j, string s, string text)
        {
            int x = j == 0 ? 100 : 200;
            checkBoxGameTables[i, j] = new CheckBox();
            checkBoxGameTables[i, j].Name = string.Format("check{0:0000}{1:0000}", i, j);
            checkBoxGameTables[i, j].Width = 60;
            checkBoxGameTables[i, j].Location = new Point(x, 10 + i * 30);
            checkBoxGameTables[i, j].Text = text;
            checkBoxGameTables[i, j].TextAlign = ContentAlignment.MiddleLeft;
            if (s[2 * i + j] == '1')
            {
                //1表示有人
                checkBoxGameTables[i, j].Enabled = false;
                checkBoxGameTables[i, j].Checked = true;
            }
            else
            {
                //0表示无人
                checkBoxGameTables[i, j].Enabled = true;
                checkBoxGameTables[i, j].Checked = false;
            }
            this.panel1.Controls.Add(checkBoxGameTables[i, j]);
            checkBoxGameTables[i, j].CheckedChanged +=
                new EventHandler(checkBox_CheckedChanged);
        }
        /// <summary>每个CheckBox的Checked属性发生变化都会触发此事件</summary>
        private void checkBox_CheckedChanged(object sender, EventArgs e)
        {
            //是否为服务器更新各桌
            if (isReceiveCommand == true)
            {
                return;
            }
            CheckBox checkbox = (CheckBox)sender;
            //Checked为true表示玩家坐到第i桌第j位上
            if (checkbox.Checked == true)
            {
                int i = int.Parse(checkbox.Name.Substring(5, 4));
                int j = int.Parse(checkbox.Name.Substring(9, 4));
                side = j;
                //字符串格式：SitDown,昵称,桌号,座位号
                //只有坐下后，服务器才保存该玩家的昵称
                service.SendToServer(string.Format("SitDown,{0},{1}", i, j));
                formPlaying = new FormPlaying(i, j, sw);
                formPlaying.Show();
            }
        }
        /// <summary>关闭窗口时触发的事件</summary>
        private void FormRoom_FormClosing(object sender, FormClosingEventArgs e)
        {
            //未与服务器连接前client为null
            if (client != null)
            {
                //不允许玩家从游戏桌直接退出整个程序
                //只允许从游戏桌返回游戏室，再从游戏室退出
                if (side != -1)
                {
                    MessageBox.Show("请先从游戏桌站起，返回游戏室，然后再退出");
                    //不让退出
                    e.Cancel = true;
                }
                else
                {
                    //服务器停止服务时,normalExited为true，其他情况为false
                    if (normalExit == false)
                    {
                        normalExit = true;
                        //通知服务器，用户从游戏室退出
                        service.SendToServer("Logout");
                    }
                    //通过关闭TcpClient对象，使服务器接收字符串为null
                    //服务器结束接收线程后,本程序的接收线程接收字符串也为null
                    //从而实现退出程序功能
                    client.Close();        //关闭client对象
                }
            }
        }
    }
}
