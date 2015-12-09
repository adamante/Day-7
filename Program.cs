using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day7
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt");
            var targetWire = "a";
            var overridedWire = "b";

            var firstResult = Day7.SolvePart1(ref input, targetWire);
            Console.WriteLine("Signal on {0}: {1}", targetWire, firstResult);

            var secondResult = Day7.SolvePart2(ref input, targetWire, overridedWire, firstResult);

            Console.WriteLine("Signal on {0} after overriding {1}: {2}", targetWire, overridedWire, secondResult);
            Console.ReadKey();
        }
    }
}
