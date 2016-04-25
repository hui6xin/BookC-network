using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace WindowsForms5
{
    class ClientService
    {
        ListBox listbox;
        StreamWriter sw;
        public ClientService(ListBox listbox, StreamWriter sw)
        {
            this.listbox = listbox;
            this.sw = sw;
        }
        public void SendToServer(string str)
        {
            try
            {
                sw.WriteLine(str);
                sw.Flush();
            }
            catch
            {
                AddItemToListBox("发送数据失败！");
            }
        }
        delegate void ListBoxDelegate(string str);
        public void AddItemToListBox(string str )
        {
            if (listbox.InvokeRequired)
            {
                ListBoxDelegate d = AddItemToListBox;
                listbox.Invoke(d, str);
            }
            else
            {
                listbox.Items.Add(str);
                listbox.SelectedIndex = listbox.Items.Count - 1;
                listbox.ClearSelected();
            }
        }
    }
}
