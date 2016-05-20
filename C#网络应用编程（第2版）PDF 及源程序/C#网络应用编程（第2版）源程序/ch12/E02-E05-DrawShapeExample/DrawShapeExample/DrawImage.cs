using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace DrawShapeExample
{
    class DrawImage : DrawRectangle
    {
        //保存图像
        private Bitmap originalBitmap;
        //构造函数
        public DrawImage(int x, int y, int width, int height, Bitmap bitmap, int id)
        {
            this.objRectangle = new Rectangle(x, y, width, height);
            this.originalBitmap = new Bitmap(bitmap);
            this.ID = id;
        }
        //绘制图像
        public override void Draw(Graphics g)
        {
            if (originalBitmap == null)
            {
                Pen p = new Pen(Color.Black, -1f);
                g.DrawRectangle(p, this.objRectangle);
            }
            else
            {
                //将bitmap设置为对其默认的透明色透明
                originalBitmap.MakeTransparent();
                g.DrawImage(originalBitmap, this.objRectangle, 0, 0, originalBitmap.Width, originalBitmap.Height, GraphicsUnit.Pixel);
            }
        }
    }
}
