using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;

namespace EncryptXmlExample
{
public partial class FormEncryptXml : Form
{
    //----注意--------------------------------------------------------
    //此示例需要先添加对System.Security.dll的引用，添加步骤：
    //“引用”-->“添加引用”-->“.NET”-->“System.Security”-->“确定”
    //----------------------------------------------------------------
    public FormEncryptXml()
    {
        InitializeComponent();
        XmlDocument xmlDoc = CreateXmlDocument("test.xml");
        richTextBoxOriFile.Text = xmlDoc.OuterXml;
    }
    /// <summary>
    /// 根据xml文件名创建XmlDocument对象
    /// </summary>
    /// <param name="xmlFileName">xml文件名</param>
    /// <returns>XmlDocument对象</returns>
    private XmlDocument CreateXmlDocument(string xmlFileName)
    {
        XmlDocument xmlDoc = new XmlDocument();
        try
        {
            //是否在元素内容中保留空白
            xmlDoc.PreserveWhitespace = true;
            xmlDoc.Load(xmlFileName);
        }
        catch (Exception err)
        {
            MessageBox.Show(err.Message);
        }
        return xmlDoc;
    }
    private void buttonEncrypt_Click(object sender, EventArgs e)
    {
        RSACryptoServiceProvider rsa = GetRSAProviderFromContainer("XML_RSA_KEY1");
        XmlDocument xmlDoc = CreateXmlDocument("test.xml");
        try
        {
            //加密“公共课成绩”元素
            Encrypt(xmlDoc, "公共课成绩", rsa, "rsaKey");
            //保存加密后的XML文件
            xmlDoc.Save("test1.xml");
            //显示加密后的结果
            richTextBoxEncrypt.Text = xmlDoc.OuterXml;
        }
        catch (Exception err)
        {
            MessageBox.Show(err.Message);
        }
        finally
        {
            rsa.Clear();
        }
    }
    /// <summary>
    /// 加密xml文档对象
    /// </summary>
    /// <param name="Doc">xml文档</param>
    /// <param name="ElementToEncrypt">要被加密的XML元素</param>
    /// <param name="Alg">不对称加密算法</param>
    /// <param name="KeyName">指定解密需要的RSA密钥名称</param>
    private void Encrypt(XmlDocument Doc, string ElementToEncrypt, RSA Alg, string KeyName)
    {
        //xmlElement表示被加密的XML元素
        XmlElement xmlElement = Doc.GetElementsByTagName(ElementToEncrypt)[0] as XmlElement;
        AesManaged aes = new AesManaged();
        //指定密钥大小为256位
        aes.KeySize = 256;
        //用AES算法加密指定的元素进行加密，得到加密后的字节数组
        EncryptedXml encryptedXml = new EncryptedXml();
        byte[] encryptedElement = encryptedXml.EncryptData(xmlElement, aes, false);
        //构造一个 EncryptedData 对象，然后用加密后的XML元素的URL标识符填充它，
        //以便解密方知道XML包含一个加密元素
        EncryptedData encryptedData = new EncryptedData();
        encryptedData.Type = EncryptedXml.XmlEncElementUrl;
        encryptedData.EncryptionMethod = new EncryptionMethod(EncryptedXml.XmlEncAES256Url);
        //ek表示加密后的<EncryptedKey>元素
        EncryptedKey ek = new EncryptedKey();
        //用RSA算法加密AES的密钥
        ek.CipherData = new CipherData(EncryptedXml.EncryptKey(aes.Key, Alg, false));
        ek.EncryptionMethod = new EncryptionMethod(EncryptedXml.XmlEncRSA15Url);
        //指定解密需要的RSA密钥名称
        ek.KeyInfo.AddClause(new KeyInfoName(KeyName));
        //将加密后的密钥添加到EncryptedData对象中
        encryptedData.KeyInfo.AddClause(new KeyInfoEncryptedKey(ek));
        //将加密后的元素添加到 EncryptedData 对象中
        encryptedData.CipherData.CipherValue = encryptedElement;
        //用EncryptedData元素替换原始XmlDocument对象中的元素
        EncryptedXml.ReplaceElement(xmlElement, encryptedData, false);
    }
    private void buttonDecrypt_Click(object sender, EventArgs e)
    {
        RSACryptoServiceProvider rsa = GetRSAProviderFromContainer("XML_RSA_KEY1");
        XmlDocument xmlDoc = CreateXmlDocument("test1.xml");
        try
        {
            //解密
            Decrypt(xmlDoc, rsa, "rsaKey");
            xmlDoc.Save("test2.xml");
            //显示解密后的结果
            richTextBoxDecrypt.Text = xmlDoc.OuterXml;
        }
        catch (Exception err)
        {
            MessageBox.Show(err.Message);
        }
        finally
        {
            rsa.Clear();
        }
    }
    /// <summary>
    /// 解密xml文档
    /// </summary>
    /// <param name="Doc">被解密的xml文档</param>
    /// <param name="Alg">不对称算法对象</param>
    /// <param name="KeyName">解密需要的RSA密钥名称</param>
    private void Decrypt(XmlDocument Doc, RSACryptoServiceProvider Alg, string KeyName)
    {
        //创建 EncryptedXml 对象以对文档进行解密
        EncryptedXml exml = new EncryptedXml(Doc);
        //添加密钥/名称映射，以将RSA密钥与要解密的文档中的元素关
        //联起来。用于密钥的名称必须与加密文档时使用的密钥名称相同
        exml.AddKeyNameMapping(KeyName, Alg);
        //解密元素
        exml.DecryptDocument();
    }
    /// <summary>
    /// 根据密钥容器名获取初始化后的RSA对象
    /// </summary>
    /// <param name="containerName">密钥容器名</param>
    /// <returns>RSA对象</returns>
    private static RSACryptoServiceProvider GetRSAProviderFromContainer(string containerName)
    {
        CspParameters cp = new CspParameters();
        cp.KeyContainerName = containerName;
        RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(cp);
        return rsa;
    }
}
}
