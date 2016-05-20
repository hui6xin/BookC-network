using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.Net;
using System.Threading;

namespace BackgroundWorkerExample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.WorkerSupportsCancellation = true;
            buttonStop.Enabled = false;
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "开始产生10000以内的随机数……\n\n";
            buttonStart.Enabled = false;
            buttonStop.Enabled = true;
            //在后台开始操作
            backgroundWorker1.RunWorkerAsync();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            //不要直接使用组件实例名称（backgroundWorker1），因为有多
            //个BackgroundWorker时，直接使用会产生耦合问题
            //应该通过下面的转换使用它
            BackgroundWorker worker = sender as BackgroundWorker;
            //下面的内容相当于线程要处理的内容。注意：不要在此事件中和控件打交道
            Random r = new Random();
            int numCount = 0;
            while (worker.CancellationPending == false)
            {
                int num = r.Next(10000);
                if (num % 5 == 0)
                {
                    numCount++;
                    worker.ReportProgress(0, num);
                    Thread.Sleep(1000);
                }
            }
            e.Result = numCount;
        }


        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            int num = (int)e.UserState;
            richTextBox1.Text += num + " ";
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                richTextBox1.Text += "\n\n操作停止，共产生"+e.Result+"个能被5整除的随机数";
            }
            else
            {
                richTextBox1.Text += "\n操作过程中产生错误：" + e.Error;
            }
           
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            backgroundWorker1.CancelAsync();
            buttonStop.Enabled = false;
            buttonStart.Enabled = true;
        }
    }
}