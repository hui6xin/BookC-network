using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EllipseObjectExample
{
    public partial class Form1 : Form
    {
        //椭圆对象构成的列表
        private List<DrawEllipse> graphicsList = new List<DrawEllipse>();
        //椭圆id标示
        private int id = 0;
        public Form1()
        {
            InitializeComponent();
        }
        //鼠标按下时触发的事件
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                id++;
                DrawEllipse w = new DrawEllipse(e.X, e.Y, 1, 1, Color.Red);
                graphicsList.Add(w);
                listBox1.Items.Add("椭圆对象：" + id.ToString());
                panel1.Refresh();
            }
        }
        //鼠标移动时触发的事件
        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (graphicsList.Count() > 0)
                {
                    //获得泛型列表的最后一个元素
                    DrawEllipse w = graphicsList.Last();
                    int x = w.Rect.X;
                    int y = w.Rect.Y;
                    w.Rect = new Rectangle(x, y, e.X - x, e.Y - y);
                    panel1.Refresh();
                }
            }
        }
        //Paint事件，重绘椭圆
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            foreach (var g in graphicsList)
            {
                g.Draw(e.Graphics);
            }
        }
        //选择不同椭圆对象时触发的事件
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (var w in graphicsList)
            {
                w.PenColor = Color.Red;
            }
            DrawEllipse dw = graphicsList[listBox1.SelectedIndex];
            dw.PenColor = Color.Blue;
            panel1.Refresh();
        }
        //单击【清除】按钮触发的事件
        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            graphicsList.Clear();
            panel1.Refresh();
        }
    }
}
