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
            var result = CompareFringeExplicitState.CompareFringes(firstRoot, secondRoot, GetNextLeaf);
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
