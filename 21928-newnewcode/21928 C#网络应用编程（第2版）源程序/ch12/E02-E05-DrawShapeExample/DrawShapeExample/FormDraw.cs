using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DrawShapeExample
{
    public partial class FormDraw : Form
    {
        public enum ToolType {Rectangle,Text,Curve,Image,count};
        private ToolType activeTool;
        ToolObject[] tools = new ToolObject[(int)ToolType.count];
        public FormDraw()
        {
            InitializeComponent();
            CC.panel = this.panel1;
            tools[(int)ToolType.Rectangle] = new ToolRectangle();
            tools[(int)ToolType.Curve] = new ToolCurve();
            tools[(int)ToolType.Text] = new ToolText();
            tools[(int)ToolType.Image] = new ToolImage();
        }
        //重绘
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            DrawObject w;
            for (int i = 0; i < CC.graphicsList.Count; i++)
            {
                w = CC.graphicsList[i];
                w.Draw(g);
            }
        }
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            panel1.Capture = false;
            if (e.Button == MouseButtons.Left)
            {
                tools[(int)activeTool].OnMouseDown(e);
            }
        }
        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left || e.Button == MouseButtons.None)
            {
                tools[(int)activeTool].OnMouseMove(e);
            }
        }
        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                tools[(int)activeTool].OnMouseUp(e);
            }
        }

        //矩形
        private void buttonRectangle_Click(object sender, EventArgs e)
        {
            activeTool = ToolType.Rectangle;
        }

        //曲线
        private void buttonCurve_Click(object sender, EventArgs e)
        {
            activeTool = ToolType.Curve;
        }

        //文本
        private void buttonText_Click(object sender, EventArgs e)
        {
            TextDialog txtDialog = new TextDialog();
            if (txtDialog.ShowDialog() == DialogResult.OK)
            {
                CC.strText = txtDialog.MyText;
                CC.color = txtDialog.MyColor;
                activeTool = ToolType.Text;
            }
        }

        private void buttonImage_Click(object sender, EventArgs e)
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
    }
}
