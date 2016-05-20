namespace DrawShapeExample
{
    partial class FormDraw
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonRectangle = new System.Windows.Forms.Button();
            this.buttonCurve = new System.Windows.Forms.Button();
            this.buttonText = new System.Windows.Forms.Button();
            this.buttonImage = new System.Windows.Forms.Button();
            this.labelTitle = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Location = new System.Drawing.Point(93, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(474, 276);
            this.panel1.TabIndex = 0;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            this.panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseMove);
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            this.panel1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseUp);
            // 
            // buttonRectangle
            // 
            this.buttonRectangle.Location = new System.Drawing.Point(12, 42);
            this.buttonRectangle.Name = "buttonRectangle";
            this.buttonRectangle.Size = new System.Drawing.Size(75, 23);
            this.buttonRectangle.TabIndex = 1;
            this.buttonRectangle.Text = "矩形";
            this.buttonRectangle.UseVisualStyleBackColor = true;
            this.buttonRectangle.Click += new System.EventHandler(this.buttonRectangle_Click);
            // 
            // buttonCurve
            // 
            this.buttonCurve.Location = new System.Drawing.Point(13, 96);
            this.buttonCurve.Name = "buttonCurve";
            this.buttonCurve.Size = new System.Drawing.Size(75, 23);
            this.buttonCurve.TabIndex = 2;
            this.buttonCurve.Text = "曲线";
            this.buttonCurve.UseVisualStyleBackColor = true;
            this.buttonCurve.Click += new System.EventHandler(this.buttonCurve_Click);
            // 
            // buttonText
            // 
            this.buttonText.Location = new System.Drawing.Point(12, 152);
            this.buttonText.Name = "buttonText";
            this.buttonText.Size = new System.Drawing.Size(75, 23);
            this.buttonText.TabIndex = 3;
            this.buttonText.Text = "文本";
            this.buttonText.UseVisualStyleBackColor = true;
            this.buttonText.Click += new System.EventHandler(this.buttonText_Click);
            // 
            // buttonImage
            // 
            this.buttonImage.Location = new System.Drawing.Point(13, 209);
            this.buttonImage.Name = "buttonImage";
            this.buttonImage.Size = new System.Drawing.Size(75, 23);
            this.buttonImage.TabIndex = 4;
            this.buttonImage.Text = "图像";
            this.buttonImage.UseVisualStyleBackColor = true;
            this.buttonImage.Click += new System.EventHandler(this.buttonImage_Click);
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Location = new System.Drawing.Point(61, 299);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(401, 12);
            this.labelTitle.TabIndex = 5;
            this.labelTitle.Text = "提示：单击左边的按钮，然后在右边的框内用鼠标左键绘制相应的图形图像";
            // 
            // FormDraw
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(579, 323);
            this.Controls.Add(this.labelTitle);
            this.Controls.Add(this.buttonImage);
            this.Controls.Add(this.buttonText);
            this.Controls.Add(this.buttonCurve);
            this.Controls.Add(this.buttonRectangle);
            this.Controls.Add(this.panel1);
            this.Name = "FormDraw";
            this.Text = "图形图像绘制举例";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonRectangle;
        private System.Windows.Forms.Button buttonCurve;
        private System.Windows.Forms.Button buttonText;
        private System.Windows.Forms.Button buttonImage;
        private System.Windows.Forms.Label labelTitle;
    }
}