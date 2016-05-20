using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace HandleObjectExample
{
    public class DrawCurve : DrawObject
    {
        public DrawCurve()
        {
        }

        public DrawCurve(Point p, Color color, int penWidth, int id)
        {
            PointList.Add(p);
            this.PenWidth = penWidth;
            this.PenColor = color;
            this.ID = id;
        }
        protected GraphicsPath areaPath = null;
        protected Pen areaPen = null;
        protected Region areaRegion = null;
        /// <summary>构成曲线的点集合</summary>
        protected List<Point> pointList = new List<Point>();
        /// <summary>获取构成曲线的点集合</summary>
        public List<Point> PointList
        {
            get { return pointList; }
        }

        #region 重写基类的方法和属性
        /// <summary>句柄数</summary>
        public override int HandleCount
        {
            get
            {
                return pointList.Count;
            }
        }
        /// <summary>画图</summary>
        public override  void Draw(Graphics g)
        {
            using (Pen pen = new Pen(PenColor, PenWidth))
            {
                Point[] pts = new Point[PointList.Count];
                PointList.CopyTo(pts);
                g.SmoothingMode = SmoothingMode.AntiAlias;
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
        /// <summary>克隆</summary>
        public override  DrawObject Clone()
        {
            DrawCurve w = new DrawCurve();
            for (int i = 0; i < this.pointList.Count; i++)
            {
                w.pointList.Add(this.pointList[i]);
            }
            w.PenColor = this.PenColor;
            w.PenWidth = this.PenWidth;
            return w;
        }
        /// <summary>画句柄</summary>
        public override void DrawTracker(Graphics g)
        {
            using (SolidBrush brush = new SolidBrush(Color.Blue))
            {
                for (int i = 1; i <= HandleCount; i++)
                {
                    g.FillRectangle(brush, GetHandleRectangle(i));
                }
            }
        }
        /// <summary>取句柄的实体矩形</summary>
        public override Rectangle GetHandleRectangle(int handleNumber)
        {
            Point point = GetHandle(handleNumber);
            return new Rectangle(point.X - PenWidth, point.Y - PenWidth, PenWidth * 2, PenWidth * 2);
        }
        /// <summary>获取某个句柄</summary>
        public override Point GetHandle(int handleNumber)
        {
            return pointList[handleNumber - 1];
        }
        /// <summary>根据句柄，获取鼠标形状</summary>
        public override Cursor GetHandleCursor(int handleNumber)
        {
            return Cursors.SizeAll;
        }
        /// <summary>判断是否选中该对象及句柄 </summary>
        public override int HitHandleTest(Point point)
        {
            if (Selected)
            {
                for (int i = 1; i <= HandleCount; i++)
                {
                    if (GetHandleRectangle(i).Contains(new Point(point.X, point.Y)))
                        return i;
                }
            }
            if (PointInObject(point))
                return 0;
            return -1;
        }
        /// <summary>当前鼠标位置是否在句柄范围内 </summary>
        public override bool PointInObject(Point point)
        {
            CreateTrackObject();
            if (areaRegion != null)
            {
                return areaRegion.IsVisible(point);
            }
            else
            {
                return false;
            }
        }
        /// <summary>判断是否相交</summary>
        public override bool IntersectsWith(Rectangle rectangle)
        {
            CreateTrackObject();
            if (areaRegion != null)
            {
                return areaRegion.IsVisible(rectangle);
            }
            else
            {
                return false;
            }
        }
        /// <summary>当曲线上的点发生移动时,重新记录鼠标点的位置</summary>
        public override void MoveHandleTo(Point point, int handleNumber)
        {
            Point p = pointList[handleNumber - 1];
            float dx = point.X - p.X;
            float dy = point.Y - p.Y;
            pointList[handleNumber - 1] = new Point(point.X, point.Y);
            ClearTrackObject();
        }
        /// <summary>移动</summary>
        public override void Move(int dx, int dy)
        {
            int n = pointList.Count;
            for (int i = 0; i < n; i++)
            {
                Point p = new Point(dx, dy);
                pointList[i] = new Point((pointList[i]).X + p.X, (pointList[i]).Y + p.Y);
            }
            ClearTrackObject();
        }
        #endregion

        /// <summary>创建聚焦用的对象</summary>
        protected virtual void CreateTrackObject()
        {
            if (pointList.Count < 2) return;
            if (areaPath != null)
            {
                areaPath.Reset();
                areaPath.FillMode = FillMode.Winding;
            }
            else
            {
                areaPath = new GraphicsPath(FillMode.Winding);
            }
            if (areaRegion != null)
            {
                areaRegion.MakeEmpty();
            }
            else
            {
                areaRegion = new Region();
            }
            if (areaPen == null)
            {
                areaPen = new Pen(Color.Black, PenWidth + 2);
            }
            Point[] pts = new Point[pointList.Count];
            pointList.CopyTo(pts);
            if (pts.Length > 1)
            {
                areaPath.AddLines(pts);
                areaPath.CloseFigure();
                if (areaPath.PathPoints.Length > 1)
                {
                    areaPath.Widen(areaPen);
                    areaRegion.Union(areaPath);
                }
                else
                {
                    areaPath.Dispose();
                    areaPath = null;
                    areaRegion.Dispose();
                    areaRegion = null;
                }
            }
        }
        /// <summary>清除聚焦用的对象</summary>
        protected virtual void ClearTrackObject()
        {
            if (areaPath != null)
            {
                areaPath.Dispose();
                areaPath = null;
            }

            if (areaPen != null)
            {
                areaPen.Dispose();
                areaPen = null;
            }

            if (areaRegion != null)
            {
                areaRegion.Dispose();
                areaRegion = null;
            }
        }

    }
}
