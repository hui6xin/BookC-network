using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading;

namespace WindowsForms5
{
    public partial class server1 : Form
    {
        private List<User> userlist = new List<User>();
        IPAddress localaddress;
        private const int port = 51888;
        private TcpListener mylistener;
        bool isnormalexit = false;
        public server1()
        {
            InitializeComponent();
            listBox1.HorizontalScrollbar = true;
            IPAddress[] addrip = Dns.GetHostAddresses(Dns.GetHostName());
            localaddress = addrip[0];
            button2.Enabled = false;
        }
        private delegate void AddItemToListBoxdelegate(string strings);
        private void AddItemToListBox(string strings)
        {
            if (listBox1.InvokeRequired)
            {
                AddItemToListBoxdelegate d = AddItemToListBox;
                listBox1.Invoke(d, strings);
            }
            else
            {
                listBox1.Items.Add(strings);
                listBox1.SelectedIndex = listBox1.Items.Count - 1;
                listBox1.ClearSelected();
            }
        }
        private void SendToAllClient(User user, string strings)
        { 
            string command=strings.Split(',')[0].ToLower();
            if(command=="login")
            {
                for(int i=0;i<userlist.Count;i++)
                {
                    Sendtoclient(userlist[i],strings);
                    if (userlist[i].userName != user.userName)
                    {
                        Sendtoclient(user, "login," + userlist[i].userName);
                    }
                }
            }
            else if(command=="logout")
            {
                for(int i=0;i<userlist.Count;i++)
                {
                    if(userlist[i].userName!=user.userName)
                    {
                        Sendtoclient(userlist[i],strings);
                    }
                }
            }
        }
        private void RemoveUser(User user)
        {
            userlist.Remove(user);
            user.close();
            AddItemToListBox(string.Format("users count: {0}", userlist.Count));
        }
        private void Sendtoclient(User user, string strings)
        {
            try
            {
                user.bw.Write(strings);
                user.bw.Flush();
                AddItemToListBox(string.Format("send to [{0}] message:{1}", user.userName, strings));
            }
            catch
            {
                AddItemToListBox(string.Format("send to [{0}] fail", user.userName));
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            mylistener = new TcpListener(localaddress, port); //new TcpListener(IPAddress.Any,0);//
            mylistener.Start();
            AddItemToListBox(string.Format("begein {0} :{1} port listen",localaddress,port));
            Thread mythread=new Thread(listenclientconnect);
            mythread.Start();
            button1.Enabled=false;
            button2.Enabled=true;
        }
        private void listenclientconnect()
        {
            TcpClient newclient = null;
            while (true)
            {
                try
                {
                    newclient = mylistener.AcceptTcpClient();
                }
                catch
                {
                    break;
                }
                User user=new User(newclient);
                Thread threadreceive=new Thread(ReciveData);
                threadreceive.Start(user);
                userlist.Add(user);
                AddItemToListBox(string.Format("[{0}]geiin",newclient.Client.RemoteEndPoint));
                AddItemToListBox(string.Format("users count: {0}",userlist.Count));
            } 
        }
        private void ReciveData(object userstate)
        { 
            User user=(User)userstate;
            TcpClient client=user.client;
            string receivingstring="";
            while(isnormalexit==false)
            {
                try
                {
                    receivingstring=user.br.ReadString();
                }
                catch
                {
                    if(isnormalexit==false)
                    {
                        AddItemToListBox(string.Format("diconnect[{0}],stop reading",client.Client.RemoteEndPoint));
                    }
                    break;
                }
                AddItemToListBox(string.Format("form [{0}]:{1}",user.client.Client.RemoteEndPoint,receivingstring));
                string[] splitstring=receivingstring.Split(',');
                switch(splitstring[0])
                {
                    case "Login":
                        user.userName=splitstring[1];
                        SendToAllClient(user,receivingstring);
                        break;
                    case "Logout":
                        SendToAllClient(user,receivingstring);
                        RemoveUser(user);
                        return;
                    case "Talk":
                        string talkstring=receivingstring.Substring(splitstring[0].Length+splitstring[1].Length+2);
                        AddItemToListBox(string.Format("{0} say to {1}:{2}",user.userName,splitstring[1],talkstring));
                        Sendtoclient(user,"talk,"+user.userName+","+talkstring);
                        foreach(User target in userlist)
                        {
                            if(target.userName==splitstring[1] && user.userName!=splitstring[1])
                            {
                                Sendtoclient(target,"talk,"+user.userName+","+talkstring);
                                break;
                            }
                        }
                        break;
                    default:
                        AddItemToListBox("unkown:"+receivingstring);
                        break;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AddItemToListBox("stop service one by one");
            isnormalexit = true;
            for (int i = userlist.Count - 1; i >= 0; i--)
            {
                RemoveUser(userlist[i]);
            }
            mylistener.Stop();
            button1.Enabled = true;
            button2.Enabled = false;
        }

        private void server1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (mylistener != null)
            {
                button2.PerformClick();
            }
        }
    }
}
