using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FullWindows
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            //button1.PointToClient(new Point( this.Width / 2, this.Height / 2) );
            //button1.PointToScreen(new Point(Screen.PrimaryScreen.Bounds.Width / 2, Screen.PrimaryScreen.Bounds.Height / 2));
            //button1.Left = this.Width / 2;
            //button1.Top = this.Height / 2;
            //button1.Left = Screen.PrimaryScreen.Bounds.Width / 2-button1.Width;
            //button1.Top = Screen.PrimaryScreen.Bounds.Height / 2-button1.Height;
            //SystemInformation.WorkingArea
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            if( form2.ShowDialog() ==DialogResult.OK )
            {
                this.Close();
            }

            this.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button1.Left = this.Width / 2;
            button1.Top = this.Height / 2;
        }
    }
}
