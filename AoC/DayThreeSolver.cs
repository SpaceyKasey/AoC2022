namespace AoC;

public class DayThreeSolver : ISolver
{
    public int Day => 3;
    public string[] Solve(string[] input, int part = 1)
    {
        var overallPriority = 0;


        if (part == 1)
        {
            foreach (var rucksack in input)
            {
                var first = rucksack.Substring(0, (int)(rucksack.Length / 2));
                var last = rucksack.Substring((int)(rucksack.Length / 2), (int)(rucksack.Length / 2));
            
                var same = first.Intersect(last).First();
            
                overallPriority += findPriority(same);
            
                Console.WriteLine($"First Section : {first} | Second Section : {last} | Same : {same} | Priority: {findPriority(same)} | Overall Priority : {overallPriority}");
            }
        }
        else
        {
            foreach (var rucksacks in input
                         .Select((x, i) => new { Index = i, Value = x })
                         .GroupBy(x => x.Index / 3)
                         .Select(x => x.Select(v => v.Value).ToList())
                         .ToList())
            {
                var intersection = rucksacks
                    .Skip(1)
                    .Aggregate(
                        new HashSet<char>(rucksacks.First()),
                        (h, e) => { h.IntersectWith(e); return h; }
                    );
               
                var same =  intersection.First();
            
                overallPriority += findPriority(same);
            
              //  Console.WriteLine($"First Section : {first} | Second Section : {last} | Same : {same} | Priority: {findPriority(same)} | Overall Priority : {overallPriority}");
            }
            
            
        }



        return new[] { overallPriority.ToString() };
    }

    private int findPriority(char same)
    {
        var ascii = (int)same;
        
        if (ascii > 96)
        {
            return ascii - 96;
        }

        return ascii - 64 + 26;
    }
}