using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EqualFringe.RandomTree
{
    class MaybeRandomTree
    {
        public int Size { get; private set; }
        public Node Root { get; private set; }

        public static readonly MaybeRandomTree Failed = new MaybeRandomTree();

        public static MaybeRandomTree Leaf(int value)
        {
            var root = Node.Leaf(value);
            return new MaybeRandomTree { Root = root, Size = 1 }; 
        }

        public static MaybeRandomTree Combine(MaybeRandomTree left, MaybeRandomTree right)
        {
            if (left == Failed || right == Failed)
            {
                return Failed;
            }
            var root = Node.Internal(left.Root, right.Root);
            return new MaybeRandomTree { Root = root, Size = left.Size + right.Size };
        }
    }
}
