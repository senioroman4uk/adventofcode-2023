namespace Day2;

internal static class GamesReader
{
    public static IEnumerable<Game> GetGames(string file)
    {
        int id = 1;
        foreach (string game in File.ReadLines(file))
        {
            string[] gameData = game.Split(":", StringSplitOptions.TrimEntries).Last().Split(';', StringSplitOptions.TrimEntries);
            var bags = new List<Bag>();
            foreach (string bd in gameData)
            {
                string[] singleBagData = bd.Split(',', StringSplitOptions.TrimEntries);
                Bag bag = CreateBag(singleBagData);
                bags.Add(bag);
            }
            
            yield return new Game(id++, bags);
        }
    }
    
    private static Bag CreateBag(string[] bagData)
    {
        int red = 0;
        int green = 0;
        int blue = 0;
    
        foreach (var color in bagData)
        {
            var colorData = color.Split(' ');
            int colorValue = int.Parse(colorData[0]);
            switch (colorData[1])
            {
                case "red":
                    red = colorValue;
                    break;
                case "green":
                    green = colorValue;
                    break;
                case "blue":
                    blue = colorValue;
                    break;
                default:
                    throw new Exception($"wrong color data {colorData[1]}");
            }
        }
    
        return new Bag(red, green, blue);
    }
}