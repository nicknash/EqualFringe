using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EqualFringe
{
    class Node
    {
        public int Value { get; private set; }
        public Node Left { get; private set; }
        public Node Right { get; private set; }
        public bool IsLeaf { get { return Left == null && Right == null; } }
        public bool HasLeft { get { return Left != null; } }
        public bool HasRight { get { return Right != null; } }

        public IEnumerable<Node> Children { get; private set; }

        public static Node Leaf(int value)
        {
            return new Node { Value = value, Children = Enumerable.Empty<Node>() };
        }

        public static Node Internal(Node left, Node right)
        {
            return new Node { Value = -1, Left = left, Right = right, Children = new[]{left,right} };
        }
    }
}
