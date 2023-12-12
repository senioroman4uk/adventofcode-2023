// See https://aka.ms/new-console-template for more information

using Day2;
using Helpers;

var fileName = Helper.GetFileName(args);
var targetConfiguration = new Bag(12, 13, 14);
var games = GamesReader.GetGames(fileName);

var validGameIds = SolutionPart1.GetValidGameIds(games, targetConfiguration);
Console.WriteLine("Valid game ids:");
var resultPart1 = 0;
foreach (int gameId in validGameIds)
{
    resultPart1 += gameId;
    Console.WriteLine(gameId);
}
Console.WriteLine("Result part 1: ");
Console.WriteLine(resultPart1);

Console.WriteLine("Result part 2: ");
var results = SolutionPart2.Solve(games);
var resultPart2 = results.Sum(x => (long)x);
Console.WriteLine(resultPart2);
