using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace formfocuscues
{
    using System.Drawing.Drawing2D;
    using System.Windows;
    public partial class mybutton2 : System.Windows.Forms.Button
    {
        private bool mouseover = false;
        public mybutton2()
        {
            InitializeComponent();
            this.Cursor = System.Windows.Forms.Cursors.Hand;
        }
        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);
        }
        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
        }
        protected override void OnNotifyMessage(Message m)
        {
            base.OnNotifyMessage(m);
        }
        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            base.OnPaintBackground(pevent);
        }
        protected override bool ShowFocusCues
        {
            get
            {
                return false;
                //return base.ShowFocusCues;
            }
        }
    }
}
