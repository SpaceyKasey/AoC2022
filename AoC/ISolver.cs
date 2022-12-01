using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC
{
    internal interface ISolver
    {
        int Day { get; }

        string[] Solve(string[] input, int part = 1);
    }
}