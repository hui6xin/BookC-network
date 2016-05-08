using System;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.IO;

namespace EncryptFileExample
{
    public partial class FormEncryptFile : Form
    {
        //密钥容器名称
        string containerName = "rsa1";
        //保存不对称加密的密钥信息
        string keyFile = "keyInfo.txt";
        //被加密的文件
        string file = "test.txt";
        public FormEncryptFile()
        {
            InitializeComponent();
            //先创建一个未被加密的UTF8编码的文件,用于测试
            CreateNewFile("aaaaaaabbbbbbbbccccc\r\n中国");
            //将未加密的文件内容显示出来
            richTextBoxOriginal.Text = File.ReadAllText(file);
        }
        /// <summary>
        /// 创建新文件
        /// </summary>
        /// <param name="text">文件名</param>
        private void CreateNewFile(string text)
        {
            if (File.Exists(file))
            {
                File.Delete(file);
            }
            File.WriteAllText(file, text);
        }
        private void buttonEncrypt_Click(object sender, EventArgs e)
        {
            CreateNewFile(richTextBoxOriginal.Text);
            //加密指定的文件，并将加密后的文件写入到test1.enc中
            EncryptFile(file, "test1.enc");
        }
        /// <summary>
        /// 加密文件
        /// </summary>
        /// <param name="sourceFileName">源文件名（要加密的文件）</param>
        /// <param name="targetFileName">目标文件名（加密后保存到的文件）</param>
        private void EncryptFile(string sourceFileName, string targetFileName)
        {
            RSACryptoServiceProvider rsa = GetRSAProviderFromContainer(containerName);
            AesManaged aes = new AesManaged();
            //创建新文件以便写入加密后的内容,如果文件已存在，则覆盖
            FileStream outFs = new FileStream(targetFileName, FileMode.Create, FileAccess.Write);
            BinaryWriter bw = new BinaryWriter(outFs);
            //写入加密后的对称加密的KEY长度和KEY内容
            byte[] encryptedAesKey = rsa.Encrypt(aes.Key, true);
            bw.Write(encryptedAesKey.Length);
            bw.Write(encryptedAesKey);
            //写入加密后的对称加密IV长度和IV内容
            byte[] encryptedAesIV = rsa.Encrypt(aes.IV, true);
            bw.Write(encryptedAesIV.Length);
            bw.Write(encryptedAesIV);
            //写入加密的内容,该段代码适用于加密任何大小和任何类型的文件
            ICryptoTransform transform = aes.CreateEncryptor();
            using (CryptoStream outStreamEncrypted = new CryptoStream(outFs, transform, CryptoStreamMode.Write))
            {
                int count = 0;
                int offset = 0;
                int blockSizeBytes = aes.BlockSize / 8;
                byte[] data = new byte[blockSizeBytes];
                int bytesRead = 0;
                FileStream inFs = new FileStream(sourceFileName, FileMode.Open);
                do
                {
                    count = inFs.Read(data, 0, blockSizeBytes);
                    offset += count;
                    outStreamEncrypted.Write(data, 0, count);
                    bytesRead += blockSizeBytes;
                } while (count > 0);
                inFs.Close();
                outStreamEncrypted.FlushFinalBlock();
                outStreamEncrypted.Close();
            }
            bw.Close();
            outFs.Close();
            MessageBox.Show("加密完毕");
        }
        private void buttonDecrypt_Click(object sender, EventArgs e)
        {
            //将test1,enc文件内容解密到file中
            DecryptFile("test1.enc", file);
            richTextBoxDecrypted.Text = File.ReadAllText(file);
        }
        /// <summary>
        /// 解密文件
        /// </summary>
        /// <param name="sourceFileName">源文件名（要解密的文件）</param>
        /// <param name="targetFileName">目标文件名（解密后的内容保存到的文件）</param>
        private void DecryptFile(string sourceFileName, string targetFileName)
        {
            FileStream inFs = new FileStream(sourceFileName, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(inFs);
            int encryptedAesKeyLength = br.ReadInt32();
            byte[] encryptedAesKey = br.ReadBytes(encryptedAesKeyLength);
            int encryptedAesIVLength = br.ReadInt32();
            byte[] encryptedAesIV = br.ReadBytes(encryptedAesIVLength);
            RSACryptoServiceProvider rsa = GetRSAProviderFromContainer(containerName);
            //获取解密后的对称密钥的KEY和IV
            byte[] aesKey = rsa.Decrypt(encryptedAesKey, true);
            byte[] aesIV = rsa.Decrypt(encryptedAesIV, true);
            int startC = encryptedAesKeyLength + encryptedAesIVLength + 8;
            int lenC = (int)inFs.Length - startC;
            AesManaged aes = new AesManaged();
            ICryptoTransform transform = aes.CreateDecryptor(aesKey, aesIV);
            using (FileStream outFs = new FileStream(targetFileName, FileMode.Create))
            {

                int count = 0;
                int offset = 0;
                int blockSizeBytes = aes.BlockSize / 8;
                byte[] data = new byte[blockSizeBytes];
                inFs.Seek(startC, SeekOrigin.Begin);
                using (CryptoStream outStreamDecrypted = new CryptoStream(outFs, transform, CryptoStreamMode.Write))
                {
                    do
                    {
                        count = inFs.Read(data, 0, blockSizeBytes);
                        offset += count;
                        outStreamDecrypted.Write(data, 0, count);
                    } while (count > 0);
                    outStreamDecrypted.FlushFinalBlock();
                    outStreamDecrypted.Close();//此句亦可省略不写
                }
                outFs.Close();//此句亦可省略不写
            }
            inFs.Close();
        }
        /// <summary>
        /// 根据密钥容器名获取初始化后的RSA对象
        /// </summary>
        /// <param name="containerName">密钥容器名</param>
        /// <returns>RSA对象</returns>
        private static RSACryptoServiceProvider GetRSAProviderFromContainer(string containerName)
        {
            CspParameters cp = new CspParameters();
            //将 ProviderType 字段初始化为值 24，该值指定 PROV_RSA_AES提供程序
            cp.ProviderType = 24;
            //如果不存在名为containerName的密钥容器，则创建之，并初始化cp
            //如果存在，则直接根据它保存的内容初始化cp
            cp.KeyContainerName = containerName;
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(cp);
            return rsa;
        }
        private void buttonExportKeyInfo_Click(object sender, EventArgs e)
        {
            string rsaKeyInfo = GetKeyInfoFromContainer();
            File.WriteAllText(keyFile, rsaKeyInfo);
            MessageBox.Show("密钥信息成功导出到" + keyFile + "中,请妥善保存该文件");
        }
        /// <summary>
        /// 获取密钥信息
        /// </summary>
        /// <returns>包含密钥信息的xml字符串</returns>
        private string GetKeyInfoFromContainer()
        {
            RSACryptoServiceProvider rsa = GetRSAProviderFromContainer(containerName);
            //true表示包含私钥
            return rsa.ToXmlString(true);
        }
        private void buttonImportFromFile_Click(object sender, EventArgs e)
        {
            SaveKeyInfoToContainer(containerName, keyFile);
            MessageBox.Show("导入成功");
        }
        /// <summary>
        /// 将密钥信息保存到密钥容器中
        /// </summary>
        /// <param name="containerName">密钥容器名</param>
        /// <param name="keyFileName">保存密钥信息的文件名</param>
        private static void SaveKeyInfoToContainer(string containerName, string keyFileName)
        {
            CspParameters cp = new CspParameters();
            //将 ProviderType 字段初始化为值 24，该值指定 PROV_RSA_AES提供程序
            cp.ProviderType = 24;
            cp.KeyContainerName = containerName;
            string rsaKeyInfo = System.IO.File.ReadAllText(keyFileName);
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(cp);
            rsa.FromXmlString(rsaKeyInfo);
            //true表示将密钥永久驻留在CSP中，false表示从密钥容器中删除该密钥
            rsa.PersistKeyInCsp = true;
        }
    }
}
