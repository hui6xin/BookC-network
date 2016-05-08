namespace EncoderDecoderExample
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
            this.textBoxOldText = new System.Windows.Forms.TextBox();
            this.textBoxEncoder = new System.Windows.Forms.TextBox();
            this.textBoxDecoder = new System.Windows.Forms.TextBox();
            this.lable1 = new System.Windows.Forms.Label();
            this.labelafter = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonRun = new System.Windows.Forms.Button();
            this.comboBoxType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBoxOldText
            // 
            this.textBoxOldText.Location = new System.Drawing.Point(71, 51);
            this.textBoxOldText.Multiline = true;
            this.textBoxOldText.Name = "textBoxOldText";
            this.textBoxOldText.Size = new System.Drawing.Size(261, 43);
            this.textBoxOldText.TabIndex = 2;
            // 
            // textBoxEncoder
            // 
            this.textBoxEncoder.Location = new System.Drawing.Point(71, 109);
            this.textBoxEncoder.Multiline = true;
            this.textBoxEncoder.Name = "textBoxEncoder";
            this.textBoxEncoder.Size = new System.Drawing.Size(261, 45);
            this.textBoxEncoder.TabIndex = 3;
            // 
            // textBoxDecoder
            // 
            this.textBoxDecoder.Location = new System.Drawing.Point(71, 168);
            this.textBoxDecoder.Multiline = true;
            this.textBoxDecoder.Name = "textBoxDecoder";
            this.textBoxDecoder.Size = new System.Drawing.Size(261, 52);
            this.textBoxDecoder.TabIndex = 4;
            // 
            // lable1
            // 
            this.lable1.AutoSize = true;
            this.lable1.Location = new System.Drawing.Point(10, 51);
            this.lable1.Name = "lable1";
            this.lable1.Size = new System.Drawing.Size(41, 12);
            this.lable1.TabIndex = 3;
            this.lable1.Text = "编码前";
            // 
            // labelafter
            // 
            this.labelafter.AutoSize = true;
            this.labelafter.Location = new System.Drawing.Point(10, 106);
            this.labelafter.Name = "labelafter";
            this.labelafter.Size = new System.Drawing.Size(41, 12);
            this.labelafter.TabIndex = 4;
            this.labelafter.Text = "编码后";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 168);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "解码后";
            // 
            // buttonRun
            // 
            this.buttonRun.Location = new System.Drawing.Point(117, 235);
            this.buttonRun.Name = "buttonRun";
            this.buttonRun.Size = new System.Drawing.Size(75, 23);
            this.buttonRun.TabIndex = 0;
            this.buttonRun.Text = "编码/解码";
            this.buttonRun.UseVisualStyleBackColor = true;
            this.buttonRun.Click += new System.EventHandler(this.buttonRun_Click);
            // 
            // comboBoxType
            // 
            this.comboBoxType.FormattingEnabled = true;
            this.comboBoxType.Location = new System.Drawing.Point(71, 20);
            this.comboBoxType.Name = "comboBoxType";
            this.comboBoxType.Size = new System.Drawing.Size(261, 20);
            this.comboBoxType.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "编码类型";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(352, 266);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBoxType);
            this.Controls.Add(this.buttonRun);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.labelafter);
            this.Controls.Add(this.lable1);
            this.Controls.Add(this.textBoxDecoder);
            this.Controls.Add(this.textBoxEncoder);
            this.Controls.Add(this.textBoxOldText);
            this.Name = "MainForm";
            this.Text = "编码/解码";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxOldText;
        private System.Windows.Forms.TextBox textBoxEncoder;
        private System.Windows.Forms.TextBox textBoxDecoder;
        private System.Windows.Forms.Label lable1;
        private System.Windows.Forms.Label labelafter;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonRun;
        private System.Windows.Forms.ComboBox comboBoxType;
        private System.Windows.Forms.Label label1;
    }
}

