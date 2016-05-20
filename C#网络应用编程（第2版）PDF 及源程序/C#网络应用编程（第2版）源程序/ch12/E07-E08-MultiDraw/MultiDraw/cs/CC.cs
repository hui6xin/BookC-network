using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace MultiDraw
{
    //封装系统中公共的属性和方法
    class CC
    {
        /// <summary>是否需要运行主窗体</summary>
        public static bool isNeedRunMainForm = false;

        /// <summary>自动备份文件名</summary>
        public static readonly string backupFileName = Application.StartupPath + "\\backup.gcs";

        ///<summary>制作状态</summary>
        public static UserState userState;

        /// <summary>文字信息</summary>
        public static TextInfo textInfo;

        /// <summary>用于判断焦点形状</summary>
        public static bool isToolPoint;

        public static Palette palette;
        public static MyService myService = new MyService();
        public static MyServer myServer;
        public static MyClient me;

        /// <summary>临时保存选择的图像</summary>
        public static Bitmap bitmap;

        /// <summary>是否为服务器发出的命令</summary>
        public static bool isServerCommand = false;

        /// <summary>对象的唯一ID</summary>
        public static int ID = 1;
        public static void SetNewID()
        {
            ObjectID myID = new ObjectID();
            myID.SetNewID();
        }

        /// <summary>
        /// 将矩形框变为从左上角到右下角
        /// </summary>
        public static Rectangle GetNormalizedRectangle(int x1, int y1, int x2, int y2)
        {
            if (x2 < x1)
            {
                int tmp = x2;
                x2 = x1;
                x1 = tmp;
            }

            if (y2 < y1)
            {
                int tmp = y2;
                y2 = y1;
                y1 = tmp;
            }
            return new Rectangle(x1, y1, x2 - x1, y2 - y1);
        }

        /// <summary>
        /// 将矩形框变为从左上角到右下角
        /// </summary>
        public static Rectangle GetNormalizedRectangle(Point p1, Point p2)
        {
            return GetNormalizedRectangle(p1.X, p1.Y, p2.X, p2.Y);
        }

        /// <summary>
        /// 将矩形框变为从左上角到右下角
        /// </summary>
        public static Rectangle GetNormalizedRectangle(Rectangle r)
        {
            return GetNormalizedRectangle(r.X, r.Y, r.X + r.Width, r.Y + r.Height);
        }
    }

    ///<summary>
    ///制作状态
    ///</summary>
    public enum UserState
    {
        /// <summary>单机制作</summary>
        SingleUser,
        /// <summary>多机制作，本机为主机</summary>
        Server,
        /// <summary>多机制作，本机为附机</summary>
        Client
    };

    /// <summary>
    /// 文字信息
    /// </summary>
    public struct TextInfo
    {
        public string text;
        public Color color;
    }

}
