using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EqualFringe.RandomTree
{
    /// <summary>
    /// This class provides a so-called size-limited critical Boltzmann sampler for generating 
    /// a random binary tree, where the distribution of the resulting sizes is "nice".
    /// 
    /// Suprisingly, this algorithm generates a random binary tree on average in O(n) time, assuming
    /// epsilon > 0.
    ///     
    /// The algorithm is from:
    /// 
    /// Duchon, Philippe, et al. “Boltzmann samplers for the random generation of combinatorial structures.” 
    /// Combinatorics Probability and Computing 13.4-5 (2004): 577-625.
    /// 
    /// This code is based on the lovely Haskell implementation at:
    /// 
    /// https://byorgey.wordpress.com/2013/04/25/random-binary-trees-with-a-size-limited-critical-boltzmann-sampler-2/
    /// </summary>
    class RandomTreeGenerator
    {
        /// <summary>
        /// Generate a random binary tree using a size-limited critical Boltzmann sampler (phew!)
        /// </summary>
        /// <param name="desiredSize">The desired size (i.e. number of nodes) of the resulting random binary tree.</param>
        /// <param name="epsilon">A number between 0 and 1, that controls the tolerance around the desired size of the returned tree. 
        /// Setting epsilon small increases the constant factors in the algorithm's run-time.</param>
        /// <returns>A random binary tree of size Allow trees of size between desiredSize * (1 - epsilon) and desiredSize * (1 + epsilon).</returns>
        public static MaybeRandomTree Generate(int desiredSize, double epsilon, Random randGen)
        {
            var wiggle = (int) (desiredSize * epsilon);
            var minSize = desiredSize - wiggle;
            var maxSize = desiredSize + wiggle;
            MaybeRandomTree candidate = MaybeRandomTree.Failed;
            while (candidate == MaybeRandomTree.Failed)
            {
                candidate = GenerateUpperBounded(maxSize, randGen);
                if (candidate.Size < minSize)
                {
                    candidate = MaybeRandomTree.Failed;
                }
            }
            return candidate;
        }

        private static MaybeRandomTree GenerateUpperBounded(int remainingNodesAllowed, Random randGen)
        {
            MaybeRandomTree result;
            if (remainingNodesAllowed <= 0)
            {
                result = MaybeRandomTree.Failed;
            }
            else
            {
                var r = randGen.NextDouble();
                if (r <= 0.5)
                {
                    result = MaybeRandomTree.Leaf(remainingNodesAllowed);
                }
                else
                {
                    var left = GenerateUpperBounded(remainingNodesAllowed - 1, randGen);
                    var right = GenerateUpperBounded(remainingNodesAllowed - 1 - left.Size, randGen);
                    result = MaybeRandomTree.Combine(left, right);
                }
            }
            return result;
        }
    }
}
