using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsForms2._3._3
{
    using System.Threading;
    class Class1
    {
        public volatile bool shouldstop;
        private Form1 form1;
        public Class1(Form1 form1)
        {
            this.form1 = form1;
        }
        public void Method1(object obj)
        {
            string s = obj as string;
            form1.AddMessage(s);
            while (shouldstop == false)
            {
                Thread.Sleep(100);
                form1.AddMessage("a");
            }
            form1.AddMessage("\n 线程Method1已终止");
        }
        public void Method2()
        {
            while (shouldstop == false )
            {
                Thread.Sleep(100);
                form1.AddMessage("b");
            }
            form1.AddMessage("\n 线程Method2已终止");
        }
    }
}
