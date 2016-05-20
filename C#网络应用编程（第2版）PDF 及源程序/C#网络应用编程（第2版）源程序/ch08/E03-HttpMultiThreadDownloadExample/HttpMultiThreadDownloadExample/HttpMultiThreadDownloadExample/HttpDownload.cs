using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Windows.Forms;
using System.Diagnostics;

namespace HttpMultiThreadDownloadExample
{
    //----------HttpDownload.cs-------------------//
    public class HttpDownload
    {
        /// <summary>
        /// 接收线程是否已经完成
        /// </summary>
        public bool IsFinish { get; set; }
        /// <summary>
        /// 线程接收文件的临时文件名
        /// </summary>
        public string TargetFileName { get; set; }
        /// <summary>
        /// 线程接收文件的起始位置
        /// </summary>
        public int StartPosition { get; set; }
        /// <summary>
        /// 线程接收文件的大小
        /// </summary>
        public int FileSize { get; set; }
        /// <summary>
        /// 接收文件的Uri
        /// </summary>
        public string SourceUri { get; set; }

        private int threadIndex;
        private ListBox listbox;
        private Stopwatch stopWatch = new Stopwatch();
        public HttpDownload(ListBox listbox, int threadIndex)
        {
            this.listbox = listbox;
            this.threadIndex = threadIndex;
        }

        /// <summary>接收线程</summary>
        public void Receive()
        {
            stopWatch.Reset();
            stopWatch.Start();
            AddStatus("线程" + threadIndex + "开始接收");
            int totalBytes = 0;
            using (FileStream fs = new FileStream(TargetFileName, System.IO.FileMode.Create))
            {
                try
                {
                    HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(SourceUri);
                    //接收的范围（起始位置，终止位置） 
                    request.AddRange(StartPosition, StartPosition + FileSize - 1);
                    //获得接收流
                    Stream stream = request.GetResponse().GetResponseStream();
                    byte[] receiveBytes = new byte[512];
                    int readBytes = stream.Read(receiveBytes, 0, receiveBytes.Length);
                    while (readBytes > 0)
                    {
                        fs.Write(receiveBytes, 0, readBytes);
                        totalBytes += readBytes;
                        readBytes = stream.Read(receiveBytes, 0, receiveBytes.Length);
                    }
                    stream.Close();
                }
                catch (Exception ex)
                {
                    AddStatus("线程" + threadIndex + "接收出错：" + ex.Message);
                }
            }
            ChangeStatus("线程" + threadIndex + "开始接收", "接收完毕!", totalBytes);
            stopWatch.Stop();
            this.IsFinish = true;
        }

        public delegate void AddStatusDelegate(string message);
        public void AddStatus(string message)
        {
            if (listbox.InvokeRequired)
            {
                AddStatusDelegate d = AddStatus;
                listbox.Invoke(d, message);
            }
            else
            {
                listbox.Items.Add(message);
            }
        }

        public delegate void ChangeStatusDelegate(string oldMessage, string newMessage, int number);
        public void ChangeStatus(string oldMessage, string newMessage, int number)
        {
            if (listbox.InvokeRequired)
            {
                ChangeStatusDelegate d = ChangeStatus;
                listbox.Invoke(d, oldMessage, newMessage, number);
            }
            else
            {
                int i = listbox.FindString(oldMessage);
                if (i != -1)
                {
                    string[] items = new string[listbox.Items.Count];
                    listbox.Items.CopyTo(items, 0);
                    items[i] = oldMessage + "  " + newMessage
                        + "  接收字节数：" + Math.Ceiling(number / 1024.0f) + "KB"
                        + "，用时：" + stopWatch.ElapsedMilliseconds / 1000.0f + " 秒";
                    listbox.Items.Clear();
                    listbox.Items.AddRange(items);
                    listbox.SelectedIndex = i;
                }
            }
        }
    }
}
