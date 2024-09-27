using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace arboles
{
    public class BinaryTreeNode<T>
    {
        public BinaryTreeNode(T data, BinaryTreeNode<T> left, BinaryTreeNode<T> right)
        {
            Data = data;
            Left = left;
            Right = right;
        }

        public BinaryTreeNode(T data)
        {
            Data = data;
        }

        public BinaryTree<T> EquivalentData { get; set; }

        public T Data { get; set; }

        public BinaryTreeNode<T> Left { get; set; }

        public BinaryTreeNode<T> Right { get; set; }
    }
}
