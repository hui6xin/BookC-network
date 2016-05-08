using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace HandleObjectExample
{
    public static class CC
    {
        public static Panel panel;
        public static List<DrawObject> graphicsList = new List<DrawObject>();
        /// <summary>用于判断焦点形状</summary>
        public static bool isToolPoint;
        private static int autoID = 1;
        /// <summary>获取新对象的ID</summary>
        public static int GetNewID()
        {
            return autoID++;
        }
        /// <summary>当前对象的ID</summary>
        public static int ID { get; set; }
        /// <summary>临时保存选择的图像</summary>
        public static Bitmap bitmap;
        public static TextInfo textInfo;
        /// <summary>文字信息</summary>
        public struct TextInfo
        {
            public string text;
            public Color color;
        }
        /// <summary>查找ID在graphicsList中的索引号，如果找不到返回-1</summary>
        public static int FindObjectIndex(int ID)
        {
            int index = -1;
            for (int i = 0; i < graphicsList.Count; i++)
            {
                if (graphicsList[i].ID == ID)
                {
                    index = i;
                    break;
                }
            }
            return index;
        }
        /// <summary>选择的对象个数</summary>
        public static int SelectionCount
        {
            get
            {
                int n = 0;

                foreach (DrawObject w in graphicsList)
                {
                    if (w.Selected) n++;
                }
                return n;
            }
        }
        /// <summary>选择的对象</summary>
        public static DrawObject GetSelectedObject(int index)
        {
            int n = -1;
            foreach (DrawObject w in graphicsList)
            {
                if (w.Selected)
                {
                    n++;
                    if (n == index)
                        return w;
                }
            }
            return null;
        }
        /// <summary>是否画鼠标左键拖放范围的矩形框</summary>
        public static bool IsDrawNetRectangle{get;set;}
        /// <summary>鼠标左键拖放范围矩形大小及位置</summary>
        public static Rectangle NetRectangle{get;set;}
        /// <summary>设置矩形框内选择的对象</summary>
        public static void SelectInRectangle(Rectangle rectangle)
        {
            foreach (DrawObject w in graphicsList)
            {
                if (w.IntersectsWith(rectangle))
                    w.Selected = true;
            }
        }
        /// <summary>全部选择</summary>
        public static void SelectAll()
        {
            foreach (DrawObject w in graphicsList)
            {
                w.Selected = true;
            }
            CC.panel.Refresh();
        }
        /// <summary>全部取消选择</summary>
        public static void UnselectAll()
        {
            foreach (DrawObject w in graphicsList)
            {
                w.Selected = false;
            }
            CC.panel.Refresh();
        }
        /// <summary>当从右向左选择时，将矩形框变为从左上角到右下角</summary>
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
        /// <summary>当从右向左选择时，将矩形框变为从左上角到右下角</summary>
        public static Rectangle GetNormalizedRectangle(Point p1, Point p2)
        {
            return GetNormalizedRectangle(p1.X, p1.Y, p2.X, p2.Y);
        }
        /// <summary>当从右向左选择时，将矩形框变为从左上角到右下角</summary>
        public static Rectangle GetNormalizedRectangle(Rectangle r)
        {
            return GetNormalizedRectangle(r.X, r.Y, r.X + r.Width, r.Y + r.Height);
        }
    }

}
