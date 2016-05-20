using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace EncryptStringExample
{
    public partial class FormEncryptString : Form
    {
        public FormEncryptString()
        {
            InitializeComponent();
            textBoxEncrypted.ReadOnly = true;
            textBoxDecrypted.ReadOnly = true;
            textBoxInput.Text = "abc123: 你好！";
        }

        private void buttonEncrypt_Click(object sender, EventArgs e)
        {
            textBoxEncrypted.Text = RSAEncrypt(textBoxInput.Text);
        }

        private void buttonDecrypt_Click(object sender, EventArgs e)
        {
            textBoxDecrypted.Text = RSADescrpt(textBoxEncrypted.Text);
        }
        /// <summary>
        /// 使用RSA算法进行解密
        /// </summary>
        /// <param name="text">要加密的字符串</param>
        /// <returns></returns>
        private string RSAEncrypt(string text)
        {
            RSACryptoServiceProvider rsa = GetRSAProviderFromContainer("rsa1");
            byte[] bytes = Encoding.Unicode.GetBytes(text);
            byte[] encryptedData = rsa.Encrypt(bytes, true);
            return Convert.ToBase64String(encryptedData);
        }
        /// <summary>
        /// 使用RSA算法进行解密
        /// </summary>
        /// <param name="text">要解密的字符串</param>
        /// <returns></returns>
        private string RSADescrpt(string text)
        {
            RSACryptoServiceProvider rsa = GetRSAProviderFromContainer("rsa1");
            byte[] encryptedData = Convert.FromBase64String(text);
            byte[] decryptedData = rsa.Decrypt(encryptedData, true);
            return Encoding.Unicode.GetString(decryptedData);
        }
        /// <summary>
        /// 获取初始化RSA对象
        /// </summary>
        /// <param name="containerName">密钥容器名</param>
        /// <returns>RSA对象</returns>
        private static RSACryptoServiceProvider GetRSAProviderFromContainer(string containerName)
        {
            CspParameters cp = new CspParameters();
            //将 ProviderType初始化为值24，该值指定PROV_RSA_AES提供程序
            cp.ProviderType = 24;
            //如果不存在名为containerName的密钥容器，则创建之，并初始化cp
            //如果存在，则直接根据它保存的内容初始化cp
            cp.KeyContainerName = containerName;
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(cp);
            return rsa;
        }

        private void buttonExportKey_Click(object sender, EventArgs e)
        {
            RSACryptoServiceProvider rsa = GetRSAProviderFromContainer("rsa1");
            string rsaKeyInfo = rsa.ToXmlString(true);
            System.IO.File.WriteAllText("keyinfo.txt", rsaKeyInfo);
            MessageBox.Show("密钥信息成功导出到keyinfo.txt中,请妥善保存该文件");
        }

        private void buttonImportKey_Click(object sender, EventArgs e)
        {
            //保存不对称密钥到密钥容器
            SaveKeyInfoToContainer("rsa1");
            MessageBox.Show("导入成功");
        }

        /// <summary>
        /// 将密钥信息保存到密钥容器中
        /// </summary>
        /// <param name="containerName">密钥容器名</param>
        private static void SaveKeyInfoToContainer(string containerName)
        {
            CspParameters cp = new CspParameters();
            //将 ProviderType 字段初始化为值 24，该值指定 PROV_RSA_AES提供程序
            cp.ProviderType = 24;
            cp.KeyContainerName = containerName;
            string rsaKeyInfo = System.IO.File.ReadAllText("keyinfo.txt");
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(cp);
            rsa.FromXmlString(rsaKeyInfo);
            //true表示将密钥永久驻留在CSP中，false表示从密钥容器中删除该密钥
            rsa.PersistKeyInCsp = true;
        }
    }
}
