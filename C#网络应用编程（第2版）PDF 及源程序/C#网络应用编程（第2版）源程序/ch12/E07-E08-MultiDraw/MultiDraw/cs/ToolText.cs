using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace MultiDraw
{
    class ToolText : ToolObject
    {
        public override void OnMouseDown(Palette palette, MouseEventArgs e)
        {
            base.OnMouseDown(palette, e);
            DrawMyText w = new DrawMyText(e.X, e.Y, CC.textInfo.text, CC.textInfo.color, CC.ID);
            AddNewObject(palette, w);
            isNewObjectAdded = true;
        }

        public override void OnMouseMove(Palette palette, MouseEventArgs e)
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
                w.MoveHandleTo(point, 2);
            }
            palette.Refresh();
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
                DrawMyText w = (DrawMyText)palette.graphics[index];
                //x1，y1,x2,y2,旋转角度,文字内容,颜色,文字高,id
                CC.me.SendToServer(string.Format("DrawMyText,{0},{1},{2},{3},{4},{5},{6},{7},{8}",
                    w.StartPoint.X, w.StartPoint.Y, w.EndPoint.X, w.EndPoint.Y, w.Angle, w.Text, w.PenColor.ToArgb(), w.FontHeight, w.ID));
                palette.graphics.Remove(CC.ID);
            }

        }
    }
}
