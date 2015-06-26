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
            var result = FringeComparisonResult.ClassifyResult(firstHasMore, new Lazy<int>(() => first.Current), secondHasMore, new Lazy<int>(() => second.Current), leafNumber);
            return result;
        }
    }
}
