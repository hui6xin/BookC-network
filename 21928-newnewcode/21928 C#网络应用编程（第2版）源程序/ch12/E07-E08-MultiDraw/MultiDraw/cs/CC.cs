using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace MultiDraw
{
    //��װϵͳ�й��������Ժͷ���
    class CC
    {
        /// <summary>�Ƿ���Ҫ����������</summary>
        public static bool isNeedRunMainForm = false;

        /// <summary>�Զ������ļ���</summary>
        public static readonly string backupFileName = Application.StartupPath + "\\backup.gcs";

        ///<summary>����״̬</summary>
        public static UserState userState;

        /// <summary>������Ϣ</summary>
        public static TextInfo textInfo;

        /// <summary>�����жϽ�����״</summary>
        public static bool isToolPoint;

        public static Palette palette;
        public static MyService myService = new MyService();
        public static MyServer myServer;
        public static MyClient me;

        /// <summary>��ʱ����ѡ���ͼ��</summary>
        public static Bitmap bitmap;

        /// <summary>�Ƿ�Ϊ����������������</summary>
        public static bool isServerCommand = false;

        /// <summary>�����ΨһID</summary>
        public static int ID = 1;
        public static void SetNewID()
        {
            ObjectID myID = new ObjectID();
            myID.SetNewID();
        }

        /// <summary>
        /// �����ο��Ϊ�����Ͻǵ����½�
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
        /// �����ο��Ϊ�����Ͻǵ����½�
        /// </summary>
        public static Rectangle GetNormalizedRectangle(Point p1, Point p2)
        {
            return GetNormalizedRectangle(p1.X, p1.Y, p2.X, p2.Y);
        }

        /// <summary>
        /// �����ο��Ϊ�����Ͻǵ����½�
        /// </summary>
        public static Rectangle GetNormalizedRectangle(Rectangle r)
        {
            return GetNormalizedRectangle(r.X, r.Y, r.X + r.Width, r.Y + r.Height);
        }
    }

    ///<summary>
    ///����״̬
    ///</summary>
    public enum UserState
    {
        /// <summary>��������</summary>
        SingleUser,
        /// <summary>�������������Ϊ����</summary>
        Server,
        /// <summary>�������������Ϊ����</summary>
        Client
    };

    /// <summary>
    /// ������Ϣ
    /// </summary>
    public struct TextInfo
    {
        public string text;
        public Color color;
    }

}
