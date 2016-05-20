namespace IPExample
{
    partial class MainForm
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
            this.buttonLocalIP = new System.Windows.Forms.Button();
            this.buttonRemoteIP = new System.Windows.Forms.Button();
            this.listBoxLocalInfo = new System.Windows.Forms.ListBox();
            this.textBoxRmoteIP = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.listBoxRemoteInfo = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // buttonLocalIP
            // 
            this.buttonLocalIP.Location = new System.Drawing.Point(64, 243);
            this.buttonLocalIP.Name = "buttonLocalIP";
            this.buttonLocalIP.Size = new System.Drawing.Size(159, 30);
            this.buttonLocalIP.TabIndex = 0;
            this.buttonLocalIP.Text = "显示本机IP信息";
            this.buttonLocalIP.UseVisualStyleBackColor = true;
            this.buttonLocalIP.Click += new System.EventHandler(this.buttonLocalIP_Click);
            // 
            // buttonRemoteIP
            // 
            this.buttonRemoteIP.Location = new System.Drawing.Point(506, 243);
            this.buttonRemoteIP.Name = "buttonRemoteIP";
            this.buttonRemoteIP.Size = new System.Drawing.Size(126, 29);
            this.buttonRemoteIP.TabIndex = 1;
            this.buttonRemoteIP.Text = "显示服务器信息";
            this.buttonRemoteIP.UseVisualStyleBackColor = true;
            this.buttonRemoteIP.Click += new System.EventHandler(this.buttonRemoteIP_Click);
            // 
            // listBoxLocalInfo
            // 
            this.listBoxLocalInfo.FormattingEnabled = true;
            this.listBoxLocalInfo.ItemHeight = 12;
            this.listBoxLocalInfo.Location = new System.Drawing.Point(21, 12);
            this.listBoxLocalInfo.Name = "listBoxLocalInfo";
            this.listBoxLocalInfo.Size = new System.Drawing.Size(249, 208);
            this.listBoxLocalInfo.TabIndex = 2;
            // 
            // textBoxRmoteIP
            // 
            this.textBoxRmoteIP.Location = new System.Drawing.Point(356, 243);
            this.textBoxRmoteIP.Multiline = true;
            this.textBoxRmoteIP.Name = "textBoxRmoteIP";
            this.textBoxRmoteIP.Size = new System.Drawing.Size(144, 29);
            this.textBoxRmoteIP.TabIndex = 3;
            this.textBoxRmoteIP.Text = "www.cctv.com";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(297, 252);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "服务器：";
            // 
            // listBoxRemoteInfo
            // 
            this.listBoxRemoteInfo.FormattingEnabled = true;
            this.listBoxRemoteInfo.ItemHeight = 12;
            this.listBoxRemoteInfo.Location = new System.Drawing.Point(299, 12);
            this.listBoxRemoteInfo.Name = "listBoxRemoteInfo";
            this.listBoxRemoteInfo.Size = new System.Drawing.Size(333, 208);
            this.listBoxRemoteInfo.TabIndex = 5;
            // 
            // FormIPExample
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(644, 285);
            this.Controls.Add(this.listBoxRemoteInfo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxRmoteIP);
            this.Controls.Add(this.listBoxLocalInfo);
            this.Controls.Add(this.buttonRemoteIP);
            this.Controls.Add(this.buttonLocalIP);
            this.Name = "FormIPExample";
            this.Text = "IPAddress类、Dns类、IPHostEntry类和IPEndPoint类的用法";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonLocalIP;
        private System.Windows.Forms.Button buttonRemoteIP;
        private System.Windows.Forms.ListBox listBoxLocalInfo;
        private System.Windows.Forms.TextBox textBoxRmoteIP;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox listBoxRemoteInfo;
    }
}

