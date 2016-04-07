using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace WindowsForms2._4._3
{
    class Account
    {
        private object lockObj = new object();
        int balance;
        Random r = new Random();
        Form1 form1;
        public Account(int initial,Form1 form1)
        {
            this.form1 = form1;
            this.balance = initial;
        }
        private int Withdraw(int amount)
        {
            if (balance < 0)
            {
                form1.AddListBoxItem("无");
            }
            lock (lockObj)
            {
                if (balance >= amount)
                {
                    string str = Thread.CurrentThread.Name + "get---";
                    str += string.Format("last:{0,-6} get:{1,-6}", balance, amount);
                    balance = balance - amount;
                    form1.AddListBoxItem(str);
                    return amount;
                }
                else
                {
                    return 0;
                }
            }
        }

        public void DoTransactions()
        {
            for (int i = 0; i < 100; i++)
            {
                Withdraw(r.Next(1, 100));
            }
        }
    }
}
