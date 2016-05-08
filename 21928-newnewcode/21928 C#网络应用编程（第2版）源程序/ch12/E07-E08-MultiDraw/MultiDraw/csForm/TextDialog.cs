using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MultiDraw
{
    public partial class TextDialog : Form
    {
        private string text;
        public string MyText
        {
            get { return text; }
            set { text = value; }
        }

        private Color _color;
        public Color MyColor
        {
            get { return _color; }
            set { _color = value; }
        }

        public TextDialog()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.AcceptButton = buttonOK;
            this.CancelButton = buttonCancel;
            this.ControlBox = false;
            this.ShowInTaskbar = false;
        }


        private void TextForm_Load(object sender, EventArgs e)
        {
            _color = Color.Black;
            textBox1.Text = "河南大学";
        }

        private void buttonFont_Click(object sender, EventArgs e)
        {
            ColorDialog c = new ColorDialog();
            if (c.ShowDialog() == DialogResult.OK)
            {
                _color = c.Color;
                textBox1.ForeColor = _color;
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            text = textBox1.Text;
        }
    }
}