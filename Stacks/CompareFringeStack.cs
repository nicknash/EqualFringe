using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EqualFringe.Stacks
{
    class CompareFringeStack
    {
        public static FringeComparisonResult CompareFringes(Node firstRoot, Node secondRoot, Func<Node, Stack<Node>, int> getNextLeaf)
        {
            var firstStack = new Stack<Node>();
            var secondStack = new Stack<Node>();
            int first = getNextLeaf(firstRoot, firstStack);
            int second = getNextLeaf(secondRoot, secondStack);
            int leafNumber = 0;
            while (first == second && firstStack.Count > 0 && secondStack.Count > 0)
            {
                first = getNextLeaf(firstStack.Pop(), firstStack);
                second = getNextLeaf(secondStack.Pop(), secondStack);
                ++leafNumber;
            }
            var result = FringeComparisonResult.ClassifyResult(
                           firstStack.Count > 0, new Lazy<int>(() => getNextLeaf(firstStack.Pop(), firstStack))
                          , secondStack.Count > 0, new Lazy<int>(() => getNextLeaf(secondStack.Pop(), secondStack)), leafNumber);
            return result;
        }
    }
}
