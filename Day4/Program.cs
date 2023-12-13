// See https://aka.ms/new-console-template for more information

using Helpers;

var fileName = Helper.GetFileName(args);

var cards = ParseCards(fileName).ToList();
Part1(cards);
Part2(cards);

IEnumerable<Card> ParseCards(string fileName)
{
    foreach (string line in File.ReadLines(fileName))
    {
        var numbers = line.Split(':', StringSplitOptions.TrimEntries).Last().Split('|', StringSplitOptions.TrimEntries);
        var winningNumbers = numbers[0].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToHashSet();
        var cardNumbers = numbers[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToHashSet();
        yield return new Card(winningNumbers, cardNumbers);
    }
}

void Part1(List<Card> list)
{
    List<int> part1Results = new();
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

    foreach (var card in list)
    {
        var winningNumbersCount = card.PresentWinningNumbers.Count;
        int winningPoints = winningNumbersCount > 0 ? Pow(2, winningNumbersCount - 1) : 0;
        part1Results.Add(winningPoints);
    }

    int part1Result = part1Results.Sum();

    Console.WriteLine("winning points per card:");
    foreach (int points in part1Results)
    {
        Console.WriteLine(points);
    }

    Console.WriteLine("Part1 result - Total Points:");
    Console.WriteLine(part1Result);
}

void Part2(List<Card> cards)
{
    var cardCounters = cards.Select<Card, (Card Card, int Count)>(c => (c, 1)).ToArray();
    for (var i = 0; i < cardCounters.Length; i++)
    {
        var counter = cardCounters[i];
        var winningNumbersCount = counter.Card.PresentWinningNumbers.Count - 1;
        for (int j = i + 1; j < cardCounters.Length && winningNumbersCount >= 0; j++, winningNumbersCount--)
        {
            cardCounters[j].Count += counter.Count;
        }
    }

    var result = cardCounters.Sum(c => c.Count);
    Console.WriteLine("Part2 result - Total Cards:");
    Console.WriteLine(result);
}

internal record Card(HashSet<int> WinningNumbers, HashSet<int> CardNumbers)
{
    public HashSet<int> PresentWinningNumbers { get; } = CardNumbers.Intersect(WinningNumbers).ToHashSet();
}
