using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using EqualFringe.RandomTree;
using EqualFringe.Enumerators;

namespace EqualFringe
{
    class Program
    {
        static void Main(string[] args)
        {
            const int desiredSize = 10000;
            const double tolerance = 0.05;
            var seed = (int) Stopwatch.GetTimestamp();
            var randGen = new Random(seed);
            var firstTree = RandomTreeGenerator.Generate(desiredSize, tolerance, randGen);
            var secondTree = RandomTreeGenerator.Generate(desiredSize, tolerance, randGen);
            Console.WriteLine("First random input has {0} nodes", firstTree.Size);
            Console.WriteLine("Second random tree has {0} nodes", secondTree.Size);

            Console.WriteLine(EqualFringeYield.CompareFringes(firstTree.Root, secondTree.Root));
            Console.WriteLine(EqualFringeExplicitEnumerators.CompareFringes(firstTree.Root, secondTree.Root));

            Console.ReadKey();
        }
    }
}
