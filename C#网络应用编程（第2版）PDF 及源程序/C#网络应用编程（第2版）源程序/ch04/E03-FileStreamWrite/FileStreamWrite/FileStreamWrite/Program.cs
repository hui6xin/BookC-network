using System;
using System.Text;
using System.IO;

namespace FileStreamWrite
{
    class Program
    {
        static void Main(string[] args)
        {
            FileStream fs = null;
            String filePath = "c:\\file1.txt";
            //将待写入数据从字符串转换为字节数组
            Encoding encoder = Encoding.UTF8;
            Byte[] bytes = encoder.GetBytes("HelloWorld!\n\r");
            try
            {
                fs = File.OpenWrite(filePath);
                //设定书写的开始位置为文件的末尾
                fs.Position = fs.Length;
                //将待写入内容追加到文件末尾
                fs.Write(bytes, 0, bytes.Length);
            }
            catch (Exception ex)
            {
                Console.WriteLine("文件打开失败{0}", ex.ToString());
            }
            finally
            {
                fs.Close();
            }
            Console.ReadLine();
        }
    }
}
