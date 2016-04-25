namespace mycontrol
{
    partial class UserControl1
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

        #region 组件设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.picBox = new System.Windows.Forms.PictureBox();
            this.btmOpen = new System.Windows.Forms.Button();
            this.lblName = new System.Windows.Forms.Label();
            this.lblheight = new System.Windows.Forms.Label();
            this.lblLength = new System.Windows.Forms.Label();
            this.lbldata = new System.Windows.Forms.Label();
            this.lblSize = new System.Windows.Forms.Label();
            this.lblwidth = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picBox)).BeginInit();
            this.SuspendLayout();
            // 
            // picBox
            // 
            this.picBox.Location = new System.Drawing.Point(21, 20);
            this.picBox.Name = "picBox";
            this.picBox.Size = new System.Drawing.Size(356, 307);
            this.picBox.TabIndex = 0;
            this.picBox.TabStop = false;
            // 
            // btmOpen
            // 
            this.btmOpen.Location = new System.Drawing.Point(323, 447);
            this.btmOpen.Name = "btmOpen";
            this.btmOpen.Size = new System.Drawing.Size(75, 23);
            this.btmOpen.TabIndex = 1;
            this.btmOpen.Text = "button1";
            this.btmOpen.UseVisualStyleBackColor = true;
            this.btmOpen.Click += new System.EventHandler(this.btmOpen_Click);
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(36, 345);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(47, 12);
            this.lblName.TabIndex = 2;
            this.lblName.Text = "lblName";
            // 
            // lblheight
            // 
            this.lblheight.AutoSize = true;
            this.lblheight.Location = new System.Drawing.Point(205, 371);
            this.lblheight.Name = "lblheight";
            this.lblheight.Size = new System.Drawing.Size(59, 12);
            this.lblheight.TabIndex = 4;
            this.lblheight.Text = "lblheight";
            // 
            // lblLength
            // 
            this.lblLength.AutoSize = true;
            this.lblLength.Location = new System.Drawing.Point(36, 371);
            this.lblLength.Name = "lblLength";
            this.lblLength.Size = new System.Drawing.Size(59, 12);
            this.lblLength.TabIndex = 5;
            this.lblLength.Text = "lblLength";
            // 
            // lbldata
            // 
            this.lbldata.AutoSize = true;
            this.lbldata.Location = new System.Drawing.Point(205, 396);
            this.lbldata.Name = "lbldata";
            this.lbldata.Size = new System.Drawing.Size(47, 12);
            this.lbldata.TabIndex = 6;
            this.lbldata.Text = "lbldata";
            // 
            // lblSize
            // 
            this.lblSize.AutoSize = true;
            this.lblSize.Location = new System.Drawing.Point(36, 396);
            this.lblSize.Name = "lblSize";
            this.lblSize.Size = new System.Drawing.Size(47, 12);
            this.lblSize.TabIndex = 7;
            this.lblSize.Text = "lblSize";
            // 
            // lblwidth
            // 
            this.lblwidth.AutoSize = true;
            this.lblwidth.Location = new System.Drawing.Point(205, 345);
            this.lblwidth.Name = "lblwidth";
            this.lblwidth.Size = new System.Drawing.Size(53, 12);
            this.lblwidth.TabIndex = 8;
            this.lblwidth.Text = "lblwidth";
            // 
            // UserControl1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblwidth);
            this.Controls.Add(this.lblSize);
            this.Controls.Add(this.lbldata);
            this.Controls.Add(this.lblLength);
            this.Controls.Add(this.lblheight);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.btmOpen);
            this.Controls.Add(this.picBox);
            this.Name = "UserControl1";
            this.Size = new System.Drawing.Size(401, 473);
            ((System.ComponentModel.ISupportInitialize)(this.picBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picBox;
        private System.Windows.Forms.Button btmOpen;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblheight;
        private System.Windows.Forms.Label lblLength;
        private System.Windows.Forms.Label lbldata;
        private System.Windows.Forms.Label lblSize;
        private System.Windows.Forms.Label lblwidth;
    }
}
