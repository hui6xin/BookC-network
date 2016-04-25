using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace SSetsystimetype
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        [DllImport("kernel32.dll", EntryPoint = "GetSystemDefaultLCID")]
        public static extern int GetSystemDefaultLCID();

        [DllImport("kernel32.dll", EntryPoint = "SetLocaleInfoA")]
        public static extern int SetLocaleInfo(int Locale, int LCType, string lpLCData);

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);

        private static readonly IntPtr HWND_BROADCAST = new IntPtr(0xffff);
        private const uint WM_SYSCOMMAND = 0x0112;
        private const int SC_MONITORPOWER = 0xf170;
        private const int WM_SETTINGCHANGE = 0x001A;

        public const int LOCALE_SLONGDATE = 0x20;
        public const int LOCALE_SSHORTDATE = 0x1F;
        public const int LOCALE_STIME = 0x1003;

        [Flags]
        public enum SendMessageTimeoutFlags : uint
        {
            SMTO_NORMAL = 0x0000,
            SMTO_BLOCK = 0x0001,
            SMTO_ABORTIFHUNG = 0x0002,
            SMTO_NOTIMEOUTIFNOTHUNG = 0x0008
        }

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessageTimeout(
           IntPtr windowHandle,
           uint Msg,
           IntPtr wParam,
           string lParam,
           SendMessageTimeoutFlags flags,
           uint timeout,
           out IntPtr result
           );
        //IntPtr result1;
        //        //修改后发送一个消息给系统 
        //        //调用
        //        SendMessageTimeout(
        //                             HWND_BROADCAST,
        //                             WM_SETTINGCHANGE,
        //                             IntPtr.Zero,
        //                             "Environment",
        //                             SendMessageTimeoutFlags.SMTO_ABORTIFHUNG,
        //                             200,
        //                             out result1);


        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessageTimeout(
           IntPtr windowHandle,
           uint Msg,
           IntPtr wParam,
           IntPtr lParam,
           SendMessageTimeoutFlags flags,
           uint timeout,
           out IntPtr result
           );
        
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, uint Msg, int wParam, string lParam);



        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int x = GetSystemDefaultLCID();
                SetLocaleInfo(x, LOCALE_STIME, "HH:mm:ss");        //时间格式
                SetLocaleInfo(x, LOCALE_SSHORTDATE, "yyyy-MM-dd");   //短日期格式 
                SetLocaleInfo(x, LOCALE_SLONGDATE, "yyyy-MM-dd");   //长日期格式 
                //SendMessage(HWND_BROADCAST, WM_SETTINGCHANGE, 0, 0);
                IntPtr result1;
                //修改后发送一个消息给系统 
                //调用
                SendMessageTimeout(
                                     HWND_BROADCAST,
                                     WM_SETTINGCHANGE,
                                     IntPtr.Zero,
                                     IntPtr.Zero,
                                     SendMessageTimeoutFlags.SMTO_ABORTIFHUNG,
                                     20,
                                     out result1);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                int x = GetSystemDefaultLCID();
                SetLocaleInfo(x, LOCALE_STIME, "HH:mm:ss");        //时间格式
                SetLocaleInfo(x, LOCALE_SSHORTDATE, "yyyy/MM/dd");   //短日期格式 
                SetLocaleInfo(x, LOCALE_SLONGDATE, "yyyy/MM/dd");   //长日期格式 
                //SendMessage(HWND_BROADCAST, WM_SETTINGCHANGE, 0, 0);
                IntPtr result1;
                //修改后发送一个消息给系统 
                //调用
                SendMessageTimeout(
                                     HWND_BROADCAST,
                                     WM_SETTINGCHANGE,
                                     IntPtr.Zero,
                                     IntPtr.Zero,
                                     SendMessageTimeoutFlags.SMTO_ABORTIFHUNG,
                                     20,
                                     out result1);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //if (progressBar1.Value < progressBar1.Maximum)
                progressBar1.Value = progressBar1.Value >= progressBar1.Maximum ? 0 : ++progressBar1.Value;
        }

        private void button3_Click(object sender, EventArgs e)
        {

            Class1.ssss = "xxxxx";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            classxx.getA();
        }

        public static Class1 _classxx;
        public static Class1 classxx {
            get 
            {
                if (_classxx == null)
                    _classxx = new Class1();
                return _classxx;
            }
            set 
            {
                if (_classxx == null)
                    _classxx = new Class1();
            }
        }
    }
}
