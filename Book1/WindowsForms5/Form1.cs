using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;

namespace WindowsForms5
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            server1 s1 = new server1();
            s1.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SyncChatClient s1 = new SyncChatClient();
            s1.Show();
            
        }
        Func<int, int, int> Add = (x, y) => x + y;
<<<<<<< HEAD
=======

        private void button3_Click(object sender, EventArgs e)
        {
            FormServer fs = new FormServer();
            fs.Show();
        }
>>>>>>> 3cb139b011e1d42bc11f9a9bd670b0c43d25fd04
    }
}
