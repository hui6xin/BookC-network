using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
//引用命名空间
using System.Diagnostics;
using System.Threading;


namespace StartStopProcess
{
    public partial class Form1 : Form
    {
        int fileIndex;
        string fileName = "Notepad.exe";
        Process process1 = new Process();
        public Form1()
        {
            InitializeComponent();
            //以详细列表方式显示
            listView1.View = View.Details;
            //参数含义：列名称，宽度（像素），水平对齐方式
            listView1.Columns.Add("进程ID", 70, HorizontalAlignment.Left);
            listView1.Columns.Add("进程名称", 70, HorizontalAlignment.Left);
            listView1.Columns.Add("占用内存", 70, HorizontalAlignment.Left);
            listView1.Columns.Add("启动时间", 70, HorizontalAlignment.Left);
            listView1.Columns.Add("文件名", 280, HorizontalAlignment.Left);
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            string argument = Application.StartupPath + "\\myfile" + fileIndex + ".txt";
            if (File.Exists(argument)==false)
            {
                File.CreateText(argument);
            }
            //设置要启动的应用程序名称及参数
            ProcessStartInfo ps = new ProcessStartInfo(fileName, argument);
            ps.WindowStyle = ProcessWindowStyle.Normal;
            fileIndex++;
            Process p = new Process();
            p.StartInfo = ps;
            p.Start();
            //等待启动完成，否则获取进程信息可能会失败
            p.WaitForInputIdle();
            RefreshListView();
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            //创建新的Process组件的数组,并将它们与指定的进程名称（Notepad）的所有进程资源相关联.
            Process[] myprocesses;
            myprocesses = Process.GetProcessesByName(Path.GetFileNameWithoutExtension(fileName));
            foreach (Process p in myprocesses)
            {
                //通过向进程主窗口发送关闭消息达到关闭进程的目的
                p.CloseMainWindow();
                //等待1000毫秒
                Thread.Sleep(1000);
                //释放与此组件关联的所有资源
                p.Close();
            }
            fileIndex = 0;
            RefreshListView();
            this.Cursor = Cursors.Default;
        }

        private void RefreshListView()
        {
            listView1.Items.Clear();
            //创建Process类型的数组,并将它们与系统内所有进程相关联
            Process[] processes;
            processes = Process.GetProcessesByName(Path.GetFileNameWithoutExtension(fileName));
            foreach (Process p in processes)
            {
                //将每个进程的进程名称、占用的物理内存以及进程开始时间加入listView中
                ListViewItem item = new ListViewItem(
                    new string[]{
                        p.Id.ToString(),
                        p.ProcessName,
                        string.Format("{0} KB", p.PrivateMemorySize64/1024.0f),
                        string.Format("{0}",p.StartTime),
                        p.MainModule.FileName
                    });
                listView1.Items.Add(item);
            }
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            RefreshListView();
        }
    }
}