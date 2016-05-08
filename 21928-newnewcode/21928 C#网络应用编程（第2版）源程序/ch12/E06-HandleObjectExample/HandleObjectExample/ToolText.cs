using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace HandleObjectExample
{
    class ToolText:ToolObject
    {
        public override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            CC.ID = CC.GetNewID();
            DrawText w = new DrawText(e.X, e.Y, CC.textInfo.text, CC.textInfo.color, CC.ID);
            AddNewObject(w);
            isNewObjectAdded = true;
        }
        public override void OnMouseMove(MouseEventArgs e)
        {
            if (isNewObjectAdded == false)
            {
                return;
            }
            Point point = new Point(e.X, e.Y);
            int index = CC.FindObjectIndex(CC.ID);
            if (e.Button == MouseButtons.Left)
            {
                DrawText w =(DrawText)CC.graphicsList[index];
                w.MoveHandleTo(point, 2);
            }
            CC.panel.Refresh();
        }
        public override void OnMouseUp(MouseEventArgs e)
        {
            if (isNewObjectAdded == false)
            {
                return;
            }
            base.OnMouseUp(e);
        }
    }
}
