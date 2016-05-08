using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.Serialization;

namespace MultiDraw
{
    //带有句柄的矩形
    class TrackRectangle : DrawObject
    {
        protected Rectangle objRectangle;
        /// <summary>矩形对象位置及大小</summary>
        public Rectangle ObjRectangle
        {
            get { return objRectangle; }
            set { objRectangle = value; }
        }

        public override void Draw(Graphics g)
        {
            throw new Exception("该方法还没有实现，请在扩充类中实现该方法");
        }

        public override DrawObject Clone()
        {
            DrawMyRectangle w = new DrawMyRectangle();
            w.objRectangle = this.objRectangle;
            AddOtherFields(w);
            return w;
        }

        ///<summary>句柄数</summary>
        public override int HandleCount {get { return 8; }}

        /// <summary>取句柄所在的点</summary>
        public override Point GetHandle(int handleNumber)
        {
            Rectangle rect = new Rectangle(objRectangle.X, objRectangle.Y, ObjRectangle.Width, ObjRectangle.Height);
            int x = rect.X;
            int y = rect.Y;
            int xCenter = x + rect.Width / 2;
            int yCenter = y + rect.Height / 2;
            switch (handleNumber)
            {
                case 1:
                    x = rect.X;
                    y = rect.Y;
                    break;
                case 2:
                    x = xCenter;
                    y = rect.Y;
                    break;
                case 3:
                    x = rect.Right;
                    y = rect.Y;
                    break;
                case 4:
                    x = rect.Right;
                    y = yCenter;
                    break;
                case 5:
                    x = rect.Right;
                    y = rect.Bottom;
                    break;
                case 6:
                    x = xCenter;
                    y = rect.Bottom;
                    break;
                case 7:
                    x = rect.X;
                    y = rect.Bottom;
                    break;
                case 8:
                    x = rect.X;
                    y = yCenter;
                    break;
            }
            return new Point(x, y);
        }

        /// <summary>画句柄</summary>
        public override void DrawTracker(Graphics g)
        {
            SolidBrush brush = new SolidBrush(Color.Black);
            for (int i = 1; i <= HandleCount; i++)
            {
                g.FillRectangle(brush, GetHandleRectangle(i));
            }
            brush.Dispose();
        }

        /// <summary>判断该对象及句柄是否被选中，-1：未选中，0：选中对象，1-8：选中句柄</summary>
        public override int HitTest(Point point)
        {
            if (Selected)
            {
                for (int i = 1; i <= HandleCount; i++)
                {
                    if (GetHandleRectangle(i).Contains(point))
                    {
                        return i;
                    }
                }
            }
            if (PointInObject(point))
            {
                return 0;
            }
            return -1;
        }

        /// <summary>
        /// 判断该点是否在对象范围内
        /// </summary>
        public override bool PointInObject(Point point)
        {
            return objRectangle.Contains(point);
        }

        /// <summary>根据句柄取光标类型</summary>
        public override Cursor GetHandleCursor(int handleNumber)
        {
            switch (handleNumber)
            {
                case 1:
                    return Cursors.SizeNWSE;
                case 2:
                    return Cursors.SizeNS;
                case 3:
                    return Cursors.SizeNESW;
                case 4:
                    return Cursors.SizeWE;
                case 5:
                    return Cursors.SizeNWSE;
                case 6:
                    return Cursors.SizeNS;
                case 7:
                    return Cursors.SizeNESW;
                case 8:
                    return Cursors.SizeWE;
                default:
                    return Cursors.Default;
            }
        }

        /// <summary>判断该矩形与rectangle是否相交</summary>
        public override bool IntersectsWith(Rectangle rectangle)
        {
            return objRectangle.IntersectsWith(rectangle);
        }

        /// <summary>移动对象</summary>
        public override void Move(int deltaX, int deltaY)
        {
            objRectangle.X += deltaX;
            objRectangle.Y += deltaY;
        }

        /// <summary>移动句柄</summary>
        public override void MoveHandleTo(Point point, int handleNumber)
        {
            int left = objRectangle.X;
            int top = objRectangle.Y;
            int right = objRectangle.Right;
            int bottom = objRectangle.Bottom;
            switch (handleNumber)
            {
                case 1:
                    left = point.X;
                    top = point.Y;
                    break;
                case 2:
                    top = point.Y;
                    break;
                case 3:
                    right = point.X;
                    top = point.Y;
                    break;
                case 4:
                    right = point.X;
                    break;
                case 5:
                    right = point.X;
                    bottom = point.Y;
                    break;
                case 6:
                    bottom = point.Y;
                    break;
                case 7:
                    left = point.X;
                    bottom = point.Y;
                    break;
                case 8:
                    left = point.X;
                    break;
            }
            objRectangle = CC.GetNormalizedRectangle(left, top, right, bottom);
        }

        /// <summary>取句柄的实体矩形</summary>
        public override Rectangle GetHandleRectangle(int n)
        {
            Point point = GetHandle(n);
            int x = point.X;
            int y = point.Y;
            if (n == 1) { x -= 4; y -= 4; }
            else if (n == 2) { x -= 2; y -= 4; }
            else if (n == 3) { x += penWidth; y -= 4; }
            else if (n == 4) { x += penWidth; y -= 2; }
            else if (n == 5) { x += penWidth; y += penWidth; }
            else if (n == 6) { x -= 2; y += penWidth; }
            else if (n == 7) { x -= 4; y += penWidth; }
            else if (n == 8) { x -= 4; y -= 2; }
            return new Rectangle(x, y, 5, 5);
        }


        #region 序列化与反序列化

        public override void LoadSerializdInfo(SerializationInfo info, int objectIndex)
        {
            base.LoadSerializdInfo(info, objectIndex);
            info.AddValue("objRectangle" + objectIndex, objRectangle, typeof(Rectangle));
        }

        public override void SaveDeserializdInfo(SerializationInfo info, int objectIndex)
        {
            base.SaveDeserializdInfo(info, objectIndex);
            objRectangle = (Rectangle)info.GetValue("objRectangle" + objectIndex, typeof(Rectangle));
        }

        #endregion

    }
}
