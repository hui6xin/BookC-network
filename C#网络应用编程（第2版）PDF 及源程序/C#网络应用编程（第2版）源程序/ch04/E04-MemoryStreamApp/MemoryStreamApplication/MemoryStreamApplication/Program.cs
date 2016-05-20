using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;


namespace MemoryStreamApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            //构造MemoryStream实例,并输出初始分配容量及使用量大小
            MemoryStream mem = new MemoryStream();
            Console.WriteLine("初始分配容量：{0}", mem.Capacity);
            Console.WriteLine("初始使用量：{0}", mem.Length);

            //将待写入数据从字符串转换为字节数组
            UnicodeEncoding encoder = new UnicodeEncoding();
            Byte[] bytes = encoder.GetBytes("新增数据");

            //向内存流中写入数据
            for (int i = 1; i < 4; i++)
            {
                Console.WriteLine("第{0}写入新数据",i);
                mem.Write(bytes, 0, bytes.Length);
            }

            //写入数据后MemoryStream实例的容量和使用量大小
            Console.WriteLine("当前分配容量：{0}", mem.Capacity);
            Console.WriteLine("当前使用量：{0}", mem.Length);
            Console.ReadLine();

        }
    }
}
