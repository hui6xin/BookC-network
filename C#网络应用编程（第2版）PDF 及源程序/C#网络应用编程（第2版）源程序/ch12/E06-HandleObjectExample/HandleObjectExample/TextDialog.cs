using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HandleObjectExample
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

        private void TextDialog_Load(object sender, EventArgs e)
        {
            _color = Color.Black;
        }
    }
}
