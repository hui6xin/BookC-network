using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsForms3._3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            textBox1.Text = "测试数据：abc，123，我，\\u03a0,\u03a0";
            //textBox2.ReadOnly = textBox3.ReadOnly = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            listBox1.Items.Add(string.Format("{0,-20}{1}", "Name", "EncodingName"));
            foreach (EncodingInfo ei in Encoding.GetEncodings())
            {
                Encoding en = ei.GetEncoding();
                listBox1.Items.Add(string.Format("{0,-20}{1}", ei.Name,en.EncodingName));
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Encoding GB2312 = Encoding.GetEncoding("GB2312");
            Encoding GB18030 = Encoding.GetEncoding("GB18030");
            Encoding ASCII = Encoding.ASCII;
            Encoding utf8 = Encoding.UTF8;
            listBox2.Items.Clear();
            listBox2.Items.Add(string.Format("{0,-20}{1}", "Name", "EncodingName"));
            listBox2.Items.Add(string.Format("{0,-20}{1}", GB2312.HeaderName, GB2312.EncodingName));
            listBox2.Items.Add(string.Format("{0,-20}{1}", GB18030.HeaderName, GB18030.EncodingName));
            listBox2.Items.Add(string.Format("{0,-20}{1}", ASCII.HeaderName, ASCII.EncodingName));
            listBox2.Items.Add(string.Format("{0,-20}{1}", utf8.HeaderName, utf8.EncodingName));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string unicodestring = "该字符串包含Unicode字符Pi(\u03a0)";
            Encoding unicode = Encoding.Unicode;
            Encoding utf8 = Encoding.UTF8;
            byte[] unicodebytes = unicode.GetBytes(unicodestring);
            byte[] utf8bytes = Encoding.Convert(Encoding.Unicode, Encoding.UTF8, unicodebytes);
            string utf8string = utf8.GetString(utf8bytes);
            listBox3.Items.Add("该字符串包含Unicode字符Pi(\\u03a0)");
            listBox3.Items.Add(utf8string);
            Encoder asciiencoder = Encoding.ASCII.GetEncoder();
            Encoder unicodeencoder = Encoding.Unicode.GetEncoder();
            Char[] chars = new Char[]{
                '\u0023',//#
                '\u0025',//%
                '\u03a0',//Pi
                '\u03a3' //sigma
            };
            //unicode
            listBox3.Items.Add(new string(chars));
            //coder
            Encoder encoder = Encoding.UTF8.GetEncoder();
            //count 
            byte[] bytes=new byte[encoder.GetByteCount(chars,0,chars.Length,true)];
            //save
            encoder.GetBytes(chars, 0, chars.Length, bytes, 0, true);
            listBox3.Items.Add(Encoding.UTF8.GetString(bytes));



            string strresult = "";
            byte[] bytess = new byte[] {85,0,110,0,105,0,99,0,111,0,100,0,101,0 };
            Decoder unidecoder = Encoding.Unicode.GetDecoder();
            int charcount = unidecoder.GetCharCount(bytess, 0, bytess.Length);
            char[] charss = new char[charcount];
            int charlen = unidecoder.GetChars(bytess, 0, bytess.Length, charss, 0);
            foreach (char c in charss)
            {
                strresult += c.ToString();
            }
            listBox3.Items.Add(strresult);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string codetype = comboBox1.SelectedItem.ToString();
            codetype = codetype.Substring(0, codetype.IndexOf('['));
            Encoder encoder = Encoding.GetEncoding(codetype).GetEncoder();
            char[] chars = textBox1.Text.ToCharArray();
            byte[] bytes = new byte[encoder.GetByteCount(chars, 0, chars.Length, true)];
            encoder.GetBytes(chars, 0, chars.Length, bytes, 0, true);
            //for (int i = 0; i < bytes.Length; i++)
            //{
            //    bytes[i] = 1;
            //}
            textBox2.Text = Convert.ToBase64String(bytes);


            Decoder decoder = Encoding.GetEncoding(codetype).GetDecoder();
            int charlen = decoder.GetChars(bytes, 0, bytes.Length, chars, 0);
            string strresult = "";
            foreach (char c in chars)
                strresult += c.ToString();
            textBox3.Text = strresult;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (EncodingInfo ei in Encoding.GetEncodings())
            {
                Encoding en = ei.GetEncoding();
                comboBox1.Items.Add(string.Format("{0}[{1}]", ei.Name, en.EncodingName));
            }
            comboBox1.SelectedIndex = comboBox1.FindString("gb2312");
            
        }
    }
}
