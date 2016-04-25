using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace STest
{
    public partial class Form1 : Form
    {
        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", EntryPoint = "FindWindowEx", SetLastError = true)]
        private static extern IntPtr FindWindowEx(IntPtr hwndParent, uint hwndChildAfter, string lpszClass, string lpszWindow);

        [DllImport("user32.dll", EntryPoint = "SendMessage", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern int SendMessage(IntPtr hwnd, uint wMsg, int wParam, int lParam);

        const uint BM_CLICK = 0xF5;

        

        public Form1()
        {
            InitializeComponent();

           


        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Text = "关闭 " + DateTime.Now;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            IntPtr msgHandle = FindWindow(null, "Form1");
            if (msgHandle != IntPtr.Zero)
            {
                //找到Button
                IntPtr btnHandle = FindWindowEx(msgHandle, 0, null, "确定");
                if (btnHandle != IntPtr.Zero)
                {
                    SendMessage(btnHandle, BM_CLICK, 0, 0);
                }
            }
        }
        


    }
}
