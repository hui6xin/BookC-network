using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace HandleObjectExample
{
     class ToolImage:ToolObject
    {
        public override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            CC.isToolPoint = false;
            CC.ID = CC.GetNewID();
            DrawImage w = new DrawImage(e.X, e.Y, 15, 15, CC.bitmap, CC.ID);
            AddNewObject(w);
            isNewObjectAdded = true;
        }
        public override void OnMouseMove(MouseEventArgs e)
        {
            if (isNewObjectAdded == false)
            { return; }
            int index = CC.FindObjectIndex(CC.ID);
            DrawImage w = (DrawImage)CC.graphicsList[index];
            if (e.Button == MouseButtons.Left)
            {
                int x = w.objRectangle.X;
                int y = w.objRectangle.Y;
                Rectangle rect = new Rectangle(x, y, e.X - x, e.Y - y);
                w.objRectangle = rect;
            }
            CC.panel.Refresh();
        }
    }
}
