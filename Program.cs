﻿using System;
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
        static void Main(string[] args)
        {
            const int desiredSize = 100000;
            const double tolerance = 0.05;
            var seed = (int) Stopwatch.GetTimestamp();
            var randGen = new Random(seed);
            var firstTree = RandomTreeGenerator.Generate(desiredSize, tolerance, randGen);
            var secondTree = RandomTreeGenerator.Generate(desiredSize, tolerance, randGen);
            Console.WriteLine("First random input has {0} nodes", firstTree.Size);
            Console.WriteLine("Second random tree has {0} nodes", secondTree.Size);

            Console.WriteLine(EqualFringeYield.CompareFringes(firstTree.Root, secondTree.Root));
            Console.WriteLine(EqualFringeExplicitEnumerators.CompareFringes(firstTree.Root, secondTree.Root));
            Console.WriteLine(EqualFringeExplicitStateRecursive.CompareFringes(firstTree.Root, secondTree.Root));
            Console.WriteLine(EqualFringeExplicitStateIterative.CompareFringes(firstTree.Root, secondTree.Root));
            Console.WriteLine(EqualFringeRecursiveStack.CompareFringes(firstTree.Root, secondTree.Root));
            Console.WriteLine(EqualFringeIterativeStack.CompareFringes(firstTree.Root, secondTree.Root));

            Console.ReadKey();
        }
    }
}
