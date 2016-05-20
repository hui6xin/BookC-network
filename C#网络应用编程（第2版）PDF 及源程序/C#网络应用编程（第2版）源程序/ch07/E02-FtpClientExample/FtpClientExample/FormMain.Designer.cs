namespace FtpClientExample
{
    partial class FormMain
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
            this.listBoxFtp = new System.Windows.Forms.ListBox();
            this.textBoxSelectedFile = new System.Windows.Forms.TextBox();
            this.listBoxInfo = new System.Windows.Forms.ListBox();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonUpload = new System.Windows.Forms.Button();
            this.buttonDownload = new System.Windows.Forms.Button();
            this.textBoxUserName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxServer = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBoxFile = new System.Windows.Forms.GroupBox();
            this.groupBoxInfo = new System.Windows.Forms.GroupBox();
            this.buttonLogin = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBoxFile.SuspendLayout();
            this.groupBoxInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // listBoxFtp
            // 
            this.listBoxFtp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxFtp.FormattingEnabled = true;
            this.listBoxFtp.ItemHeight = 12;
            this.listBoxFtp.Location = new System.Drawing.Point(3, 17);
            this.listBoxFtp.Name = "listBoxFtp";
            this.listBoxFtp.Size = new System.Drawing.Size(338, 124);
            this.listBoxFtp.TabIndex = 0;
            this.listBoxFtp.SelectedIndexChanged += new System.EventHandler(this.listBoxFtp_SelectedIndexChanged);
            this.listBoxFtp.DoubleClick += new System.EventHandler(this.listBoxFtp_DoubleClick);
            // 
            // textBoxSelectedFile
            // 
            this.textBoxSelectedFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSelectedFile.Location = new System.Drawing.Point(364, 117);
            this.textBoxSelectedFile.Name = "textBoxSelectedFile";
            this.textBoxSelectedFile.ReadOnly = true;
            this.textBoxSelectedFile.Size = new System.Drawing.Size(181, 21);
            this.textBoxSelectedFile.TabIndex = 44;
            // 
            // listBoxInfo
            // 
            this.listBoxInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxInfo.ItemHeight = 12;
            this.listBoxInfo.Location = new System.Drawing.Point(3, 17);
            this.listBoxInfo.Name = "listBoxInfo";
            this.listBoxInfo.Size = new System.Drawing.Size(523, 100);
            this.listBoxInfo.TabIndex = 1;
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Location = new System.Drawing.Point(379, 10);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.PasswordChar = '*';
            this.textBoxPassword.Size = new System.Drawing.Size(70, 21);
            this.textBoxPassword.TabIndex = 41;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(349, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 40;
            this.label3.Text = "密码";
            // 
            // buttonUpload
            // 
            this.buttonUpload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonUpload.Location = new System.Drawing.Point(385, 71);
            this.buttonUpload.Name = "buttonUpload";
            this.buttonUpload.Size = new System.Drawing.Size(64, 23);
            this.buttonUpload.TabIndex = 31;
            this.buttonUpload.Text = "上传文件";
            this.buttonUpload.Click += new System.EventHandler(this.buttonUpload_Click);
            // 
            // buttonDownload
            // 
            this.buttonDownload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDownload.Location = new System.Drawing.Point(470, 71);
            this.buttonDownload.Name = "buttonDownload";
            this.buttonDownload.Size = new System.Drawing.Size(64, 23);
            this.buttonDownload.TabIndex = 32;
            this.buttonDownload.Text = "下载文件";
            this.buttonDownload.Click += new System.EventHandler(this.buttonDownload_Click);
            // 
            // textBoxUserName
            // 
            this.textBoxUserName.Location = new System.Drawing.Point(241, 10);
            this.textBoxUserName.Name = "textBoxUserName";
            this.textBoxUserName.Size = new System.Drawing.Size(97, 21);
            this.textBoxUserName.TabIndex = 42;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(359, 105);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 39;
            this.label4.Text = "所选文件";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(199, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 38;
            this.label2.Text = "用户名";
            // 
            // textBoxServer
            // 
            this.textBoxServer.Location = new System.Drawing.Point(71, 10);
            this.textBoxServer.Name = "textBoxServer";
            this.textBoxServer.Size = new System.Drawing.Size(117, 21);
            this.textBoxServer.TabIndex = 43;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 37;
            this.label1.Text = "服务器IP";
            // 
            // groupBoxFile
            // 
            this.groupBoxFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxFile.Controls.Add(this.listBoxFtp);
            this.groupBoxFile.Location = new System.Drawing.Point(14, 46);
            this.groupBoxFile.Name = "groupBoxFile";
            this.groupBoxFile.Size = new System.Drawing.Size(344, 155);
            this.groupBoxFile.TabIndex = 35;
            this.groupBoxFile.TabStop = false;
            this.groupBoxFile.Text = "服务器目录和文件列表";
            // 
            // groupBoxInfo
            // 
            this.groupBoxInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxInfo.Controls.Add(this.listBoxInfo);
            this.groupBoxInfo.Location = new System.Drawing.Point(14, 207);
            this.groupBoxInfo.Name = "groupBoxInfo";
            this.groupBoxInfo.Size = new System.Drawing.Size(529, 123);
            this.groupBoxInfo.TabIndex = 36;
            this.groupBoxInfo.TabStop = false;
            this.groupBoxInfo.Text = "服务器返回信息";
            // 
            // buttonLogin
            // 
            this.buttonLogin.Location = new System.Drawing.Point(470, 10);
            this.buttonLogin.Name = "buttonLogin";
            this.buttonLogin.Size = new System.Drawing.Size(63, 23);
            this.buttonLogin.TabIndex = 33;
            this.buttonLogin.Text = "登录";
            this.buttonLogin.Click += new System.EventHandler(this.buttonLogin_Click);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(364, 155);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(181, 32);
            this.label5.TabIndex = 37;
            this.label5.Text = "提示：请双击“返回上层目录”或者双击“目录”改变当前目录";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(555, 342);
            this.Controls.Add(this.textBoxSelectedFile);
            this.Controls.Add(this.textBoxPassword);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.buttonUpload);
            this.Controls.Add(this.buttonDownload);
            this.Controls.Add(this.textBoxUserName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxServer);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBoxFile);
            this.Controls.Add(this.groupBoxInfo);
            this.Controls.Add(this.buttonLogin);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FTP客户端举例";
            this.groupBoxFile.ResumeLayout(false);
            this.groupBoxInfo.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxFtp;
        private System.Windows.Forms.TextBox textBoxSelectedFile;
        private System.Windows.Forms.ListBox listBoxInfo;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonUpload;
        private System.Windows.Forms.Button buttonDownload;
        private System.Windows.Forms.TextBox textBoxUserName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxServer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBoxFile;
        private System.Windows.Forms.GroupBox groupBoxInfo;
        private System.Windows.Forms.Button buttonLogin;
        private System.Windows.Forms.Label label5;

    }
}

