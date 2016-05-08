using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace AsyncSendMailExample
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
            Application.Run(new FormSendAsync());
        }
    }
}
