using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;
using System.IO;
using System.Diagnostics;
using System.Drawing;
namespace MultiDraw
{
    public class MyService
    {
        public MyService(){}
        public MainForm mainForm;
        public void DataProcessing(string s)
        {
            string[] splitString = s.Split(',');
            int x, y, index, width, height;
            int id;
            string text, idString;
            string xArrayString, yArrayString;
            Color color;
            string IPEndPointString;
            switch (splitString[0])
            {
                case "MoveObject":
                    //x,y,移动的对象ID集合,IPEndPoint
                    index = 1;
                    x = int.Parse(splitString[index++]);
                    y = int.Parse(splitString[index++]);
                    idString = splitString[index++];
                    IPEndPointString = splitString[index++];
                    if (IPEndPointString != CC.me.client.Client.LocalEndPoint.ToString())
                    {
                        this.MoveObject(x, y, idString);
                    }
                    break;
                case "MoveObjectHandle":
                    //左上角x坐标，左上角y坐标,一到多个对象的ID字符串,IPEndPoint
                    index = 1;
                    x = int.Parse(splitString[index++]);
                    y = int.Parse(splitString[index++]);
                    idString = splitString[index++];
                    IPEndPointString = splitString[index++];
                    if (IPEndPointString != CC.me.client.Client.LocalEndPoint.ToString())
                    {
                        this.MoveObjectHandle(x, y, idString);
                    }
                    break;
                case "DrawMyText":
                    //x1，y1,x2,y2,旋转角度,文字内容,颜色,文字高,id
                    index = 1;
                    x = int.Parse(splitString[index++]);
                    y = int.Parse(splitString[index++]);
                    Point p1 = new Point(x, y);
                    x = int.Parse(splitString[index++]);
                    y = int.Parse(splitString[index++]);
                    Point p2 = new Point(x, y);
                    float angle = float.Parse(splitString[index++]);
                    text = splitString[index++];
                    color = Color.FromArgb(int.Parse(splitString[index++]));
                    int fontHeight = int.Parse(splitString[index++]);
                    id = int.Parse(splitString[index++]);
                    this.DrawMyText(p1, p2, angle, text, color, fontHeight, id);
                    break;
                case "DrawMyRectangle":
                    //左上角x坐标，左上角y坐标,宽,高,颜色,id
                    index = 1;
                    x = int.Parse(splitString[index++]);
                    y = int.Parse(splitString[index++]);
                    width = int.Parse(splitString[index++]);
                    height = int.Parse(splitString[index++]);
                    color = Color.FromArgb(int.Parse(splitString[index++]));
                    id = int.Parse(splitString[index++]);
                    this.DrawMyRectangle(x, y, width, height, color, id);
                    break;
                case "DrawMyCurve":
                    //x点数,y点数,线条宽度,线条颜色,id
                    index = 1;
                    xArrayString = splitString[index++];
                    yArrayString = splitString[index++];
                    width = int.Parse(splitString[index++]);
                    color = Color.FromArgb(int.Parse(splitString[index++]));
                    id = int.Parse(splitString[index++]);
                    this.DrawMyCurve(xArrayString, yArrayString, width, color, id);
                    break;
                default:
                    Debug.Print("什么意思啊：" + s);
                    break;
            }
        }

        /// <summary>画曲线，参数：x点数,y点数,线条宽度,线条颜色,id</summary>
        private void DrawMyCurve(string xString, string yString, int width, Color color, int id)
        {
            string[] xArray = xString.Split('@');
            string[] yArray = yString.Split('@');
            List<Point> list = new List<Point>();
            for (int i = 0; i < xArray.Length; i++)
            {
                list.Add(new Point(int.Parse(xArray[i]), int.Parse(yArray[i])));
            }
            DrawMyCurve w = new DrawMyCurve(list, width, color, id);
            CC.palette.graphics.Add(w);
            w.Selected = false;
            RefreshPalette();
        }
        /// <summary>画矩形,参数：左上角x坐标，左上角y坐标,宽,高,颜色,id</summary>
        private void DrawMyRectangle(int x, int y, int width, int height, Color color, int id)
        {
            DrawMyRectangle w = new DrawMyRectangle(x, y, width, height, color,id);
            CC.palette.graphics.Add(w);
            w.Selected = false;
            RefreshPalette();
        }
        /// <summary>画文字,参数：左上角顶点，右上角顶点,角度,文字内容,颜色,文字大小,id</summary>
        private void DrawMyText(Point p1, Point p2, float angle, string text, Color color, int fontHeight, int id)
        {
            DrawMyText w = new DrawMyText(p1, p2, angle, text, color, fontHeight, id);
            CC.palette.graphics.Add(w);
            w.Selected = false;
            RefreshPalette();
        }
        /// <summary>移动图形对象到指定位置,参数：层号,左上角x坐标,左上角y坐标,对象的ID</summary>
        public void MoveObject(int dx, int dy, string idString)
        {
            string[] ids = idString.Split('@');
            for (int i = 0; i < ids.Length; i++)
            {
                int index = FindObjectIndex(int.Parse(ids[i]));
                if (index != -1)
                {
                    DrawObject w = (DrawObject)CC.palette.graphics[index];
                    w.Move(dx, dy);
                    w.Selected = false;
                    RefreshPalette();
                }
            }
        }
        /// <summary>移动对象手柄到指定位置,参数：左上角x坐标,左上角y坐标,对象的ID和句柄号</summary>
        public void MoveObjectHandle(int x, int y, string idString)
        {
            string[] s = idString.Split('@');
            int id = int.Parse(s[0]);
            int handleNumber = int.Parse(s[1]);
            int index = FindObjectIndex(id);
            if (index != -1)
            {
                DrawObject w = (DrawObject)CC.palette.graphics[index];
                CC.isServerCommand = true;
                w.MoveHandleTo(new Point(x, y), handleNumber);
                CC.isServerCommand = false;
                w.Selected = false;
                RefreshPalette();
            }
        }
        /// <summary>找对应对象的id,如果找不到，返回-1，否则返回该对象的序号</summary>
        public int FindObjectIndex(int ID)
        {
            int index = -1;
            for (int i = 0; i < CC.palette.graphics.Count; i++)
            {
                if (CC.palette.graphics[i].ID == ID)
                {
                    index = i;
                    break;
                }
            }
            return index;
        }
        delegate void RefreshPaletteNoParaDelegate();
        public void RefreshPalette()
        {
            if (CC.palette.InvokeRequired == true)
            {
                RefreshPaletteNoParaDelegate d = RefreshPalette;
                CC.palette.Invoke(d);
            }
            else
            {
               CC.palette.Capture = false;
                CC.palette.Refresh();
            }
        }
    }
}
