using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSTTree
{
    class Node
    {
        public int value = 0;
        public Node left = null;
        public Node right = null;
    }
    class BST
    {
        public Node root = null;
        public BST()
        {
            root = null;
        }
        public Node returnroot()
        {
            return root;
        }
        public int insert(int v, int count)
        {
            Node newNode = new Node();
            newNode.value = v;
            if (root == null)
            {
                root = newNode;
                return count + 1;
            }
            else
            {
                Node current = root;
                Node parent;
                while (true)
                {
                    parent = current;
                    if (v < current.value)
                    {
                        current = current.left;
                        if (current == null)
                        {
                            parent.left = newNode;
                            return count + 1;
                        }
                    }
                    else if (v > current.value)
                    {
                        current = current.right;
                        if (current == null)
                        {
                            parent.right = newNode;
                            return count + 1;
                        }
                    }
                    else if (v == current.value)
                    {
                        //Console.WriteLine("{0} already exists in this tree", v);
                        return count;
                    }
                }
            }
        }
        public void inorder(Node localroot)
        {
            if (localroot != null)
            {
                inorder(localroot.left);
                Console.Write("{0} ", localroot.value);
                inorder(localroot.right);
            }
        }
        public int height(Node localroot)
        {
            if (localroot == null)
            {
                return 0;
            }
            return 1 + Math.Max(height(localroot.left), height(localroot.right));
        }

    }
}
