using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using EqualFringe.RandomTree;

namespace EqualFringe
{
    class Program
    {
        static void Main(string[] args)
        {            
            var tree = RandomTreeGenerator.Generate(1000, 0.05, new Random((int) Stopwatch.GetTimestamp()));
            Console.WriteLine("Random input has {0} nodes", tree.Size);
            
            Console.ReadKey();
        }
    }
}
