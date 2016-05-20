using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace DrawShapeExample
{
    class ToolImage:ToolObject
    {
        //鼠标按下时触发的事件
        public override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                CC.ID = CC.GetNewID();
                DrawImage w = new DrawImage(e.X, e.Y, 10, 10, CC.bitmap,CC.ID);
                this.AddNewObject(w);
                this.isNewObjectAdded = true;
            }
        }
        //鼠标移动时触发的事件
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
