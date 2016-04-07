using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace WindowsForms5
{
    class Service
    {
        private ListBox listbox;
        private delegate void AllItemDelegate(string str);
        private AllItemDelegate addtemdelegate;
        public Service(ListBox listbox)
        {
            this.listbox = listbox;
            addtemdelegate = new AllItemDelegate(AddItem);
        }

        public void AddItem(string str)
        {
            if (listbox.InvokeRequired)
            {
                listbox.Invoke(addtemdelegate, str);
            }
            else
            {
                listbox.Items.Add(str);
                listbox.SelectedIndex = listbox.Items.Count - 1;
                listbox.ClearSelected();
            }
        }
        public void SendToOne(GoUser user, string str)
        {
            try
            {
                user.sw.WriteLine(str);
                user.sw.Flush();
                AddItem(string.Format("sendto{0} sth{1} ",user.userName,str));
            }
            catch
            {
                AddItem(string.Format("fail send to{0}",user.userName));
            }
        }
        public void SendToBoth(GameTable gameTable, string str)
        {
            for (int i = 0; i < 2; i++)
            {
                if (gameTable.gamePlayer[i].someone == true)
                {
                    SendToOne(gameTable.gamePlayer[i].user, str);
                }
            }
        }
        public void SendToAll(System.Collections.Generic.List<GoUser> userlist, string str)
        {
            for (int i = 0; i < userlist.Count; i++)
            {
                SendToOne(userlist[i], str);
            }

        }
    }
}
