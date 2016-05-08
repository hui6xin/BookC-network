using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace DrawShapeExample
{
    class ToolRectangle:ToolObject
    {
        public override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                CC.ID= CC.GetNewID();
                DrawRectangle w = new DrawRectangle(e.X, e.Y, 1, 1, Color.Red,CC.ID);
                this.AddNewObject(w);
                this.isNewObjectAdded = true;
            }
        }

        public override void OnMouseMove(MouseEventArgs e)
        {
            if (this.isNewObjectAdded == false)
            { return; }
            if (e.Button == MouseButtons.Left)
            {
                int index= CC.FindObjectIndex(CC.ID);
                DrawRectangle w = (DrawRectangle)CC.graphicsList[index];
                int x = w.objRectangle.X;
                int y = w.objRectangle.Y;
                Rectangle rect = new Rectangle(x, y, e.X - x, e.Y - y);
                w.objRectangle = rect;             
            }
            CC.panel.Refresh();
        }
    }
}
