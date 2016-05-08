using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace test4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //显示现有的编码类型
            foreach (EncodingInfo ei in Encoding.GetEncodings())
            {
                Encoding en = ei.GetEncoding();
                this.comboBoxType.Items.Add(en.HeaderName);
            }
            this.comboBoxType.SelectedIndex = 0;

        }

        private void buttonRun_Click(object sender, EventArgs e)
        {
            //编码
            String strCodeType = this.comboBoxType.SelectedItem.ToString();
            Encoding encoder = Encoding.GetEncoding(strCodeType);
            Byte[] bytes = encoder.GetBytes(this.textBoxOldText.Text);
            textBoxEncoder.Text = Convert.ToBase64String(bytes);
            //解码
            Encoding decoder = Encoding.GetEncoding(strCodeType);
            string strResult = decoder.GetString(bytes);
            textBoxDecoder.Text = strResult;

        }
    }
}
