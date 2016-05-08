using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace DrawShapeExample
{
    class ToolText:ToolObject
    {
        public override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.isNewObjectAdded = true;
                CC.ID = CC.GetNewID();
                DrawText w = new DrawText(e.X, e.Y, CC.strText, CC.color,CC.ID);
                this.AddNewObject(w);
                CC.panel.Refresh();
            }
        }
        public override void OnMouseMove(MouseEventArgs e)
        {
            if (this.isNewObjectAdded == false)
            { return; }
            int index = CC.FindObjectIndex(CC.ID);
            DrawText w = (DrawText)CC.graphicsList[index];
            if (e.Button == MouseButtons.Left)
            {
                Point point = new Point(e.X, e.Y);
                float distance = CC.GetDistance(w.startPoint, point) - w.text.Length;
                int height = (int)(distance / w.text.Length);
                if (height > 0)
                {
                    w.fontHeight = height;
                    w.endPoint = point;
                    w.angle = (float)(Math.Atan2(w.endPoint.Y - w.startPoint.Y, w.endPoint.X - w.startPoint.X) * 180.0 / Math.PI);
                }
            }
            CC.panel.Refresh();
        }
    }
}
