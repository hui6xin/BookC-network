using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsForms2._2._1
{
    using  System.Diagnostics;
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AutoResizeColumns();
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.MultiSelect = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GetAllProcess();
        }
        Process[] myProcess;
        private void GetAllProcess()
        {
            dataGridView1.Rows.Clear();
            myProcess = Process.GetProcesses();
            foreach (Process p in myProcess)
            {
                int newRowIndex = dataGridView1.Rows.Add();
                DataGridViewRow row = dataGridView1.Rows[newRowIndex];
                row.Cells[0].Value = p.Id;
                row.Cells[1].Value = p.ProcessName;
                row.Cells[2].Value = string.Format("{0:###,##0.00}MB", 
                    p.PeakWorkingSet64 / 1024.0f / 1024.0f);
                //有些进程无法获取启动时间和文件名信息，所以要用try
                try
                {
                    row.Cells[3].Value = string.Format("{0}",p.StartTime);
                    row.Cells[4].Value = p.MainModule.FileName;
                }
                catch
                {
                    row.Cells[3].Value = "";
                    row.Cells[4].Value = "";
                }
            }
        }
        private void ShowProcessInfo(Process p)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("进程名称：" + p.ProcessName + ", ID:" + p.Id);
            try
            {
                sb.AppendLine("进程优先级：" + p.BasePriority + "(优先级类别：" + p.PriorityClass + ")");
                ProcessModule m = p.MainModule;
                sb.AppendLine("文件名:" + m.FileName);
                sb.AppendLine("版本:" + m.FileVersionInfo.FileVersion);
                sb.AppendLine("描述" + m.FileVersionInfo.FileDescription);
                sb.AppendLine("语言" + m.FileVersionInfo.Language);
                sb.AppendLine("_________________________________");
                if (p.Modules != null)
                {
                    ProcessModuleCollection pmc = p.Modules;
                    sb.AppendLine("调用的模块（.dll):");
                    for (int i = 1; i < pmc.Count; i++)
                    {
                        sb.AppendLine(
                            "模块名:" + pmc[i].ModuleName + "\t" +
                            "版本:" + pmc[i].FileVersionInfo.FileVersion + "\t" +
                            "描述:" + pmc[i].FileVersionInfo.FileDescription);
                    }
                }
            }
            catch
            {
                sb.AppendLine("其他信息：无法获取");
            }
            richTextBox1.Text = sb.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GetAllProcess();
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            DataGridView.HitTestInfo h = dataGridView1.HitTest(e.X, e.Y);
            if (h.Type == DataGridViewHitTestType.Cell || h.Type == DataGridViewHitTestType.RowHeader)
            {
                dataGridView1.Rows[h.RowIndex].Selected = true;
                int processid = (int)dataGridView1.CurrentRow.Cells[0].Value;
                ShowProcessInfo(Process.GetProcessById(processid));
            }
        }
    }
}
