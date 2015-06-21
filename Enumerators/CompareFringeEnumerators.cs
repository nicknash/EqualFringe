using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EqualFringe.Enumerators
{
    class CompareFringeEnumerators
    {
        public static FringeComparisonResult Compare(IEnumerator<int> first, IEnumerator<int> second)
        {
            int leafNumber = 0;
            bool firstHasMore; 
            bool secondHasMore;
            do
            {
                firstHasMore = first.MoveNext();
                secondHasMore = second.MoveNext();
                ++leafNumber;
            } while (firstHasMore && secondHasMore && first.Current == second.Current);
            var result = FringeComparisonResult.Equal;
            if (firstHasMore && secondHasMore)
            {
                result = FringeComparisonResult.FoundUnequalLeaves(first.Current, second.Current, leafNumber);
            }
            if (firstHasMore && !secondHasMore)
            {
                result = FringeComparisonResult.FirstHasMoreLeavesThanSecond(first.Current);
            }
            if (!firstHasMore && secondHasMore)
            {
                result = FringeComparisonResult.SecondHasMoreLeavesThanFirst(second.Current);
            }
            return result;
        }
    }
}
