using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace DrawShapeExample
{
    class DrawRectangle:DrawObject
    {
        public Rectangle objRectangle;
        public DrawRectangle() { }
        public DrawRectangle(int x, int y, int width, int height, Color penColor,int id)
        {
            this.objRectangle = new Rectangle(x, y, width, height);
            this.penColor = penColor;
            this.ID = id;
        }
        //重写基类的方法
        public override void Draw(Graphics g)
        {
            using (Pen pen = new Pen(this.penColor))
            {
                g.DrawRectangle(pen, this.objRectangle);
            }
        }
    }
}
