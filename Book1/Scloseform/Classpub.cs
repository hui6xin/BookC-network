using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Net.NetworkInformation;//port
using System.Net.Security;

namespace Scloseform
{
    class Classpub
    {
        #region msg
        public const int USER = 0x0400;
        public const int UM_1 = USER + 1;
        public const int UM_2 = USER + 2;

        public const int WM_CLOSE = 0x0010;
        #endregion

        #region api
        [DllImport("user32.dll")]
        public static extern void PostMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        public static extern void PostMessage(IntPtr hWnd, int msg, int wParam, int lParam);


        //消息发送API
        [DllImport("User32.dll", EntryPoint = "SendMessage")]
        public static extern int SendMessage(
            IntPtr hWnd,        // 信息发往的窗口的句柄
            int Msg,            // 消息ID
            int wParam,         // 参数1
            int lParam          //参数2
        );


        #endregion


        [DllImport("User32.dll", EntryPoint = "FindWindow")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("User32.dll", EntryPoint = "FindWindowEx")]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpClassName, string lpWindowName);

        //public static int GetFirstAvailablePort()
        //{
        //    int MAX_PORT = 6000; //系统tcp/udp端口数最大是65535            
        //    int BEGIN_PORT = 5000;//从这个端口开始检测

        //    for (int i = BEGIN_PORT; i < MAX_PORT; i++)
        //    {
        //        if ( PortIsAvailable(i) ) 
        //            return i;
        //    }

        //    return -1;
        //}


    }

}
