using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CptS321
{
    public abstract class Node
    {
        protected double number;
        public double nnumber
        {
            get
            {
                return number;
            }
            set
            {
                number = value;
            }
        }
    }

    public class numNode:Node
    {
        public numNode()
        {
            number = 0;
        }

    }
    public class varNode:Node
    {
        protected string name;
        public string nname
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }
        public varNode()
        {
            number = 0;
            name = "";
        }
    }
    public class opNode:Node
    {
        protected Node left;
        protected Node right;
        protected char exp;

        public Node rright
        {
            get
            {
                return right;
            }
            set
            {
                right = value;
            }
        }
        public Node lleft
        {
            get
            {
                return left;
            }
            set
            {
                left = value;
            }
        }
        public char eexp
        {
            get
            {
                return exp;
            }
            set
            {
                exp = value;
            }
        }
        public opNode()
        {
            left = null;
            right = null;
            exp = ' ';
            number = 0;
        }
    }
}
