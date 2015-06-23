using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EqualFringe.ExplicitState
{
    class EqualFringeExplicitStateRecursive
    {
        public static FringeComparisonResult CompareFringes(Node firstRoot, Node secondRoot)
        {
            var first = GetNextLeaf(new State(firstRoot));
            var second = GetNextLeaf(new State(secondRoot));
            int leafNumber = 1;
            while (first != State.NoMoreLeaves && second != State.NoMoreLeaves)
            {                
                if (first.LeafValue != second.LeafValue)
                {
                    return FringeComparisonResult.FoundUnequalLeaves(first.LeafValue, second.LeafValue, leafNumber);
                }
                first = first.Continue(GetNextLeaf);
                second = second.Continue(GetNextLeaf);
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

        private static State GetNextLeaf(State s)
        {
            if (s.IsLeaf)
            {
                return s;
            }
            var nextStates = s.ChildrenToProcess;
            while (nextStates.MoveNext())
            {
                return GetNextLeaf(nextStates.Current);
            }
            return s.Continue(GetNextLeaf);
        }
    }
}
