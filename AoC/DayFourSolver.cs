namespace AoC;

public class DayFourSolver : ISolver
{

    private class Set
    {
        public int Min { get; }
        public int Max { get;  }

        public Set(int min, int max)
        {
            Min = min;
            Max = max;
        }

        public bool Contains(Set compareSet)
        {
            return Min <= compareSet.Min && Max >= compareSet.Max;
        }
        
        public bool Overlaps(Set compareSet)
        {
            return (Min <= compareSet.Min && compareSet.Min <= Max) || (Min <= compareSet.Max && compareSet.Max <= Max);
        }
    }
    
    public int Day => 4;
    
    public string[] Solve(string[] input, int part = 1)
    {
        var count = 0;
        foreach (var group in input)
        {
            var setsRaw = group.Split(',');
            var sets = new List<Set>();
            foreach (var set in setsRaw)
            {
                var ranges = set.Split('-');
                var thisset = new Set(int.Parse(ranges[0]), int.Parse(ranges[1]));
                
                if (part== 1 && (sets.Any(x => thisset.Contains(x) || x.Contains(thisset))))
                {
                    count++;
                    continue;
                }
                
                if (part== 2 && (sets.Any(x => thisset.Overlaps(x) || x.Overlaps(thisset))))
                {
                    count++;
                    continue;
                }

                
                sets.Add(thisset);
            }

        }

        return new[] {  count.ToString() };
    }
}