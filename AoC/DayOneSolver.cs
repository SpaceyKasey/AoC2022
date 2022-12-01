using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC
{
    internal class DayOneSolver : ISolver
    {
        public int Day => 1;

        public string[] Solve(string[] input, int part = 1)
        {
            var current = 0;
            var allElves = new List<int>();

            foreach (var line in input)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    allElves.Add(current);
                    current = 0;
                    continue;
                }

                var val = int.TryParse(line, out var valNull) ? valNull : 0;
                current += val;
            }

            allElves.Sort();
            allElves.Reverse();

            var top = allElves.Take(part == 1 ? 1 : 3).Sum();

            return new[] { top.ToString() };
        }
    }
}