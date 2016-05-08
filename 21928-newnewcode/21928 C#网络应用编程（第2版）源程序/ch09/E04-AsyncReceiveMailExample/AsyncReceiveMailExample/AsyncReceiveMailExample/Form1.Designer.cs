namespace AsyncReceiveMailExample
{
    partial class Form1
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
            this.groupBoxOperation = new System.Windows.Forms.GroupBox();
            this.listBoxOperation = new System.Windows.Forms.ListBox();
            this.groupBoxState = new System.Windows.Forms.GroupBox();
            this.listBoxStatus = new System.Windows.Forms.ListBox();
            this.buttonDisconnect = new System.Windows.Forms.Button();
            this.buttonConnect = new System.Windows.Forms.Button();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.labelPwd = new System.Windows.Forms.Label();
            this.textBoxPOP3Server = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBoxConnect = new System.Windows.Forms.GroupBox();
            this.textBoxUser = new System.Windows.Forms.TextBox();
            this.labelUser = new System.Windows.Forms.Label();
            this.groupBoxOperation.SuspendLayout();
            this.groupBoxState.SuspendLayout();
            this.groupBoxConnect.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxOperation
            // 
            this.groupBoxOperation.Controls.Add(this.listBoxOperation);
            this.groupBoxOperation.Location = new System.Drawing.Point(394, 12);
            this.groupBoxOperation.Name = "groupBoxOperation";
            this.groupBoxOperation.Size = new System.Drawing.Size(349, 150);
            this.groupBoxOperation.TabIndex = 8;
            this.groupBoxOperation.TabStop = false;
            this.groupBoxOperation.Text = "邮件信息";
            // 
            // listBoxOperation
            // 
            this.listBoxOperation.ItemHeight = 12;
            this.listBoxOperation.Location = new System.Drawing.Point(8, 24);
            this.listBoxOperation.Name = "listBoxOperation";
            this.listBoxOperation.ScrollAlwaysVisible = true;
            this.listBoxOperation.Size = new System.Drawing.Size(331, 112);
            this.listBoxOperation.TabIndex = 0;
            // 
            // groupBoxState
            // 
            this.groupBoxState.Controls.Add(this.listBoxStatus);
            this.groupBoxState.Location = new System.Drawing.Point(12, 177);
            this.groupBoxState.Name = "groupBoxState";
            this.groupBoxState.Size = new System.Drawing.Size(731, 114);
            this.groupBoxState.TabIndex = 9;
            this.groupBoxState.TabStop = false;
            this.groupBoxState.Text = "状态";
            // 
            // listBoxStatus
            // 
            this.listBoxStatus.ItemHeight = 12;
            this.listBoxStatus.Location = new System.Drawing.Point(8, 16);
            this.listBoxStatus.Name = "listBoxStatus";
            this.listBoxStatus.ScrollAlwaysVisible = true;
            this.listBoxStatus.Size = new System.Drawing.Size(713, 88);
            this.listBoxStatus.TabIndex = 17;
            // 
            // buttonDisconnect
            // 
            this.buttonDisconnect.Enabled = false;
            this.buttonDisconnect.Location = new System.Drawing.Point(203, 112);
            this.buttonDisconnect.Name = "buttonDisconnect";
            this.buttonDisconnect.Size = new System.Drawing.Size(72, 24);
            this.buttonDisconnect.TabIndex = 7;
            this.buttonDisconnect.Text = "断开连接";
            this.buttonDisconnect.Click += new System.EventHandler(this.buttonDisconnect_Click);
            // 
            // buttonConnect
            // 
            this.buttonConnect.Location = new System.Drawing.Point(91, 112);
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
            this.textBoxPassword.Size = new System.Drawing.Size(240, 21);
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
            this.textBoxPOP3Server.Size = new System.Drawing.Size(240, 21);
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
            this.groupBoxConnect.Location = new System.Drawing.Point(12, 12);
            this.groupBoxConnect.Name = "groupBoxConnect";
            this.groupBoxConnect.Size = new System.Drawing.Size(349, 150);
            this.groupBoxConnect.TabIndex = 5;
            this.groupBoxConnect.TabStop = false;
            this.groupBoxConnect.Text = "连接操作";
            // 
            // textBoxUser
            // 
            this.textBoxUser.Location = new System.Drawing.Point(75, 46);
            this.textBoxUser.Name = "textBoxUser";
            this.textBoxUser.Size = new System.Drawing.Size(240, 21);
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
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(762, 303);
            this.Controls.Add(this.groupBoxOperation);
            this.Controls.Add(this.groupBoxState);
            this.Controls.Add(this.groupBoxConnect);
            this.Name = "Form1";
            this.Text = "异步接收举例";
            this.groupBoxOperation.ResumeLayout(false);
            this.groupBoxState.ResumeLayout(false);
            this.groupBoxConnect.ResumeLayout(false);
            this.groupBoxConnect.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxOperation;
        private System.Windows.Forms.ListBox listBoxOperation;
        private System.Windows.Forms.GroupBox groupBoxState;
        private System.Windows.Forms.ListBox listBoxStatus;
        private System.Windows.Forms.Button buttonDisconnect;
        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.Label labelPwd;
        private System.Windows.Forms.TextBox textBoxPOP3Server;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBoxConnect;
        private System.Windows.Forms.TextBox textBoxUser;
        private System.Windows.Forms.Label labelUser;
    }
}

