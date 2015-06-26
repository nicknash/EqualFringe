using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EqualFringe.Stacks
{
    class EqualFringeRecursiveStack
    {
        public static FringeComparisonResult CompareFringes(Node firstRoot, Node secondRoot)
        {
            var result = CompareFringeStack.CompareFringes(firstRoot, secondRoot, GetNextLeaf);
            return result;
        }
                
        private static int GetNextLeaf(Node where, Stack<Node> rightChildren)
        {
            if (where.IsLeaf)
            {
                return where.Value;
            }
            Node next = where.Right;
            if (where.HasLeft)
            {
                next = where.Left;
                if (where.HasRight)
                {
                    rightChildren.Push(where.Right);
                }
            }
            return GetNextLeaf(next, rightChildren);
        }
    }
}
