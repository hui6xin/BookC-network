using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace DrawShapeExample
{
    class ToolCurve:ToolObject
    {
        private int minDistance = 10;
        private Point myLastPoint;
        public override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            { 
                
                CC.ID = CC.GetNewID();
                Point p = new Point(e.X, e.Y);
                DrawCurve w = new DrawCurve(p, Color.Red, 2,CC.ID);
                this.AddNewObject(w);
                myLastPoint = p;
                this.isNewObjectAdded = true;
            }
        }
        public override void OnMouseMove(MouseEventArgs e)
        {
            if (this.isNewObjectAdded == false)
            { return; }
            Point point = new Point(e.X, e.Y);
            int index = CC.FindObjectIndex(CC.ID);
            DrawCurve w = (DrawCurve)CC.graphicsList[index];
             if (e.Button == MouseButtons.Left)
             {
                int dx = myLastPoint.X - point.X;
                int dy = myLastPoint.Y - point.Y;
                int distance = (int)Math.Sqrt(dx * dx + dy * dy);
                if (distance >= minDistance)
                {
                    w.PointList.Add(point);
                    myLastPoint = point;
                }
            }
            CC.panel.Refresh();
        }
    }
}
