using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace P2PVideo
{
    class WebCamera
    {
        const short WM_CAP = 1024;
        const int WM_CAP_DRIVER_CONNECT = WM_CAP + 10;
        const int WM_CAP_DRIVER_DISCONNECT = WM_CAP + 11;
        const int WM_CAP_EDIT_COPY = WM_CAP + 30;
        const int WM_CAP_SET_PREVIEW = WM_CAP + 50;
        const int WM_CAP_SET_PREVIEWRATE = WM_CAP + 52;
        const int WM_CAP_SET_SCALE = WM_CAP + 53;
        const int WS_CHILD = 1073741824;
        const int WS_VISIBLE = 268435456;
        const short SWP_NOMOVE = 2;
        const short SWP_NOSIZE = 1;
        const short SWP_NOZORDER = 4;
        const short HWND_BOTTOM = 1;
        int iDevice = 0;
        int hHwnd;

        public int HHwnd
        {
            get { return hHwnd; }
            set { hHwnd = value; }
        }
        public int IDevice
        {
            get { return iDevice; }
            set { iDevice = value; }
        }

        [System.Runtime.InteropServices.DllImport("user32", EntryPoint = "SendMessageA")]
        public static extern int SendMessage(int hwnd, int wMsg, int wParam, [MarshalAs(UnmanagedType.AsAny)] 
			object lParam);
        [System.Runtime.InteropServices.DllImport("user32", EntryPoint = "SetWindowPos")]
        static extern int SetWindowPos(int hwnd, int hWndInsertAfter, int x, int y, int cx, int cy, int wFlags);
        [System.Runtime.InteropServices.DllImport("user32")]
        static extern bool DestroyWindow(int hndw);
        [System.Runtime.InteropServices.DllImport("avicap32.dll")]
        static extern int capCreateCaptureWindowA(string lpszWindowName, int dwStyle, int x, int y, int nWidth, short nHeight, int hWndParent, int nID);
        [System.Runtime.InteropServices.DllImport("avicap32.dll")]
        static extern bool capGetDriverDescriptionA(short wDriver, string lpszName, int cbName, string lpszVer, int cbVer);
        public bool OpenPreviewWindow(PictureBox picCapture)
        {
            int iHeight = picCapture.Height;
            int iWidth = picCapture.Width;

            //在pictureBox中打开预览界面
            hHwnd = capCreateCaptureWindowA(iDevice.ToString(), (WS_VISIBLE | WS_CHILD), 0, 0, 640, 480, picCapture.Handle.ToInt32(), 0);

            //连接摄像设备
            if (SendMessage(hHwnd, WM_CAP_DRIVER_CONNECT, iDevice, 0) == 1)
            {
                //设定预览界面的尺度缩放大小
                SendMessage(hHwnd, WM_CAP_SET_SCALE, 1, 0);
                //设定预览时每milliseconds的帧率
                SendMessage(hHwnd, WM_CAP_SET_PREVIEWRATE, 66, 0);
                //开始从摄像机获取预览图片
                SendMessage(hHwnd, WM_CAP_SET_PREVIEW, 1, 0);
                //调整预览窗体大小为picturebox大小
                SetWindowPos(hHwnd, HWND_BOTTOM, 0, 0, iWidth, iHeight, (SWP_NOMOVE | SWP_NOZORDER));
                return true;
            }
            else
            {
                //连接错误，则关闭窗体
                DestroyWindow(hHwnd);
                return false;
            }
        }
        public void ClosePreviewWindow()
        {
            //和设备断开连接
            SendMessage(hHwnd, WM_CAP_DRIVER_DISCONNECT, iDevice, 0);
            //关闭窗体
            DestroyWindow(hHwnd);
        }
        public void CaptureWindow()
        {
            //将捕获的视频帧存放到剪贴板上
            SendMessage(hHwnd, WM_CAP_EDIT_COPY, 0, 0);
        }
    }
}
