using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace HandleObjectExample
{
    public class ToolCurve:ToolObject
    {
        private int minDistance = 20;
        private Point myLastPoint;
        public override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            CC.isToolPoint = false;
            CC.ID = CC.GetNewID();
            Point p = new Point(e.X, e.Y);
            DrawCurve w = new DrawCurve(p, Color.Red, 2, CC.ID);
            AddNewObject(w);
            myLastPoint = p;
            isNewObjectAdded = true;
        }
        public override void OnMouseMove(MouseEventArgs e)
        {
            if (isNewObjectAdded == false)
            {
                return;
            }
            Point point = new Point(e.X, e.Y);
            int index = CC.FindObjectIndex(CC.ID);
            DrawCurve w = (DrawCurve)CC.graphicsList[index];
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
                    w.PointList.Add(point);
                    myLastPoint = point;
                }
            }
            CC.panel.Refresh();
        }
    }
}
