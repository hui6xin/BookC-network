namespace GameServer
{
    partial class FormServer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonStart = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxMaxTables = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxMaxUsers = new System.Windows.Forms.TextBox();
            this.buttonStop = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(306, 229);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(78, 23);
            this.buttonStart.TabIndex = 2;
            this.buttonStart.Text = "启动服务";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(12, 12);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(505, 196);
            this.listBox1.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 250);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(203, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "游戏室同时开出的游戏桌数(1-100)：";
            // 
            // textBoxMaxTables
            // 
            this.textBoxMaxTables.Location = new System.Drawing.Point(242, 245);
            this.textBoxMaxTables.MaxLength = 4;
            this.textBoxMaxTables.Name = "textBoxMaxTables";
            this.textBoxMaxTables.Size = new System.Drawing.Size(39, 21);
            this.textBoxMaxTables.TabIndex = 6;
            this.textBoxMaxTables.Text = "5";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(31, 222);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(203, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "游戏室允许进入的最多人数(1-300)：";
            // 
            // textBoxMaxUsers
            // 
            this.textBoxMaxUsers.Location = new System.Drawing.Point(242, 217);
            this.textBoxMaxUsers.MaxLength = 4;
            this.textBoxMaxUsers.Name = "textBoxMaxUsers";
            this.textBoxMaxUsers.Size = new System.Drawing.Size(39, 21);
            this.textBoxMaxUsers.TabIndex = 6;
            this.textBoxMaxUsers.Text = "15";
            // 
            // buttonStop
            // 
            this.buttonStop.Location = new System.Drawing.Point(405, 229);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(78, 23);
            this.buttonStop.TabIndex = 2;
            this.buttonStop.Text = "停止服务";
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // FormServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(529, 271);
            this.Controls.Add(this.textBoxMaxUsers);
            this.Controls.Add(this.textBoxMaxTables);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.buttonStop);
            this.Controls.Add(this.buttonStart);
            this.Name = "FormServer";
            this.Text = "服务器端";
            this.Load += new System.EventHandler(this.FormServer_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormDDServer_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxMaxTables;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxMaxUsers;
        private System.Windows.Forms.Button buttonStop;
    }
}

