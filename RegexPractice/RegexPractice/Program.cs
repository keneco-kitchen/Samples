using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace RegexPractice
{
    class Program
    {
        static void Main(string[] args)
        {
            var l_reg = new Regex("^[\x20-\x7e]{4,32}$");
            var l_test = new List<string>()
            {
                "abcd",
                "ab",
                "This is a pen",
                "%%%%%",
                "青い空",
                "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa",
            };
            foreach(var l_item in l_test)
            {
                if(l_reg.IsMatch(l_item))
                {
                    Console.WriteLine($"{l_item} is match.");
                }
                else
                {
                    Console.WriteLine($"{l_item} is not match.");
                }
            }
            Console.ReadKey();
        }
    }
}
