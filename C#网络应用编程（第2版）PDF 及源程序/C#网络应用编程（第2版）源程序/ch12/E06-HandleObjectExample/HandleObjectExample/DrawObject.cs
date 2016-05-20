using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace HandleObjectExample
{
    /// <summary>
    /// 所有绘图对象的基类
    /// </summary>
    public abstract class DrawObject
    {
        public DrawObject()
        {
            this.PenWidth = 2;
        }
        /// <summary>
        /// 是否选择了该对象
        /// </summary>
        public bool Selected { get; set; }
        /// <summary>
        /// 画笔颜色
        /// </summary>
        public Color PenColor { get; set; }
        /// <summary>
        /// 画笔宽度
        /// </summary>
        public int PenWidth { get; set; }
        /// <summary>
        /// 绘制图像的ID
        /// </summary>
        public int ID { get; set; }       
        /// <summary>画图</summary>
        public abstract void Draw(Graphics g);
        /// <summary>克隆</summary>
        public abstract DrawObject Clone();

        #region 扩充类必须实现的属性或方法

        /// <summary>句柄数</summary>
        public abstract int HandleCount { get; }
        /// <summary>获取句柄所在的点</summary>
        public abstract Point GetHandle(int handleNumber);
        /// <summary>取句柄的实体矩形</summary>
        public abstract Rectangle GetHandleRectangle(int handleNumber);
        /// <summary>绘制句柄</summary>
        public abstract void DrawTracker(Graphics g);
        /// <summary>判断是否选中该对象及句柄</summary>
        public abstract int HitHandleTest(Point point);
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

        #endregion
    }
}
