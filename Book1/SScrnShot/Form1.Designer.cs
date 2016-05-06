namespace SScrnShot
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
            this.components = new System.ComponentModel.Container();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.flashToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gifToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.delay5sToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.capToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmi_exit = new System.Windows.Forms.ToolStripMenuItem();
            this.lbl_CutImage = new System.Windows.Forms.Label();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.BalloonTipText = "xxxx";
            this.notifyIcon1.BalloonTipTitle = "x1x";
            this.notifyIcon1.Text = "jitugongju";
            this.notifyIcon1.Visible = true;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuToolStripMenuItem,
            this.flashToolStripMenuItem,
            this.gifToolStripMenuItem,
            this.delay5sToolStripMenuItem,
            this.capToolStripMenuItem,
            this.settingToolStripMenuItem,
            this.toolStripMenuItem1,
            this.tsmi_exit});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(125, 164);
            // 
            // menuToolStripMenuItem
            // 
            this.menuToolStripMenuItem.Name = "menuToolStripMenuItem";
            this.menuToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.menuToolStripMenuItem.Text = "menu";
            // 
            // flashToolStripMenuItem
            // 
            this.flashToolStripMenuItem.Name = "flashToolStripMenuItem";
            this.flashToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.flashToolStripMenuItem.Text = "flash";
            // 
            // gifToolStripMenuItem
            // 
            this.gifToolStripMenuItem.Name = "gifToolStripMenuItem";
            this.gifToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.gifToolStripMenuItem.Text = "gif";
            // 
            // delay5sToolStripMenuItem
            // 
            this.delay5sToolStripMenuItem.Name = "delay5sToolStripMenuItem";
            this.delay5sToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.delay5sToolStripMenuItem.Text = "delay 5s";
            // 
            // capToolStripMenuItem
            // 
            this.capToolStripMenuItem.Name = "capToolStripMenuItem";
            this.capToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.capToolStripMenuItem.Text = "cap";
            // 
            // settingToolStripMenuItem
            // 
            this.settingToolStripMenuItem.Name = "settingToolStripMenuItem";
            this.settingToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.settingToolStripMenuItem.Text = "setting";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(121, 6);
            // 
            // tsmi_exit
            // 
            this.tsmi_exit.Name = "tsmi_exit";
            this.tsmi_exit.Size = new System.Drawing.Size(124, 22);
            this.tsmi_exit.Text = "exit";
            this.tsmi_exit.Click += new System.EventHandler(this.tsmi_exit_Click);
            // 
            // lbl_CutImage
            // 
            this.lbl_CutImage.AutoSize = true;
            this.lbl_CutImage.Location = new System.Drawing.Point(97, 109);
            this.lbl_CutImage.Name = "lbl_CutImage";
            this.lbl_CutImage.Size = new System.Drawing.Size(41, 12);
            this.lbl_CutImage.TabIndex = 1;
            this.lbl_CutImage.Text = "label1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.lbl_CutImage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.TopMost = true;
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem flashToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gifToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem delay5sToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem capToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem tsmi_exit;
        private System.Windows.Forms.Label lbl_CutImage;
    }
}

