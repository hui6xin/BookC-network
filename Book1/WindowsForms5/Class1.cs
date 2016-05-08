using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Management;
using System.Collections.Generic;

using System;using System.Collections.Generic;
using System.Linq;using System.Text;
using System.Threading.Tasks;
namespace CheckWriter
{
    class Program    
    {        
        private static string[] _lessTwentyNumbers = new string[]{"Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight",
            "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen","Sixteen", "Seventeen", "Eighteen", "Nineteen"};
        private static string[] _lessHundredNumbers = new string[]{"", "", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty","Ninety"};
        private static string[] _scaleNumbers = new string[] { "", "Thousand", "Million", "Billion" };
        static void Main(string[] args)        
        {           
            string s = ConvertDecimalToWords(1258276.25);           
            Console.WriteLine(s);           
            Console.Read();        
        }        
        /// 
        /// 将数字转为英文描述        
        ///  
        private static string ConvertNumberToWords(int number)        
        {            
            if (number == 0)            
        {                
                return _lessTwentyNumbers[0];            
            }            int[] digitGroups = new int[4];           
            int positive = Math.Abs(number);           
            // 将数字按三位一段保存到数组里           
            for (int i = 0; i < 4; i++)           
            {               
                digitGroups[i] = positive % 1000;               
                positive /= 1000;            
            }           
            string[] groupText = new string[4];           
            for (int i = 0; i < 4; i++)            
            {               
                // 每三位一段转为相应的英文字符                
                groupText[i] = ConvertThreeDigitToWords(digitGroups[i]);            
            }            
            string combined = groupText[0];            
            for (int i = 1; i < 4; i++)            
            {               
                if (digitGroups[i] != 0)             
                {                   
                    // 每一段加上相应的这一段的级别数字           
                    string prefix = groupText[i] + " " + _scaleNumbers[i];   
                    if (!string.IsNullOrEmpty(prefix))                  
                    {                     
                        if (string.IsNullOrEmpty(combined))     
                        {                    
                            combined = prefix;              
                        }                     
                        else                  
                        {                      
                            combined = prefix + ", " + combined;        
                        }                   
                    }             
                }      
            }         
            if (number < 0)         
            {               
                combined = "Negative " + combined;     
            }           
            return combined;       
        }       
        /// 
        /// 处理三位数以内的数字转为英文描述        
        /// 
        private static string  ConvertThreeDigitToWords(int threeDigits)       
        {           
            string groupText = "";          
            // 获取百位        
            int hundreds = threeDigits / 100;      
            int tensUnits = threeDigits % 100;      
            if (hundreds != 0)          
            {             
                groupText += _lessTwentyNumbers[hundreds] + " Hundred";   
                if (tensUnits != 0)              
                {                  
                    groupText += " and ";                
                }          
            }          
            // 获取十位        
            int tens = tensUnits / 10;      
            int units = tensUnits % 10;    
            if (tens >= 2)          
            {                
                groupText += _lessHundredNumbers[tens]; 
                if (units != 0)             
                {                  
                    groupText += " " + _lessTwentyNumbers[units];     
                }        
            }           
            else if (tensUnits != 0)        
            {             
                groupText += _lessTwentyNumbers[tensUnits];    
            }          
            return groupText;  
        }        
        private static string ConvertDecimalToWords(double number)    
        {         
            int i = (int)number;      
            int p = (int)((number - i) * 100);     
            return ConvertNumberToWords(i) + " DOLLARS AND " + ConvertNumberToWords(p) + " CENTS";
            string sss = "111";
        }   
        //上面的代码中两个核心函数是ConvertNumberToWords和ConvertThreeDigitToWords，ConvertThreeDigitToWords的作用主要是能将小于1000的整数转为相应的金额，而ConvertNumberToWords负责将不同段的金额组合成完整的金额，主要是加上了该金额对应的位，例如本程序由于只要求对20亿以内的数字进行处理，因此分为千，百万和十亿三档。理解好了这两个函数基本就能知道是怎么做的。
        //（编辑： dotnetstudio）
        public static string ConvertDecimalToRoman(int number)       
        {          
            int[] decArray = {1000,900,500,400,100,90,50,40,10,9,5,4,1};     
            string[] romAarry = { "M", "CM", "D", "CD", "C", "XC", "L", "XL", "X", "IX", "V", "IV", "I" };     
            int i = 0;        
            string output = "";   
            while (number > 0)          
            {          
                while (number >= decArray[i])    
                {                
                    number = number - decArray[i];  
                    output = output + romAarry[i];           
                }            
                i++;     
            }       
            return output;  
        }

        //测试数据
        //7                        VII
        //1981                 MCMLXXXI
        //99                     XCIX
        //700                   DCC
        //经测试，测试数据全部通过。

        /// 
        /// 罗马数字转十进制数        /// 
        ///
        public static int ConvertRomanToDecimal(string number)      
        {            
            Dictionary<string,int> dic = new System.Collections.Generic.Dictionary<string,int>();       
            dic.Add("M", 1000);        
            dic.Add("CM", 900);            
            dic.Add("D", 500);     
            dic.Add("CD", 400);    
            dic.Add("C", 100);      
            dic.Add("XC", 90);   
            dic.Add("L", 50);          
            dic.Add("XL", 40);        
            dic.Add("X", 10);        
            dic.Add("IX", 9);        
            dic.Add("V", 5);         
            dic.Add("IV", 4);      
            dic.Add("I", 1);      
            int len = number.Length;    
            if (len == 1)        
            {            
                return dic[number];   
            }          
            if (len > 1)      
            {              
                int i = 0;         
                int sum = 0;           
                while (i < len)             
                {                 
                    int step = 1;      
                    if (len - i > 1)      
                    {                  
                    step = 2;          
                    }                  
                    string cnum = number.Substring(i, step);     
                    if (dic.ContainsKey(cnum))             
                    {                       
                        sum += dic[cnum];                 
                        i = i + step;                 
                    }                    
                    else                
                    {                  
                        sum += dic[number.Substring(i, 1)];  
                        i = i + 1;               
                    }              
                 }           
                 return sum;    
            }          
            return -1;    
       }    
        /// 
        /// 加法器        /// 
        /// 输入罗马加法公式        ///    
        public static string RomanCalculator(string s)      
        {           
            string[] array = s.Split('+');         
            int sum = ConvertRomanToDecimal(array[0].Trim()) + ConvertRomanToDecimal(array[1].Trim());   
            return ConvertDecimalToRoman(sum);    
        }

        //调用RomanCalculator()函数，输入如下的测试数据进行测试。
        //Input                            Output
        //XX + II                           XXII
        //I + V                              VI
        //II + II                             IV
        //CCC + CCC               DC
        //D + D                           M
            }
}