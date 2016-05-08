namespace ReceiveMailExample
{
    partial class FormReceiveMail
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
            this.groupBoxConnect = new System.Windows.Forms.GroupBox();
            this.buttonDisconnect = new System.Windows.Forms.Button();
            this.buttonConnect = new System.Windows.Forms.Button();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.labelPwd = new System.Windows.Forms.Label();
            this.textBoxPOP3Server = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxUser = new System.Windows.Forms.TextBox();
            this.labelUser = new System.Windows.Forms.Label();
            this.groupBoxMailContent = new System.Windows.Forms.GroupBox();
            this.richTextBoxOriginalMail = new System.Windows.Forms.RichTextBox();
            this.groupBoxOperation = new System.Windows.Forms.GroupBox();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.buttonRead = new System.Windows.Forms.Button();
            this.listBoxOperation = new System.Windows.Forms.ListBox();
            this.groupBoxState = new System.Windows.Forms.GroupBox();
            this.listBoxStatus = new System.Windows.Forms.ListBox();
            this.groupBoxConnect.SuspendLayout();
            this.groupBoxMailContent.SuspendLayout();
            this.groupBoxOperation.SuspendLayout();
            this.groupBoxState.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxConnect
            // 
            this.groupBoxConnect.Controls.Add(this.buttonDisconnect);
            this.groupBoxConnect.Controls.Add(this.buttonConnect);
            this.groupBoxConnect.Controls.Add(this.textBoxPassword);
            this.groupBoxConnect.Controls.Add(this.labelPwd);
            this.groupBoxConnect.Controls.Add(this.textBoxPOP3Server);
            this.groupBoxConnect.Controls.Add(this.label1);
            this.groupBoxConnect.Controls.Add(this.textBoxUser);
            this.groupBoxConnect.Controls.Add(this.labelUser);
            this.groupBoxConnect.Location = new System.Drawing.Point(8, 12);
            this.groupBoxConnect.Name = "groupBoxConnect";
            this.groupBoxConnect.Size = new System.Drawing.Size(339, 111);
            this.groupBoxConnect.TabIndex = 1;
            this.groupBoxConnect.TabStop = false;
            this.groupBoxConnect.Text = "连接操作";
            // 
            // buttonDisconnect
            // 
            this.buttonDisconnect.Enabled = false;
            this.buttonDisconnect.Location = new System.Drawing.Point(260, 73);
            this.buttonDisconnect.Name = "buttonDisconnect";
            this.buttonDisconnect.Size = new System.Drawing.Size(72, 24);
            this.buttonDisconnect.TabIndex = 7;
            this.buttonDisconnect.Text = "断开连接";
            this.buttonDisconnect.Click += new System.EventHandler(this.buttonDisconnect_Click);
            // 
            // buttonConnect
            // 
            this.buttonConnect.Location = new System.Drawing.Point(261, 19);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(72, 24);
            this.buttonConnect.TabIndex = 6;
            this.buttonConnect.Text = "建立连接";
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Location = new System.Drawing.Point(75, 73);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.PasswordChar = '*';
            this.textBoxPassword.Size = new System.Drawing.Size(168, 21);
            this.textBoxPassword.TabIndex = 5;
            // 
            // labelPwd
            // 
            this.labelPwd.AutoSize = true;
            this.labelPwd.Location = new System.Drawing.Point(4, 76);
            this.labelPwd.Name = "labelPwd";
            this.labelPwd.Size = new System.Drawing.Size(29, 12);
            this.labelPwd.TabIndex = 4;
            this.labelPwd.Text = "密码";
            // 
            // textBoxPOP3Server
            // 
            this.textBoxPOP3Server.Location = new System.Drawing.Point(75, 19);
            this.textBoxPOP3Server.Name = "textBoxPOP3Server";
            this.textBoxPOP3Server.Size = new System.Drawing.Size(168, 21);
            this.textBoxPOP3Server.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "POP3服务器";
            // 
            // textBoxUser
            // 
            this.textBoxUser.Location = new System.Drawing.Point(75, 46);
            this.textBoxUser.Name = "textBoxUser";
            this.textBoxUser.Size = new System.Drawing.Size(168, 21);
            this.textBoxUser.TabIndex = 3;
            // 
            // labelUser
            // 
            this.labelUser.AutoSize = true;
            this.labelUser.Location = new System.Drawing.Point(4, 49);
            this.labelUser.Name = "labelUser";
            this.labelUser.Size = new System.Drawing.Size(53, 12);
            this.labelUser.TabIndex = 2;
            this.labelUser.Text = "用户信箱";
            // 
            // groupBoxMailContent
            // 
            this.groupBoxMailContent.Controls.Add(this.richTextBoxOriginalMail);
            this.groupBoxMailContent.Location = new System.Drawing.Point(357, 12);
            this.groupBoxMailContent.Name = "groupBoxMailContent";
            this.groupBoxMailContent.Size = new System.Drawing.Size(382, 294);
            this.groupBoxMailContent.TabIndex = 2;
            this.groupBoxMailContent.TabStop = false;
            this.groupBoxMailContent.Text = "邮件原文";
            // 
            // richTextBoxOriginalMail
            // 
            this.richTextBoxOriginalMail.Location = new System.Drawing.Point(6, 20);
            this.richTextBoxOriginalMail.Name = "richTextBoxOriginalMail";
            this.richTextBoxOriginalMail.Size = new System.Drawing.Size(370, 268);
            this.richTextBoxOriginalMail.TabIndex = 0;
            this.richTextBoxOriginalMail.Text = "";
            this.richTextBoxOriginalMail.WordWrap = false;
            // 
            // groupBoxOperation
            // 
            this.groupBoxOperation.Controls.Add(this.buttonDelete);
            this.groupBoxOperation.Controls.Add(this.buttonRead);
            this.groupBoxOperation.Controls.Add(this.listBoxOperation);
            this.groupBoxOperation.Location = new System.Drawing.Point(8, 129);
            this.groupBoxOperation.Name = "groupBoxOperation";
            this.groupBoxOperation.Size = new System.Drawing.Size(339, 177);
            this.groupBoxOperation.TabIndex = 3;
            this.groupBoxOperation.TabStop = false;
            this.groupBoxOperation.Text = "邮件信息";
            // 
            // buttonDelete
            // 
            this.buttonDelete.Enabled = false;
            this.buttonDelete.Location = new System.Drawing.Point(192, 144);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(64, 22);
            this.buttonDelete.TabIndex = 2;
            this.buttonDelete.Text = "删除信件";
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // buttonRead
            // 
            this.buttonRead.Enabled = false;
            this.buttonRead.Location = new System.Drawing.Point(69, 144);
            this.buttonRead.Name = "buttonRead";
            this.buttonRead.Size = new System.Drawing.Size(64, 22);
            this.buttonRead.TabIndex = 1;
            this.buttonRead.Text = "阅读信件";
            this.buttonRead.Click += new System.EventHandler(this.buttonRead_Click);
            // 
            // listBoxOperation
            // 
            this.listBoxOperation.ItemHeight = 12;
            this.listBoxOperation.Location = new System.Drawing.Point(8, 24);
            this.listBoxOperation.Name = "listBoxOperation";
            this.listBoxOperation.ScrollAlwaysVisible = true;
            this.listBoxOperation.Size = new System.Drawing.Size(317, 112);
            this.listBoxOperation.TabIndex = 0;
            // 
            // groupBoxState
            // 
            this.groupBoxState.Controls.Add(this.listBoxStatus);
            this.groupBoxState.Location = new System.Drawing.Point(2, 312);
            this.groupBoxState.Name = "groupBoxState";
            this.groupBoxState.Size = new System.Drawing.Size(737, 114);
            this.groupBoxState.TabIndex = 4;
            this.groupBoxState.TabStop = false;
            this.groupBoxState.Text = "状态";
            // 
            // listBoxStatus
            // 
            this.listBoxStatus.ItemHeight = 12;
            this.listBoxStatus.Location = new System.Drawing.Point(8, 16);
            this.listBoxStatus.Name = "listBoxStatus";
            this.listBoxStatus.ScrollAlwaysVisible = true;
            this.listBoxStatus.Size = new System.Drawing.Size(723, 88);
            this.listBoxStatus.TabIndex = 17;
            // 
            // FormReceiveMail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(744, 432);
            this.Controls.Add(this.groupBoxState);
            this.Controls.Add(this.groupBoxOperation);
            this.Controls.Add(this.groupBoxMailContent);
            this.Controls.Add(this.groupBoxConnect);
            this.Name = "FormReceiveMail";
            this.Text = "同步接收举例";
            this.groupBoxConnect.ResumeLayout(false);
            this.groupBoxConnect.PerformLayout();
            this.groupBoxMailContent.ResumeLayout(false);
            this.groupBoxOperation.ResumeLayout(false);
            this.groupBoxState.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxConnect;
        private System.Windows.Forms.Button buttonDisconnect;
        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.Label labelPwd;
        private System.Windows.Forms.TextBox textBoxUser;
        private System.Windows.Forms.Label labelUser;
        private System.Windows.Forms.GroupBox groupBoxMailContent;
        private System.Windows.Forms.RichTextBox richTextBoxOriginalMail;
        private System.Windows.Forms.GroupBox groupBoxOperation;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button buttonRead;
        private System.Windows.Forms.ListBox listBoxOperation;
        private System.Windows.Forms.GroupBox groupBoxState;
        private System.Windows.Forms.ListBox listBoxStatus;
        private System.Windows.Forms.TextBox textBoxPOP3Server;
        private System.Windows.Forms.Label label1;
    }
}

