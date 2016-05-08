using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
//添加的命名空间引用
using System.Net;
using System.Net.Sockets;
using System.IO;
namespace ReceiveMailExample
{
    public partial class FormReceiveMail : Form
    {
        private TcpClient tcpClient;
        private NetworkStream networkStream;
        private StreamReader sr;
        private StreamWriter sw;
        public FormReceiveMail()
        {
            InitializeComponent();
            textBoxPOP3Server.Text = "pop3.126.com";
            textBoxPassword.Text = "1234567";
            textBoxUser.Text = "myname@126.com";
        }

        //单击建立连接按钮触发的事件
        private void buttonConnect_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            listBoxStatus.Items.Clear();
            try
            {
                //建立与POP3服务器的连接，使用默认端口110
                tcpClient = new TcpClient(textBoxPOP3Server.Text, 110);
                listBoxStatus.Items.Add("与pop3服务器连接成功");
            }
            catch
            {
                MessageBox.Show("与服务器连接失败");
                return;
            }
            string str;
            networkStream = tcpClient.GetStream();
            sr = new StreamReader(networkStream, Encoding.Default);
            sw = new StreamWriter(networkStream, Encoding.Default);
            sw.AutoFlush = true;
            //读取服务器回送的连接信息
            str = GetResponse();
            if (CheckResponse(str) == false) return;
            //向服务器发送用户名，请求确认
            SendToServer("USER " + textBoxUser.Text);
            str = GetResponse();
            if (CheckResponse(str) == false) return;
            //向服务器发送密码，请求确认
            SendToServer("PASS " + textBoxPassword.Text);
            str = GetResponse();
            if (CheckResponse(str) == false) return;
            //向服务器发送LIST命令，请求获取邮件总数和总字节数
            SendToServer("LIST");
            str = GetResponse();
            if (CheckResponse(str) == false) return;
            string[] splitString = str.Split(' ');
            //从字符串中取子串获取邮件总数
            int count = int.Parse(splitString[1]);
            //判断邮箱中是否有邮件
            if (count > 0)
            {
                listBoxOperation.Items.Clear();
                groupBoxOperation.Text = "信箱中共有 " + splitString[1] + " 封邮件";
                //向邮件列表框中添加邮件
                for (int i = 0; i < count; i++)
                {
                    str = GetResponse();
                    splitString = str.Split(' ');
                    listBoxOperation.Items.Add(string.Format(
                        "第{0}封：{1}字节", splitString[0], splitString[1]));
                }
                listBoxOperation.SelectedIndex = 0;
                //读出结束符
                str = GetResponse();
                //设置对应状态信息
                buttonRead.Enabled = true;
                buttonDelete.Enabled = true;
            }
            else
            {
                groupBoxOperation.Text = "信箱中没有邮件";
                buttonRead.Enabled = false;
                buttonDelete.Enabled = false;
            }
            buttonConnect.Enabled = false;
            buttonDisconnect.Enabled = true;
            Cursor.Current = Cursors.Default;
        }

        private bool SendToServer(string str)
        {
            try
            {
                sw.WriteLine(str);
                sw.Flush();
                listBoxStatus.Items.Add("发送：" + str);
                return true;
            }
            catch (Exception err)
            {
                listBoxStatus.Items.Add("发送失败：" + err.Message);
                return false;
            }
        }

        private string GetResponse()
        {
            string str = null;
            try
            {
                str = sr.ReadLine();
                if (str == null)
                {
                    listBoxStatus.Items.Add("收到：null");
                }
                else
                {
                    listBoxStatus.Items.Add("收到：" + str);
                    if (str.StartsWith("-ERR"))
                    {
                        str = null;
                    }
                }
            }
            catch (Exception ex)
            {
                listBoxStatus.Items.Add("接收失败：" + ex.Message);
            }
            return str;
        }

