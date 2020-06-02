using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace CptS321
{
    class Program
    {
        static void Main(string[] args)
        {
            ExpTree mytree = new ExpTree("A1-12-C1");

            int menu = 1;
            string words;
            int numbers;
            double result;

            while (menu != 4)
            {
                Console.WriteLine("current expression=\"{0}\")", mytree.rregex);
                Console.WriteLine("   1 = Enter a new expression");
                Console.WriteLine("   2 = Set a variable value");
                Console.WriteLine("   3 = Evaluate tree");
                Console.WriteLine("   4 = Quit");

                menu = Convert.ToInt32(Console.ReadLine());
                if (menu == 1)
                {
                    Console.Write("Enter new expression: ");
                    words = Console.ReadLine();
                    mytree.rregex = words;
                    mytree = new ExpTree(words);
                }
                else if (menu == 2)
                {
                    Console.Write("Enter variable name: ");
                    words = Console.ReadLine();
                    Console.Write("Enter variable value: ");
                    numbers = Convert.ToInt32(Console.ReadLine());
                    mytree.setVar(words, numbers);
                }
                else if (menu == 3)
                {
                    result = mytree.Eval();
                    Console.WriteLine("{0}", result);

                }
            }
            Console.WriteLine("Done");


        }
    }
}
