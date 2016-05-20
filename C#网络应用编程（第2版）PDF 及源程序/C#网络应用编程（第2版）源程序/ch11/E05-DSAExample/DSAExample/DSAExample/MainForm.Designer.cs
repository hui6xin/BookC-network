namespace DSAExample
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
            this.buttonVerify = new System.Windows.Forms.Button();
            this.textBoxVerifyResult = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxHashValue = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxVerifyHashValue = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonOK = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonVerify
            // 
            this.buttonVerify.Location = new System.Drawing.Point(356, 210);
            this.buttonVerify.Name = "buttonVerify";
            this.buttonVerify.Size = new System.Drawing.Size(88, 23);
            this.buttonVerify.TabIndex = 17;
            this.buttonVerify.Text = "验证Hash";
            this.buttonVerify.UseVisualStyleBackColor = true;
            this.buttonVerify.Click += new System.EventHandler(this.buttonVerify_Click);
            // 
            // textBoxVerifyResult
            // 
            this.textBoxVerifyResult.Location = new System.Drawing.Point(123, 267);
            this.textBoxVerifyResult.Name = "textBoxVerifyResult";
            this.textBoxVerifyResult.Size = new System.Drawing.Size(479, 21);
            this.textBoxVerifyResult.TabIndex = 18;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(28, 270);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 16;
            this.label3.Text = "验证结果";
            // 
            // textBoxHashValue
            // 
            this.textBoxHashValue.Location = new System.Drawing.Point(111, 20);
            this.textBoxHashValue.Name = "textBoxHashValue";
            this.textBoxHashValue.Size = new System.Drawing.Size(479, 21);
            this.textBoxHashValue.TabIndex = 19;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 15;
            this.label4.Text = "Hash值";
            // 
            // textBoxVerifyHashValue
            // 
            this.textBoxVerifyHashValue.Location = new System.Drawing.Point(111, 61);
            this.textBoxVerifyHashValue.Multiline = true;
            this.textBoxVerifyHashValue.Name = "textBoxVerifyHashValue";
            this.textBoxVerifyHashValue.Size = new System.Drawing.Size(479, 86);
            this.textBoxVerifyHashValue.TabIndex = 21;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 12;
            this.label2.Text = "签名的Hash值";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBoxVerifyHashValue);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.textBoxHashValue);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(596, 169);
            this.groupBox1.TabIndex = 22;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "发送方";
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(185, 210);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(88, 23);
            this.buttonOK.TabIndex = 17;
            this.buttonOK.Text = "生成Hash";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(620, 317);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonVerify);
            this.Controls.Add(this.textBoxVerifyResult);
            this.Controls.Add(this.label3);
            this.Name = "Form1";
            this.Text = "数字签名举例";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonVerify;
        private System.Windows.Forms.TextBox textBoxVerifyResult;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxHashValue;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxVerifyHashValue;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonOK;
    }
}

