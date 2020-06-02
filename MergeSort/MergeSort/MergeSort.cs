using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace MergeSort
{
    class MergeSort
    {
        //Print a list
        public static void Printlist(int[] array)
        {
            Console.Write("[");
            for (int i = 0; i < array.Length - 1; i++)
            {
                Console.Write(array[i] + ", ");
            }
            Console.Write(array[array.Length - 1]);
            Console.WriteLine("]");
        }

        //Threaded MergeSort
        public static int[] Threaded(int[] array)
        {
            int[] left;
            int[] right;
            int[] result = new int[array.Length];

            if (array.Length <= 1)
            {
                return array;
            }

            int midPoint = array.Length / 2;

            left = new int[midPoint];

            if (array.Length % 2 == 0)
            {
                right = new int[midPoint];
            }
            else
            {
                right = new int[midPoint + 1];
            }

            //populate left array  
            for (int i = 0; i < midPoint; i++)
            {
                left[i] = array[i];
            }

            //populate right array          
            int x = 0;
            for (int i = midPoint; i < array.Length; i++)
            {
                right[x] = array[i];
                x++;
            }
            //Do Threading On Each Side

            Thread LeftThread = new Thread(() => left = Threaded(left));
            LeftThread.Start();
            Thread RightThread = new Thread(() => right = Threaded(right));
            RightThread.Start();

            LeftThread.Join();
            RightThread.Join();

            result = merge(left, right);

            return result;
        }
        //Classic MergeSort, No Threading
        public static int[] Classic(int[] array)
        {
            int[] left;
            int[] right;
            int[] result = new int[array.Length];

            if (array.Length <= 1)
            {
                return array;
            }

            int midPoint = array.Length / 2;
  
            left = new int[midPoint];

            if (array.Length % 2 == 0)
            {
                right = new int[midPoint];
            }
            else
            {
                right = new int[midPoint + 1];
            }

            //populate left array  
            for (int i = 0; i < midPoint; i++)
            {
                left[i] = array[i];
            }

            //populate right array          
            int x = 0;
            for (int i = midPoint; i < array.Length; i++)
            {
                right[x] = array[i];
                x++;
            }
            left = Classic(left);  
            right = Classic(right);

            result = merge(left, right);

            return result;
        }

        //Auxillary function for MergeSort 
        private static int[] merge(int[] left, int[] right)
        {
            int resultLength = right.Length + left.Length;
            int[] result = new int[resultLength];
             
            int indexLeft = 0, indexRight = 0, indexResult = 0;
 
            while (indexLeft < left.Length || indexRight < right.Length)
            { 
                if (indexLeft < left.Length && indexRight < right.Length)
                {  
                    if (left[indexLeft] <= right[indexRight])
                    {
                        result[indexResult] = left[indexLeft];
                        indexLeft++;
                        indexResult++;
                    }  
                    else
                    {
                        result[indexResult] = right[indexRight];
                        indexRight++;
                        indexResult++;
                    }
                } 
                else if (indexLeft < left.Length)
                {
                    result[indexResult] = left[indexLeft];
                    indexLeft++;
                    indexResult++;
                } 
                else if (indexRight < right.Length)
                {
                    result[indexResult] = right[indexRight];
                    indexRight++;
                    indexResult++;
                }

            }
            return result;
        }
    }
}
