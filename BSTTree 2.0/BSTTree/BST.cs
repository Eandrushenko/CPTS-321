using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSTTree
{
    public class Node<T> where T :IComparable
    {
        public T value;
        public Node<T> left = null;
        public Node<T> right = null;

        //overloaded operators
        static public bool operator >(Node<T> left, Node<T> right)
        {
            if (left.value.CompareTo(right.value) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        static public bool operator <(Node<T> left, Node<T> right)
        {
            if (left.value.CompareTo(right.value) < 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        static public bool operator ==(Node<T> left, Node<T> right)
        {
            if (System.Object.ReferenceEquals(left, null))
            {
                if (System.Object.ReferenceEquals(right, null))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (System.Object.ReferenceEquals(right, null))
            {
                if (System.Object.ReferenceEquals(left, null))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return left.value.Equals(right.value);
        }
        static public bool operator !=(Node<T> left, Node<T> right)
        {
            if (!(System.Object.ReferenceEquals(left, null)))
            {
                if (!(System.Object.ReferenceEquals(right, null)))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (!(System.Object.ReferenceEquals(right, null)))
            {
                if (!(System.Object.ReferenceEquals(left, null)))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return !(left.value.Equals(right.value));
        }
        static public bool operator >=(Node<T> left, Node<T> right)
        {
            if ((left.value.CompareTo(right.value) == 0) || (left.value.CompareTo(right.value) > 0))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        static public bool operator <=(Node<T> left, Node<T> right)
        {
            if ((left.value.CompareTo(right.value) == 0) || (left.value.CompareTo(right.value) < 0))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    class BST<T> : BinTree<T> where T :IComparable
    {
        public Node<T> root = null;
        public BST()
        {
            root = null;
        }
        public Node<T> returnroot()
        {
            return root;
        }
        public override int insert(T v, int count)
        {
            Node<T> newNode = new Node<T>();
            newNode.value = v;
            if (root == null)
            {
                root = newNode;
                return count + 1;
            }
            else
            {
                Node<T> current = root;
                Node<T> parent;
                while (true)
                {
                    parent = current;
                    if (newNode < current)
                    {
                        current = current.left;
                        if (current == null)
                        {
                            parent.left = newNode;
                            return count + 1;
                        }
                    }
                    else if (newNode > current)
                    {
                        current = current.right;
                        if (current == null)
                        {
                            parent.right = newNode;
                            return count + 1;
                        }
                    }
                    else if (newNode == current)
                    {
                        //Console.WriteLine("{0} already exists in this tree", v);
                        return count;
                    }
                }
            }
        }
        public override void inorder(Node<T> localroot)
        {
            if (!(localroot == null))
            {
                inorder(localroot.left);
                Console.Write("{0} ", localroot.value);
                inorder(localroot.right);
            }
        }
        public override void preorder(Node<T> localroot)
        {
            if (!(localroot == null))
            {
                Console.Write("{0} ", localroot.value);
                preorder(localroot.left);
                preorder(localroot.right);
            }
        }
        public override void postorder(Node<T> localroot)
        {
            if (!(localroot == null))
            {
                postorder(localroot.left);
                postorder(localroot.right);
                Console.Write("{0} ", localroot.value);
            }
        }
        public override int height(Node<T> localroot)
        {
            if (localroot == null)
            {
                return 0;
            }
            return 1 + Math.Max(height(localroot.left), height(localroot.right));
        }
        public override bool contains(Node<T> localroot, T v)
        {
            Node<T> cur = localroot;
            Node<T> newNode = new Node<T>();
            newNode.value = v;
            while (!(cur == null))
            {
                if (newNode > cur)
                {
                    cur = cur.right;
                }
                else if (newNode < cur)
                {
                    cur = cur.left;
                }
                else
                {
                    return true;
                }
            }
            return false;


            
        }

    }
}
