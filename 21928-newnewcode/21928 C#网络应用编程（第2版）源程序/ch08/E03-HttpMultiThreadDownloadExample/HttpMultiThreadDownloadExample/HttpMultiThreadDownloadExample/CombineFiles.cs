using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using System.Windows.Forms;
using System.Net;

namespace HttpMultiThreadDownloadExample
{
    //------------CombineFiles.cs-------------------//
    public class CombineFiles
    {
        //所有线程是否全部下载完毕
        private bool downloadFinish;
        private HttpDownload[] down;
        private ListBox listbox;
        string targetFileName;
        public CombineFiles(ListBox listbox, HttpDownload[] down, string targetFileName)
        {
            this.listbox = listbox;
            this.down = down;
            this.targetFileName = targetFileName;
        }

        public void Combine()
        {
            while (true)
            {
                downloadFinish = true;
                for (int i = 0; i < down.Length; i++)
                {
                    //有未结束线程，等待
                    if (down[i].IsFinish == false)
                    {
                        downloadFinish = false;
                        Thread.Sleep(100);
                        break;
                    }
                }
                //所有线程均已结束，停止等待
                if (downloadFinish == true)
                {
                    break;
                }
            }
            AddStatus("下载完毕，开始合并临时文件!");
            FileStream targetFileStream;
            FileStream sourceFileStream;
            int readfile;
            byte[] bytes = new byte[8192];
            targetFileStream = new FileStream(targetFileName, FileMode.Create);
            for (int k = 0; k < down.Length; k++)
            {
                sourceFileStream = new FileStream(down[k].TargetFileName, FileMode.Open);
                while (true)
                {
                    readfile = sourceFileStream.Read(bytes, 0, bytes.Length);
                    if (readfile > 0)
                    {
                        targetFileStream.Write(bytes, 0, readfile);
                    }
                    else
                    {
                        break;
                    }
                }
                sourceFileStream.Close();
            }
            targetFileStream.Close();
            //删除临时文件
            for (int i = 0; i < down.Length; i++)
            {
                File.Delete(down[i].TargetFileName);
            }
            DateTime dt = DateTime.Now;
            AddStatus("合并完毕！");
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
                listbox.SelectedIndex = -1;
            }
        }
    }
}
