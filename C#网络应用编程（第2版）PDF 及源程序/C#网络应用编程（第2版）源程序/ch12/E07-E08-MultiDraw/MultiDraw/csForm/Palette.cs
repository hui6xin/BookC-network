using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace MultiDraw
{
    public partial class Palette : UserControl
    {
        public Palette()
        {
            InitializeComponent();
            tools = new ToolObject[Enum.GetNames(typeof(ToolType)).Length];
            tools[(int)ToolType.Pointer] = new ToolPointer();
            tools[(int)ToolType.Rectangle] = new ToolRectangle();
            tools[(int)ToolType.Text] = new ToolText();
            tools[(int)ToolType.Curve] = new ToolCurve();
            tools[(int)ToolType.Image] = new ToolImage();
            activeTool = ToolType.Pointer;
        }

        private bool isDrawNetRectangle = false;
        /// <summary>是否画鼠标左键拖放范围的矩形框</summary>
        public bool IsDrawNetRectangle
        {
            get { return isDrawNetRectangle; }
            set { isDrawNetRectangle = value; }
        }
        private Rectangle netRectangle;
        /// <summary>鼠标左键拖放范围矩形大小及位置</summary>
        public Rectangle NetRectangle
        {
            get { return netRectangle; }
            set { netRectangle = value; }
        }
        private GraphicsList _graphics = new GraphicsList();
        /// <summary>绘图对象列表</summary>
        public GraphicsList graphics
        {
            get { return _graphics; }
            set { _graphics = value; }
        }
        /// <summary>工具类型</summary>
        public enum ToolType
        {
            Pointer,
            Rectangle,
            Text,
            Curve,
            Image
        };

        private ToolType activeTool;
        public ToolType ActiveTool
        {
            get { return activeTool; }
            set { activeTool = value; }
        }
        private ToolObject[] tools;
        private void Palette_MouseDown(object sender, MouseEventArgs e)
        {
            this.Capture = false;
            if (e.Button == MouseButtons.Left)
            {
                tools[(int)activeTool].OnMouseDown(this, e);
            }
            else if (e.Button == MouseButtons.Right)
            {
                ActiveTool = Palette.ToolType.Pointer;
            }

        }
        private void Palette_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left || e.Button == MouseButtons.None)
            {
               tools[(int)activeTool].OnMouseMove(this, e);
            }
            else
            {
                this.Cursor = Cursors.Default;
            }
        }
        private void Palette_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                tools[(int)activeTool].OnMouseUp(this, e);
            }

        }
        private void Palette_Paint(object sender, PaintEventArgs e)
        {
            this._graphics.Draw(e.Graphics);

            //画鼠标左键拖放范围的选择框
            if (IsDrawNetRectangle == true)
            {
                ControlPaint.DrawFocusRectangle(e.Graphics, netRectangle, Color.Black, Color.Transparent);
            }

        }
        /// <summary>将Graphics序列化到fileName中</summary>
        public void SerializeObject(GraphicsList serializedGraphics, string fileName)
        {
            try
            {
                using (Stream stream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                {
                    IFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(stream, serializedGraphics);
                }
            }
            catch (Exception err)
            {
                MessageBox.Show("保存文件失败，原因：" + err.Message);
            }
        }
        /// <summary>将fileName反序列化到Graphics中</summary>
        public void DeserializeObject(string fileName)
        {
            if (File.Exists(fileName) == false)
            {
                return;
            }
            try
            {
                using (Stream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                {
                    IFormatter formatter = new BinaryFormatter();
                    CC.palette.graphics = (GraphicsList)formatter.Deserialize(stream);

                    //刚打开文件时，初始化ID
                    int id = 0;
                        for (int i = 0; i < graphics.Count; i++)
                        {
                            if (graphics[i].ID > id)
                            {
                                id = graphics[i].ID;
                            }
                        }
                    id++;
                    if (CC.userState == UserState.SingleUser)
                    {
                        CC.ID = id;
                    }
                    else
                    {
                        CC.myServer.ID = id;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("打开文件失败，文件名： " + fileName + "\n\n原因： " + ex.Message);
            }
        }
        public void DeleteSelectedObjects()
        {
            int count = _graphics.Count;
            string str = "";
            for (int i = 0; i < count; i++)
            {
                if (_graphics[i].Selected)
                {
                    str += string.Format("@{0}", _graphics[i].ID);
                }
            }
            if (str.Length == 0)
            {
                return;
            }
            if (CC.userState == UserState.SingleUser)
            {
                _graphics.DeleteSelection();
                this.Refresh();
            }
            else
            {
                 CC.me.SendToServer(string.Format("DeleteObjects,{0}", str.Remove(0, 1)));
            }
        }
    }
}
