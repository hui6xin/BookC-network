namespace HandleObjectExample
{
    partial class FormDraw
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonCurve = new System.Windows.Forms.Button();
            this.buttonRectangle = new System.Windows.Forms.Button();
            this.buttonImage = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.buttonText = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Location = new System.Drawing.Point(108, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(733, 315);
            this.panel1.TabIndex = 0;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            this.panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseMove);
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            this.panel1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseUp);
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Location = new System.Drawing.Point(6, 334);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(835, 31);
            this.label1.TabIndex = 11;
            this.label1.Text = "提示：单击按钮选择要绘制的图形，然后在Panel1中拖动鼠标绘制，鼠标右击停止绘制。单击按钮选择对象或者通过鼠标拖放选择对象后，平移选中的对象。";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonCurve
            // 
            this.buttonCurve.Location = new System.Drawing.Point(6, 77);
            this.buttonCurve.Name = "buttonCurve";
            this.buttonCurve.Size = new System.Drawing.Size(86, 23);
            this.buttonCurve.TabIndex = 12;
            this.buttonCurve.Text = "曲线";
            this.buttonCurve.UseVisualStyleBackColor = true;
            this.buttonCurve.Click += new System.EventHandler(this.buttonCurve_Click);
            // 
            // buttonRectangle
            // 
            this.buttonRectangle.Location = new System.Drawing.Point(6, 30);
            this.buttonRectangle.Name = "buttonRectangle";
            this.buttonRectangle.Size = new System.Drawing.Size(86, 23);
            this.buttonRectangle.TabIndex = 13;
            this.buttonRectangle.Text = "矩形";
            this.buttonRectangle.UseVisualStyleBackColor = true;
            this.buttonRectangle.Click += new System.EventHandler(this.buttonRectangle_Click);
            // 
            // buttonImage
            // 
            this.buttonImage.Location = new System.Drawing.Point(6, 171);
            this.buttonImage.Name = "buttonImage";
            this.buttonImage.Size = new System.Drawing.Size(86, 23);
            this.buttonImage.TabIndex = 14;
            this.buttonImage.Text = "图像";
            this.buttonImage.UseVisualStyleBackColor = true;
            this.buttonImage.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(6, 218);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(86, 23);
            this.button2.TabIndex = 15;
            this.button2.Text = "全选对象";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(6, 265);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(86, 23);
            this.button3.TabIndex = 16;
            this.button3.Text = "清除选中";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // buttonText
            // 
            this.buttonText.Location = new System.Drawing.Point(6, 124);
            this.buttonText.Name = "buttonText";
            this.buttonText.Size = new System.Drawing.Size(86, 23);
            this.buttonText.TabIndex = 17;
            this.buttonText.Text = "文字";
            this.buttonText.UseVisualStyleBackColor = true;
            this.buttonText.Click += new System.EventHandler(this.buttonText_Click);
            // 
            // FormDraw
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(850, 379);
            this.Controls.Add(this.buttonText);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.buttonImage);
            this.Controls.Add(this.buttonRectangle);
            this.Controls.Add(this.buttonCurve);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Name = "FormDraw";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonCurve;
        private System.Windows.Forms.Button buttonRectangle;
        private System.Windows.Forms.Button buttonImage;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button buttonText;
    }
}

