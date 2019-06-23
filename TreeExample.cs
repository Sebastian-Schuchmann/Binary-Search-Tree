using System;
using BST;

class TreeExample
{
    static void Main(string[] args)
    {
        //Building a Tree
        var Tree = new BinarySearchTree<int>();

        //Insert a Value
        Tree.Insert(3);

        //Insert Values
        int[] Values = { 10, 5, 20, 6, 8, 15, 40, 1, 2, 9, 6 };
        Tree.Insert(Values);

        //Print Values (Sorted / InOrder)
        Tree.Traverse((Node<int> node) => Console.Write(node.Value + ", "), TraversalType.InOrder);
        Console.WriteLine();

        //Print Values in reverse sorted Order
        Tree.Traverse((Node<int> node) => Console.Write(node.Value + ", "), TraversalType.OutOrder);
        Console.WriteLine();

        //You can do any operation you want in Tree.Traverse() but, be warned...
        //... you can change the values and this can mess up the Tree!

        //Find a Node with a Value
        var Node = Tree.Search(8);

        //Delete a Node
        Tree.DeleteNode(Node);
    }
}