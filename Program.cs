using System;
using System.IO;
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
        private static void WriteTree(Node root, string fileName)
        {
            using (var f = new StreamWriter(fileName))
            {
                f.WriteLine("graph G { ");
                f.WriteLine(@"node [style=filled,label="""",shape=circle,fillcolor=black,height=0.08]");
                var nodes = new Stack<Tuple<int, Node>>();                
                int nextLabel = 1;
                nodes.Push(Tuple.Create(0, root));
                while (nodes.Count > 0)
                {
                    var n = nodes.Pop();
                    var parentLabel = n.Item1;
                    foreach (Node m in n.Item2.Children)
                    {
                        f.WriteLine("{0} -- {1}", parentLabel, nextLabel);
                        nodes.Push(Tuple.Create(nextLabel, m));
                        ++nextLabel;
                    }
                }
                f.WriteLine("}");
            }
        }

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
            const int desiredSize = 250;
            const double tolerance = 0.1;
            var seed = (int) Stopwatch.GetTimestamp();
            var randGen = new Random(seed);
            Console.WriteLine("Generating random trees of desired size {0} with tolerance {1}", desiredSize, tolerance);

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

            Console.WriteLine("Writing first random tree.");
            WriteTree(firstTree.Root, "random_tree.dot");
            Console.WriteLine("Done");

            Console.ReadKey();
        }
    }
}
