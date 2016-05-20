namespace EncryptFileExample
{
    partial class FormEncryptFile
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
            this.buttonImportFromFile = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.richTextBoxDecrypted = new System.Windows.Forms.RichTextBox();
            this.buttonExportKeyInfo = new System.Windows.Forms.Button();
            this.buttonEncrypt = new System.Windows.Forms.Button();
            this.richTextBoxOriginal = new System.Windows.Forms.RichTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonDecrypt = new System.Windows.Forms.Button();
            this.groupBox4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonImportFromFile
            // 
            this.buttonImportFromFile.Location = new System.Drawing.Point(515, 211);
            this.buttonImportFromFile.Name = "buttonImportFromFile";
            this.buttonImportFromFile.Size = new System.Drawing.Size(87, 23);
            this.buttonImportFromFile.TabIndex = 50;
            this.buttonImportFromFile.Text = "导入密钥";
            this.buttonImportFromFile.UseVisualStyleBackColor = true;
            this.buttonImportFromFile.Click += new System.EventHandler(this.buttonImportFromFile_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.richTextBoxDecrypted);
            this.groupBox4.Location = new System.Drawing.Point(385, 21);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(355, 166);
            this.groupBox4.TabIndex = 49;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "解密后的内容";
            // 
            // richTextBoxDecrypted
            // 
            this.richTextBoxDecrypted.Location = new System.Drawing.Point(6, 20);
            this.richTextBoxDecrypted.Name = "richTextBoxDecrypted";
            this.richTextBoxDecrypted.Size = new System.Drawing.Size(336, 133);
            this.richTextBoxDecrypted.TabIndex = 42;
            this.richTextBoxDecrypted.Text = "";
            // 
            // buttonExportKeyInfo
            // 
            this.buttonExportKeyInfo.Location = new System.Drawing.Point(392, 211);
            this.buttonExportKeyInfo.Name = "buttonExportKeyInfo";
            this.buttonExportKeyInfo.Size = new System.Drawing.Size(87, 23);
            this.buttonExportKeyInfo.TabIndex = 51;
            this.buttonExportKeyInfo.Text = "导出密钥";
            this.buttonExportKeyInfo.UseVisualStyleBackColor = true;
            this.buttonExportKeyInfo.Click += new System.EventHandler(this.buttonExportKeyInfo_Click);
            // 
            // buttonEncrypt
            // 
            this.buttonEncrypt.Location = new System.Drawing.Point(146, 211);
            this.buttonEncrypt.Name = "buttonEncrypt";
            this.buttonEncrypt.Size = new System.Drawing.Size(87, 23);
            this.buttonEncrypt.TabIndex = 46;
            this.buttonEncrypt.Text = "加密";
            this.buttonEncrypt.UseVisualStyleBackColor = true;
            this.buttonEncrypt.Click += new System.EventHandler(this.buttonEncrypt_Click);
            // 
            // richTextBoxOriginal
            // 
            this.richTextBoxOriginal.Location = new System.Drawing.Point(6, 20);
            this.richTextBoxOriginal.Name = "richTextBoxOriginal";
            this.richTextBoxOriginal.Size = new System.Drawing.Size(336, 142);
            this.richTextBoxOriginal.TabIndex = 42;
            this.richTextBoxOriginal.Text = "";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.richTextBoxOriginal);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(355, 175);
            this.groupBox1.TabIndex = 47;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "原始内容";
            // 
            // buttonDecrypt
            // 
            this.buttonDecrypt.Location = new System.Drawing.Point(269, 211);
            this.buttonDecrypt.Name = "buttonDecrypt";
            this.buttonDecrypt.Size = new System.Drawing.Size(87, 23);
            this.buttonDecrypt.TabIndex = 45;
            this.buttonDecrypt.Text = "解密";
            this.buttonDecrypt.UseVisualStyleBackColor = true;
            this.buttonDecrypt.Click += new System.EventHandler(this.buttonDecrypt_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(752, 249);
            this.Controls.Add(this.buttonImportFromFile);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.buttonExportKeyInfo);
            this.Controls.Add(this.buttonEncrypt);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonDecrypt);
            this.Name = "Form1";
            this.Text = "文件加密解密";
            this.groupBox4.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonImportFromFile;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RichTextBox richTextBoxDecrypted;
        private System.Windows.Forms.Button buttonExportKeyInfo;
        private System.Windows.Forms.Button buttonEncrypt;
        private System.Windows.Forms.RichTextBox richTextBoxOriginal;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonDecrypt;
    }
}

