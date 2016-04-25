using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace SChangetoGray
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Multiselect = false;
            openFileDialog1.Title = "请选择txt文件";
            openFileDialog1.Filter = "所有文件(*.*)|*.*";

            string sfilename = "";
            if (DialogResult.OK == openFileDialog1.ShowDialog())
            {
                sfilename = openFileDialog1.FileName;
                textBox1.Text = sfilename;
            }
            if (!File.Exists(sfilename))
                return;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox4.SizeMode = PictureBoxSizeMode.StretchImage;

            pictureBox1.Image = Image.FromFile(sfilename); 
           
            Bitmap bm = new Bitmap(sfilename);
            //Bitmap bm = new Bitmap(pictureBox1.Image, new Size(100, 100));

            Bitmap bm1 = toback(bm, 0);
            pictureBox2.Image = bm1;
            Bitmap bm2 = togray(bm, 1);
            pictureBox3.Image = bm2;
            Bitmap bm3 = togray(bm, 2);
            pictureBox4.Image = bm3;
        }

        private Bitmap togray(Bitmap bitmaptemp,int imode)
        {
            Bitmap bitmapreturn = new Bitmap(bitmaptemp.Width,bitmaptemp.Height);
            for (int iwidth = 0; iwidth < bitmaptemp.Width; iwidth++)
            {
                for (int iheight = 0; iheight < bitmaptemp.Height; iheight++)
                {
                    Color cl=bitmaptemp.GetPixel(iwidth, iheight);
                    int t = 0;
                    switch(imode)
                    {
                        case 0:
                            t = (cl.R + cl.G + cl.B) / 3;
                            break;
                        case 1:
                            double itotal = cl.R + cl.G + cl.B;
                            double Rper = itotal != 0 ? (double)cl.R / itotal : 0;
                            double Gper = itotal != 0 ? (double)cl.G / itotal : 0;
                            double Bper = itotal != 0 ? (double)cl.B / itotal : 0;
                            t = (int)(cl.R * Rper) + (int)(cl.G * Gper) + (int)(cl.B * Bper);
                        break;
                        case 2:
                            t =(int)((float)cl.R * 0.114f + (float)cl.G * 0.587f + (float)cl.B * 0.299f);
                            break;
                        default:
                        break;
                    }
                    bitmapreturn.SetPixel(iwidth, iheight, Color.FromArgb(t, t, t));
                }
            }
            return bitmapreturn;
        }
        private Bitmap toback(Bitmap bitmaptemp, int imode)
        {
            Bitmap bitmapreturn = new Bitmap(bitmaptemp.Width, bitmaptemp.Height);
            for (int iwidth = 0; iwidth < bitmaptemp.Width; iwidth++)
            {
                for (int iheight = 0; iheight < bitmaptemp.Height; iheight++)
                {
                    Color cl = bitmaptemp.GetPixel(iwidth, iheight);
                    int ta = 0;
                    int tr = 0;
                    int tg = 0;
                    int tb = 0;
                    switch (imode)
                    {
                        case 0:
                            ta = cl.A;
                            tr = 255-cl.R;
                            tg = 255 - cl.G;
                            tb = 255 - cl.G;
                            break;
                        default:
                            break;
                    }
                    bitmapreturn.SetPixel(iwidth, iheight, Color.FromArgb(ta,tr, tg, tb));
                }
            }
            return bitmapreturn;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string foldername = "";
            if (DialogResult.OK == folderBrowserDialog1.ShowDialog())
            {
                foldername = folderBrowserDialog1.SelectedPath;
                textBox2.Text = foldername;
            }
            if ( Directory.Exists(foldername) )
            {
                    listBox1.Items.Clear();
                    string[] s=Directory.GetFiles(foldername);
                            //Directory.Get
                    foreach (string sname in s)
                    {
                        AddListBoxItem(sname);
                    }
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            //saveFileDialog1.ShowDialog();
        }
        private delegate void  AddListBoxItemDelegate(string str);
        public void AddListBoxItem(string p)
        {
            //if(File.Exists(p))
            //{
            //    FileInfo fi = new FileInfo(p);
            //    p = fi.Name;

            //    //FileInfo.Exists：获取指定文件是否存在；
            //    //FileInfo.Name，FileInfo.Extensioin：获取文件的名称和扩展名；
            //    //FileInfo.FullName：获取文件的全限定名称（完整路径）；
            //    //FileInfo.Directory：获取文件所在目录，返回类型为DirectoryInfo；
            //    //FileInfo.DirectoryName：获取文件所在目录的路径（完整路径）；
            //    //FileInfo.Length：获取文件的大小（字节数）；
            //    //FileInfo.IsReadOnly：获取文件是否只读；
            //    //FileInfo.Attributes：获取或设置指定文件的属性，返回类型为FileAttributes枚举，可以是多个值的组合
            //    //FileInfo.CreationTime、FileInfo.LastAccessTime、FileInfo.LastWriteTime：分别用于获取文件的创建时间、访问时间、修改时间；
            //}
            if (listBox1.InvokeRequired)
            {
                AddListBoxItemDelegate d = AddListBoxItem;
                listBox1.Invoke(d, p);
            }
            else
            {
                listBox1.Items.Add(p);
            }
        }

        
    }
}
