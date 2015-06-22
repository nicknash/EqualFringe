using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EqualFringe.Enumerators
{
    class EqualFringeExplicitEnumerators
    {
        public static FringeComparisonResult CompareFringes(Node firstRoot, Node secondRoot)
        {
            var result = CompareFringeEnumerators.Compare(new LeafEnumerator(firstRoot), new LeafEnumerator(secondRoot));
            return result;
        }
    }
}
