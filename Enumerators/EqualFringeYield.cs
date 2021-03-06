﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EqualFringe.Enumerators
{
    class EqualFringeYield
    {
        public static FringeComparisonResult CompareFringes(Node firstRoot, Node secondRoot)
        {
            var firstFringe = GetFringe(firstRoot);
            var secondFringe = GetFringe(secondRoot);
            var result = CompareFringeEnumerators.Compare(firstFringe.GetEnumerator(), secondFringe.GetEnumerator());
            return result;
        }

        public static IEnumerable<int> GetFringe(Node root)
        {
            if (root.IsLeaf)
            {
                yield return root.Value;
            }
            foreach (Node child in root.Children)
            {
                foreach (var v in GetFringe(child))
                {
                    yield return v;
                }
            }
        }
    }
}
