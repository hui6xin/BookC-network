using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Threading;
namespace lockExample
{
    class Account
    {
        private Object lockedObj = new Object();
        int balance;
        Random r = new Random();
        Form1 form1;
        public Account(int initial, Form1 form1)
        {
            this.form1 = form1;
            this.balance = initial;
        }
        /// <summary>
        /// 取款
        /// </summary>
        /// <param name="amount">取款金额</param>
        /// <returns>余额</returns>
        private int Withdraw(int amount)
        {
            if (balance < 0)
            {
                form1.AddListBoxItem("余额：" + balance + " 已经为负值了，还想取呵！");
            }

            //将lock (lockedObj)这句注释掉，看看会发生什么情况
            lock (lockedObj)
            {
                if (balance >= amount)
                {
                    string str = Thread.CurrentThread.Name + "取款---";
                    str+= string.Format("取款前余额：{0,-6}取款：{1,-6}", balance, amount);
                    balance = balance - amount;
                    str += "取款后余额：" + balance;
                    form1.AddListBoxItem(str);
                    return amount;
                }
                else
                {
                    return 0;
                }
            }
        }
        /// <summary>自动取款</summary>
        public void DoTransactions()
        {
            for (int i = 0; i < 100; i++)
            {
                Withdraw(r.Next(1, 100));
            }
        }
    }
}
