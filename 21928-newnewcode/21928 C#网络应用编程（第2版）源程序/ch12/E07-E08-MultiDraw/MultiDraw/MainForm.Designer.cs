namespace MultiDraw
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabelUserState = new System.Windows.Forms.ToolStripLabel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonGraphics = new System.Windows.Forms.Button();
            this.buttonCurve = new System.Windows.Forms.Button();
            this.buttonText = new System.Windows.Forms.Button();
            this.buttonRectangle = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.编辑ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.全部选择ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.导出图片ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.打印预览ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabelUserState});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(688, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabelUserState
            // 
            this.toolStripLabelUserState.Name = "toolStripLabelUserState";
            this.toolStripLabelUserState.Size = new System.Drawing.Size(143, 22);
            this.toolStripLabelUserState.Text = "toolStripLabelUserState";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 406);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(688, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonGraphics);
            this.panel1.Controls.Add(this.buttonCurve);
            this.panel1.Controls.Add(this.buttonText);
            this.panel1.Controls.Add(this.buttonRectangle);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 49);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(139, 357);
            this.panel1.TabIndex = 2;
            // 
            // buttonGraphics
            // 
            this.buttonGraphics.Location = new System.Drawing.Point(31, 246);
            this.buttonGraphics.Name = "buttonGraphics";
            this.buttonGraphics.Size = new System.Drawing.Size(75, 23);
            this.buttonGraphics.TabIndex = 1;
            this.buttonGraphics.Text = "图像";
            this.buttonGraphics.UseVisualStyleBackColor = true;
            this.buttonGraphics.Click += new System.EventHandler(this.buttonGraphics_Click);
            // 
            // buttonCurve
            // 
            this.buttonCurve.Location = new System.Drawing.Point(31, 182);
            this.buttonCurve.Name = "buttonCurve";
            this.buttonCurve.Size = new System.Drawing.Size(75, 23);
            this.buttonCurve.TabIndex = 1;
            this.buttonCurve.Text = "曲线";
            this.buttonCurve.UseVisualStyleBackColor = true;
            this.buttonCurve.Click += new System.EventHandler(this.buttonCurve_Click);
            // 
            // buttonText
            // 
            this.buttonText.Location = new System.Drawing.Point(31, 118);
            this.buttonText.Name = "buttonText";
            this.buttonText.Size = new System.Drawing.Size(75, 23);
            this.buttonText.TabIndex = 1;
            this.buttonText.Text = "文字";
            this.buttonText.UseVisualStyleBackColor = true;
            this.buttonText.Click += new System.EventHandler(this.buttonText_Click);
            // 
            // buttonRectangle
            // 
            this.buttonRectangle.Location = new System.Drawing.Point(31, 54);
            this.buttonRectangle.Name = "buttonRectangle";
            this.buttonRectangle.Size = new System.Drawing.Size(75, 23);
            this.buttonRectangle.TabIndex = 1;
            this.buttonRectangle.Text = "矩形";
            this.buttonRectangle.UseVisualStyleBackColor = true;
            this.buttonRectangle.Click += new System.EventHandler(this.buttonRectangle_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.编辑ToolStripMenuItem,
            this.导出图片ToolStripMenuItem,
            this.打印预览ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(688, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 编辑ToolStripMenuItem
            // 
            this.编辑ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.全部选择ToolStripMenuItem,
            this.删除ToolStripMenuItem});
            this.编辑ToolStripMenuItem.Name = "编辑ToolStripMenuItem";
            this.编辑ToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
            this.编辑ToolStripMenuItem.Text = "编辑";
            // 
            // 全部选择ToolStripMenuItem
            // 
            this.全部选择ToolStripMenuItem.Name = "全部选择ToolStripMenuItem";
            this.全部选择ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.全部选择ToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.全部选择ToolStripMenuItem.Text = "全部选择";
            this.全部选择ToolStripMenuItem.Click += new System.EventHandler(this.全部选择ToolStripMenuItem_Click);
            // 
            // 删除ToolStripMenuItem
            // 
            this.删除ToolStripMenuItem.Name = "删除ToolStripMenuItem";
            this.删除ToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.删除ToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.删除ToolStripMenuItem.Text = "删除选择的对象";
            this.删除ToolStripMenuItem.Click += new System.EventHandler(this.删除ToolStripMenuItem_Click);
            // 
            // 导出图片ToolStripMenuItem
            // 
            this.导出图片ToolStripMenuItem.Name = "导出图片ToolStripMenuItem";
            this.导出图片ToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.导出图片ToolStripMenuItem.Text = "导出图片";
            this.导出图片ToolStripMenuItem.Click += new System.EventHandler(this.导出图片ToolStripMenuItem_Click);
            // 
            // 打印预览ToolStripMenuItem
            // 
            this.打印预览ToolStripMenuItem.Name = "打印预览ToolStripMenuItem";
            this.打印预览ToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.打印预览ToolStripMenuItem.Text = "打印预览";
            this.打印预览ToolStripMenuItem.Click += new System.EventHandler(this.打印预览ToolStripMenuItem_Click);
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(509, 17);
            this.toolStripStatusLabel1.Text = "说明：提供的代码仅仅是为了说明设计思路，请先看懂代码，然后在此基础上设计自己的功能。";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(688, 428);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "多机联合绘图系统（部分代码）设计举例";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonText;
        private System.Windows.Forms.Button buttonRectangle;
        private System.Windows.Forms.Button buttonCurve;
        private System.Windows.Forms.Button buttonGraphics;
        private System.Windows.Forms.ToolStripLabel toolStripLabelUserState;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 编辑ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 删除ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 全部选择ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 导出图片ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 打印预览ToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;

    }
}

