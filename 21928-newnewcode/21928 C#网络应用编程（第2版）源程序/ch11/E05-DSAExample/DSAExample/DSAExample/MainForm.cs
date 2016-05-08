using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace DSAExample
{
public partial class MainForm : Form
{
    byte[] hashValue;
    byte[] signedHashValue;
    DSAParameters dsaKeyInfo;
    public MainForm()
    {
        InitializeComponent();
    }
    private void buttonOK_Click(object sender, EventArgs e)
    {
        try
        {
            DSACryptoServiceProvider dsa = new DSACryptoServiceProvider();
            //随机生成20个Hash值
            List<byte> list = new List<byte>();
            Random r = new Random();
            for (int i = 0; i < 20; i++)
            {
                list.Add((byte)r.Next(255));
            }
            hashValue = list.ToArray();
            //导出公约和私钥
            dsaKeyInfo = dsa.ExportParameters(true);
            //得到签名的Hash值
            signedHashValue = DSASignHash(hashValue, dsaKeyInfo, "SHA1");
            //此处应该将hashValue、signedHashValue以及公钥发
            //送给接收方。为简化起见，这里仅将Hash和签名的Hash显示出来
            textBoxHashValue.Text = GetHashString(hashValue);
            textBoxVerifyHashValue.Text = GetHashString(signedHashValue);
        }
        catch (ArgumentNullException err)
        {
            MessageBox.Show(err.Message);
        }
    }
    /// <summary>
    /// 根据哈希值拼接字符串
    /// </summary>
    /// <param name="bytes">哈希值</param>
    /// <returns>拼接的字符串</returns>
    private string GetHashString(byte[] bytes)
    {
        string s = "";
        for (int i = 0; i < bytes.Length; i++)
        {
            s += bytes[i].ToString() + ",";
        }
        s = s.TrimEnd(',');
        return s;
    }
    /// <summary>
    /// 使用DSA算法签名哈希值
    /// </summary>
    /// <param name="HashToSign">要被签名的哈希值</param>
    /// <param name="dsaKeyInfo">DSA密钥信息</param>
    /// <param name="HashAlg">指定哈希算法</param>
    /// <returns>签名后的结果</returns>
    private byte[] DSASignHash(byte[] HashToSign, DSAParameters dsaKeyInfo, string HashAlg)
    {
        try
        {
            DSACryptoServiceProvider dsa = new DSACryptoServiceProvider();
            dsa.ImportParameters(dsaKeyInfo);
            DSASignatureFormatter DSAFormatter = new DSASignatureFormatter(dsa);
            DSAFormatter.SetHashAlgorithm(HashAlg);
            return DSAFormatter.CreateSignature(HashToSign);
        }
        catch (CryptographicException err)
        {
            MessageBox.Show(err.Message);
            return null;
        }
    }
    private void buttonVerify_Click(object sender, EventArgs e)
    {
        //为简化起见，此处假定接收方已经接收到发
        //送方发送的hashValue、signedHashValue以及公钥
        //同时保证接收方和签名方使用相同的哈希算法（均为“SHA1”）
        try
        {
            DSACryptoServiceProvider dsa = new DSACryptoServiceProvider();
            dsa.ImportParameters(dsaKeyInfo);
            DSASignatureDeformatter DSADeformatter = new DSASignatureDeformatter(dsa);
            DSADeformatter.SetHashAlgorithm("SHA1");
            if (DSADeformatter.VerifySignature(hashValue, signedHashValue))
            {
                textBoxVerifyResult.Text = "验证成功";
            }
            else
            {
                textBoxVerifyResult.Text = "验证失败";
            }
        }
        catch (CryptographicException err)
        {
            MessageBox.Show(err.Message);
        }
    }
}
}