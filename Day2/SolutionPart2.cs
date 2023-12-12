namespace Day2;

internal static class SolutionPart2
{
    public static IReadOnlyCollection<int> Solve(IEnumerable<Game> games)
    {
        var results = new List<int>();
        foreach (var game in games)
        {
            int maxRed;
            int maxGreen;
            int maxBlue;
            maxRed = maxGreen = maxBlue = 0;
            
            foreach (var bag in game.CubeCollection)
            {
                maxRed = Math.Max(maxRed, bag.Red);
                maxGreen = Math.Max(maxGreen, bag.Green);
                maxBlue = Math.Max(maxBlue, bag.Blue);
            }
            results.Add(maxRed * maxGreen * maxBlue);
        }

        return results;
    }
}