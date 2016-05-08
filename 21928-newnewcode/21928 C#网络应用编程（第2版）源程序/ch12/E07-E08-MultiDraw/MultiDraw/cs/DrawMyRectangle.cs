using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace MultiDraw
{
    class DrawMyRectangle : TrackRectangle
    {
        public DrawMyRectangle()
        {
        }
        public DrawMyRectangle(int x, int y, int width, int height, Color penColor, int id)
        {
            this.objRectangle = new Rectangle(x, y, width, height);
            this.penColor = penColor;
            this.id = id;
        }

        public override void Draw(Graphics g)
        {
            Pen pen = new Pen(penColor);
            g.DrawRectangle(pen, objRectangle);
            pen.Dispose();
        }

    }
}
