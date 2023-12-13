// See https://aka.ms/new-console-template for more information

using Helpers;

var fileName = Helper.GetFileName(args);

List<int> results = new();


foreach (var card in ParseCards(fileName))
{
    var winningNumbersCount = card.TotalCount - card.LooseNumbers.Count;
    int winningPoints = winningNumbersCount > 0 ? Pow(2, winningNumbersCount - 1) : 0;
    results.Add(winningPoints);
}

int result = results.Sum();

Console.WriteLine("winning points per card:");
foreach (int points in results)
{
    Console.WriteLine(points);
}
Console.WriteLine("Total points:");
Console.WriteLine(result);

int Pow(int @base, int power)
{
    if (power < 0)
    {
        throw new Exception("power must be greater than 0");
    }
    
    if (power == 0)
    {
        return 1;
    }

    if (power == 1)
    {
        return @base;
    }

    var result = @base;
    for (int i = 1; i < power; i++)
    {
        result *= @base;
    }

    return result;
}

IEnumerable<Card> ParseCards(string fileName)
{
    int id = 0;
    foreach (string line in File.ReadLines(fileName))
    {
        var numbers = line.Split(':', StringSplitOptions.TrimEntries).Last().Split('|', StringSplitOptions.TrimEntries);
        var winningNumbers = numbers[0].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToHashSet();
        var cardNumbers = numbers[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToHashSet();
        yield return new Card(id++, winningNumbers, cardNumbers);
    }
}

internal record Card(int Id, HashSet<int> WinningNumbers, HashSet<int> CardNumbers)
{
    public int TotalCount { get; } = CardNumbers.Count;
    
    public HashSet<int> LooseNumbers { get; } = CardNumbers.Except(WinningNumbers).ToHashSet();
}
