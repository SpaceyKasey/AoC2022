using System.Reflection;

namespace AoC
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            if (args.Length != 3 || !int.TryParse(args[0], out var day) || !int.TryParse(args[1], out var part))
            {
                Console.WriteLine("Usage: [Day] [Part] [Filename]");
                Console.WriteLine("eg: AoC 1 1 Puzzle1.txt");
                return;
            }

            var filename = args[2];

            var interfaceType = typeof(ISolver);
            var all = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(x => x.GetTypes())
                .Where(x => interfaceType.IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                .Select(x => Activator.CreateInstance(x) as ISolver);

            var solver = all.FirstOrDefault(x => x != null && x.Day == day);

            if (solver == default)
            {
                Console.WriteLine("Sorry, solver not found");
                return;
            }

            var result = solver.Solve(System.IO.File.ReadAllLines(filename), part);

            foreach (var line in result)
            {
                Console.WriteLine(line);
            }
        }
    }
}