//---------------Service.cs-------------------//
using System.Windows.Forms;
namespace GameServer
{
    class Service
    {
        private ListBox listbox;
        private delegate void AddItemDelegate(string str);
        private AddItemDelegate addItemDelegate;
        public Service(ListBox listbox)
        {
            this.listbox = listbox;
            addItemDelegate = new AddItemDelegate(AddItem);
        }
        /// <summary>��listbox��׷����Ϣ<</summary>
        /// <param name="str">Ҫ׷�ӵ���Ϣ</param>
        public void AddItem(string str)
        {
            if (listbox.InvokeRequired)
            {
                listbox.Invoke(addItemDelegate, str);
            }
            else
            {
                listbox.Items.Add(str);
                listbox.SelectedIndex = listbox.Items.Count - 1;
                listbox.ClearSelected();
            }
        }
        /// <summary>��ĳ���ͻ��˷�����Ϣ</summary>
        /// <param name="gameTable">ָ���ͻ�</param>
        /// <param name="gameTable">��Ϣ</param>
        public void SendToOne(User user, string str)
        {
            try
            {
                user.sw.WriteLine(str);
                user.sw.Flush();
                AddItem(string.Format("��{0}����{1}", user.userName, str));
            }
            catch
            {
                AddItem(string.Format("��{0}������Ϣʧ��", user.userName));
            }
        }
        /// <summary>��ͬһ���������ͻ��˷�����Ϣ</summary>
        /// <param name="gameTable">ָ����Ϸ��</param>
        /// <param name="str">��Ϣ</param>
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
        /// <summary>�����пͻ��˷�����Ϣ</summary>
        /// <param name="gameTable">�ͻ��б�</param>
        /// <param name="gameTable">��Ϣ</param>
        public void SendToAll(System.Collections.Generic.List<User> userList, string str)
        {
            for (int i = 0; i < userList.Count; i++)
            {
                SendToOne(userList[i], str);
            }
        }
    }
}
