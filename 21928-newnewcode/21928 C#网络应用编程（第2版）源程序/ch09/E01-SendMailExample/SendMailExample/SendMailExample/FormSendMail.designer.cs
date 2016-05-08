namespace SendMailExample
{
    partial class FormSendMail
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.textBoxSend = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxReceive = new System.Windows.Forms.TextBox();
            this.buttonSend = new System.Windows.Forms.Button();
            this.textBoxBody = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxSubject = new System.Windows.Forms.TextBox();
            this.buttonAddFile = new System.Windows.Forms.Button();
            this.listBoxFileName = new System.Windows.Forms.ListBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxSmtpServer = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxDisplayName = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.radioButtonSsl = new System.Windows.Forms.RadioButton();
            this.radioButtonNo = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // textBoxSend
            // 
            this.textBoxSend.Location = new System.Drawing.Point(84, 54);
            this.textBoxSend.Name = "textBoxSend";
            this.textBoxSend.Size = new System.Drawing.Size(130, 21);
            this.textBoxSend.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "发件人信箱";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(352, 59);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "登录密码";
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Location = new System.Drawing.Point(411, 54);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.PasswordChar = '*';
            this.textBoxPassword.Size = new System.Drawing.Size(117, 21);
            this.textBoxPassword.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 91);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "收件人信箱";
            // 
            // textBoxReceive
            // 
            this.textBoxReceive.Location = new System.Drawing.Point(84, 88);
            this.textBoxReceive.Name = "textBoxReceive";
            this.textBoxReceive.Size = new System.Drawing.Size(444, 21);
            this.textBoxReceive.TabIndex = 1;
            // 
            // buttonSend
            // 
            this.buttonSend.Location = new System.Drawing.Point(453, 290);
            this.buttonSend.Name = "buttonSend";
            this.buttonSend.Size = new System.Drawing.Size(75, 23);
            this.buttonSend.TabIndex = 12;
            this.buttonSend.Text = "发送";
            this.buttonSend.UseVisualStyleBackColor = true;
            this.buttonSend.Click += new System.EventHandler(this.buttonSend_Click);
            // 
            // textBoxBody
            // 
            this.textBoxBody.Location = new System.Drawing.Point(66, 152);
            this.textBoxBody.Multiline = true;
            this.textBoxBody.Name = "textBoxBody";
            this.textBoxBody.Size = new System.Drawing.Size(462, 75);
            this.textBoxBody.TabIndex = 13;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(12, 156);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(41, 12);
            this.label10.TabIndex = 7;
            this.label10.Text = "内　容";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(13, 126);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 12);
            this.label8.TabIndex = 4;
            this.label8.Text = "主  题";
            // 
            // textBoxSubject
            // 
            this.textBoxSubject.Location = new System.Drawing.Point(66, 121);
            this.textBoxSubject.Name = "textBoxSubject";
            this.textBoxSubject.Size = new System.Drawing.Size(462, 21);
            this.textBoxSubject.TabIndex = 11;
            // 
            // buttonAddFile
            // 
            this.buttonAddFile.Location = new System.Drawing.Point(453, 252);
            this.buttonAddFile.Name = "buttonAddFile";
            this.buttonAddFile.Size = new System.Drawing.Size(75, 23);
            this.buttonAddFile.TabIndex = 14;
            this.buttonAddFile.Text = "添加附件";
            this.buttonAddFile.UseVisualStyleBackColor = true;
            this.buttonAddFile.Click += new System.EventHandler(this.buttonAddAttachment_Click);
            // 
            // listBoxFileName
            // 
            this.listBoxFileName.FormattingEnabled = true;
            this.listBoxFileName.ItemHeight = 12;
            this.listBoxFileName.Location = new System.Drawing.Point(66, 238);
            this.listBoxFileName.Name = "listBoxFileName";
            this.listBoxFileName.Size = new System.Drawing.Size(381, 88);
            this.listBoxFileName.TabIndex = 15;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 238);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 7;
            this.label6.Text = "附　件";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "SMTP服务器";
            // 
            // textBoxSmtpServer
            // 
            this.textBoxSmtpServer.Location = new System.Drawing.Point(83, 17);
            this.textBoxSmtpServer.Name = "textBoxSmtpServer";
            this.textBoxSmtpServer.Size = new System.Drawing.Size(131, 21);
            this.textBoxSmtpServer.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(220, 59);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "显示名称";
            // 
            // textBoxDisplayName
            // 
            this.textBoxDisplayName.Location = new System.Drawing.Point(279, 54);
            this.textBoxDisplayName.Name = "textBoxDisplayName";
            this.textBoxDisplayName.Size = new System.Drawing.Size(59, 21);
            this.textBoxDisplayName.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(232, 23);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(203, 12);
            this.label7.TabIndex = 0;
            this.label7.Text = "是否使用安全套接字(SSL)加密连接：";
            // 
            // radioButtonSsl
            // 
            this.radioButtonSsl.AutoSize = true;
            this.radioButtonSsl.Location = new System.Drawing.Point(443, 21);
            this.radioButtonSsl.Name = "radioButtonSsl";
            this.radioButtonSsl.Size = new System.Drawing.Size(35, 16);
            this.radioButtonSsl.TabIndex = 18;
            this.radioButtonSsl.TabStop = true;
            this.radioButtonSsl.Text = "是";
            this.radioButtonSsl.UseVisualStyleBackColor = true;
            // 
            // radioButtonNo
            // 
            this.radioButtonNo.AutoSize = true;
            this.radioButtonNo.Location = new System.Drawing.Point(486, 21);
            this.radioButtonNo.Name = "radioButtonNo";
            this.radioButtonNo.Size = new System.Drawing.Size(35, 16);
            this.radioButtonNo.TabIndex = 18;
            this.radioButtonNo.TabStop = true;
            this.radioButtonNo.Text = "否";
            this.radioButtonNo.UseVisualStyleBackColor = true;
            // 
            // FormSendMail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(546, 344);
            this.Controls.Add(this.radioButtonNo);
            this.Controls.Add(this.radioButtonSsl);
            this.Controls.Add(this.listBoxFileName);
            this.Controls.Add(this.buttonAddFile);
            this.Controls.Add(this.buttonSend);
            this.Controls.Add(this.textBoxBody);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.textBoxSubject);
            this.Controls.Add(this.textBoxSmtpServer);
            this.Controls.Add(this.textBoxSend);
            this.Controls.Add(this.textBoxDisplayName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.textBoxPassword);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxReceive);
            this.Name = "FormSendMail";
            this.Text = "同步发送举例";
            this.Load += new System.EventHandler(this.FormSendMail_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxSend;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxReceive;
        private System.Windows.Forms.Button buttonSend;
        private System.Windows.Forms.TextBox textBoxBody;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBoxSubject;
        private System.Windows.Forms.Button buttonAddFile;
        private System.Windows.Forms.ListBox listBoxFileName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxSmtpServer;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxDisplayName;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.RadioButton radioButtonSsl;
        private System.Windows.Forms.RadioButton radioButtonNo;
    }
}