        private bool CheckResponse(string responseString)
        {
            if (responseString == null)
            {
                return false;
            }
            else
            {
                if (responseString.StartsWith("+OK"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        //单击断开连接按钮触发的事件
        private void buttonDisconnect_Click(object sender, EventArgs e)
        {
            SendToServer("QUIT");
            sr.Close();
            sw.Close();
            networkStream.Close();
            tcpClient.Close();
            listBoxOperation.Items.Clear();
            richTextBoxOriginalMail.Clear();
            listBoxStatus.Items.Clear();
            groupBoxOperation.Text = "邮件信息";
            buttonConnect.Enabled = true;
            buttonDisconnect.Enabled = false;
        }

        //单击阅读信件按钮触发的事件
        private void buttonRead_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            richTextBoxOriginalMail.Clear();
            string mailIndex = listBoxOperation.SelectedItem.ToString();
            mailIndex = mailIndex.Substring(1, mailIndex.IndexOf("封") - 1);
            SendToServer("RETR " + mailIndex);
            string str = GetResponse();
            if (CheckResponse(str) == false) return;
            try
            {
                string receiveData = sr.ReadLine();
                if (receiveData.StartsWith("-ERR") == true)
                {
                    listBoxStatus.Items.Add(receiveData);
                }
                else
                {
                    while (receiveData != ".")
                    {
                        richTextBoxOriginalMail.AppendText(receiveData + "\r\n");
                        receiveData = sr.ReadLine();
                    }
                }
            }
            catch (InvalidOperationException err)
            {
                listBoxStatus.Items.Add("Error: " + err.ToString());
            }
            Cursor.Current = Cursors.Default;
        }

        private void DecodeMailHeader(string mail)
        {
            string header = "";
            int pos = mail.IndexOf("\n\n");
            if (pos == -1)
            {
                header = GetDecodedHeader(mail);
            }
            else
            {
                header = mail.Substring(0, pos + 2);
            }
            //richTextBoxDecode.AppendText(GetDecodedHeader(header));
        }

        private string GetDecodedHeader(string header)
        {
            StringBuilder s = new StringBuilder();
            s.AppendLine("Subject:" + GetEncodedValueString(header, "Subject: ", false));
            s.AppendLine("From:" + GetEncodedValueString(header, "From: ", false).Trim());
            s.AppendLine("To:" + GetEncodedValueString(header, "To: ", true).Trim());
            s.AppendLine("Date:" + GetDateTimeFromString(GetValueString(header, "Date: ", false, false)));
            s.AppendLine("Cc:" + GetEncodedValueString(header, "Cc: ", true).Trim());
            s.AppendLine("ContentType:" + GetValueString(header, "Content-Type: ", false, true));
            return s.ToString();
        }

        /// <summary>
        /// 把时间转成字符串形式
        /// </summary>
        /// <param name="DateTimeString"></param>
        /// <returns></returns>
        private string GetDateTimeFromString(string DateTimeString)
        {
            if (DateTimeString == "")
            {
                return null;
            }
            try
            {
                string strDateTime;
                if (DateTimeString.IndexOf("+") != -1)
                {
                    strDateTime = DateTimeString.Substring(0, DateTimeString.IndexOf("+"));
                }
                else if (DateTimeString.IndexOf("-") != -1)
                {
                    strDateTime = DateTimeString.Substring(0, DateTimeString.IndexOf("-"));
                }
                else
                {
                    strDateTime = DateTimeString;
                }
                DateTime dt = DateTime.Parse(strDateTime);
                return dt.ToString();
            }
            catch
            {
                return null;
            }
        }

        private string GetEncodedValueString(string SourceString, string Key, bool SplitBySemicolon)
        {
            int j;
            string strValue;
            string strSource = SourceString.ToLower();
            string strReturn = "";
            j = strSource.IndexOf(Key.ToLower());
            if (j != -1)
            {
                j += Key.Length;
                int kk = strSource.IndexOf("\n", j);
                strValue = SourceString.Substring(j, kk - j).TrimEnd();
                do
                {
                    if (strValue.IndexOf("=?") != -1)
                    {
                        if (SplitBySemicolon == true)
                        {
                            strReturn += ConvertStringEncodingFromBase64(strValue) + "; ";
                        }
                        else
                        {
                            strReturn += ConvertStringEncodingFromBase64(strValue);
                        }
                    }
                    else
                    {
                        strReturn += strValue;
                    }
                    j += strValue.Length + 2;
                    if (strSource.IndexOf("\r\n", j) == -1)
                    {
                        break;
                    }
                    else
                    {
                        strValue = SourceString.Substring(j, strSource.IndexOf("\r\n", j) - j).TrimEnd();
                    }
                }
                while (strValue.StartsWith(" ") || strValue.StartsWith("\t"));
            }
            else
            {
                strReturn = "";
            }
            return strReturn;
        }

        private string GetValueString(string SourceString, string Key, bool ContainsQuotationMarks, bool ContainsSemicolon)
        {
            int j;
            string strReturn;
            string strSource = SourceString.ToLower();
            j = strSource.IndexOf(Key.ToLower());
            if (j != -1)
            {
                j += Key.Length;
                strReturn = SourceString.Substring(j, strSource.IndexOf("\n", j) - j).TrimStart().TrimEnd();
                if (ContainsSemicolon == true)
                {
                    if (strReturn.IndexOf(";") != -1)
                    {
                        strReturn = strReturn.Substring(0, strReturn.IndexOf(";"));
                    }
                }
                if (ContainsQuotationMarks == true)
                {
                    int i = strReturn.IndexOf("\"");
                    int k;
                    if (i != -1)
                    {
                        k = strReturn.IndexOf("\"", i + 1);
                        if (k != -1)
                        {
                            strReturn = strReturn.Substring(i + 1, k - i - 1);
                        }
                        else
                        {
                            strReturn = strReturn.Substring(i + 1);
                        }
                    }
                }
                return strReturn;
            }
            else
            {
                return "";
            }
        }

        private string ConvertStringEncodingFromBase64(string SourceString)
        {
            try
            {
                if (SourceString.IndexOf("=?") == -1)
                {
                    return SourceString;
                }
                else
                {
                    int i = SourceString.IndexOf("?");
                    int j = SourceString.IndexOf("?", i + 1);
                    int k = SourceString.IndexOf("?", j + 1);
                    char chrTransEnc = SourceString[j + 1];
                    switch (chrTransEnc)
                    {
                        case 'B':
                            return ConvertStringEncodingFromBase64Ex(SourceString.Substring(k + 1, SourceString.IndexOf("?", k + 1) - k - 1), SourceString.Substring(i + 1, j - i - 1));
                        default:
                            throw new Exception("unhandled content transfer encoding");
                    }
                }
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// 把Base64编码转换成字符串
        /// </summary>
        /// <param name="SourceString"></param>
        /// <param name="Charset"></param>
        /// <returns></returns>
        private string ConvertStringEncodingFromBase64Ex(string SourceString, string Charset)
        {
            try
            {
                Encoding enc;
                if (Charset == "")
                    enc = Encoding.Default;
                else
                    enc = Encoding.GetEncoding(Charset);

                return enc.GetString(Convert.FromBase64String(SourceString));
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// 把字符串转换成Base64Ex编码
        /// </summary>
        /// <param name="SourceString"></param>
        /// <param name="Charset"></param>
        /// <param name="AutoWordWrap"></param>
        /// <returns></returns>
        private string ConvertStringEncodingToBase64Ex(string SourceString, string Charset, bool AutoWordWrap)
        {
            Encoding enc = Encoding.GetEncoding(Charset);
            byte[] buffer = enc.GetBytes(SourceString);
            string strContent = Convert.ToBase64String(buffer);
            StringBuilder strTemp = new StringBuilder();
            int ii = 0;
            for (int i = 0; i <= strContent.Length / 76 - 1; i++)
            {
                strTemp.Append(strContent.Substring(76 * i, 76) + "\r\n");
                ii++;
            }
            strTemp.Append(strContent.Substring(76 * (ii)));
            strContent = strTemp.ToString();

            return strContent;
        }

        private string ConvertStringEncoding(string SourceString, string Charset)
        {
            try
            {
                Encoding enc;
                if (Charset == "8bit" || Charset == "")
                {
                    enc = Encoding.Default;
                }
                else
                {
                    enc = Encoding.GetEncoding(Charset);
                }
                return enc.GetString(Encoding.ASCII.GetBytes(SourceString));
            }
            catch
            {
                return Encoding.Default.GetString(Encoding.ASCII.GetBytes(SourceString));
            }
        }

        //单击删除信件按钮触发的事件
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            string parameter = listBoxOperation.SelectedItem.ToString();
            parameter = parameter.Substring(1, parameter.IndexOf("封") - 1);
            SendToServer("DELE " + parameter);
            string str = GetResponse();
            if (CheckResponse(str) == false) return;
            richTextBoxOriginalMail.Clear();
            int j = listBoxOperation.SelectedIndex;
            listBoxOperation.Items.Remove(listBoxOperation.Items[j].ToString());
            MessageBox.Show("删除成功");
        }
    }
}