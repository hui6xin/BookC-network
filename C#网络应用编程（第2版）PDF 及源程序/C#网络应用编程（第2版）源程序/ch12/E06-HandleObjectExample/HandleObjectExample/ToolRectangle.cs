using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace HandleObjectExample
{
    class ToolRectangle : ToolObject
    {
        public ToolRectangle()
        {
        }

        public override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            CC.isToolPoint = false;
            CC.ID = CC.GetNewID();
            DrawRectangle w = new DrawRectangle(e.X, e.Y, 15, 15, Color.Red,CC.ID);
            AddNewObject(w);
            isNewObjectAdded = true;
        }
        public override void OnMouseMove(MouseEventArgs e)
        {
            if (isNewObjectAdded == false)
            { return; }
            int index = CC.FindObjectIndex(CC.ID);
            DrawRectangle w = (DrawRectangle)CC.graphicsList[index];
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
