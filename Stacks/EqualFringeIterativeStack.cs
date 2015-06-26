using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EqualFringe.Stacks
{
    class EqualFringeIterativeStack
    {
        public static FringeComparisonResult CompareFringes(Node firstRoot, Node secondRoot)
        {
            var result = CompareFringeStack.CompareFringes(firstRoot, secondRoot, GetNextLeaf);
            return result;
        }
        
        private static int GetNextLeaf(Node where, Stack<Node> rightChildren)
        {
            Node current = where;
            while (!current.IsLeaf)
            {
                Node next = current.Right;
                if (current.HasLeft)
                {
                    next = current.Left;
                    if (current.HasRight)
                    {
                        rightChildren.Push(current.Right);
                    }
                }
                current = next;
            }
            return current.Value;
        }

    }
}
