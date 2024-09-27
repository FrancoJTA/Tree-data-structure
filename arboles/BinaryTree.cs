using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace arboles
{
    public class BinaryTree<T>
    {
        public readonly Queue<BinaryTreeNode<T>> _inOrderTraversalResult = new Queue<BinaryTreeNode<T>>();
        public readonly Queue<BinaryTreeNode<T>> _preOrderTraversalResult = new Queue<BinaryTreeNode<T>>();
        public readonly Queue<BinaryTreeNode<T>> _postOrderTraversalResult = new Queue<BinaryTreeNode<T>>();
        public readonly Queue<BinaryTreeNode<T>> _balancedBinaryTreeNodes = new Queue<BinaryTreeNode<T>>();

        public BinaryTreeNode<T> Root { get; set; }

        public virtual BinaryTreeNode<T> BalanceHelper(List<BinaryTreeNode<T>> nodes, int start, int end)
        {
            if (start > end)
            {
                return null;
            }

            int mid = (start + end) / 2;
            BinaryTreeNode<T> node = nodes[mid];
            node.Left = BalanceHelper(nodes, start, mid - 1);
            node.Right = BalanceHelper(nodes, mid + 1, end);

            return node;
        }
        public int Compare(T data1, T data2, IComparer<T> comparer = null)
        {
            if (comparer == null) comparer = Comparer<T>.Default;
            return comparer.Compare(data1, data2);
        }
        public void Clear()
        {
            Root = null;
        }
        public Queue<BinaryTreeNode<T>> BreadthFirst(BinaryTreeNode<T> currentNode = null)
        {
            var output = new Queue<BinaryTreeNode<T>>();

            if (currentNode == null) currentNode = Root;
            var tempQueue = new Queue<BinaryTreeNode<T>>();

            if (currentNode == null) return new Queue<BinaryTreeNode<T>>();

            tempQueue.Enqueue(currentNode);

            while (tempQueue.Count > 0)
            {
                currentNode = tempQueue.Dequeue();
                output.Enqueue(currentNode);

                if (currentNode.Left != null) tempQueue.Enqueue(currentNode.Left);
                if (currentNode.Right != null) tempQueue.Enqueue(currentNode.Right);
            }

            return output;
        }

        public Queue<BinaryTreeNode<T>> InOrderTraversal(BinaryTreeNode<T> node = null)
        {
            if (node == null) node = Root;

            return InOrderTraversalHelper(node);
        }
        public Queue<BinaryTreeNode<T>> InOrderTraversalHelper(BinaryTreeNode<T> node)
        {
            if (node != null)
            {
                InOrderTraversalHelper(node.Left);
                _inOrderTraversalResult.Enqueue(node);
                InOrderTraversalHelper(node.Right);
            }

            return _inOrderTraversalResult;
        }

        public Queue<BinaryTreeNode<T>> PreOrderTraversal(BinaryTreeNode<T> node = null)
        {
            if (node == null) node = Root;
            return InOrderTraversalHelper(node);
        }
        public void Insert(T data, IComparer<T> comparer = null)
        {
            if (comparer == null) comparer = Comparer<T>.Default;

            var currentNode = Root;
            BinaryTreeNode<T> prev = null;

            while (currentNode != null)
            {
                int compareResult = comparer.Compare(data, currentNode.Data);

                if (compareResult == 0) return;

                prev = currentNode;
                currentNode = compareResult < 0 ? currentNode.Left : currentNode.Right;
            }

            if (Root == null) Root = new BinaryTreeNode<T>(data);

            else
            {
                int compareResult = comparer.Compare(data, prev.Data);

                if (compareResult < 0) prev.Left = new BinaryTreeNode<T>(data);
                else prev.Right = new BinaryTreeNode<T>(data);
            }
        }
        public virtual BinaryTreeNode<T> Balance()
        {
            List<BinaryTreeNode<T>> inOrder = new List<BinaryTreeNode<T>>();
            var q = InOrderTraversal();
            foreach (var a in q) inOrder.Add(a);
            int n = inOrder.Count;
            return BalanceHelper(inOrder, 0, n - 1);
        }
        public int NumberOfLeavesCounter(BinaryTreeNode<T> currentNode)
        {
            if (currentNode == null) return 0;
            if (currentNode.Left == null && currentNode.Right == null) return 1;
            else return NumberOfLeavesCounter(currentNode.Left) + NumberOfLeavesCounter(currentNode.Right) + 1;
        }

        public int levelFromRootCounter(BinaryTreeNode<T> node)
        {
            int levelCounter = 1;
            var currNode = Root;

            while (node != currNode)
            {
                if (Compare(currNode.Data, node.Data) == 1)
                {
                    currNode = currNode.Left;
                    levelCounter++;
                }
                else if (Compare(currNode.Data, node.Data) == -1)
                {
                    currNode = currNode.Right;
                    levelCounter++;
                }
            }

            return levelCounter;
        }
        public void Delete(T key)
        {
            Root = DeleteHelper(Root, key);
        }

        private BinaryTreeNode<T> DeleteHelper(BinaryTreeNode<T> root, T key)
        {
            if (root == null)
            {
                return root;
            }
            IComparer<T> comparer = Comparer<T>.Default;
            int result = comparer.Compare(root.Data, key);

            if (result > 0)
            {
                root.Left = DeleteHelper(root.Left, key);
            }
            else if (result < 0)
            {
                root.Right = DeleteHelper(root.Right, key);
            }
            else
            {
                if (root.Left == null)
                {
                    return root.Right;
                }
                else if (root.Right == null)
                {
                    return root.Left;
                }
                root.Data = MinimumValue(root.Right);
                root.Right = DeleteHelper(root.Right, root.Data);
            }

            return root;
        }
        T MinimumValue(BinaryTreeNode<T> root)
        {
            T minv = root.Data;

            while (root.Left != null)
            {
                minv = root.Left.Data;
                root = root.Left;
            }

            return minv;
        }

        public BinaryTreeNode<T> Search(T data, IComparer<T> comparer = null)
        {
            if (comparer == null) comparer = Comparer<T>.Default;

            var currentNode = Root;
            BinaryTreeNode<T> prev = null;

            while (currentNode != null)
            {
                int compareResult = comparer.Compare(data, currentNode.Data);

                if (compareResult == 0) return currentNode;

                prev = currentNode;
                currentNode = compareResult < 0 ? currentNode.Left : currentNode.Right;
            }

            return null;
        }
        public Queue<BinaryTreeNode<T>> PreOrderTraversalHelper(BinaryTreeNode<T> node = null)
        {
            if (node != null)
            {
                _preOrderTraversalResult.Enqueue(node);
                PreOrderTraversalHelper(node.Left);
                PreOrderTraversalHelper(node.Right);
            }

            return _preOrderTraversalResult;
        }

        public Queue<BinaryTreeNode<T>> PostOrderTraversalHelper(BinaryTreeNode<T> node = null)
        {
            if (node != null)
            {
                PostOrderTraversalHelper(node.Left);
                PostOrderTraversalHelper(node.Right);
                _postOrderTraversalResult.Enqueue(node);
            }

            return _postOrderTraversalResult;
        }

        public BinaryTreeNode<T> GetParent(BinaryTreeNode<T> toGetParent, BinaryTreeNode<T> root)
        {
            var result = toGetParent;

            if (root.Left != null)
                result = toGetParent == root.Left ? root : GetParent(result, root.Left);

            if (root.Right != null)
                result = toGetParent == root.Right ? root : GetParent(result, root.Right);

            return result;
        }
    }
}
