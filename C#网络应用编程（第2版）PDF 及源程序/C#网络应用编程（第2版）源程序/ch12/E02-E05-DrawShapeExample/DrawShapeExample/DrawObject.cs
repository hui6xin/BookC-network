using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace DrawShapeExample
{
    /// <summary>
    /// 所有绘图对象的基类
    /// </summary>
    public abstract class DrawObject
    {
        public Color penColor { get; set; }//画笔的颜色
        public float PenWidth { get; set; }//画笔的粗细
        public int ID { get; set; }//绘制图像的ID
        public abstract void Draw(Graphics g);//绘图方法
    }
}
