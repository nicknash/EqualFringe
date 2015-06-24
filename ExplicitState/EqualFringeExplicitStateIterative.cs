using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EqualFringe.ExplicitState
{
    class EqualFringeExplicitStateIterative
    {
        public static FringeComparisonResult CompareFringes(Node firstRoot, Node secondRoot)
        {
            var result = CompareFringeExplicitState.CompareFringes(firstRoot, secondRoot, GetNextLeaf);
            return result;
        }

        private static State GetNextLeaf(State s)
        {
            State current = s;
            while (current != null && !current.IsLeaf)
            {
                var nextStates = current.ChildrenToProcess;
                bool advanced = false;                
                while (nextStates.MoveNext())
                {
                    current = nextStates.Current;
                    advanced = true;
                    break;
                }
                if (advanced)
                {
                    continue;
                }
                current = current.Parent;
            }
            return current ?? State.NoMoreLeaves;
        }

    }
}
