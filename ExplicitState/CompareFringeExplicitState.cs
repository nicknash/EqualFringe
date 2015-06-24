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
            int leafNumber = 1;
            while (first != State.NoMoreLeaves && second != State.NoMoreLeaves)
            {
                if (first.LeafValue != second.LeafValue)
                {
                    return FringeComparisonResult.FoundUnequalLeaves(first.LeafValue, second.LeafValue, leafNumber);
                }
                first = first.Continue(getNextLeaf);
                second = second.Continue(getNextLeaf);
                ++leafNumber;
            }
            var result = FringeComparisonResult.Equal;
            if (first == State.NoMoreLeaves && second != State.NoMoreLeaves)
            {
                result = FringeComparisonResult.SecondHasMoreLeavesThanFirst(second.LeafValue);
            }
            else if (second == State.NoMoreLeaves)
            {
                result = FringeComparisonResult.FirstHasMoreLeavesThanSecond(first.LeafValue);
            }
            return result;
        }
    }
}
