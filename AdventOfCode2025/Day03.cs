namespace AdventOfCode2025;

public class Day03
{
    public static void Day03Part1()
    {
        // find highest H1 in 0 to length - 1
        // find highest H2 in H1 to length
        var total = 0;

        var lines = File.ReadLines("../../../Inputs/Day03.txt");
        foreach (var line in lines)
        {
            var length = line.Length;

            var firstString = line.Substring(0, length - 1);
            char firstDigit2;
            int firstIndex2;
            (firstDigit2, firstIndex2) = FindHighestDigitAndIndex(firstString);

            var secondString = line.Substring(firstIndex2 + 1);
            char secondDigit2;
            (secondDigit2, _) = FindHighestDigitAndIndex(secondString);

            //Console.WriteLine(firstDigit + " " + secondDigit);

            total += (firstDigit2 - '0') * 10 + (secondDigit2 - '0');
        }

        Console.WriteLine("Day03Part1: " + total);
    }

    private static (char, int) FindHighestDigitAndIndex(string s)
    {
        var highestDigit = '0';
        var highestIndex = 0;
        var currentIndex = 0;
        foreach (var c in s)
        {
            if (c > highestDigit)
            {
                highestDigit = c;
                highestIndex = currentIndex;
            }
            currentIndex++;
        }

        return (highestDigit, highestIndex);
    }

    public static void Day03Part2()
    {
        long total = 0;

        var lines = File.ReadLines("../../../Inputs/Day03.txt");
        foreach (var line in lines)
        {
            var highestInLine = FindHighestDigitRecursive(line, 0, 12);
            //Console.WriteLine(highestInLine);
            total += highestInLine;
        }

        Console.WriteLine("Day03Part1: " + total);
    }

    private static long FindHighestDigitRecursive(string line, int startIndex, int digitNum)
    {
        //Console.WriteLine($"{startIndex} {digitNum}");

        var lengthSubstring = line.Length - digitNum + 1;

        var highestDigit = '0';
        var highestIndex = startIndex;
        char c;

        for (int i = startIndex; i < lengthSubstring; i++)
        {
            c = line[i];
            if (c > highestDigit)
            {
                highestDigit = c;
                highestIndex = i;
            }
        }

        if (digitNum == 1)
        {
            return (highestDigit - '0');
        }
        else
        {
            //Console.WriteLine($"HIGHEST DIGIT: {highestDigit}");
            return ((highestDigit - '0') * (long)Math.Pow(10, digitNum - 1)) + FindHighestDigitRecursive(line, highestIndex + 1, digitNum - 1);
        }
    }
}