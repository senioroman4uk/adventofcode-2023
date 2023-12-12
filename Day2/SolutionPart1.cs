namespace Day2;

internal static class SolutionPart1
{
    public static IReadOnlyCollection<int> GetValidGameIds(IEnumerable<Game> games, Bag targetConfiguration)
    {
        List<int> validGameIds = new List<int>();
        foreach (Game game in games)
        {
            bool isValid = true;
            foreach (Bag bag in game.CubeCollection)
            {
                if (bag.Red > targetConfiguration.Red || bag.Green > targetConfiguration.Green || bag.Blue > targetConfiguration.Blue)
                {
                    isValid = false;
                }
            }
    
            if (isValid)
            {
                validGameIds.Add(game.Id);
            }
        }

        return validGameIds;
    }
}