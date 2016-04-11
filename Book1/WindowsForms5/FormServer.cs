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
        System.Collections.Generic.List<User> userlist=new List<User>();
        private int maxTables;
        private GameTable[] gameTable;
        IPAddress localAddress;

        private int port=51888;
        private TcpClient myListener;
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
            if(int.TryParse(textBox2.Text,out maxTables)==false ||int.TryParse(textBox1.Text,out maxTables)==false )
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
        }
    }
}
