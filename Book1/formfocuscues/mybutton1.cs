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
    public partial class mybutton1 : System.Windows.Forms.Button
    {

        private bool mouseover = false;

        public mybutton1()
        {
            InitializeComponent();
            // 

            // todo: 在此处添加构造函数逻辑 

            // 

            this.Cursor = System.Windows.Forms.Cursors.Hand;

        }

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {

            // base.onpaint (e); 

            // System.Drawing.Graphics pp=this.creategraphics(); 

            // e.Graphics.clear(Color.transparent); 

            // e.Graphics.drawellipse(new System.Drawing.Pen(System.Drawing.Color.whitesmoke,5),0,0,this.Width,this.Height); 

            // System.Drawing.solidbrush dd=new solidbrush(System.Drawing.Color.whitesmoke); 

            // e.Graphics.fillellipse(dd,0,0,this.Width,this.Height); 



            // (this.backcolor.tostring ()) 

            Color c5 = Color.FromArgb

            (255, 255, 255);

            Color c2 = Color.FromArgb

            (192, 192, 192);

            if (mouseover)
            {

                c5 = Color.FromArgb(245, 245, 245);

                //c2=Color.FromArgb(192,192,192); 

                c2 = Color.FromArgb(180, 175, 190);

            }

            Brush b = new System.Drawing.Drawing2D.LinearGradientBrush

            (ClientRectangle, c5, c2, LinearGradientMode.Vertical);

            //System.Drawing.region=new region( 

            int offsetwidth = this.Width / 50;

            Point[] points = new Point[8];

            points[0].X = offsetwidth;

            points[0].Y = 0;



            points[1].X = this.Width - offsetwidth;

            points[1].Y = 0;



            points[2].X = this.Width;

            points[2].Y = offsetwidth;



            points[3].X = this.Width;

            points[3].Y = this.Height - offsetwidth;



            points[4].X = this.Width - offsetwidth;

            points[4].Y = this.Height;



            points[5].X = offsetwidth;

            points[5].Y = this.Height;



            points[6].X = 0;

            points[6].Y = this.Height - offsetwidth;



            points[7].X = 0;

            points[7].Y = offsetwidth;

            // e.Graphics.fillrectangle (b, ClientRectangle); 

            e.Graphics.FillPolygon(b, points, FillMode.Winding);

            if (this.Focused)
            {

                int offsetwidth1 = (this.Width - 5) / 50 + 2;

                Point[] points1 = new Point[8];

                points1[0].X = offsetwidth1;

                points1[0].Y = 2;



                points1[1].X = this.Width - offsetwidth1;

                points1[1].Y = 2;



                points1[2].X = this.Width - 1;

                points1[2].Y = offsetwidth1;



                points1[3].X = this.Width - 1;

                points1[3].Y = this.Height - offsetwidth1;



                points1[4].X = this.Width - offsetwidth1;

                points1[4].Y = this.Height - 1;



                points1[5].X = 1;

                points1[5].Y = this.Height - 1;



                points1[6].X = 2;

                points1[6].Y = this.Height - offsetwidth1;



                points1[7].X = 2;

                points1[7].Y = offsetwidth1;

                // e.Graphics.drawpolygon(new Pen(Color.yellow,2),points1); 

                Pen p = new Pen(Color.Orange, 2);

                Pen p1 = new Pen(Color.Wheat, 2);

                //p.dashstyle=dashstyle.dashdot; 

                e.Graphics.DrawLine(p1, points1[0], points1[1]);



                e.Graphics.DrawLine(p, points1[1], points1[2]);

                e.Graphics.DrawLine(p, points1[2], points1[3]);

                e.Graphics.DrawLine(p, points1[3], points1[4]);

                e.Graphics.DrawLine(p, points1[4], points1[5]);

                e.Graphics.DrawLine(p, points1[5], points1[6]);

                e.Graphics.DrawLine(p1, points1[6], points1[7]);

                e.Graphics.DrawLine(p1, points1[7], points1[0]);

            }

            e.Graphics.DrawPolygon(new Pen(Color.DarkBlue, 2), points);

            // e.Graphics.DrawLine(new Pen(Color.darkblue,2),new Point(0,0),new Point(this.Width,0)); 

            // e.Graphics.DrawLine(new Pen(Color.darkblue,2),new Point(0,0),new Point(0,this.Height)); 

            // e.Graphics.DrawLine(new Pen(Color.darkblue,2),new Point(this.Width,this.Height),new Point(this.Width,0)); 

            // e.Graphics.DrawLine(new Pen(Color.darkblue,2),new Point(this.Width,this.Height),new Point(0,this.Height)); 

            StringFormat drawformat = new StringFormat();

            drawformat.FormatFlags = StringFormatFlags.DisplayFormatControl;

            drawformat.LineAlignment = StringAlignment.Center;

            drawformat.Alignment = System.Drawing.StringAlignment.Center;



            e.Graphics.DrawString(this.Text, this.Font, new LinearGradientBrush(this.ClientRectangle, Color.Black, Color.Black, LinearGradientMode.Vertical), this.ClientRectangle, drawformat);

            b.Dispose();





        }

        protected override void OnLeave(EventArgs e)
        {

            base.OnLeave(e);

        }



        // protected override void onmousehover(EventArgs e) 

        // { 

        // 

        // mouseover=true; 

        // this.invalidate(false); 

        // base.onmousehover (e); 

        // } 

        protected override void OnMouseEnter(EventArgs e)
        {

            mouseover = true;

            this.Invalidate(false);

            base.OnMouseEnter(e);

        }



        protected override void OnNotifyMessage(System.Windows.Forms.Message m)
        {

            base.OnNotifyMessage(m);

        }



        protected override void OnMouseLeave(EventArgs e)
        {

            mouseover = false;

            this.Invalidate(false);

            base.OnMouseLeave(e);

        }

        private void drawbutton(System.Drawing.Graphics g)
        {



        }

        protected override void OnPaintBackground(System.Windows.Forms.PaintEventArgs pevent)
        {



            // Color c5 = Color.FromArgb 

            // (255,255,255); 

            // Color c2 = Color.FromArgb 

            // (192,192,192); 

            // if(mouseover) 

            // { 

            // c5=Color.FromArgb(245,245,245); 

            // //c2=Color.FromArgb(192,192,192); 

            // c2=Color.FromArgb(180,175,190); 

            // } 

            // brush b = new System.Drawing.Drawing2D.LinearGradientBrush 

            // (ClientRectangle, c5, c2, LinearGradientMode.vertical); 



            //pevent.Graphics .drawrectangle(new Pen(Color.transparent,2),this.ClientRectangle); 

            pevent.Graphics.Clear(Color.Wheat);

            //base.onpaintbackground (pevent); 



        }

    }

}
