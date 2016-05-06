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

        private void button3_Click(object sender, EventArgs e)
        {
            int[] height = { 0, 1, 0, 2, 1, 0, 1, 2, 2, 1, 3, 1 };
            if (height.Length <= 2)
            {
                label1.Text = "0";
                return;
            }
            int ret = 0;
            int l = 0;
            int r = height.Count() - 1;
            int left = height[0];
            int right = height[r];
            while (l < r)
            {
                if (left <= right)
                {
                    l++;
                    if (height[l] >= left)
                    {
                        left = height[l];
                    }
                    else 
                        ret += (left - height[l]);
                }
                else
                {
                    r--;
                    if (height[r] >= right)
                    {
                        right = height[r];
                    }
                    else ret += (right - height[r]);
                }
            }
            label1.Text= ret.ToString();
        }
        


    }
}
