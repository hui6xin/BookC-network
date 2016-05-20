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
    /// <summary>向服务器发送数据</summary>
    public void SendToServer(string str)
    {
        try
        {
            sw.WriteLine(str);
            sw.Flush();
        }
        catch
        {
            AddItemToListBox("发送数据失败");
        }
    }
    delegate void ListBoxDelegate(string str);
    /// <summary>在listbox中追加信息<</summary>
    /// <param name="str">要追加的信息</param>
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