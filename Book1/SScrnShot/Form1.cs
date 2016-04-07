using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SScrnShot
{
    public partial class Form1 : Form
    {
        #region
        /// <summary>
        /// 向全局原子表添加一个字符串，并返回这个字符串的唯一标识符（原子ATOM）。
        /// </summary>
        /// <param name="lpString">自己设定的一个字符串</param>
        /// <returns></returns>
        [System.Runtime.InteropServices.DllImport("Kernel32.dll")]
        public static extern Int32 GlobalAddAtom(string lpString);

        /// <summary>
        /// 注册热键
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="id"></param>
        /// <param name="fsModifiers"></param>
        /// <param name="vk"></param>
        /// <returns></returns>
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, Keys vk);

        /// <summary>
        /// 取消热键注册
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        /// <summary>
        /// 热键ID
        /// </summary>
        public int hotKeyId = 100;

        /// <summary>
        /// 热键模式:0=Ctrl + Alt + A, 1=Ctrl + Shift + A
        /// </summary>
        public int HotKeyMode = 1;

        /// <summary>
        /// 控制键的类型
        /// </summary>
        public enum KeyModifiers : uint
        {
            None = 0,
            Alt = 1,
            Control = 2,
            Shift = 4,
            Windows = 8
        }

        /// <summary>
        /// 用于保存截取的整个屏幕的图片
        /// </summary>
        protected Bitmap screenImage;
        #endregion

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.notifyIcon1.Icon = Properties.Resources.cutImage;
            this.notifyIcon1.Visible = true;
            //this.notifyIcon1.ShowBalloonTip(1000);
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            //隐藏窗口
            this.Hide();

            //注册快捷键
            //注：HotKeyId的合法取之范围是0x0000到0xBFFF之间，GlobalAddAtom函数得到的值在0xC000到0xFFFF之间，所以减掉0xC000来满足调用要求。
            this.hotKeyId = GlobalAddAtom("Screenshot") - 0xC000;
            if (this.hotKeyId == 0)
            {
                //如果获取失败，设定一个默认值；
                this.hotKeyId = 0xBFFE;
            }

            if (this.HotKeyMode == 0)
            {
                RegisterHotKey(Handle, hotKeyId, (uint)KeyModifiers.Control | (uint)KeyModifiers.Alt, Keys.A);
            }
            else
            {
                RegisterHotKey(Handle, hotKeyId, (uint)KeyModifiers.Control | (uint)KeyModifiers.Shift, Keys.A);
            }

            this.lbl_CutImage.Hide();
        }
        /// <summary>
        /// 处理快捷键事件
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m)
        {
            //if (m.Msg == 0x0014)
            //{
            //    return; // 禁掉清除背景消息
            //}
            const int WM_HOTKEY = 0x0312;
            switch (m.Msg)
            {
                case WM_HOTKEY:
                    IntPtr i = m.WParam;
                    ShowForm();
                    break;
                default:
                    break;
            }
            base.WndProc(ref m);
        }
        /// <summary>
        /// 如果窗口为可见状态，则隐藏窗口；
        /// 否则则显示窗口
        /// </summary>
        protected void ShowForm()
        {
            if (this.Visible)
            {
                this.Hide();
            }
            else
            {
                Bitmap bkImage = new Bitmap(Screen.AllScreens[0].Bounds.Width, Screen.AllScreens[0].Bounds.Height);
                Graphics g = Graphics.FromImage(bkImage);
                g.CopyFromScreen(new Point(0, 0), new Point(0, 0), Screen.AllScreens[0].Bounds.Size, CopyPixelOperation.SourceCopy);
                screenImage = (Bitmap)bkImage.Clone();
                g.FillRectangle(new SolidBrush(Color.FromArgb(64, Color.Gray)), Screen.PrimaryScreen.Bounds);
                this.BackgroundImage = bkImage;

                this.ShowInTaskbar = false;
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                this.Width = Screen.PrimaryScreen.Bounds.Width;
                this.Height = Screen.PrimaryScreen.Bounds.Height;
                this.Location = Screen.PrimaryScreen.Bounds.Location;

                this.WindowState = FormWindowState.Maximized;
                this.Show();
            }
        }

        /// <summary>
        /// 当窗口正在关闭时进行验证
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.ApplicationExitCall)
            {
                e.Cancel = false;
                UnregisterHotKey(this.Handle, hotKeyId);
            }
            else
            {
                this.Hide();
                e.Cancel = true;
            }
        }

        private void tsmi_exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
