using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace MultiDraw
{
    class DrawMyCurve : TrackCurve
    {

        public DrawMyCurve()
        {
        }

        public override DrawObject Clone()
        {
            DrawMyCurve w = new DrawMyCurve();
            for (int i = 0; i < this.pointList.Count; i++)
            {
                w.pointList.Add(this.pointList[i]);
            }
            w.penColor = this.penColor;
            w.penWidth = this.penWidth;
            AddOtherFields(w);
            return w;
        }

        public DrawMyCurve(Point p, Color mycolor, int myPenWidth, int id)
        {
            AddPoint(p);
            this.penColor = mycolor;
            this.penWidth = myPenWidth;
            this.id = id;
        }

        public DrawMyCurve(List<Point> myPointList, int myPenWidth, Color mycolor, int id)
        {
            for (int i = 0; i < myPointList.Count; i++)
            {
                AddPoint(myPointList[i]);
            }
            this.penColor = mycolor;
            this.penWidth = myPenWidth;
            this.id = id;
        }

        public void AddPoint(Point p)
        {
            pointList.Add(p);
        }

        public override void Draw(Graphics g)
        {
            Point[] pts = new Point[pointList.Count];
            pointList.CopyTo(pts);
            Pen pen = new Pen(penColor, penWidth);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            AdjustableArrowCap myArrow = new AdjustableArrowCap(4, 4, true);
            pen.CustomEndCap = myArrow;
            if (pts.Length < 3)
            {
                if (pts.Length > 1)
                {
                    g.DrawLine(pen, pts[0], pts[1]);
                }
            }
            else
            {
                g.DrawCurve(pen, pts);
            }
            pen.Dispose();
        }
    }
}
