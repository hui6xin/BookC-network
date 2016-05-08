using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HandleObjectExample
{
    public partial class FormDraw : Form
    {
        //定义鼠标操作：Pointer鼠标指针、Rectangle矩形、Text文本、Curve曲线、Image图像、count工具的个数
        public enum ToolType{Pointer,Rectangle,Text,Curve,Image,count};
        private ToolType activeTool;
        ToolObject[] tools = new ToolObject[(int)ToolType.count];
        public FormDraw()
        {
            InitializeComponent();
            CC.panel = panel1;
            tools[(int)ToolType.Pointer] = new ToolPointer();
            tools[(int)ToolType.Rectangle] = new ToolRectangle();
            tools[(int)ToolType.Curve] = new ToolCurve();
            tools[(int)ToolType.Text] = new ToolText();
            tools[(int)ToolType.Image] = new ToolImage();
        }
        //在Panel内点击鼠标
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            panel1.Capture = false;
            if (e.Button == MouseButtons.Left)
            {
                tools[(int)activeTool].OnMouseDown(e);
            }
            else if (e.Button == MouseButtons.Right)
            {
                activeTool = ToolType.Pointer;
            }
        }
        //在Panel内移动鼠标
        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left || e.Button == MouseButtons.None)
            {
                tools[(int)activeTool].OnMouseMove(e);
            }
            else
            {
                this.Cursor = Cursors.Default;
            }
        }
        //在Panel内释放鼠标
        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                tools[(int)activeTool].OnMouseUp(e);
            }
        }
        //图像重绘
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            DrawObject w;
            for (int i = 0; i <CC.graphicsList.Count; i++)
            {
                w = CC.graphicsList[i];

                if (w.IntersectsWith(Rectangle.Round(g.ClipBounds)))
                {
                    w.Draw(g);
                }

                if (w.Selected == true)
                {
                    w.DrawTracker(g);
                }
            }
            //画鼠标左键拖放范围的选择框
            if (CC.IsDrawNetRectangle == true)
            {
                ControlPaint.DrawFocusRectangle(e.Graphics, CC.NetRectangle, Color.Black, Color.Transparent);
            }

        }
        //曲线
        private void buttonCurve_Click(object sender, EventArgs e)
        {
            activeTool = ToolType.Curve;
        }
        //矩形
        private void buttonRectangle_Click(object sender, EventArgs e)
        {
            activeTool = ToolType.Rectangle;
        }
        //图像
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog f = new OpenFileDialog();
            f.Multiselect = false;
            f.CheckPathExists = true;
            f.Title = "添加图像";
            f.Filter = "JPEG (*.jpg)|*.jpg|Bitmap (*.bmp)|*.bmp|PNG (*.png)|*.png|GIF (*.gif)|*.gif|All files|*.*";
            if (f.ShowDialog() == DialogResult.OK)
            {
               CC.bitmap = (Bitmap)Bitmap.FromFile(f.FileName, true);
               activeTool = ToolType.Image;
            }
        }
        //选中对象
        private void button2_Click(object sender, EventArgs e)
        {
            CC.SelectAll();
            activeTool = ToolType.Pointer;
        }
        //清除选中对象
        private void button3_Click(object sender, EventArgs e)
        {
            CC.UnselectAll();
            activeTool = ToolType.Pointer;
        }
        //文字
        private void buttonText_Click(object sender, EventArgs e)
        {
            TextDialog td = new TextDialog();
            if (td.ShowDialog() == DialogResult.OK)
            {
                CC.textInfo.text = td.MyText;
                CC.textInfo.color = td.MyColor;
                activeTool= ToolType.Text;
            }
        }
    }
}
