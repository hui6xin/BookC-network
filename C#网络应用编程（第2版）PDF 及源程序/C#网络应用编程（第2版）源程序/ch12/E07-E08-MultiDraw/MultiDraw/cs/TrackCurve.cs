using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.Serialization;

namespace MultiDraw
{
    class TrackCurve : DrawObject
    {
        /// <summary>
        /// 构成曲线的点数组
        /// </summary>
        protected List<Point> pointList = new List<Point>();
        public List<Point> PointList
        {
            get { return pointList; }
        }

        protected GraphicsPath areaPath = null;
        protected Pen areaPen = null;
        protected Region areaRegion = null;

        public override void Draw(Graphics g)
        {
            throw new Exception("该方法还没有实现，请在扩充类中实现该方法");
        }

        public override DrawObject Clone()
        {
            DrawMyCurve w = new DrawMyCurve();
            for (int i = 0; i < this.pointList.Count; i++)
            {
                w.pointList.Add(this.pointList[i]);
            }
            base.AddOtherFields(w);
            return w;
        }

        /// <summary>画句柄</summary>
        public override void DrawTracker(Graphics g)
        {
            if (CC.isToolPoint == true)
            {
                SolidBrush brush = new SolidBrush(Color.Blue);
                for (int i = 1; i <= HandleCount; i++)
                {
                    g.FillRectangle(brush, GetHandleRectangle(i));
                }
                brush.Dispose();
            }
        }

        /// <summary>取句柄的实体矩形</summary>
        public override Rectangle GetHandleRectangle(int handleNumber)
        {
            Point point = GetHandle(handleNumber);
            return new Rectangle(point.X - penWidth, point.Y - penWidth, penWidth * 2, penWidth * 2);
        }

        public override int HandleCount
        {
            get { return pointList.Count; }
        }

        /// <summary>获得句柄</summary>
        public override Point GetHandle(int handleNumber)
        {
            return pointList[handleNumber - 1];
        }

        /// <summary>根据句柄,获得鼠标形状</summary>
        public override Cursor GetHandleCursor(int handleNumber)
        {
            return Cursors.SizeAll;
        }

        /// <summary>当前鼠标位置是否在句柄范围内</summary>
        public override int HitTest(Point point)
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

        /// <summary>位移函数</summary>
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
                areaPen = new Pen(Color.Black, penWidth + 2);
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


        #region 序列化与反序列化

        public override void LoadSerializdInfo(SerializationInfo info, int objectIndex)
        {
            base.LoadSerializdInfo(info, objectIndex);
            info.AddValue("pointList" + objectIndex, pointList, typeof(List<Point>));
        }

        public override void SaveDeserializdInfo(SerializationInfo info, int objectIndex)
        {
            base.SaveDeserializdInfo(info, objectIndex);
            pointList = (List<Point>)info.GetValue("pointList" + objectIndex, typeof(List<Point>));
        }

        #endregion

    }
}
