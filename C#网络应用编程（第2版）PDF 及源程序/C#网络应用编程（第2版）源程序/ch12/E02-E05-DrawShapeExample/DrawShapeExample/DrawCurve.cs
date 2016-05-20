using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace DrawShapeExample
{
    class DrawCurve:DrawObject
    {
        public List<Point> PointList = new List<Point>();
        public DrawCurve(Point p, Color color, int penWidth,int id)
        {
            PointList.Add(p);
            this.PenWidth = penWidth;
            this.penColor = color;
            this.ID = id;
        }
        public override void Draw(Graphics g)
        {
            using (Pen pen = new Pen(this.penColor,PenWidth))
            {
                Point[] pts = new Point[PointList.Count];
                PointList.CopyTo(pts);
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
            }
        }
    }
}
