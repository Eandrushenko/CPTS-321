using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSTTree
{
    public abstract class BinTree<T> where T :IComparable
    {
        public abstract int insert(T v, int count);
        public abstract bool contains(Node<T> localroot, T v);
        public abstract void inorder(Node<T> localroot);
        public abstract void preorder(Node<T> localroot);
        public abstract void postorder(Node<T> localroot);
        public abstract int height(Node<T> localroot);
    }
}
