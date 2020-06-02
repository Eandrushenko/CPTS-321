using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Set up
            List<int> numberlist = new List<int>();
            int length = 10000;
            int range = 20000;
            Makelist(numberlist, length, range);

            //Part 1 - Hashset Method
            int result = 0;
            result = UniqueHash(numberlist);
            this.textBox1.AppendText("1. HashSet method: ");
            this.textBox1.AppendText(result.ToString());
            this.textBox1.AppendText(" Unique Numbers\n");

            //Part 1 Time complexity explanation
            this.textBox1.AppendText("The Time Complexity is: O(n), Linear Time\n\n");
            this.textBox1.AppendText("List length = n, inserting every element into the list is n inserts.\n");
            this.textBox1.AppendText("Checking if a key already exists is an O(1) operation, as I assume we have a good hash\n");
            this.textBox1.AppendText("If it's a bad hash this can be O(n) but we will stick with O(1) for checking keys.\n");
            this.textBox1.AppendText("Getting the size of the hash can be O(1) if the length is stored somewhere, but\n");
            this.textBox1.AppendText("we will assume it isn't. Thus we have to do a count which is another O(n) operation which leaves us\n");
            this.textBox1.AppendText("with O(n) from inserts + O(n) from counting the hash size = O(2n) which is linear time hence O(n).\n");

            //Part 2 O(1) Storage Method
            result = UniqueStorage(numberlist, length);
            this.textBox1.AppendText("2. O(1) storage method: ");
            this.textBox1.AppendText(result.ToString());
            this.textBox1.AppendText(" Unique Numbers\n");

            //Part 3 Sorted Method
            result = UniqueSorted(numberlist, length);
            this.textBox1.AppendText("3. Sorted method: ");
            this.textBox1.AppendText(result.ToString());
            this.textBox1.AppendText(" Unique Numbers\n");




        }
        //create the list for 10k integers in a range of 0-20k
        private void Makelist(List<int> mylist, int listlength, int range)
        {
            Random Rand = new Random();
            int i = 0;
            int number = 0;

            while (i < listlength)
            {
                number = Rand.Next(0, range);
                mylist.Add(number);
                i++;
            }

        }
        private int UniqueHash(List<int> mylist)
        {
            Dictionary<string, int> map = new Dictionary<string, int>();
            foreach (int number in mylist)
            {
                if (!map.ContainsKey(number.ToString()))
                {
                    map.Add(number.ToString(), number);
                }
            }
            return map.Count;
        }

        private int UniqueStorage(List<int> mylist, int listlength)
        {
            int unique_count = 0;
            int i = 0, j = 0;
            while (i < listlength)
            {
                unique_count++;
                j = i + 1;
                while (j < listlength)
                {
                    if (mylist[i] == mylist[j])
                    {
                        unique_count--;
                        j = listlength; //break out of the inner while loop so we dont continue subtracting from unique_count if more duplicates appear
                    }
                    j++;
                }
                i++;
            }
            return unique_count;

        }

        private int UniqueSorted(List<int> mylist, int listlength)
        {
            int unique_count = listlength;
            mylist.Sort();
            int i = 0;
            while (i < listlength - 1)
            {
                if (mylist[i] == mylist[i + 1])
                {
                    unique_count--;
                }
                i++;
            }
            return unique_count;
        }
    }
}