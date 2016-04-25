using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace mycontrol
{
    using System.IO;
    public partial class UserControl1 : UserControl
    {
        public UserControl1()
        {
            InitializeComponent();
        }

        private void btmOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofdPic = new OpenFileDialog();
            ofdPic.Filter = "JPG(*.JPG;*.JPEG);gif文件(*.GIF)|*.jpg;*.jpeg;*.gif";
            ofdPic.FilterIndex = 1;
            ofdPic.RestoreDirectory = true;
            ofdPic.FileName = "";
            if (ofdPic.ShowDialog() == DialogResult.OK)
            {
                string sPicPaht = ofdPic.FileName.ToString();
                FileInfo fiPicInfo = new FileInfo(sPicPaht);
                long lPicLong = fiPicInfo.Length / 1024;
                string sPicName = fiPicInfo.Name;
                string sPicDirectory = fiPicInfo.Directory.ToString();
                string sPicDirectoryPath = fiPicInfo.DirectoryName;
                Bitmap bmPic = new Bitmap(sPicPaht);

                lblName.Text = sPicName;
                lblLength.Text = lPicLong.ToString() + " KB";
                lblSize.Text = bmPic.Size.Width.ToString() + "×" + bmPic.Size.Height.ToString();

                lblwidth.Text=bmPic.Width.ToString();
                lblheight.Text=bmPic.Height.ToString();
                lbldata.Text = fiPicInfo.CreationTime.ToString();

                picBox.SizeMode = PictureBoxSizeMode.StretchImage;
                if (lPicLong > 400)
                {
                    MessageBox.Show("此文件大小為" + lPicLong + "K；已超過最大限制的K范圍！");
                }
                else
                {
                    Point ptLoction = new Point(bmPic.Size);
                    if (ptLoction.X > picBox.Size.Width || ptLoction.Y > picBox.Size.Height)
                    {
                        picBox.SizeMode = PictureBoxSizeMode.Zoom;
                    }
                    else
                    {
                        picBox.SizeMode = PictureBoxSizeMode.CenterImage;
                    }
                }
                picBox.LoadAsync(sPicPaht);
                
            }

        }
    }
}
