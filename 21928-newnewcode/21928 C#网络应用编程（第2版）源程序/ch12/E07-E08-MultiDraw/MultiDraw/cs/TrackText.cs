using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Runtime.Serialization;

namespace MultiDraw
{
    class TrackText : DrawObject
    {
        protected Point startPoint;
        public Point StartPoint
        {
            get { return startPoint; }
        }

        protected Point endPoint;
        public Point EndPoint
        {
            get { return endPoint; }
        }

        protected int fontHeight;
        public int FontHeight
        {
            get { return fontHeight; }
        }

        protected string text;
        public string Text
        {
            get { return text; }
        }

        protected float angle = 0;
        public float Angle
        {
            get { return angle; }
        }

        protected Font font;
        protected GraphicsPath areaPath = null;
        protected Pen areaPen = null;
        protected Region areaRegion = null;

        public override void Draw(Graphics g)
        {
            throw new Exception("该方法还没有实现，请在扩充类中实现该方法");
        }

        protected float GetDistance(Point p1, Point p2)
        {
            int dx = p2.X - p1.X;
            int dy = p2.Y - p1.Y;
            int distance = (int)Math.Sqrt(dx * dx + dy * dy);
            return distance;
        }

        public override DrawObject Clone()
        {
            DrawMyText w = new DrawMyText();
            w.font = this.font;
            w.text = this.text;
            w.startPoint = this.startPoint;
            w.endPoint = this.endPoint;
            w.angle = this.angle;
            base.AddOtherFields(w);
            return w;
        }

        /// <summary>画句柄</summary>
        public override void DrawTracker(Graphics g)
        {
            SolidBrush brush = new SolidBrush(Color.Blue);
            for (int i = 1; i <= HandleCount; i++)
            {
                g.FillRectangle(brush, GetHandleRectangle(i));
            }
            brush.Dispose();
            Matrix matrix = new Matrix();
            matrix.RotateAt(angle, startPoint);
            g.Transform = matrix;
            g.DrawRectangle(Pens.Red,startPoint.X,startPoint.Y,GetDistance(startPoint,endPoint),fontHeight);
            g.ResetTransform();
            matrix.Dispose();
        }

        /// <summary>取句柄的实体矩形</summary>
        public override Rectangle GetHandleRectangle(int handleNumber)
        {
            Point point = GetHandle(handleNumber);
            return new Rectangle(point.X - penWidth, point.Y - penWidth, penWidth * 2, penWidth * 2);
        }

        /// <summary>
        /// 1：左边，2：右边
        /// </summary>
        public override int HandleCount
        {
            get { return 2; }
        }

        /// <summary>获得句柄</summary>
        public override Point GetHandle(int handleNumber)
        {
            if (handleNumber == 1)
            {
                return startPoint;
            }
            else
            {
                return endPoint;
            }
        }

        /// <summary>根据句柄,获得鼠标形状</summary>
        public override Cursor GetHandleCursor(int handleNumber)
        {
            return Cursors.SizeWE;
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
            if (handleNumber == 1)
            {
                return;
            }
            else
            {
                float distance = GetDistance(startPoint, point) - text.Length;
                int height = (int)(distance / text.Length);
                if (height > 0)
                {
                    this.fontHeight = height;
                    endPoint = point;
                    angle = (float)(Math.Atan2(endPoint.Y - startPoint.Y, endPoint.X - startPoint.X) * 180.0 / Math.PI);
                }
            }
            ClearTrackObject();
        }

        /// <summary>位移函数</summary>
        public override void Move(int dx, int dy)
        {
            startPoint = new Point(startPoint.X + dx, startPoint.Y + dy);
            endPoint = new Point(endPoint.X + dx, endPoint.Y + dy);
            ClearTrackObject();
        }

        /// <summary>创建聚焦用的对象</summary>
        protected virtual void CreateTrackObject()
        {
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
                areaPen = new Pen(CC.palette.BackColor, fontHeight);
            }
            if (GetDistance(startPoint, endPoint) > 0)
            {
                areaPath.AddLine(startPoint, endPoint);
                areaPath.CloseFigure();
                areaPath.Widen(areaPen);
                areaRegion.Union(areaPath);
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
            info.AddValue("startPoint" + objectIndex, startPoint, typeof(Point));
            info.AddValue("endPoint" + objectIndex, endPoint, typeof(Point));
            info.AddValue("fontHeight" + objectIndex, fontHeight);
            info.AddValue("text" + objectIndex, text);
            info.AddValue("angle" + objectIndex, angle);
        }

        public override void SaveDeserializdInfo(SerializationInfo info, int objectIndex)
        {
            base.SaveDeserializdInfo(info, objectIndex);
            startPoint = (Point)info.GetValue("startPoint" + objectIndex, typeof(Point));
            endPoint = (Point)info.GetValue("endPoint" + objectIndex, typeof(Point));
            fontHeight = info.GetInt32("fontHeight" + objectIndex);
            text = info.GetString("text" + objectIndex);
            angle = info.GetSingle("angle" + objectIndex);
        }

        #endregion

    }
}
