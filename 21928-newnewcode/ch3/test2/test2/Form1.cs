using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.NetworkInformation;

namespace test2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

       

        private void Form1_Load(object sender, EventArgs e)
        {
            IPGlobalProperties properties = IPGlobalProperties.GetIPGlobalProperties();
            TcpConnectionInformation[] connections = properties.GetActiveTcpConnections();
            foreach (TcpConnectionInformation t in connections)
            {
                listBoxResult.Items.Add("Local endpoint: " + t.LocalEndPoint.Address);
                listBoxResult.Items.Add("Remote endpoint: " + t.RemoteEndPoint.Address);
                listBoxResult.Items.Add("State：" + t.State);
            }

        }
    }
}
