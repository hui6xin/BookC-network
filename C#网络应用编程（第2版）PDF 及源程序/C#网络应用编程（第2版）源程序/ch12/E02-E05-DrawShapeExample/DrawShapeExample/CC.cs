using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace DrawShapeExample
{
    class CC
    {
        public static Panel panel { get; set; }
        public static List<DrawObject> graphicsList = new List<DrawObject>();
        public static Bitmap bitmap { get; set; }
        public static string strText { get; set; }
        public static Color color { get; set; }
        private static int autoID = 0;
        /// <summary>获取新对象的ID</summary>
        public static int GetNewID()
        {
            return autoID++;
        }
        /// <summary>当前对象的ID</summary>
        public static int ID { get; set; }
        //计算两点间的距离
        public static float GetDistance(Point p1, Point p2)
        {
            int dx = p2.X - p1.X;
            int dy = p2.Y - p1.Y;
            int distance = (int)Math.Sqrt(dx * dx + dy * dy);
            return distance;
        }
        /// <summary>
        /// 查找ID在graphicsList中的索引号，如果找不到返回-1
        /// </summary>
        /// <param name="ID">对象的ID</param>
        /// <returns>对象在graphicsList中的索引号，-1表示未找到</returns>
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
    }
}
