using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
//添加的命名空间引用
using System.Net;
using System.Net.Mail;
using System.IO;
using System.Net.Sockets;
using System.Net.Mime;

namespace AsyncSendMailExample
{
    public partial class FormSendAsync : Form
    {
        SmtpClient smtpClient;
        public FormSendAsync()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBoxSmtpServer.Text = "smtp.gmail.com";
            textBoxSend.Text = "lisiname@gmail.com";
            textBoxDisplayName.Text = "李斯";
            textBoxPassword.Text = "lisiname12345";
            textBoxReceive.Text = "mytestname@126.com";
            textBoxSubject.Text = "测试mytest";
            textBoxBody.Text = "This is a test（测试）";
            radioButtonSsl.Checked = true;
            buttonCancel.Enabled = false;

        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            buttonSend.Enabled = false;
            buttonCancel.Enabled = true;

            this.Cursor = Cursors.WaitCursor;
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(textBoxSend.Text, textBoxDisplayName.Text, System.Text.Encoding.UTF8);
            mailMessage.To.Add(textBoxReceive.Text);
            mailMessage.Subject = textBoxSubject.Text;
            mailMessage.SubjectEncoding = System.Text.Encoding.Default;
            mailMessage.Body = textBoxBody.Text;
            mailMessage.BodyEncoding = System.Text.Encoding.Default;
            mailMessage.IsBodyHtml = false;
            mailMessage.Priority = MailPriority.Normal;
            //添加附件
            Attachment attachment = null;
            if (listBoxFileName.Items.Count > 0)
            {
                for (int i = 0; i < listBoxFileName.Items.Count; i++)
                {
                    string pathFileName = listBoxFileName.Items[i].ToString();
                    string extName = Path.GetExtension(pathFileName).ToLower();
                    //这里仅举例说明如何判断附件类型
                    if (extName == ".rar" || extName == ".zip")
                    {
                        attachment = new Attachment(pathFileName, MediaTypeNames.Application.Zip);
                    }
                    else
                    {
                        attachment = new Attachment(pathFileName, MediaTypeNames.Application.Octet);
                    }
                    ContentDisposition cd = attachment.ContentDisposition;
                    cd.CreationDate = System.IO.File.GetCreationTime(pathFileName);
                    cd.ModificationDate = System.IO.File.GetLastWriteTime(pathFileName);
                    cd.ReadDate = System.IO.File.GetLastAccessTime(pathFileName);
                    mailMessage.Attachments.Add(attachment);
                }
            }
            smtpClient = new SmtpClient();
            smtpClient.Host = textBoxSmtpServer.Text;
            smtpClient.Port = 25;
            //是否使用安全套接字层加密连接
            smtpClient.EnableSsl = radioButtonSsl.Checked;
            //不使用默认凭证,注意此句必须放在client.Credentials的上面
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential(textBoxSend.Text, textBoxPassword.Text);
            //邮件通过网络直接发送到服务器
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.SendCompleted += new SendCompletedEventHandler(smtpClient_SendCompleted);
            try
            {
                smtpClient.SendAsync(mailMessage, smtpClient);
                MessageBox.Show("发送成功");
            }
            catch (SmtpException smtpError)
            {
                MessageBox.Show("发送失败:" + smtpError.StatusCode
                    + "\n\n" + smtpError.Message
                    + "\n\n" + smtpError.StackTrace);
            }
            finally
            {
                mailMessage.Dispose();
                smtpClient = null;
                this.Cursor = Cursors.Default;
                buttonCancel.Enabled = false;
            }

        }

        private void smtpClient_SendCompleted(object sender, AsyncCompletedEventArgs e)
        {
            SmtpClient smtpClient = (SmtpClient)e.UserState;
            if (e.Cancelled)
            {
                MessageBox.Show("发送被取消");
            }
            if (e.Error != null)
            {
                MessageBox.Show(string.Format("[{0}] 发送失败", e.Error.ToString()));
            }
            else
            {
                MessageBox.Show("邮件发送成功！");

            }
            buttonCancel.Enabled = false;
        }

        private void buttonAddAttachment_Click(object sender, EventArgs e)
        {
            OpenFileDialog myOpenFileDialog = new OpenFileDialog();
            myOpenFileDialog.CheckFileExists = true;
            //只接收有效的文件名
            myOpenFileDialog.ValidateNames = true;
            //允许一次选择多个文件作为附件
            myOpenFileDialog.Multiselect = true;
            myOpenFileDialog.ShowDialog();
            if (myOpenFileDialog.FileNames.Length > 0)
            {
                listBoxFileName.Items.AddRange(myOpenFileDialog.FileNames);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            smtpClient.SendAsyncCancel();
        }

    }
}