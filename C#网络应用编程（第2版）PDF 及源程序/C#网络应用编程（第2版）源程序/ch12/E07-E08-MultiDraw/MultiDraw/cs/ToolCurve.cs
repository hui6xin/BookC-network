using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace MultiDraw
{
    class ToolCurve:ToolObject
    {
        private int minDistance = 20;
        private Point myLastPoint;

        public override void OnMouseDown(Palette palette, MouseEventArgs e)
        {
            base.OnMouseDown(palette, e);
            CC.isToolPoint = false;
            Point p = new Point(e.X, e.Y);
            DrawMyCurve w = new DrawMyCurve(p, Color.Red, 2, CC.ID);
            AddNewObject(palette, w);
            myLastPoint = p;
            isNewObjectAdded = true;
        }

        public override void OnMouseMove(Palette palette, MouseEventArgs e)
        {
            if (isNewObjectAdded == false)
            {
                return;
            }
            Point point = new Point(e.X, e.Y);
            int index = CC.myService.FindObjectIndex(CC.ID);
            DrawMyCurve w = (DrawMyCurve)palette.graphics[index];
            if (e.Button == MouseButtons.Left)
            {
                int dx = myLastPoint.X - point.X;
                int dy = myLastPoint.Y - point.Y;
                int distance = (int)Math.Sqrt(dx * dx + dy * dy);
                if (distance < minDistance)
                {
                    if (w.PointList.Count > 1)
                    {
                        w.MoveHandleTo(point, w.HandleCount);
                    }
                }
                else
                {
                    w.AddPoint(point);
                    myLastPoint = point;
                }
            }
            palette.Refresh();
        }

        public override void OnMouseUp(Palette palette, MouseEventArgs e)
        {
            if (isNewObjectAdded == false)
            {
                return;
            }
            base.OnMouseUp(palette, e);
            int index = CC.myService.FindObjectIndex(CC.ID);
            DrawMyCurve w = (DrawMyCurve)palette.graphics[index];
            if (CC.userState != UserState.SingleUser)
            {
                int count = w.PointList.Count;
                if (count > 2)
                {
                    StringBuilder x1 = new StringBuilder(count);
                    StringBuilder y1 = new StringBuilder(count);
                    for (int i = 0; i < count; i++)
                    {
                        x1.Append("@" + w.PointList[i].X);
                        y1.Append("@" + w.PointList[i].Y);
                    }
                    x1 = x1.Remove(0, 1);
                    y1 = y1.Remove(0, 1);
                    //x点数,y点数,线条宽度,线条颜色,id
                    CC.me.SendToServer(string.Format("DrawMyCurve,{0},{1},{2},{3},{4}",
                        x1, y1, w.PenWidth, w.PenColor.ToArgb(), CC.ID));
                }
                palette.graphics.Remove(CC.ID);
            }
        }

    }
}
