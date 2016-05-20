using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace MultiDraw
{
    public class ToolObject
    {
        protected bool isNewObjectAdded = false;
        public virtual void OnMouseDown(Palette palette, MouseEventArgs e)
        {
            isNewObjectAdded = false;
            CC.SetNewID();
        }
        public virtual void OnMouseMove(Palette palette, MouseEventArgs e)
        {
            if (isNewObjectAdded == false)
            {
                return;
            }
            Point point = new Point(e.X, e.Y);
            int index = CC.myService.FindObjectIndex(CC.ID);
            if (e.Button == MouseButtons.Left)
            {
                DrawObject w = palette.graphics[index];
                w.MoveHandleTo(point, 5);
            }
            palette.Refresh();
        }
        public virtual void OnMouseUp(Palette palette, MouseEventArgs e)
        {
            palette.Capture = false;
            palette.Refresh();
            isNewObjectAdded = false;
        }
        /// <summary>
        /// 添加新的图形对象
        /// </summary>
        protected void AddNewObject(Palette palette, DrawObject w)
        {
            palette.graphics.UnselectAll();
            w.Selected = true;
            palette.graphics.Add(w);
            palette.Capture = true;
            palette.Refresh();
        }
    }
}
