using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading;
using System.Security.Cryptography;

namespace WindowsForms5
{
    public partial class FormServer : Form
    {    
        private int maxUsers;
        System.Collections.Generic.List<GoUser> userlist = new List<GoUser>();
        private int maxTables;
        private GameTable[] gameTable;
        IPAddress localAddress;

        private int port=51888;
        private TcpListener myListener;
        private Service service;

        public FormServer()
        {
            InitializeComponent();
            service=new Service(listBox1);
        }

        private void FormServer_Load(object sender, EventArgs e)
        {
            listBox1.HorizontalScrollbar=true;
            IPAddress[] addrIP=Dns.GetHostAddresses(Dns.GetHostName());
            localAddress=addrIP[0];
            button2.Enabled=false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(int.TryParse(textBox2.Text,out maxTables)==false ||int.TryParse(textBox1.Text,out maxUsers)==false )
            {
                MessageBox.Show("请输入规定范围内的正整数");
            }
            if(maxUsers<1 || maxUsers>300)
            {
                MessageBox.Show("人数范围1-300,请重新输入");
                return;
            }
            if(maxTables<1 || maxTables>100)
            {
                MessageBox.Show("桌数范围1==100，请重新输入");
                return;
            }
            textBox1.Enabled=textBox2.Enabled=false;
            gameTable=new GameTable[maxTables];
            for(int i=0;i<maxTables;i++)
            {
                gameTable[i]=new GameTable(listBox1);
            }
            myListener = new TcpListener(localAddress, port);
            myListener.Start();
            service.AddItem(string.Format("begein listen in {0}:{1}", localAddress, port));
            //create new thread to listen
            ThreadStart ts = new ThreadStart(ListenClientConnect);
            Thread myThread = new Thread(ts);
            myThread.Start();
            button1.Enabled = false;
            button2.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //stop sending
            for (int i = 0; i < maxTables; i++)
            {
                gameTable[i].StopTimer();
            }
            service.AddItem(string.Format("current users:{0}", userlist.Count));
            service.AddItem("bigen stop service,to let user log off onebyeone");
            for (int i = 0; i < userlist.Count; i++)
            {
                //client recived data is null
                //service receivedata is null
                userlist[i].client.Close();
            }
            myListener.Stop();
            button1.Enabled = true;
            button2.Enabled = false;
            textBox1.Enabled = textBox2.Enabled = false;
        }
        private void ListenClientConnect()
        {
            while (true)
            {
                TcpClient newclient = null;
                try
                {
                    //wait for users
                    newclient = myListener.AcceptTcpClient();
                }
                catch
                {
                    break;
                }
                ParameterizedThreadStart pts = new ParameterizedThreadStart(ReciveData);
                Thread threadReceive = new Thread(pts);
                GoUser user = new GoUser(newclient);
                threadReceive.Start(user);
                userlist.Add(user);
                service.AddItem(string.Format("{0} in ", newclient.Client.RemoteEndPoint));
                service.AddItem(string.Format("usercount :{0}",userlist.Count));
            }
        }
        private void ReciveData(object obj)
        {
            GoUser user = (GoUser)obj;
            TcpClient client = user.client;
            bool normalExit = false;
            bool exitWhile = false;
            while (exitWhile == false)
            {
                string receiveString = null;
                try
                {
                    receiveString = user.sr.ReadLine();
                }
                catch
                {
                    service.AddItem("receving error!");
                }
                //tcpclient 对象套接字进行了封装，如果tcpclient对象关闭了但是底层套借此未关闭并不产生异常，但是读取结果为null
                if (receiveString == null)
                {
                    if (normalExit == false)
                    {
                        //if stoped ,connected is false
                        if (client.Connected == true)
                        {
                            service.AddItem(string.Format("与{0}失去联系，已经终止接受改用户信息", client.Client.RemoteEndPoint));
                        }
                        RemoveClientfromPlayer(user);
                    }
                    break;
                }
                service.AddItem(string.Format("from :{0}:{1}", user.userName, receiveString));
                string[] splitstring = receiveString.Split(',');
                int tableIndex = -1;
                int side = -1;
                int anotherSide = -1;
                string sendString = "";
                string command = splitstring[0].ToLower();
                switch (command)
                {
                    case "login":
                        if (userlist.Count > maxUsers)
                        {
                            sendString = "sorry";
                            service.SendToOne(user, sendString);
                            service.AddItem(" more people ,refuse " + splitstring[1] + " in");
                            exitWhile = true;
                        }
                        else
                        {
                            //将用户昵称保存在用户表中
                            //由于是引用类型，因此直接给user赋值也就是给userlist中的user赋值
                            user.userName = string.Format("[{0}--{1}]", splitstring[1], client.Client.RemoteEndPoint);

                            sendString = "Tables," + this.GetOnlineString();
                            service.SendToOne(user, sendString);
                        }
                        break;
                    case "logout":
                        service.AddItem(string.Format("{0} log off", user.userName));
                        normalExit = true;
                        exitWhile = true;
                        break;
                    case "sitdown":
                        tableIndex = int.Parse(splitstring[1]);
                        side = int.Parse(splitstring[2]);
                        gameTable[tableIndex].gamePlayer[side].user = user;
                        gameTable[tableIndex].gamePlayer[side].someone=true;
                        service.AddItem(string.Format("{0} setin {1}table {2}sites",user.userName,tableIndex+1,side+1));
                        anotherSide=(side+1)%2;
                        if(gameTable[tableIndex].gamePlayer[anotherSide].someone==true)
                        {
                            sendString=string.Format("SitDown,{0},{1}",anotherSide,gameTable[tableIndex].gamePlayer[anotherSide].user.userName);
                            service.SendToOne(user,sendString);
                        }
                        sendString=string.Format("SitDown,{0},{1}",anotherSide,gameTable[tableIndex].gamePlayer[anotherSide].user.userName);
                        service.SendToBoth(gameTable[tableIndex],sendString);
                        //send to all 
                        service.SendToAll(userlist,"Tables,"+this.GetOnlineString());
                        break;
                    case "getup":
                        tableIndex=int.Parse(splitstring[1]);
                        side=int.Parse(splitstring[2]);
                        service.AddItem(string.Format("{0}left,retrun to room",user.userName));
                        gameTable[tableIndex].StopTimer();
                        service.SendToBoth(gameTable[tableIndex],string.Format("Getup,{0},{1}",side,user.userName));
                        gameTable[tableIndex].gamePlayer[side].someone=false;
                        gameTable[tableIndex].gamePlayer[side].started=false;
                        gameTable[tableIndex].gamePlayer[side].grade=0;
                        anotherSide=(side+1)%2;
                        if(gameTable[tableIndex].gamePlayer[anotherSide].someone==true)
                        {
                            gameTable[tableIndex].gamePlayer[anotherSide].started=false;
                            gameTable[tableIndex].gamePlayer[anotherSide].grade=0;
                        }
                        service.SendToAll(userlist,"Tables,"+this.GetOnlineString());
                        break;
                    case "level":
                        tableIndex=int.Parse(splitstring[1]);
                        gameTable[tableIndex].SetTimerLevel((6-int.Parse(splitstring[2]))*100);
                        service.SendToBoth(gameTable[tableIndex],receiveString);
                        break;
                    case "talk":
                        tableIndex=int.Parse(splitstring[1]);
                        sendString=string.Format("Talk,{0},{1}",user.userName,receiveString.Substring(splitstring[0].Length+splitstring[1].Length));
                        service.SendToBoth(gameTable[tableIndex],sendString);
                        break;
                    case "start":
                        tableIndex=int.Parse(splitstring[1]);
                        side=int.Parse(splitstring[2]);
                        gameTable[tableIndex].gamePlayer[side].started=true;
                        if(side==0)
                        {
                            anotherSide=1;
                            sendString="Message,黑方已经开始。";
                        }
                        else
                        {
                            anotherSide=0;
                            sendString="Message,白方已经开始。";
                        }
                        service.SendToBoth(gameTable[tableIndex],sendString);
                        if(gameTable[tableIndex].gamePlayer[anotherSide].started==true)
                        {
                            gameTable[tableIndex].ResetGrid();
                            gameTable[tableIndex].StartTimer();
                        }
                        break;
                    case "unsetdot":
                        tableIndex=int.Parse(splitstring[1]);
                        side=int.Parse(splitstring[2]);
                        int xi=int.Parse(splitstring[3]);
                        int xj=int.Parse(splitstring[4]);
                        int color=int.Parse(splitstring[5]);
                        gameTable[tableIndex].UnsetDot(xi,xj,color);
                        break;
                    default:
                        service.SendToAll(userlist,"unkown "+receiveString);
                        break;
                }
            }
            userlist.Remove(user);
            client.Close();
            service.AddItem(string.Format("有一个退出，剩余连接用户数：{0}",userlist.Count));
        }
        private void RemoveClientfromPlayer(GoUser user)
        {
            for (int i = 0; i < gameTable.Length; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    if (gameTable[i].gamePlayer[j].user != null)
                    {
                        if (gameTable[i].gamePlayer[j].user == user)
                        {
                            StopPlayer(i, j);
                            return;
                        }
                    }
                }
            }
        }
        private void StopPlayer(int i, int j)
        {
            gameTable[i].StopTimer();
            gameTable[i].gamePlayer[j].someone = false;
            gameTable[i].gamePlayer[j].started = false;
            gameTable[i].gamePlayer[j].grade = 0;
            int otherSide = (j + 1) % 2;
            if (gameTable[i].gamePlayer[otherSide].someone == true)
            {
                gameTable[i].gamePlayer[otherSide].started = false;
                gameTable[i].gamePlayer[otherSide].grade = 0;
                if (gameTable[i].gamePlayer[otherSide].user.client.Connected == true)
                {
                    service.SendToOne(gameTable[i].gamePlayer[otherSide].user, string.Format("Lost,{0},{1}", j, gameTable[i].gamePlayer[j].user.userName));
                }
            }
        }
        private string GetOnlineString()
        {
            string str = "";
            for(int i=0;i<gameTable.Length;i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    str += gameTable[i].gamePlayer[j].someone == true ? "1" : "0";
                }
            }
            return str;
        }

        private void FormServer_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (myListener != null)
            {
                button2_Click(null, null);
            }
        }
    }
}
