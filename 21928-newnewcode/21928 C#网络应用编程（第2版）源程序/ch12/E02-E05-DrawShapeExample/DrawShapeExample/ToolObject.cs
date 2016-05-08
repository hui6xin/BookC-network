using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DrawShapeExample
{
    //工具类的基类
    class ToolObject
    {

        protected bool isNewObjectAdded = false;
        public virtual void OnMouseDown(MouseEventArgs e)
        {
        }

        public virtual void OnMouseMove(MouseEventArgs e)
        {
        }

        public virtual void OnMouseUp(MouseEventArgs e)
        {
            CC.panel.Capture = false;
            CC.panel.Refresh();
            isNewObjectAdded = false;
        }

        ///// <summary>添加新的图形对象</summary>
        protected void AddNewObject(DrawObject w)
        {
            CC.panel.Capture = true;
            CC.graphicsList.Add(w);
            CC.panel.Refresh();
        }
    }
}
