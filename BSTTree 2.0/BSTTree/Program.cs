using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSTTree
{
    class Program
    {
        static void Main(string[] args)
        {
            BST<int> tree = new BST<int>();

            string numberlist;

            Console.WriteLine("Enter a collection of numbers in the range [0, 100], separated by spaces:");
            numberlist = Console.ReadLine();

            //extract the numbers and put them into the tree, keep count as inserts are made
            int x = 0;
            int count = 0;
            string[] splitter = numberlist.Split(' ');
            foreach (string number in splitter)
            {
                x = int.Parse(number); //string to int
                count = tree.insert(x, count);
            }

            //inorder traversal of the tree
            Console.Write("Tree Contents: ");
            tree.inorder(tree.returnroot());
            Console.Write("\n");
            Console.WriteLine("Tree Statistics:");

            //display count of the tree
            Console.Write("   Number of nodes: ");
            Console.WriteLine(count);

            //count height of the tree
            Console.Write("   Number of levels: ");
            int height = 0;
            height = tree.height(tree.returnroot());
            Console.WriteLine(height);

            //calcuate minimum height for n nodes
            double minheight = Math.Ceiling(Math.Log(count + 1, 2));
            Console.Write("   Minimum number of levels that a tree with {0} nodes could have = ", count);
            Console.WriteLine("{0}", minheight);
            Console.WriteLine("Done");



        }
    }
}
