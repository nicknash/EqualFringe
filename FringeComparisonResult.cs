using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EqualFringe
{
    class FringeComparisonResult
    {
        private readonly string _description;
        public static readonly FringeComparisonResult Equal = new FringeComparisonResult("Fringes are equal.");

        public FringeComparisonResult(string description)
        {
            _description = description;
        }

        public static FringeComparisonResult FirstHasMoreLeavesThanSecond(int first)
        {
            return new FringeComparisonResult(String.Format("First fringe has a leaf with value {0} but second fringe has no more leaves.", first));
        }

        public static FringeComparisonResult SecondHasMoreLeavesThanFirst(int second)
        {
            return new FringeComparisonResult(String.Format("Second fringe has a leaf with value {0} but first fringe has no more leaves.", second));
        }

        public static FringeComparisonResult FoundUnequalLeaves(int first, int second, int leafNumber)
        {
            return new FringeComparisonResult(String.Format("Fringes are unequal at leaf number {0}, first fringe has a leaf with value {1} but second fringe has a leaf with value {2}.", leafNumber, first, second));
        }

        public static FringeComparisonResult ClassifyResult(bool firstHasMore, Lazy<int> first, bool secondHasMore, Lazy<int> second, int leafNumber)
        {
            var result = FringeComparisonResult.Equal;
            if (firstHasMore && secondHasMore)
            {
                result = FringeComparisonResult.FoundUnequalLeaves(first.Value, second.Value, leafNumber);
            }
            if (firstHasMore && !secondHasMore)
            {
                result = FringeComparisonResult.FirstHasMoreLeavesThanSecond(first.Value);
            }
            if (!firstHasMore && secondHasMore)
            {
                result = FringeComparisonResult.SecondHasMoreLeavesThanFirst(second.Value);
            }
            return result;
        }

        public override string ToString()
        {
            return _description;
        }
    }
}
