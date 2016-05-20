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
using System.Net.Mime;

namespace SendMailExample
{
    public partial class FormSendMail : Form
    {
        public FormSendMail()
        {
            InitializeComponent();
        }

        private void FormSendMail_Load(object sender, EventArgs e)
        {
            textBoxSmtpServer.Text = "smtp.gmail.com";
            textBoxSend.Text = "lisiname@gmail.com";
            textBoxDisplayName.Text = "李斯";
            textBoxPassword.Text = "lisiname12345";
            textBoxReceive.Text = "mytestname@126.com";
            textBoxSubject.Text = "测试mytest";
            textBoxBody.Text = "This is a test（测试）";
            radioButtonSsl.Checked = true;
        }

        //单击【发送】按钮触发的事件
        private void buttonSend_Click(object sender, EventArgs e)
        {
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
                    cd.CreationDate = File.GetCreationTime(pathFileName);
                    cd.ModificationDate = File.GetLastWriteTime(pathFileName);
                    cd.ReadDate = File.GetLastAccessTime(pathFileName);
                    mailMessage.Attachments.Add(attachment);
                }
            }
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = textBoxSmtpServer.Text;
            smtpClient.Port = 25;
            //是否使用安全套接字层加密连接
            smtpClient.EnableSsl = radioButtonSsl.Checked;
            //不使用默认凭证,注意此句必须放在client.Credentials的上面
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential(textBoxSend.Text, textBoxPassword.Text);
            //邮件通过网络直接发送到服务器
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            try
            {
                smtpClient.Send(mailMessage);
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
            }
        }

        //单击【添加附件】按钮触发的事件
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
    }
}
