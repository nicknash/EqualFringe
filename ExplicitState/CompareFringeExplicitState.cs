using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EqualFringe.ExplicitState
{
    class CompareFringeExplicitState
    {
        public static FringeComparisonResult CompareFringes(Node firstRoot, Node secondRoot, Func<State, State> getNextLeaf)
        {
            var first = getNextLeaf(new State(firstRoot));
            var second = getNextLeaf(new State(secondRoot));
            int leafNumber = 0;
            while (first.HasMoreLeaves && second.HasMoreLeaves && first.LeafValue == second.LeafValue)
            {
                first = first.Continue(getNextLeaf);
                second = second.Continue(getNextLeaf);
            }
            var result = FringeComparisonResult.ClassifyResult(first.HasMoreLeaves, new Lazy<int>(() => first.LeafValue), second.HasMoreLeaves, new Lazy<int>(() => second.LeafValue), leafNumber);
            return result;
        }
    }
}
