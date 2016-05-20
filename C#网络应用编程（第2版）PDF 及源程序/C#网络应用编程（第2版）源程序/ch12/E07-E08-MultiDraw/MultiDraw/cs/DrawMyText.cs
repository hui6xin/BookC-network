using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Diagnostics;

namespace MultiDraw
{
    class DrawMyText : TrackText
    {
        public DrawMyText()
        {
        }

        public DrawMyText(int x, int y, string textToDraw, Color textColor, int id)
        {
            this.text = textToDraw;
            this.penColor = textColor;
            this.fontHeight = 1;
            this.id = id;
            startPoint = new Point(x, y);
            endPoint = new Point(x + (int)(fontHeight*text.Length), y);
        }

        public DrawMyText(Point p1, Point p2, float angle, string textToDraw, Color textColor, int fontHeight, int id)
        {
            startPoint = p1;
            endPoint = p2;
            this.angle = angle;
            this.text = textToDraw;
            this.penColor = textColor;
            this.fontHeight = fontHeight;
            this.id = id;
        }

        public override void Draw(Graphics g)
        {
            Brush b = new SolidBrush(penColor);
            if (this.font != null)
            {
                this.font.Dispose();
            }
            this.font = new Font("ËÎÌו", fontHeight, FontStyle.Regular, GraphicsUnit.Pixel);
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
