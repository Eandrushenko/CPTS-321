//Elijah Andrushenko
//011476324
//Fibonacci.cs
//This page deals with all the Fibonacci fucntions and calculations etc...




using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace NotepadApp
{
    class Fibonacci
    {
        //Function to find Fibonacci Number
        public static BigInteger Fib(int N)
        {
            BigInteger a = 0, b = 1, c;
            if (N == 1)         
            {
                return a;
            }
            else
            {
                for (int i = 3; i <= N; i++)        
                {
                    c = a + b;                       
                    a = b;                           
                    b = c;                           
                }
                return b;
            }
        }
    }
    class FibonacciTextReader : System.IO.TextReader
    {
        private int nLines; 
        private int currentLine = 1;         
        public FibonacciTextReader(int n)
        {
            nLines = n;
        }

        public override string ReadLine()
        {
            return Fibonacci.Fib(currentLine).ToString();  //.ToString() is needed to print to the file
        }

        public override string ReadToEnd()
        {
            string fibNumbers = "";
            while (currentLine <= nLines)
            {
                fibNumbers += $"{currentLine}: " + ReadLine(); 
                if (currentLine != nLines)              
                {
                    fibNumbers += Environment.NewLine;
                }
                ++currentLine;
            }
            return fibNumbers;
        }
    }
}