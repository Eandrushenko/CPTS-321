using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics; //Used for Stopwatch


namespace MergeSort
{
    class Program
    {
        static int[] makelist(int size)
        {
            int[] array = new int[size];
            Random randomInt = new Random();

            for (int i = 0; i < size; i++)
            {
                array[i] = randomInt.Next(1, 10000);
            }
            return array;
        }

        static void testSorts(int size)
        {
            MergeSort merger = new MergeSort();
            int[] list = new int[size];

            Console.Write(" Starting test for size " + size);

            list = makelist(size);

            Stopwatch C1 = Stopwatch.StartNew();
            MergeSort.Classic(list);
            C1.Stop();

            Stopwatch C2 = Stopwatch.StartNew();
            MergeSort.Threaded(list);
            C2.Stop();

            Console.WriteLine(" - Test Completed:");
            Console.WriteLine("   Normal Sort time (ms):       " + C1.ElapsedMilliseconds);
            Console.WriteLine("   Threaded Sort time (ms):     " + C2.ElapsedMilliseconds);
        }

        static void Main(string[] args)
        {

            Console.WriteLine("Starting tests of merge sort vs. threaded merge sort");
            Console.WriteLine("   Array sizes under test: [8, 64, 256, 1028]");
            testSorts(8);
            testSorts(64);
            testSorts(256);
            testSorts(1024);
            Console.Write("Program Complete, ");
        }
    }
}
