using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Runtime.Serialization;

namespace MultiDraw
{
    public abstract class DrawObject
    {

        private bool selected;
        /// <summary>是否选择了该对象</summary>
        public bool Selected
        {
            get { return selected; }
            set { selected = value;}
        }

        protected Color penColor;
        /// <summary>画笔颜色</summary>
        public Color PenColor
        {
            get { return penColor; }
            set { penColor = value; }
        }

        protected int penWidth = 2;
        /// <summary>画笔宽度</summary>
        public int PenWidth
        {
            get { return penWidth; }
            set { penWidth = value; }
        }

        protected int id;
        public int ID
        {
            get { return id; }
            set { id = value; }
        }


        #region 扩充类必须实现的属性或方法

        /// <summary>句柄数</summary>
        public abstract int HandleCount
        {
            get;
        }

        /// <summary>克隆</summary>
        public abstract DrawObject Clone();

        /// <summary>画图</summary>
        public abstract void Draw(Graphics g);

        /// <summary>取句柄所在的点</summary>
        public abstract Point GetHandle(int handleNumber);

        /// <summary>画句柄</summary>
        public abstract void DrawTracker(Graphics g);

        /// <summary>判断是否选中该对象及句柄</summary>
        public abstract int HitTest(Point point);

        /// <summary>判断该点是否在对象范围内</summary>
        public abstract bool PointInObject(Point point);

        /// <summary>根据句柄取光标类型</summary>
        public abstract Cursor GetHandleCursor(int handleNumber);

        /// <summary>判断是否相交</summary>
        public abstract bool IntersectsWith(Rectangle rectangle);

        /// <summary>移动对象</summary>
        public abstract void Move(int deltaX, int deltaY);

        /// <summary>移动句柄</summary>
        public abstract void MoveHandleTo(Point point, int handleNumber);

        /// <summary>取句柄的实体矩形</summary>
        public abstract Rectangle GetHandleRectangle(int n);

        #endregion


        protected void AddOtherFields(DrawObject w)
        {
            w.penColor = this.penColor;
            w.penWidth = this.penWidth;
            w.ID = CC.ID;
        }

        #region 序列化与反序列化

        //[OnSerializing]   //OnSerializing特性表示序列化时自动调用自定义的方法SerializdInfo
        public virtual void LoadSerializdInfo(SerializationInfo info, int objectIndex)
        {
            info.AddValue("penColor"+objectIndex, penColor, typeof(Color));
            info.AddValue("penWidth"+objectIndex, penWidth);
            info.AddValue("ID"+objectIndex, ID);
        }

        //[OnDeserializing]   //OnDeserializing特性表示反序列化时自动调用自定义的方法DeserializdInfo
        /// <summary>反序列化</summary>
        public virtual void SaveDeserializdInfo(SerializationInfo info, int objectIndex)
        {
            penColor = (Color)info.GetValue("penColor" + objectIndex, typeof(Color));
            penWidth = info.GetInt32("penWidth" + objectIndex);
            ID = info.GetInt32("ID" + objectIndex);
        }

        #endregion

    }
}
