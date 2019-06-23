using System;

namespace BST
{
    enum TraversalType
    {
        PreOrder,
        InOrder,
        OutOrder,
        PostOrder
    }

    class Node<T> where T : IComparable<T>, IEquatable<T>
    {
        public T Value;
        public Node<T> Left;
        public Node<T> Right;

        public Node(T val)
        {
            Value = val;
        }

        public bool HasTwoChildren()
        {
            if (Left != null && Right != null)
                return true;
            return false;
        }

        public bool HasOneChild()
        {
            if (Left != null ^ Right != null)
                return true;
            return false;
        }

        public bool HasNoChildren()
        {
            if (!HasTwoChildren() && !HasOneChild())
                return true;
            return false;
        }

        //Return limited to only childs
        public Node<T> Child()
        {
            if (HasOneChild())
            {
                if (Left != null) return Left;
                if (Right != null) return Right;
            }
            return null;
        }
    }

    class BinarySearchTree<T> where T : IComparable<T>, IEquatable<T>
    {
        public Node<T> Root;

        public void Insert(T Value)
        {
            if (Root == null)
            {
                Root = new Node<T>(Value);
            }
            else
            {
                InsertHelper(Value, Root);
            }
        }

        public void DeleteNode(Node<T> NodeToDelete)
        {
            //No children -> Just Delete
            if (NodeToDelete.HasNoChildren())
            {
                if (NodeToDelete.Equals(Root))
                {
                    Root = null;
                    return;
                }

                Traverse((Node<T> node) =>
                {
                    if (node.Left != null)
                    {
                        if (node.Left.Equals(NodeToDelete))
                            node.Left = null;
                    }
                    if (node.Right != null)
                    {
                        if (node.Right.Equals(NodeToDelete))
                            node.Right = null;
                    }
                }, TraversalType.PostOrder);
                return;
            }

            //Exactly one child -> Swap 
            if (NodeToDelete.HasOneChild())
            {
                if (NodeToDelete.Equals(Root))
                {
                    Root = Root.Child();
                    return;
                }

                Traverse((Node<T> node) =>
                {
                    if (node.Left != null)
                    {
                        if (node.Left.Equals(NodeToDelete))
                            node.Left = NodeToDelete.Child();
                    }
                    if (node.Right != null)
                    {
                        if (node.Right.Equals(NodeToDelete))
                            node.Right = NodeToDelete.Child();
                    }
                }, TraversalType.PostOrder);

                return;
            }

            //Find Minimum of Right sub-Tree, Copy Value, Delete Minimum 
            if (NodeToDelete.HasTwoChildren())
            {
                Node<T> SuccessorNode = MinValue(NodeToDelete.Right);
                NodeToDelete.Value = SuccessorNode.Value;
                DeleteNode(SuccessorNode);

                return;
            }

        }

        public void Insert(T[] Values)
        {
            if (Values.Length == 0)
                return;

            int index = 0;

            if (Root == null)
            {
                Root = new Node<T>(Values[0]);
                index++;
            }

            for (int i = index; i < Values.Length; i++)
                InsertHelper(Values[i], Root);
        }

        public Node<T> Search(T Value)
        {
            if (Root.Value.Equals(Value))
                return Root;

            else
            {
                return SearchHelper(Value, Root);
            }
        }

        private Node<T> SearchHelper(T Value, Node<T> node)
        {
            if (node == null)
                return node;

            if (Value.Equals(node.Value))
                return node;

            if (Value.CompareTo(node.Value) == -1) //-1 = "<"
            {
                node = SearchHelper(Value, node.Left);
            }
            else
            {
                node = SearchHelper(Value, node.Right);
            }

            return node;
        }

        private Node<T> InsertHelper(T Value, Node<T> node)
        {
            if (node == null)
            {
                node = new Node<T>(Value);
                return node;
            }
            if (Value.CompareTo(node.Value) == -1) //-1 = "<"
            {
                node.Left = InsertHelper(Value, node.Left);
            }
            else
            {
                node.Right = InsertHelper(Value, node.Right);
            }

            return node;
        }

        private Node<T> MinValue(Node<T> startingNode)
        {
            T minVal = startingNode.Value;
            while (startingNode.Left != null)
            {
                minVal = startingNode.Left.Value;
                startingNode = startingNode.Left;
            }
            return startingNode;
        }

        public void Traverse(Action<Node<T>> Process, TraversalType TravType)
        {
            if (Root == null)
            {
                return;
            }

            switch (TravType)
            {
                case TraversalType.InOrder: TraverseHelperInOrder(Root, Process); break;
                case TraversalType.OutOrder: TraverseHelperOutOrder(Root, Process); break;
                case TraversalType.PostOrder: TraverseHelperPostOrder(Root, Process); break;
                case TraversalType.PreOrder: TraverseHelperPreOrder(Root, Process); break;
            }
        }

        private void TraverseHelperPreOrder(Node<T> node, Action<Node<T>> Process)
        {
            if (node == null)
            {
                return;
            }

            Process(node);
            TraverseHelperPreOrder(node.Left, Process);
            TraverseHelperPreOrder(node.Right, Process);
        }
        private void TraverseHelperInOrder(Node<T> node, Action<Node<T>> Process)
        {
            if (node == null)
            {
                return;
            }

            TraverseHelperInOrder(node.Left, Process);
            Process(node);
            TraverseHelperInOrder(node.Right, Process);
        }
        private void TraverseHelperPostOrder(Node<T> node, Action<Node<T>> Process)
        {
            if (node == null)
            {
                return;
            }

            TraverseHelperPostOrder(node.Left, Process);
            TraverseHelperPostOrder(node.Right, Process);
            Process(node);
        }
        private void TraverseHelperOutOrder(Node<T> node, Action<Node<T>> Process)
        {
            if (node == null)
            {
                return;
            }

            TraverseHelperOutOrder(node.Right, Process);
            Process(node);
            TraverseHelperOutOrder(node.Left, Process);
        }
    }
}