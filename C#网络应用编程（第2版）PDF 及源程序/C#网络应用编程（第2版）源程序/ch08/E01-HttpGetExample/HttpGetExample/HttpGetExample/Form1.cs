using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;

namespace HttpGetExample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            textBox1.Text = "图片";
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            Encoding gb2312Encoding = Encoding.GetEncoding("GB2312");
            string uri = "http://www.baidu.com/s?wd=" +
                System.Web.HttpUtility.UrlEncode(textBox1.Text, gb2312Encoding);
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(uri);
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                Stream stream = response.GetResponseStream();
                StreamReader sr = new StreamReader(stream, Encoding.Default);
                richTextBox1.Text = sr.ReadToEnd();
                stream.Close();
                sr.Close();
                stream.Close();
            }
            webBrowser1.DocumentText = richTextBox1.Text;
        }
    }
}
