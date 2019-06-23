# Binary-Search-Tree

### Binary Search Trees in general
A binary search trees (BST) is a container that allows fast lookup, addition and removal of items, and can be used to implement either dynamic sets of items, or lookup tables that allow finding an item by its key (e.g., finding the phone number of a person by name).

Binary search trees keep their items in sorted order, so that lookup and other operations can use the principle of binary search: when looking for a item in a tree (or a place to insert a new item), they traverse the tree from root to leaf, making comparisons to item stored in the nodes of the tree and deciding, on the basis of the comparison, to continue searching in the left or right subtrees.

### About
This Project implements a Binary Search Tree in C# and has functions for **Traversal, Insertion, Deletion and Search**.

### Example


```` c#
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
