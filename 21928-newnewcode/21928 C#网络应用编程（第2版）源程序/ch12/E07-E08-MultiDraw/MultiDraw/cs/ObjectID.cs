using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace MultiDraw
{
    //�ṩͼ��ͼ��ID�ķ���
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

        /// <summary>��ȡ��ID������������ã���ע��˷������ܷ��ھ�̬ʵ����</summary>
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
                        if (Environment.TickCount - n < 10000)  //���ȴ�10��
                        {
                            System.Threading.Thread.Sleep(20);
                        }
                        else
                        {
                            MessageBox.Show("�����������ID��ʱ");
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
