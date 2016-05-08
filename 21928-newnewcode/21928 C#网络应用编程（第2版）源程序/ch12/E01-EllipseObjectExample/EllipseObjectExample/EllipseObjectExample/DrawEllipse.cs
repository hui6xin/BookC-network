using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace EllipseObjectExample
{
    public class DrawEllipse
    {
        public Rectangle Rect { get; set; }
        public Color PenColor { get; set; }
        public DrawEllipse() { }
        public DrawEllipse(int x, int y, int width, int height, Color penColor)
        {
            this.Rect = new Rectangle(x, y, width, height);
            this.PenColor = penColor;
        }

        public void Draw(Graphics g)
        {
            using (Pen pen = new Pen(PenColor))
            {
                g.DrawEllipse(pen, Rect);
            }
        }
    }
}
