using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace MultiDraw
{
    //提供图形图像ID的方法
    public class ObjectID
    {
        public ObjectID()
        {
        }

        delegate void ShowWaitCursorDelegate(Cursor cursor);
        private void ShowWaitCursor(Cursor cursor)
        {
            if (CC.palette.InvokeRequired == true)
            {
                ShowWaitCursorDelegate d = ShowWaitCursor;
                CC.palette.Invoke(d, cursor);
            }
            else
            {
                CC.palette.Cursor = cursor;
            }
        }

        /// <summary>获取新ID（单机多机公用），注意此方法不能放在静态实例中</summary>
        public void SetNewID()
        {
            Cursor cursor = CC.palette.Cursor;
            ShowWaitCursor(Cursors.WaitCursor);
            if (CC.userState == UserState.SingleUser)
            {
                CC.ID++;
            }
            else
            {
                CC.me.SendToServer("GetID");
                int n = Environment.TickCount;
                while (true)
                {
                    if (CC.me.FinishGetNewID == false)
                    {
                        if (Environment.TickCount - n < 10000)  //最多等待10秒
                        {
                            System.Threading.Thread.Sleep(20);
                        }
                        else
                        {
                            MessageBox.Show("向服务器申请ID超时");
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }
            ShowWaitCursor(cursor);
        }

    }
}
