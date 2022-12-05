using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AoC;

public class DayFiveSolver : ISolver
{   
    public class BoxMatrix
    {
        public List<List<string>> MatrixData = new List<List<string>>();

        public BoxMatrix(string[] boxInput)
        {
            //     [D]    
            // [N] [C]    
            // [Z] [M] [P]
            //  1   2   3 
            var count = 0;
            var indexes = new List<int>();
            
            foreach (var line in boxInput.Reverse())
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                if (!line.Contains('['))
                {
                    //This is the row definition, should be the first line
                    var cleaned = line.Replace("   ", " ").Trim();
                    var split = cleaned.Split(' ');
                    count = split.Length;
                    for (var ix = 0; ix < count; ix++)
                    {
                        MatrixData.Add(new List<string>());
                        indexes.Add(line.IndexOf((ix+1).ToString()));
                    }
                    continue;
                }

                for (var ix = 0; ix < count; ix++)
                {
                    var value = line[indexes[ix]];
                    
                    if(string.IsNullOrWhiteSpace(value.ToString())) continue;
                    
                    MatrixData[ix].Add(line[indexes[ix]].ToString());
                    
                }
                
            }
        }

        public void MoveBox(int from, int to)
        {
            var boxData = MatrixData[from - 1].Last();
            MatrixData[from - 1].RemoveAt(MatrixData[from - 1].Count - 1);
            MatrixData[to - 1].Add(boxData);
        }
        
        public void MoveStack(int from, int to, int count)
        {
            var boxData = MatrixData[from - 1].Skip(Math.Max(0, MatrixData[from - 1].Count() - count)).ToArray();
            MatrixData[from - 1].RemoveRange(MatrixData[from - 1].Count - count, count);
            MatrixData[to - 1].AddRange(boxData);
        }
    }

    public int Day => 5;
    public string[] Solve(string[] input, int part = 1)
    {
        var inputList = input.ToList();
        //Part one, parse the box data
        //Find the blank line
        var endDataIndex = inputList.FindIndex( string.IsNullOrWhiteSpace);
        var boxMatrix = new BoxMatrix(input[0..endDataIndex]);
        
        //Part Two, process commands
        foreach (var line in input[endDataIndex..input.Length])
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }
            
            // lines are move x from y to z
            var matches = Regex.Matches(line, "[0-9]+");

            var count = int.Parse(matches[0].Value);
            var from = int.Parse(matches[1].Value);
            var to = int.Parse(matches[2].Value);

            if (part == 1)
            {
                for (int ix = 0; ix < count; ix++)
                {
                    boxMatrix.MoveBox(from,to);
                }
            }
            else
            {
                boxMatrix.MoveStack(from, to, count);
            }

        }

        return new[] { string.Join("", boxMatrix.MatrixData.Select(x => x.Last())) };
    }
}