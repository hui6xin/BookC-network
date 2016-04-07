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
    public partial class mybutton3 : Panel
    {
        public mybutton3()
        {
            InitializeComponent();
        }

        private void mybutton3_Paint(object sender, PaintEventArgs e)
        {
            
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias; //使绘图质量最高，即消除锯齿
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;

            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            path.AddEllipse(0, 0, this.Width, this.Height);
            this.Region = new Region(path);
        }
        protected override void OnMouseEnter(EventArgs e)
        {
            //base.OnMouseEnter(e);
            //Graphics g = this.CreateGraphics();

            //g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias; //使绘图质量最高，即消除锯齿
            //g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            //g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;

            //g.DrawEllipse(new Pen(Color.Blue), 0, 0, this.Width, this.Height);
            //g.Dispose();
        }
        //圆形
        //private void Form1_Paint(object sender, PaintEventArgs e)
        //{
        //    string filename = "icon.png";//如果不是png类型，须转换
        //    System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(filename);
        //    for (int y = 0; y < 100; y++)
        //    {
        //        for (int x = 0; x < 100; x++)
        //        {
        //            if ((x - 50) * (x - 50) + (y - 50) * (y - 50) > 50 * 50)
        //            {
        //                bitmap.SetPixel(x, y, System.Drawing.Color.FromArgb(0, 255, 255, 255));
        //            }
        //        }
        //    }

        //    Graphics g = CreateGraphics();
        //    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
        //    g.DrawImage(bitmap, new Point(50, 50));
        //    g.DrawEllipse(new Pen(Color.LightGray), 50, 50, 100, 100);
        //    g.Dispose();
        //}
    }
}
