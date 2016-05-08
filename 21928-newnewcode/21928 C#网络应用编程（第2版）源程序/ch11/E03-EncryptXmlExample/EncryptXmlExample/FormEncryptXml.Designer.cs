namespace EncryptXmlExample
{
    partial class FormEncryptXml
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
            this.richTextBoxDecrypt = new System.Windows.Forms.RichTextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.richTextBoxEncrypt = new System.Windows.Forms.RichTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.richTextBoxOriFile = new System.Windows.Forms.RichTextBox();
            this.buttonDecrypt = new System.Windows.Forms.Button();
            this.buttonEncrypt = new System.Windows.Forms.Button();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // richTextBoxDecrypt
            // 
            this.richTextBoxDecrypt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxDecrypt.Location = new System.Drawing.Point(3, 17);
            this.richTextBoxDecrypt.Name = "richTextBoxDecrypt";
            this.richTextBoxDecrypt.Size = new System.Drawing.Size(299, 153);
            this.richTextBoxDecrypt.TabIndex = 24;
            this.richTextBoxDecrypt.Text = "";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.richTextBoxDecrypt);
            this.groupBox3.Location = new System.Drawing.Point(307, 9);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(305, 173);
            this.groupBox3.TabIndex = 28;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "解密后的文件内容（文件名：test2.xml）";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.richTextBoxEncrypt);
            this.groupBox2.Location = new System.Drawing.Point(11, 220);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(601, 205);
            this.groupBox2.TabIndex = 29;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "加密后的文件内容（文件名：test1.xml）";
            // 
            // richTextBoxEncrypt
            // 
            this.richTextBoxEncrypt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxEncrypt.Location = new System.Drawing.Point(3, 17);
            this.richTextBoxEncrypt.Name = "richTextBoxEncrypt";
            this.richTextBoxEncrypt.Size = new System.Drawing.Size(595, 185);
            this.richTextBoxEncrypt.TabIndex = 24;
            this.richTextBoxEncrypt.Text = "";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.richTextBoxOriFile);
            this.groupBox1.Location = new System.Drawing.Point(14, 9);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(270, 176);
            this.groupBox1.TabIndex = 30;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "原始文件内容（文件名：test.xml）";
            // 
            // richTextBoxOriFile
            // 
            this.richTextBoxOriFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxOriFile.Location = new System.Drawing.Point(3, 17);
            this.richTextBoxOriFile.Name = "richTextBoxOriFile";
            this.richTextBoxOriFile.Size = new System.Drawing.Size(264, 156);
            this.richTextBoxOriFile.TabIndex = 24;
            this.richTextBoxOriFile.Text = "";
            // 
            // buttonDecrypt
            // 
            this.buttonDecrypt.Location = new System.Drawing.Point(352, 191);
            this.buttonDecrypt.Name = "buttonDecrypt";
            this.buttonDecrypt.Size = new System.Drawing.Size(128, 23);
            this.buttonDecrypt.TabIndex = 26;
            this.buttonDecrypt.Text = "解密";
            this.buttonDecrypt.UseVisualStyleBackColor = true;
            this.buttonDecrypt.Click += new System.EventHandler(this.buttonDecrypt_Click);
            // 
            // buttonEncrypt
            // 
            this.buttonEncrypt.Location = new System.Drawing.Point(156, 191);
            this.buttonEncrypt.Name = "buttonEncrypt";
            this.buttonEncrypt.Size = new System.Drawing.Size(128, 23);
            this.buttonEncrypt.TabIndex = 27;
            this.buttonEncrypt.Text = "加密";
            this.buttonEncrypt.UseVisualStyleBackColor = true;
            this.buttonEncrypt.Click += new System.EventHandler(this.buttonEncrypt_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(631, 437);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonDecrypt);
            this.Controls.Add(this.buttonEncrypt);
            this.Name = "Form1";
            this.Text = "Form1";
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBoxDecrypt;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RichTextBox richTextBoxEncrypt;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RichTextBox richTextBoxOriFile;
        private System.Windows.Forms.Button buttonDecrypt;
        private System.Windows.Forms.Button buttonEncrypt;
    }
}

