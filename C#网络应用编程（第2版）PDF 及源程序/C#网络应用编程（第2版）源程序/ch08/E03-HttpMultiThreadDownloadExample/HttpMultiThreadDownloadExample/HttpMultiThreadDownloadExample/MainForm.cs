using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Threading;
using System.IO;
using System.Diagnostics;
using System.IO.Compression;

namespace HttpMultiThreadDownloadExample
{
    //---------------MainForm.cs---------------------
    public partial class MainForm : Form
    {
        /// <summary>
        /// 同时接收线程数
        /// </summary>
        public const int threadNumber = 5;

        public MainForm()
        {
            InitializeComponent();
            textBox1.Text = "http://localhost:2749/download/歌曲.wmv";
            textBox2.Text = "歌曲.wmv";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            HttpDownloadFile(textBox1.Text, textBox2.Text);
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="sourceUri">源文件的Uri</param>
        /// <param name="targetFileName">保存的目标文件</param>
        private void HttpDownloadFile(string sourceUri, string targetFileName)
        {
            if (IsWebResourceAvailable(sourceUri) == false)
            {
                MessageBox.Show("指定的资源无效！");
                return;
            }
            listBox1.Items.Add("同时接收线程数:" + threadNumber);
            HttpWebRequest request;
            long fileSize = 0;
            try
            {
                request = (HttpWebRequest)HttpWebRequest.Create(sourceUri);
                request.Method = WebRequestMethods.Http.Head;
                //取得目标文件的长度
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                fileSize = response.ContentLength;
                listBox1.Items.Add("文件大小: " + Math.Ceiling(fileSize / 1024.0f) + "KB");
                response.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            //平均分配每个线程应该接收文件的大小
            int downloadFileSize = (int)(fileSize / threadNumber);
            HttpDownload[] d = new HttpDownload[threadNumber];
            //初始化线程参数
            for (int i = 0; i < threadNumber; i++)
            {
                d[i] = new HttpDownload(listBox1, i);
                //每个线程接收文件的起始点
                d[i].StartPosition = downloadFileSize * i;
                if (i < threadNumber - 1)
                {
                    //每个线程接收文件的长度
                    d[i].FileSize = downloadFileSize;
                }
                else
                {
                    d[i].FileSize = (int)(fileSize - downloadFileSize * (i - 1));
                }
                d[i].IsFinish = false;
                d[i].TargetFileName = Path.GetFileNameWithoutExtension(targetFileName) + ".$$" + i;
                d[i].SourceUri = textBox1.Text;
            }
            //定义线程数组，启动接收线程
            Thread[] threads = new Thread[threadNumber];
            for (int i = 0; i < threadNumber; i++)
            {
                threads[i] = new Thread(d[i].Receive);
                threads[i].Start();
            }
            //合并各线程接收的文件
            CombineFiles c = new CombineFiles(listBox1, d, textBox2.Text);
            Thread t = new Thread(c.Combine);
            t.Start();
        }

        public static bool IsWebResourceAvailable(string uri)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
                request.Method = WebRequestMethods.Http.Head;
                request.Timeout = 2000;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                return (response.StatusCode == HttpStatusCode.OK);
            }
            catch (WebException ex)
            {
                System.Diagnostics.Trace.Write(ex.Message);
                return false;
            }
        }
    }
}
