using System;
using System.Collections.Generic;
using System.Text;

using System.IO;

namespace StreamReaderApp
{
    class Program
    {
        static void Main(string[] args)
        {
            String s = "";
            using (StreamReader sr = new StreamReader("C:\\file1.txt"))
            {
                s = sr.ReadToEnd();
            }
            //using块结束，StreamReader对象占用资源被释放。
            Console.WriteLine(s);
            Console.ReadLine();
        }
    }
}
