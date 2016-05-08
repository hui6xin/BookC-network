namespace EncryptStringExample
{
    partial class FormEncryptString
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
            this.buttonImportKey = new System.Windows.Forms.Button();
            this.buttonExportKey = new System.Windows.Forms.Button();
            this.buttonDecrypt = new System.Windows.Forms.Button();
            this.buttonEncrypt = new System.Windows.Forms.Button();
            this.textBoxDecrypted = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxEncrypted = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxInput = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonImportKey
            // 
            this.buttonImportKey.Location = new System.Drawing.Point(317, 138);
            this.buttonImportKey.Name = "buttonImportKey";
            this.buttonImportKey.Size = new System.Drawing.Size(69, 23);
            this.buttonImportKey.TabIndex = 34;
            this.buttonImportKey.Text = "导入密钥";
            this.buttonImportKey.UseVisualStyleBackColor = true;
            this.buttonImportKey.Click += new System.EventHandler(this.buttonImportKey_Click);
            // 
            // buttonExportKey
            // 
            this.buttonExportKey.Location = new System.Drawing.Point(223, 138);
            this.buttonExportKey.Name = "buttonExportKey";
            this.buttonExportKey.Size = new System.Drawing.Size(69, 23);
            this.buttonExportKey.TabIndex = 35;
            this.buttonExportKey.Text = "导出密钥";
            this.buttonExportKey.UseVisualStyleBackColor = true;
            this.buttonExportKey.Click += new System.EventHandler(this.buttonExportKey_Click);
            // 
            // buttonDecrypt
            // 
            this.buttonDecrypt.Location = new System.Drawing.Point(130, 138);
            this.buttonDecrypt.Name = "buttonDecrypt";
            this.buttonDecrypt.Size = new System.Drawing.Size(58, 23);
            this.buttonDecrypt.TabIndex = 36;
            this.buttonDecrypt.Text = "解密";
            this.buttonDecrypt.UseVisualStyleBackColor = true;
            this.buttonDecrypt.Click += new System.EventHandler(this.buttonDecrypt_Click);
            // 
            // buttonEncrypt
            // 
            this.buttonEncrypt.Location = new System.Drawing.Point(42, 138);
            this.buttonEncrypt.Name = "buttonEncrypt";
            this.buttonEncrypt.Size = new System.Drawing.Size(60, 23);
            this.buttonEncrypt.TabIndex = 37;
            this.buttonEncrypt.Text = "加密";
            this.buttonEncrypt.UseVisualStyleBackColor = true;
            this.buttonEncrypt.Click += new System.EventHandler(this.buttonEncrypt_Click);
            // 
            // textBoxDecrypted
            // 
            this.textBoxDecrypted.Location = new System.Drawing.Point(108, 95);
            this.textBoxDecrypted.Name = "textBoxDecrypted";
            this.textBoxDecrypted.Size = new System.Drawing.Size(311, 21);
            this.textBoxDecrypted.TabIndex = 41;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 98);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 12);
            this.label3.TabIndex = 40;
            this.label3.Text = "解密后的字符串";
            // 
            // textBoxEncrypted
            // 
            this.textBoxEncrypted.Location = new System.Drawing.Point(108, 55);
            this.textBoxEncrypted.Name = "textBoxEncrypted";
            this.textBoxEncrypted.Size = new System.Drawing.Size(311, 21);
            this.textBoxEncrypted.TabIndex = 39;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 12);
            this.label2.TabIndex = 38;
            this.label2.Text = "加密后的字符串";
            // 
            // textBoxInput
            // 
            this.textBoxInput.Location = new System.Drawing.Point(108, 16);
            this.textBoxInput.Name = "textBoxInput";
            this.textBoxInput.Size = new System.Drawing.Size(311, 21);
            this.textBoxInput.TabIndex = 32;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 33;
            this.label1.Text = "原始字符串";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(432, 182);
            this.Controls.Add(this.buttonImportKey);
            this.Controls.Add(this.buttonExportKey);
            this.Controls.Add(this.buttonDecrypt);
            this.Controls.Add(this.buttonEncrypt);
            this.Controls.Add(this.textBoxDecrypted);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxEncrypted);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxInput);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "字符串加密解密";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonImportKey;
        private System.Windows.Forms.Button buttonExportKey;
        private System.Windows.Forms.Button buttonDecrypt;
        private System.Windows.Forms.Button buttonEncrypt;
        private System.Windows.Forms.TextBox textBoxDecrypted;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxEncrypted;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxInput;
        private System.Windows.Forms.Label label1;
    }
}

