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
    //[System.ComponentModel.Designer("System.Windows.Forms.Design.ParentControlDesigner,   System.Design ")]
    public partial class mybutton : ContainerControl 
    {
        public mybutton()
        {
            InitializeComponent();
            //this
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
