using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MultiDraw
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            PreMainForm fm = new PreMainForm();
            fm.ShowDialog();
            if (CC.isNeedRunMainForm == true)
            {
                fm.Dispose();
                Application.Run(new MainForm());
            }
        }
    }
}