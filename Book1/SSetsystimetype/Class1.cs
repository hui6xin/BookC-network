using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSetsystimetype
{
    public class Class1
    {
        public static string sss;
        public static string ssss {
            get 
            {
                return sss;
            }
            set 
            {
                if (sss != "kkkkkk")
                    sss = "kkk";
            }
        }
        public void getA()
        {
            sss = "getA";
        }
    }
}
