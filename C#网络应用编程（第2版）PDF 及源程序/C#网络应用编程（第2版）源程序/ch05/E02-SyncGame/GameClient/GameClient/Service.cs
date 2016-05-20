//------Service.cs----------//
using System.Windows.Forms;
using System.IO;
namespace GameClient
{
class Service
{
    ListBox listbox;
    StreamWriter sw;
    public Service(ListBox listbox, StreamWriter sw)
    {
        this.listbox = listbox;
        this.sw = sw;
    }
    /// <summary>���������������</summary>
    public void SendToServer(string str)
    {
        try
        {
            sw.WriteLine(str);
            sw.Flush();
        }
        catch
        {
            AddItemToListBox("��������ʧ��");
        }
    }
    delegate void ListBoxDelegate(string str);
    /// <summary>��listbox��׷����Ϣ<</summary>
    /// <param name="str">Ҫ׷�ӵ���Ϣ</param>
    public void AddItemToListBox(string str)
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