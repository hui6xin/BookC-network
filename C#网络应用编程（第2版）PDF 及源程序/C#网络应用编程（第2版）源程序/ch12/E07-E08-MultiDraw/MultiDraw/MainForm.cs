using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using System.IO;
using System.Drawing.Printing;

namespace MultiDraw
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            this.SuspendLayout();
            CC.palette = new Palette();
            CC.palette.Dock = DockStyle.Fill;
            this.Controls.Add(CC.palette);
            this.ResumeLayout();
            SetUserState();

            CC.myService.mainForm = this;
            if (CC.userState == UserState.Server || CC.userState== UserState.SingleUser)
            {
                CC.palette.DeserializeObject(CC.backupFileName);
            }
            else
            {
                CC.me.SendToServer("Login");
            }
        }

        public void SetUserState()
        {
            if (CC.userState == UserState.SingleUser)
            {
                this.toolStripLabelUserState.Text = "制作方式：单机制作";
            }
            else if (CC.userState == UserState.Server)
            {
                this.toolStripLabelUserState.Text = string.Format("制作方式：多机联合制作，本机为主机（{0}），与本机连接用户数：{1}", CC.me.LocalIPString, CC.myServer.Users.Count - 1);
            }
            else if (CC.userState == UserState.Client)
            {
                this.toolStripLabelUserState.Text = string.Format("制作方式：多机联合制作，本机为附机（{0}）， 主机：{1}", CC.me.LocalIPString, CC.me.client.Client.RemoteEndPoint);
            }
        }

        private void buttonRectangle_Click(object sender, EventArgs e)
        {
            CC.palette.ActiveTool = Palette.ToolType.Rectangle;
        }

        private void buttonText_Click(object sender, EventArgs e)
        {
            TextDialog td = new TextDialog();
            if (td.ShowDialog() == DialogResult.OK)
            {
                CC.textInfo.text = td.MyText;
                CC.textInfo.color = td.MyColor;
                CC.palette.ActiveTool = Palette.ToolType.Text;
            }
        }

        private void buttonCurve_Click(object sender, EventArgs e)
        {
            CC.palette.ActiveTool = Palette.ToolType.Curve;
        }

        private void buttonGraphics_Click(object sender, EventArgs e)
        {
            OpenFileDialog f = new OpenFileDialog();
            f.Multiselect = false;
            f.CheckPathExists = true;
            f.Title = "添加图像";
            f.Filter = "JPEG (*.jpg)|*.jpg|Bitmap (*.bmp)|*.bmp|PNG (*.png)|*.png|GIF (*.gif)|*.gif|All files|*.*";
            if (f.ShowDialog() == DialogResult.OK)
            {
                CC.bitmap = (Bitmap)Bitmap.FromFile(f.FileName,true);
                CC.palette.ActiveTool = Palette.ToolType.Image;
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //保存本次制作信息
            CC.palette.SerializeObject(CC.palette.graphics, CC.backupFileName);
            if (CC.userState != UserState.SingleUser)
            {
                if (CC.userState == UserState.Server)
                {

                    if (MessageBox.Show("本机为主机，一旦退出制作，所有附机均无法继续制作，确实要退出吗？", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        Debug.Print("停止服务，并使用户退出!");
                        CC.myServer.SendToAllUser("ServerExit");
                        Thread.Sleep(1000);
                        CC.myServer.myListener.Stop();
                        Debug.Print("主机退出制作!");
                    }
                    else
                    {
                        e.Cancel = true;
                    }
                }
                else
                {
                    CC.me.SendToServer("Logout");
                    Thread.Sleep(1000);
                    CC.me.normalExit = true;
                    Debug.Print("附机退出制作!");
                }
            }

        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CC.palette.DeleteSelectedObjects();
        }

        private void 全部选择ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CC.palette.graphics.SelectAll();
        }

        private void 导出图片ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap myBitmap = DrawImage();
            string fileName = Application.StartupPath + "\\myDraw.jpg";
            myBitmap.Save(fileName,System.Drawing.Imaging.ImageFormat.Jpeg);
            MessageBox.Show("\n导出成功\n\n导出位置： " + fileName);
        }

        private static Bitmap DrawImage()
        {
            Bitmap myBitmap = new Bitmap(CC.palette.Width, CC.palette.Height);
            Graphics g = Graphics.FromImage(myBitmap);
            g.Clear(Color.White);
            CC.palette.graphics.Draw(g);
            Pen p = new Pen(Color.Black, 1f);
            Rectangle myRectangle = new Rectangle(1, 1, CC.palette.Width - 2, CC.palette.Height - 2);
            g.DrawRectangle(p, myRectangle);
            p.Dispose();
            return myBitmap;
        }


        private MemoryStream streamToPrint;
        private void 打印预览ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintDocument printDocument1 = new PrintDocument();
            printDocument1.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);
            Bitmap myBitmap = DrawImage();
            streamToPrint = new MemoryStream();
            myBitmap.Save(streamToPrint, System.Drawing.Imaging.ImageFormat.Jpeg);
            PrintPreviewDialog p = new PrintPreviewDialog();
            p.UseAntiAlias = true;
            p.Document = printDocument1;
            p.PrintPreviewControl.Zoom = 0.5f;
            p.Height = CC.palette.Height;
            p.ShowDialog();

        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            System.Drawing.Image image = System.Drawing.Image.FromStream(this.streamToPrint);
            image.RotateFlip(RotateFlipType.Rotate270FlipNone);
            int x = e.MarginBounds.X;
            int y = e.MarginBounds.Y;
            int width = image.Width;
            int height = image.Height;
            if ((width / e.MarginBounds.Width) > (height / e.MarginBounds.Height))
            {
                width = e.MarginBounds.Width;
                height = image.Height * e.MarginBounds.Width / image.Width;
            }
            else
            {
                height = e.MarginBounds.Height;
                width = image.Width * e.MarginBounds.Height / image.Height;
            }
            System.Drawing.Rectangle destRect = new System.Drawing.Rectangle(x, y, width, height);
            e.Graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, System.Drawing.GraphicsUnit.Pixel);
        }
    }
}