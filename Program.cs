using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using EqualFringe.RandomTree;
using EqualFringe.Enumerators;
using EqualFringe.ExplicitState;
using EqualFringe.Stacks;

namespace EqualFringe
{
    class Program
    {
        private static void RunAlgorithm(Node firstRoot, Node secondRoot, string name, Func<Node, Node, FringeComparisonResult> algorithm)
        {
            Console.WriteLine("Running algorithm: {0}", name);
            var sw = new Stopwatch();
            sw.Start();
            Console.WriteLine(" * " + algorithm(firstRoot, secondRoot));
            Console.WriteLine(" * Took: {0}", sw.Elapsed);
        }

        private static Tuple<string, Func<Node, Node, FringeComparisonResult>> GetAlgorithm(string name, Func<Node, Node, FringeComparisonResult> algorithm)
        {
            return Tuple.Create(name, algorithm);
        }

        static void Main(string[] args)
        {
            const int desiredSize = 100000;
            const double tolerance = 0.05;
            var seed = (int) Stopwatch.GetTimestamp();
            var randGen = new Random(seed);
            Console.WriteLine("Generating random trees of desired size: {0} with tolerance {1}", desiredSize, tolerance);

            var firstTree = RandomTreeGenerator.Generate(desiredSize, tolerance, randGen);
            var secondTree = RandomTreeGenerator.Generate(desiredSize, tolerance, randGen);
            Console.WriteLine("First random tree has {0} nodes", firstTree.Size);
            Console.WriteLine("Second random tree has {0} nodes", secondTree.Size);

            var algorithms = new[]{ GetAlgorithm("Yield", EqualFringeYield.CompareFringes)
                                  , GetAlgorithm("Explicit Enumerator", EqualFringeExplicitEnumerators.CompareFringes)
                                  , GetAlgorithm("Explicit State (Recursive)", EqualFringeExplicitStateRecursive.CompareFringes)
                                  , GetAlgorithm("Explicit State (Iterative)", EqualFringeExplicitStateIterative.CompareFringes)
                                  , GetAlgorithm("Stack (Recursive)", EqualFringeRecursiveStack.CompareFringes)
                                  , GetAlgorithm("Stack (Iterative)", EqualFringeIterativeStack.CompareFringes)
                                  };

            foreach (var a in algorithms)
            {
                RunAlgorithm(firstTree.Root, secondTree.Root, a.Item1, a.Item2);
            }

            Console.ReadKey();
        }
    }
}
