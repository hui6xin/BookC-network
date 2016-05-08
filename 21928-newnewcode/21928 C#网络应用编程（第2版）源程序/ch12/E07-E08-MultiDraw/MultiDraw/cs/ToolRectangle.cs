using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace MultiDraw
{
    class ToolRectangle : ToolObject
    {
        public ToolRectangle()
        {
        }

        public override void OnMouseDown(Palette palette, MouseEventArgs e)
        {
            base.OnMouseDown(palette, e);
            DrawMyRectangle w = new DrawMyRectangle(e.X, e.Y, 15, 15, Color.Red, CC.ID);
            AddNewObject(palette, w);
            isNewObjectAdded = true;
        }

        public override void OnMouseUp(Palette palette, MouseEventArgs e)
        {
            if (isNewObjectAdded == false)
            {
                return;
            }
            base.OnMouseUp(palette, e);
            if (CC.userState != UserState.SingleUser)
            {
                int index = CC.myService.FindObjectIndex(CC.ID);
                DrawMyRectangle w = (DrawMyRectangle)palette.graphics[index];
                //左上角x坐标，左上角y坐标,宽,高,颜色,id
                CC.me.SendToServer(string.Format("DrawMyRectangle,{0},{1},{2},{3},{4},{5}",
                    w.ObjRectangle.X, w.ObjRectangle.Y, w.ObjRectangle.Width, w.ObjRectangle.Height, w.PenColor.ToArgb(), CC.ID));
                palette.graphics.Remove(CC.ID);
            }
        }
    }
}
