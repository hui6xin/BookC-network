namespace PnrpCloud
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
            this.textBoxCloud = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // textBoxCloud
            // 
            this.textBoxCloud.Location = new System.Drawing.Point(2, 3);
            this.textBoxCloud.Multiline = true;
            this.textBoxCloud.Name = "textBoxCloud";
            this.textBoxCloud.Size = new System.Drawing.Size(407, 240);
            this.textBoxCloud.TabIndex = 0;
            // 
            // FormCloud
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(409, 246);
            this.Controls.Add(this.textBoxCloud);
            this.Name = "FormCloud";
            this.Text = "群信息";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxCloud;


    }
}

