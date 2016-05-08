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
using System.Text.RegularExpressions;
namespace HttpPostExample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            textBox1.Text = "this is a book，中文信息";
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            string viewState = null;
            string eventValidation = null;
            string uriString = "http://localhost:2749/Default.aspx";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uriString);
            request.Method = WebRequestMethods.Http.Get;
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                Stream stream1 = response.GetResponseStream();
                StreamReader sr = new StreamReader(stream1, Encoding.UTF8);
                string htmlText = sr.ReadToEnd();
                stream1.Close();
                viewState = GetHiddenField(htmlText, "__VIEWSTATE");
                eventValidation = GetHiddenField(htmlText, "__EVENTVALIDATION");
                richTextBox1.Text = htmlText;
            }
            request = (HttpWebRequest)WebRequest.Create(uriString);
            request.Method = WebRequestMethods.Http.Post;
            request.AllowAutoRedirect = true;
            string s = "TextBox1=" + System.Web.HttpUtility.UrlEncode(textBox1.Text) +
                "&Button1=" + System.Web.HttpUtility.UrlEncode("提交");
            if (viewState != null)
            {
                s += "&__VIEWSTATE=" + System.Web.HttpUtility.UrlEncode(viewState);
            }
            if (eventValidation != null)
            {
                s += "&__EVENTVALIDATION=" +
                    System.Web.HttpUtility.UrlEncode(eventValidation);
            }
            byte[] bytes = Encoding.UTF8.GetBytes(s);
            request.ContentType = "application/x-www-form-urlencoded";
            //request.ContentType = "multipart/form-data";
            request.ContentLength = bytes.Length;
            Stream stream = request.GetRequestStream();
            stream.Write(bytes, 0, bytes.Length);
            stream.Close();
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                Stream dataStream = response.GetResponseStream();
                StreamReader sr1 = new StreamReader(dataStream, Encoding.UTF8);
                webBrowser1.DocumentText = sr1.ReadToEnd();
                dataStream.Close();
                sr1.Close();
            }
        }

        private static string GetHiddenField(string htmlText, string matchFieldName)
        {
            Regex r = new Regex("<input type=\"hidden\"" +
                " name=\"" + matchFieldName + "\"" +
                " id=\"" + matchFieldName + "\"" +
                " value=\"(?<matchValue>[^\"]+)\"");
            Match m = r.Match(htmlText);
            if (m.Success)
            {
                return m.Groups["matchValue"].Value;
            }
            return null;
        }
    }
}
