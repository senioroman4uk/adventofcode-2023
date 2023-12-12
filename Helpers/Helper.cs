namespace Helpers;

public static class Helper
{
    public static string GetFileName(string[] args)
    {
        string fileName = "input.txt";
        if (args.Length == 1)
        {
            fileName = args[0];
        }

        return fileName;
    }
}