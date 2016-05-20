namespace ListNeighber
{
    partial class FormListNeighber
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
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnPeerName = new System.Windows.Forms.ColumnHeader();
            this.columnIPEndPoint = new System.Windows.Forms.ColumnHeader();
            this.columnInfo = new System.Windows.Forms.ColumnHeader();
            this.columnHostName = new System.Windows.Forms.ColumnHeader();
            this.buttonRefresh = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnPeerName,
            this.columnIPEndPoint,
            this.columnInfo,
            this.columnHostName});
            this.listView1.Location = new System.Drawing.Point(12, 8);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(872, 176);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Width = 0;
            // 
            // columnPeerName
            // 
            this.columnPeerName.Text = "对等名称";
            this.columnPeerName.Width = 180;
            // 
            // columnIPEndPoint
            // 
            this.columnIPEndPoint.Text = "端点";
            this.columnIPEndPoint.Width = 180;
            // 
            // columnInfo
            // 
            this.columnInfo.Text = "描述信息";
            this.columnInfo.Width = 250;
            // 
            // columnHostName
            // 
            this.columnHostName.Text = "主机名";
            this.columnHostName.Width = 250;
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.Location = new System.Drawing.Point(394, 190);
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.Size = new System.Drawing.Size(75, 23);
            this.buttonRefresh.TabIndex = 1;
            this.buttonRefresh.Text = "刷新";
            this.buttonRefresh.UseVisualStyleBackColor = true;
            this.buttonRefresh.Click += new System.EventHandler(this.buttonRefresh_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(887, 225);
            this.Controls.Add(this.buttonRefresh);
            this.Controls.Add(this.listView1);
            this.Name = "Form1";
            this.Text = "我的Peer邻居";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnPeerName;
        private System.Windows.Forms.ColumnHeader columnIPEndPoint;
        private System.Windows.Forms.ColumnHeader columnInfo;
        private System.Windows.Forms.Button buttonRefresh;
        private System.Windows.Forms.ColumnHeader columnHostName;
        private System.Windows.Forms.ColumnHeader columnHeader1;
    }
}

