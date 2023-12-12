// See https://aka.ms/new-console-template for more information

using System.Text;
using Helpers;

var fileName = Helper.GetFileName(args);
var targetConfiguration = new Bag(12, 13, 14);
var games = GetGames(fileName);

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

Console.WriteLine("Valid game ids:");
var result = 0;
foreach (int gameId in validGameIds)
{
    result += gameId;
    Console.WriteLine(gameId);
}
Console.WriteLine("Result: ");
Console.WriteLine(result);

IEnumerable<Game> GetGames(string file)
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

Bag CreateBag(string[] bagData)
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

/// <summary>
/// Represents a set of cubes for red, green and blue color
/// </summary>
/// <param name="Red"></param>
/// <param name="Green"></param>
/// <param name="Blue"></param>
record Bag(int Red, int Green, int Blue);

/// <summary>
/// 
/// </summary>
/// <param name="Id">Game id</param>
/// <param name="CubeCollection">A collection of cube subsets that elf showed to you</param>
record Game(int Id, IReadOnlyCollection<Bag> CubeCollection)
{
    protected virtual bool PrintMembers(StringBuilder builder)
    {
        builder.Append($"Id = {Id}, ");
        builder.Append("CubeCollection = [");
        builder.Append(string.Join(", ", CubeCollection.Select(c => c.ToString())));
        builder.Append(']');
        return true;
    }
};