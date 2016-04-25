using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace WindowsForms5
{
    class ClientServer
    {
        ListBox listbox;
        StreamWriter sw;
        public ClientServer(ListBox lisbox, StreamWriter sw)
        {
            this.listbox = lisbox;
            this.sw = sw;
        }
        public void SendToServer(string str)
        {
            try
            {
                sw.Write(str);
                sw.Flush();
            }
            catch
            {
                AddItemToListbox("send failed");
            }
        }
    }
}
