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

            var parsedInput = Day7.ParseInput(ref input);

            var firstResult = Day7.Solve(ref parsedInput, targetWire);
            Console.WriteLine("Signal on {0}: {1}", targetWire, firstResult);

            //Overriding input
            parsedInput.Parameters[overridedWire] = new[] { firstResult.ToString() };

            var secondResult = Day7.Solve(ref parsedInput, targetWire);
            Console.WriteLine("Signal on {0} after overriding {1}: {2}", targetWire, overridedWire, secondResult);

            Console.ReadKey();
        }
    }
}
