namespace P2PVideo
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.listViewUsers = new System.Windows.Forms.ListView();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.richTextBoxReceive = new System.Windows.Forms.RichTextBox();
            this.richTextBoxSend = new System.Windows.Forms.RichTextBox();
            this.buttonShowUsers = new System.Windows.Forms.Button();
            this.buttonShowVideo = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonSend = new System.Windows.Forms.Button();
            this.buttonLocalShow = new System.Windows.Forms.Button();
            this.pictureBoxFriend = new System.Windows.Forms.PictureBox();
            this.buttonVedioCall = new System.Windows.Forms.Button();
            this.labelTips = new System.Windows.Forms.Label();
            this.picture_comming = new System.Windows.Forms.PictureBox();
            this.picCaptureMine = new System.Windows.Forms.PictureBox();
            this.timerSend = new System.Windows.Forms.Timer(this.components);
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxFriend)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picture_comming)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCaptureMine)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.listViewUsers);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(937, 487);
            this.splitContainer1.SplitterDistance = 225;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 0;
            // 
            // listViewUsers
            // 
            this.listViewUsers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewUsers.Location = new System.Drawing.Point(0, 0);
            this.listViewUsers.Name = "listViewUsers";
            this.listViewUsers.Size = new System.Drawing.Size(225, 487);
            this.listViewUsers.TabIndex = 0;
            this.listViewUsers.UseCompatibleStateImageBehavior = false;
            this.listViewUsers.View = System.Windows.Forms.View.List;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Margin = new System.Windows.Forms.Padding(0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.BackColor = System.Drawing.Color.LightGray;
            this.splitContainer2.Panel1.Controls.Add(this.richTextBoxReceive);
            this.splitContainer2.Panel1.Controls.Add(this.richTextBoxSend);
            this.splitContainer2.Panel1.Controls.Add(this.buttonShowUsers);
            this.splitContainer2.Panel1.Controls.Add(this.buttonShowVideo);
            this.splitContainer2.Panel1.Controls.Add(this.buttonClose);
            this.splitContainer2.Panel1.Controls.Add(this.buttonSend);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.BackColor = System.Drawing.Color.LightGray;
            this.splitContainer2.Panel2.Controls.Add(this.buttonLocalShow);
            this.splitContainer2.Panel2.Controls.Add(this.pictureBoxFriend);
            this.splitContainer2.Panel2.Controls.Add(this.buttonVedioCall);
            this.splitContainer2.Panel2.Controls.Add(this.labelTips);
            this.splitContainer2.Panel2.Controls.Add(this.picture_comming);
            this.splitContainer2.Panel2.Controls.Add(this.picCaptureMine);
            this.splitContainer2.Size = new System.Drawing.Size(713, 487);
            this.splitContainer2.SplitterDistance = 473;
            this.splitContainer2.SplitterWidth = 1;
            this.splitContainer2.TabIndex = 2;
            // 
            // richTextBoxReceive
            // 
            this.richTextBoxReceive.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.richTextBoxReceive.Location = new System.Drawing.Point(16, 12);
            this.richTextBoxReceive.Name = "richTextBoxReceive";
            this.richTextBoxReceive.Size = new System.Drawing.Size(442, 260);
            this.richTextBoxReceive.TabIndex = 11;
            this.richTextBoxReceive.Text = "";
            // 
            // richTextBoxSend
            // 
            this.richTextBoxSend.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.richTextBoxSend.Location = new System.Drawing.Point(16, 309);
            this.richTextBoxSend.Name = "richTextBoxSend";
            this.richTextBoxSend.Size = new System.Drawing.Size(442, 137);
            this.richTextBoxSend.TabIndex = 10;
            this.richTextBoxSend.Text = "";
            // 
            // buttonShowUsers
            // 
            this.buttonShowUsers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonShowUsers.ForeColor = System.Drawing.Color.LightGray;
            this.buttonShowUsers.Image = global::P2PVideo.Properties.Resources.MovePreviousHS;
            this.buttonShowUsers.Location = new System.Drawing.Point(3, 205);
            this.buttonShowUsers.Name = "buttonShowUsers";
            this.buttonShowUsers.Size = new System.Drawing.Size(10, 39);
            this.buttonShowUsers.TabIndex = 9;
            this.buttonShowUsers.UseVisualStyleBackColor = true;
            this.buttonShowUsers.Click += new System.EventHandler(this.buttonShowUsers_Click);
            // 
            // buttonShowVideo
            // 
            this.buttonShowVideo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonShowVideo.ForeColor = System.Drawing.Color.LightGray;
            this.buttonShowVideo.Image = global::P2PVideo.Properties.Resources.MoveNextHS;
            this.buttonShowVideo.Location = new System.Drawing.Point(461, 205);
            this.buttonShowVideo.Name = "buttonShowVideo";
            this.buttonShowVideo.Size = new System.Drawing.Size(10, 39);
            this.buttonShowVideo.TabIndex = 8;
            this.buttonShowVideo.UseVisualStyleBackColor = true;
            this.buttonShowVideo.Click += new System.EventHandler(this.buttonShowVideo_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(383, 452);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 7;
            this.buttonClose.Text = "取消";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // buttonSend
            // 
            this.buttonSend.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.buttonSend.Location = new System.Drawing.Point(283, 452);
            this.buttonSend.Name = "buttonSend";
            this.buttonSend.Size = new System.Drawing.Size(75, 23);
            this.buttonSend.TabIndex = 6;
            this.buttonSend.Text = "发送";
            this.buttonSend.UseVisualStyleBackColor = true;
            this.buttonSend.Click += new System.EventHandler(this.buttonSend_Click);
            // 
            // buttonLocalShow
            // 
            this.buttonLocalShow.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.buttonLocalShow.Location = new System.Drawing.Point(145, 266);
            this.buttonLocalShow.Name = "buttonLocalShow";
            this.buttonLocalShow.Size = new System.Drawing.Size(57, 23);
            this.buttonLocalShow.TabIndex = 40;
            this.buttonLocalShow.Text = "本地预览";
            this.buttonLocalShow.UseVisualStyleBackColor = true;
            this.buttonLocalShow.Click += new System.EventHandler(this.buttonLocalShow_Click);
            // 
            // pictureBoxFriend
            // 
            this.pictureBoxFriend.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBoxFriend.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBoxFriend.Image = global::P2PVideo.Properties.Resources.vedio;
            this.pictureBoxFriend.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBoxFriend.InitialImage")));
            this.pictureBoxFriend.Location = new System.Drawing.Point(3, 12);
            this.pictureBoxFriend.Name = "pictureBoxFriend";
            this.pictureBoxFriend.Size = new System.Drawing.Size(223, 182);
            this.pictureBoxFriend.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxFriend.TabIndex = 39;
            this.pictureBoxFriend.TabStop = false;
            // 
            // buttonVedioCall
            // 
            this.buttonVedioCall.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.buttonVedioCall.Location = new System.Drawing.Point(62, 266);
            this.buttonVedioCall.Name = "buttonVedioCall";
            this.buttonVedioCall.Size = new System.Drawing.Size(57, 23);
            this.buttonVedioCall.TabIndex = 37;
            this.buttonVedioCall.Text = "视频呼叫";
            this.buttonVedioCall.UseVisualStyleBackColor = true;
            this.buttonVedioCall.Click += new System.EventHandler(this.buttonVedioCall_Click);
            // 
            // labelTips
            // 
            this.labelTips.Location = new System.Drawing.Point(3, 222);
            this.labelTips.Name = "labelTips";
            this.labelTips.Size = new System.Drawing.Size(223, 31);
            this.labelTips.TabIndex = 35;
            this.labelTips.Text = "呼叫提示信息";
            this.labelTips.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // picture_comming
            // 
            this.picture_comming.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.picture_comming.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.picture_comming.Image = global::P2PVideo.Properties.Resources.vedio;
            this.picture_comming.InitialImage = ((System.Drawing.Image)(resources.GetObject("picture_comming.InitialImage")));
            this.picture_comming.Location = new System.Drawing.Point(74, 502);
            this.picture_comming.Name = "picture_comming";
            this.picture_comming.Size = new System.Drawing.Size(211, 171);
            this.picture_comming.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picture_comming.TabIndex = 34;
            this.picture_comming.TabStop = false;
            // 
            // picCaptureMine
            // 
            this.picCaptureMine.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.picCaptureMine.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.picCaptureMine.Image = global::P2PVideo.Properties.Resources.vedio;
            this.picCaptureMine.InitialImage = ((System.Drawing.Image)(resources.GetObject("picCaptureMine.InitialImage")));
            this.picCaptureMine.Location = new System.Drawing.Point(3, 295);
            this.picCaptureMine.Name = "picCaptureMine";
            this.picCaptureMine.Size = new System.Drawing.Size(223, 182);
            this.picCaptureMine.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picCaptureMine.TabIndex = 33;
            this.picCaptureMine.TabStop = false;
            // 
            // timerSend
            // 
            this.timerSend.Tick += new System.EventHandler(this.timerSend_Tick);
            // 
            // FormP2P
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.ClientSize = new System.Drawing.Size(937, 487);
            this.Controls.Add(this.splitContainer1);
            this.Name = "FormP2P";
            this.Text = "P2P视频聊天";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormP2P_FormClosing);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxFriend)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picture_comming)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCaptureMine)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.ListView listViewUsers;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonSend;
        private System.Windows.Forms.Button buttonShowUsers;
        private System.Windows.Forms.Button buttonShowVideo;
        private System.Windows.Forms.RichTextBox richTextBoxReceive;
        private System.Windows.Forms.RichTextBox richTextBoxSend;
        private System.Windows.Forms.Button buttonVedioCall;
        private System.Windows.Forms.Label labelTips;
        internal System.Windows.Forms.PictureBox picture_comming;
        internal System.Windows.Forms.PictureBox picCaptureMine;
        internal System.Windows.Forms.PictureBox pictureBoxFriend;
        private System.Windows.Forms.Button buttonLocalShow;
        private System.Windows.Forms.Timer timerSend;
    }
}