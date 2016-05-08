using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


using System.Net.NetworkInformation;

namespace EncoderDecoderExample
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            textBoxOldText.Text = "测试数据：abc,123，我";
            textBoxEncoder.ReadOnly = textBoxDecoder.ReadOnly = true;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //显示现有的编码类型
            foreach (EncodingInfo ei in Encoding.GetEncodings())
            {
                Encoding en = ei.GetEncoding();
                comboBoxType.Items.Add(string.Format("{0}[{1}]", en.HeaderName, en.EncodingName));
            }
            comboBoxType.SelectedIndex = comboBoxType.FindString("gb2312");
        }

        private void buttonRun_Click(object sender, EventArgs e)
        {
            //编码
            String codeType = this.comboBoxType.SelectedItem.ToString();
            codeType = codeType.Substring(0, codeType.IndexOf('['));
            Encoder encoder = Encoding.GetEncoding(codeType).GetEncoder();
            char[] chars = this.textBoxOldText.Text.ToCharArray();
            Byte[] bytes = new Byte[encoder.GetByteCount(chars, 0, chars.Length, true)];
            encoder.GetBytes(chars, 0, chars.Length, bytes, 0, true);
            textBoxEncoder.Text = Convert.ToBase64String(bytes);

            //解码
            Decoder decoder = Encoding.GetEncoding(codeType).GetDecoder();
            int charLen = decoder.GetChars(bytes, 0, bytes.Length, chars, 0);
            String strResult = "";
            foreach (char c in chars)
                strResult = strResult + c.ToString();
            textBoxDecoder.Text = strResult;
        }
    }
}
