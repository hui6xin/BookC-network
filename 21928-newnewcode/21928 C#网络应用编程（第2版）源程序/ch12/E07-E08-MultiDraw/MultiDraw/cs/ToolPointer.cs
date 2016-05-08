using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace MultiDraw
{
    class ToolPointer : ToolObject
    {
        private enum SelectionMode
        {
            None,
            NetSelection,
            Move,
            Size
        }
        private SelectionMode selectMode = SelectionMode.None;
        private DrawObject resizedObject;
        private int resizedObjectHandle;

        private Point lastPoint = new Point(0, 0);
        private Point startPoint = new Point(0, 0);

        public ToolPointer()
        {
        }

        public override void OnMouseDown(Palette palette, MouseEventArgs e)
        {
            CC.isToolPoint = true;

            Point p = new Point(e.X, e.Y);
            selectMode = SelectionMode.None;
            int n = palette.graphics.SelectionCount;
            for (int i = n - 1; i >= 0; i--)
            {
                DrawObject w = palette.graphics.GetSelectedObject(i);

                int handleNumber = w.HitTest(p);

                if (handleNumber > 0)
                {
                    selectMode = SelectionMode.Size;
                    resizedObject = w;
                    resizedObjectHandle = handleNumber;
                    palette.graphics.UnselectAll();
                    w.Selected = true;
                    break;
                }
            }
            if (selectMode == SelectionMode.None)
            {
                int n1 = palette.graphics.Count;
                DrawObject w = null;
                //查找是否有对象被选中
                for (int i = n1 - 1; i >= 0; i--)
                {

                    if (palette.graphics[i].HitTest(p) == 0)
                    {
                        w = palette.graphics[i];
                        break;
                    }
                }
                if (w != null)
                {
                    selectMode = SelectionMode.Move;
                    if ((Control.ModifierKeys & Keys.Control) == 0 && !w.Selected)
                        palette.graphics.UnselectAll();
                    w.Selected = true;
                    palette.Cursor = Cursors.SizeAll;
                }
            }
            if (selectMode == SelectionMode.None)
            {
                if ((Control.ModifierKeys & Keys.Control) == 0)
                {
                    palette.graphics.UnselectAll();
                }
                selectMode = SelectionMode.NetSelection;
                palette.IsDrawNetRectangle = true;
            }
            lastPoint.X = p.X;
            lastPoint.Y = p.Y;
            startPoint.X = p.X;
            startPoint.Y = p.Y;
            palette.Capture = true;
            palette.NetRectangle = CC.GetNormalizedRectangle(startPoint, lastPoint);
            palette.Refresh();
        }

        /// <summary>鼠标移动事件</summary>
        public override void OnMouseMove(Palette palette, MouseEventArgs e)
        {
            Point p = new Point(e.X, e.Y);
            if (e.Button == MouseButtons.None)
            {
                Cursor cursor = null;
                if (palette.graphics != null)
                {
                    for (int i = palette.graphics.Count - 1; i >= 0; i--)
                    {
                        int n = palette.graphics[i].HitTest(p);
                        if (n == 0)
                        {
                            cursor = Cursors.Hand;
                        }
                        if (n > 0)
                        {
                            cursor = palette.graphics[i].GetHandleCursor(n);
                            break;
                        }
                    }
                }

                if (cursor == null)
                {
                    int x = 0;
                    for (int j = palette.graphics.Count - 1; j >= 0; j--)
                    {
                        if (palette.graphics[j].Selected)
                        {
                            x++;
                        }
                    }
                    if (x == 1)
                    {
                        for (int j = palette.graphics.Count - 1; j >= 0; j--)
                        {
                            if (palette.graphics[j].Selected && palette.graphics[j].PointInObject(p))
                            {
                                cursor = Cursors.NoMove2D;
                            }
                        }
                    }
                }
                palette.Cursor = cursor;
                return;
            }
            if (e.Button != MouseButtons.Left) return;

            int dx = p.X - lastPoint.X;
            int dy = p.Y - lastPoint.Y;

            lastPoint.X = p.X;
            lastPoint.Y = p.Y;

            if (selectMode == SelectionMode.Size)
            {
                if (resizedObject != null)
                {
                    resizedObject.MoveHandleTo(p, resizedObjectHandle);
                    palette.Refresh();
                }
            }

            if (selectMode == SelectionMode.Move)
            {
                int n = palette.graphics.SelectionCount;
                for (int i = n - 1; i >= 0; i--)
                {
                    palette.graphics.GetSelectedObject(i).Move(dx, dy);
                }
                palette.Cursor = Cursors.SizeAll;
                palette.Refresh();
            }

            if (selectMode == SelectionMode.NetSelection)
            {
                palette.NetRectangle = CC.GetNormalizedRectangle(startPoint, lastPoint);
                palette.Refresh();
                return;
            }
        }

        public override void OnMouseUp(Palette palette, MouseEventArgs e)
        {
            if (selectMode == SelectionMode.NetSelection)
            {
                palette.graphics.SelectInRectangle(palette.NetRectangle);
                selectMode = SelectionMode.None;
                palette.IsDrawNetRectangle = false;
            }
            int dx = (int)(lastPoint.X - startPoint.X);
            int dy = (int)(lastPoint.Y - startPoint.Y);
            string s = "";
            if (selectMode == SelectionMode.Size)
            {
                if (resizedObject != null)
                {
                    if (CC.userState != UserState.SingleUser)
                    {
                        s = resizedObject.ID.ToString() + "@" + resizedObjectHandle;
                        //x,y,移动对象ID和句柄,IPEndPoint
                        CC.me.SendToServer(string.Format("MoveObjectHandle,{0},{1},{2},{3}", lastPoint.X, lastPoint.Y, s, CC.me.client.Client.LocalEndPoint));
                    }
                }
            }
            if (selectMode == SelectionMode.Move)
            {
                if (CC.userState != UserState.SingleUser)
                {
                    int n = palette.graphics.SelectionCount;
                    s = "";
                    for (int i = n - 1; i >= 0; i--)
                    {
                        DrawObject w = palette.graphics.GetSelectedObject(i);
                        s += w.ID + "@";
                    }
                    //x,y,移动的对象ID集合,IPEndPoint
                    CC.me.SendToServer(string.Format("MoveObject,{0},{1},{2},{3}", dx, dy, s.TrimEnd('@'), CC.me.client.Client.LocalEndPoint));
                }
            }

            if (resizedObject != null)
            {
                resizedObject = null;
            }
            palette.Capture = false;
            palette.Refresh();
        }
    }
}
