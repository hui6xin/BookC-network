using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace HandleObjectExample
{
    public class ToolPointer:ToolObject
    {
        private enum SelectionMode
        {
            None,//初始状态
            NetSelection,//选择图形图像模式
            Move,//移动
            Size//调整大小
        }
        private SelectionMode selectMode = SelectionMode.None;
        private DrawObject resizedObject;
        private int resizedObjectHandle;

        private Point lastPoint = new Point(0, 0);
        private Point startPoint = new Point(0, 0);

        public ToolPointer()
        {
        }

        public override void OnMouseDown(MouseEventArgs e)
        {
            CC.isToolPoint = true;

            Point p = new Point(e.X, e.Y);
            selectMode = SelectionMode.None;
            int n = CC.SelectionCount;
            for (int i = n - 1; i >= 0; i--)
            {
                DrawObject w = CC.GetSelectedObject(i);
                int handleNumber = w.HitHandleTest(p);
                if (handleNumber > 0)
                {
                    selectMode = SelectionMode.Size;
                    resizedObject = w;
                    resizedObjectHandle = handleNumber;
                    CC.UnselectAll();
                    w.Selected = true;
                    break;
                }
            }
            if (selectMode == SelectionMode.None)
            {
                int n1 = CC.graphicsList.Count;
                DrawObject w = null;
                //查找是否有对象被选中
                for (int i = n1 - 1; i >= 0; i--)
                {

                    if (CC.graphicsList[i].HitHandleTest(p) == 0)
                    {
                        w = CC.graphicsList[i];
                        break;
                    }
                }
                if (w != null)
                {
                    selectMode = SelectionMode.Move;
                    if ((Control.ModifierKeys & Keys.Control) == 0 && !w.Selected)
                        CC.UnselectAll();
                    w.Selected = true;
                    CC.panel.Cursor = Cursors.SizeAll;
                }
            }
            if (selectMode == SelectionMode.None)
            {
                if ((Control.ModifierKeys & Keys.Control) == 0)
                {
                    CC.UnselectAll();
                }
                selectMode = SelectionMode.NetSelection;
                CC.IsDrawNetRectangle = true;
            }
            lastPoint.X = p.X;
            lastPoint.Y = p.Y;
            startPoint.X = p.X;
            startPoint.Y = p.Y;
            CC.panel.Capture = true;
            CC.NetRectangle = CC.GetNormalizedRectangle(startPoint, lastPoint);
            CC.panel.Refresh();
        }
        /// <summary>鼠标移动事件</summary>
        public override void OnMouseMove(MouseEventArgs e)
        {
            Point p = new Point(e.X, e.Y);
            if (e.Button == MouseButtons.None)
            {
                Cursor cursor = null;
                if (CC.graphicsList != null)
                {
                    for (int i = CC.graphicsList.Count - 1; i >= 0; i--)
                    {
                        int n = CC.graphicsList[i].HitHandleTest(p);
                        if (n == 0)
                        {
                            cursor = Cursors.Hand;//选择
                        }
                        if (n > 0)
                        {
                            cursor = CC.graphicsList[i].GetHandleCursor(n);
                            break;
                        }
                    }
                }
                if (cursor == null)
                {
                    int x = 0;
                    for (int j = CC.graphicsList.Count - 1; j >= 0; j--)
                    {
                        if (CC.graphicsList[j].Selected)
                        {
                            x++;
                        }
                    }
                    if (x == 1)
                    {
                        for (int j = CC.graphicsList.Count - 1; j >= 0; j--)
                        {
                            if (CC.graphicsList[j].Selected && CC.graphicsList[j].PointInObject(p))
                            {
                                cursor = Cursors.NoMove2D;
                            }
                        }
                    }
                }
                CC.panel.Cursor = cursor;
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
                    CC.panel.Refresh();
                }
            }
            if (selectMode == SelectionMode.Move)
            {
                int n = CC.SelectionCount;
                for (int i = n - 1; i >= 0; i--)
                {
                    CC.GetSelectedObject(i).Move(dx, dy);
                }
                CC.panel.Cursor = Cursors.SizeAll;
                CC.panel.Refresh();
            }
            if (selectMode == SelectionMode.NetSelection)
            {
                CC.NetRectangle = CC.GetNormalizedRectangle(startPoint, lastPoint);
                CC.panel.Refresh();
                return;
            }
        }
        public override void OnMouseUp(MouseEventArgs e)
        {
            if (selectMode == SelectionMode.NetSelection)
            {
                CC.SelectInRectangle(CC.NetRectangle);
                selectMode = SelectionMode.None;
                CC.IsDrawNetRectangle = false;
            }
            int dx = (int)(lastPoint.X - startPoint.X);
            int dy = (int)(lastPoint.Y - startPoint.Y);

            if (resizedObject != null)
            {
                resizedObject = null;
            }
            CC.panel.Capture = false;
            CC.panel.Refresh();
        }
    }
}
