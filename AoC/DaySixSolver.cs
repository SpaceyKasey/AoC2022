namespace AoC;

public class DaySixSolver : ISolver
{
    public int Day => 6;
    public string[] Solve(string[] input, int part = 1)
    {
        var results = new List<string>();
        var distinctSearch = (part == 1 ? 4 : 14);
        foreach (var message in input)
        {
            for (var ix = distinctSearch; ix < message.Length; ix++)
            {
                if ( message[(ix - distinctSearch)..ix].Distinct().Count() != distinctSearch) continue;
                
                results.Add(ix.ToString());
                
                break;
            }
        }
        return results.ToArray();
    }
}