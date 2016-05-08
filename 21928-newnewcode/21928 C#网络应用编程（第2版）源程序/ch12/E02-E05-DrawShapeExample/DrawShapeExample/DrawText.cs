using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace DrawShapeExample
{
    class DrawText:DrawObject
    {
        public Point startPoint { get; set; }//起始点
        public Point endPoint { get; set; }//结束点
        public string text { get; set; }//要显示的文字
        public float angle { get; set; }//旋转的角度
        public int fontHeight { get; set; }//字体的大小
        public Font font { get; set; }//文字的字体
        public DrawText(int x, int y, string textToDraw, Color textColor,int id)
        {
            this.text = textToDraw;
            this.penColor = textColor;
            this.fontHeight = 1;
            this.startPoint = new Point(x, y);
            this.endPoint = new Point(x + (int)(fontHeight * text.Length), y);
            this.ID = id;
        }
        public DrawText(Point p1, Point p2, float angle, string textToDraw, Color textColor, int fontHeight)
        {
            this.startPoint = p1;
            this.endPoint = p2;
            this.angle = angle;
            this.text = textToDraw;
            this.penColor = textColor;
            this.fontHeight = fontHeight;
        }
        public override void Draw(Graphics g)
        {
            Brush b = new SolidBrush(penColor);
            if (this.font != null)
            {
                this.font.Dispose();
            }
            this.font = new Font("宋体", fontHeight, FontStyle.Regular, GraphicsUnit.Pixel);
            Matrix matrix = new Matrix();
            matrix.RotateAt(angle, startPoint);
            g.Transform = matrix;
            g.DrawString(text, font, b, startPoint);
            g.ResetTransform();
            matrix.Dispose();
            b.Dispose();
        }
    }
}
