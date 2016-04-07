using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SPersonalArrayList
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MyArrayList array = new MyArrayList(10);
            array.Add("Jack");
            array.Add("Tom");
            foreach (object i in array)
            {
                Console.WriteLine(i);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MyArrayList<string> array = new MyArrayList<string>(10);
            array.Add("Jack");
            array.Add("Tom");
            foreach (string str in array)
            {
                Console.WriteLine(str);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            foreach (int i in PowersOf2.Power(2, 8))
            {
                Console.Write("{0} ", i);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }
    }
}
