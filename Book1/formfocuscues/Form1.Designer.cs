namespace formfocuscues
{
    partial class Form1
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
            this.userControl11 = new formfocuscues.UserControl1();
            this.mybutton31 = new formfocuscues.mybutton3();
            this.mybutton21 = new formfocuscues.mybutton2();
            this.mybutton11 = new formfocuscues.mybutton1();
            this.mybutton1 = new formfocuscues.mybutton();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.mybutton1.SuspendLayout();
            this.SuspendLayout();
            // 
            // userControl11
            // 
            this.userControl11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.userControl11.Location = new System.Drawing.Point(402, 51);
            this.userControl11.Name = "userControl11";
            this.userControl11.Size = new System.Drawing.Size(365, 304);
            this.userControl11.TabIndex = 4;
            // 
            // mybutton31
            // 
            this.mybutton31.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.mybutton31.Location = new System.Drawing.Point(21, 291);
            this.mybutton31.Name = "mybutton31";
            this.mybutton31.Size = new System.Drawing.Size(200, 100);
            this.mybutton31.TabIndex = 3;
            // 
            // mybutton21
            // 
            this.mybutton21.Cursor = System.Windows.Forms.Cursors.Hand;
            this.mybutton21.Location = new System.Drawing.Point(21, 246);
            this.mybutton21.Name = "mybutton21";
            this.mybutton21.Size = new System.Drawing.Size(75, 23);
            this.mybutton21.TabIndex = 2;
            this.mybutton21.Text = "mybutton21";
            this.mybutton21.UseVisualStyleBackColor = true;
            // 
            // mybutton11
            // 
            this.mybutton11.Cursor = System.Windows.Forms.Cursors.Hand;
            this.mybutton11.Location = new System.Drawing.Point(21, 185);
            this.mybutton11.Name = "mybutton11";
            this.mybutton11.Size = new System.Drawing.Size(75, 23);
            this.mybutton11.TabIndex = 1;
            this.mybutton11.Text = "mybutton11";
            this.mybutton11.UseVisualStyleBackColor = true;
            // 
            // mybutton1
            // 
            this.mybutton1.Controls.Add(this.button1);
            this.mybutton1.Location = new System.Drawing.Point(21, 12);
            this.mybutton1.Name = "mybutton1";
            this.mybutton1.Size = new System.Drawing.Size(318, 142);
            this.mybutton1.TabIndex = 0;
            this.mybutton1.Text = "mybutton1";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(81, 96);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(465, 420);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(820, 496);
            this.Controls.Add(this.userControl11);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.mybutton31);
            this.Controls.Add(this.mybutton21);
            this.Controls.Add(this.mybutton11);
            this.Controls.Add(this.mybutton1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.mybutton1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private mybutton mybutton1;
        private mybutton1 mybutton11;
        private mybutton2 mybutton21;
        private mybutton3 mybutton31;
        private System.Windows.Forms.Button button1;
        private UserControl1 userControl11;
        private System.Windows.Forms.Button button2;
    }
}