// See https://aka.ms/new-console-template for more information

using Helpers;
using KTrie;

var stringDigits = new StringTrie<int>
{
    { "one", 1 },
    { "two", 2 },
    { "three", 3 },
    { "four", 4 },
    { "five", 5 },
    { "six", 6 },
    { "seven", 7 },
    { "eight", 8 },
    { "nine", 9 }
};

var stringDigitsReverse = new StringTrie<int>
{
    { new string("one".Reverse().ToArray()), 1 },
    { new string("two".Reverse().ToArray()), 2 },
    { new string("three".Reverse().ToArray()), 3 },
    { new string("four".Reverse().ToArray()), 4 },
    { new string("five".Reverse().ToArray()), 5 },
    { new string("six".Reverse().ToArray()), 6 },
    { new string("seven".Reverse().ToArray()), 7 },
    { new string("eight".Reverse().ToArray()), 8 },
    { new string("nine".Reverse().ToArray()), 9 },
};
var calibrationValues = new List<int>();
var fileName = Helper.GetFileName(args);
foreach (string line in File.ReadLines(fileName))
{
    var firstDigit = ExtractCalibrationValueDigit(line);
    var secondDigit = ExtractCalibrationValueDigit(line, true);
    var calibrationValue = firstDigit * 10 + secondDigit;
    calibrationValues.Add(calibrationValue);
}

var sumOfCalibrationValues = 0;
Console.WriteLine("Calibration values:");
for (var index = 0; index < calibrationValues.Count; index++)
{
    var value = calibrationValues[index];
    sumOfCalibrationValues += value;
    Console.WriteLine(index + "-" + value);
}

Console.WriteLine("Sum of calibration values:");
Console.WriteLine(sumOfCalibrationValues);

int ExtractCalibrationValueDigit(string line, bool reverse = false)
{
    var symbolsSeen = string.Empty;
    var symbols = reverse ? line.Reverse() : line.AsEnumerable();
    var prefixTreeToUse = reverse ? stringDigitsReverse : stringDigits;
    foreach (var symbol in symbols)
    {
        if (char.IsDigit(symbol))
        {
            return symbol - '0';
        }

        symbolsSeen += symbol;
        var prefixes = prefixTreeToUse.GetByPrefix(symbolsSeen);
        if (!prefixes.Any())
        {
            symbolsSeen = symbolsSeen[1..];
        }
        else if (prefixTreeToUse.TryGetValue(symbolsSeen, out int digit))
        {
            return digit;
        }
    }

    throw new Exception("couldn't find a digit in a line");
}