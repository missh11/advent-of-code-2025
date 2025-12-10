namespace AdventOfCode2025;

public class Day01
{
    public static void Day01Part1()
    {
        var dialAt = 50;
        var zeroCounter = 0;

        var lines = File.ReadLines("../../../Inputs/Day01.txt");
        foreach (var line in lines)
        {
            var rotations = Int32.Parse(line.Substring(1));
            if (line.StartsWith('R'))
            {
                dialAt = (dialAt + rotations) % 100;
            }
            else
            {
                dialAt = ((dialAt - rotations) % 100 + 100) % 100;
            }
            if (dialAt == 0) zeroCounter++;
        }

        Console.WriteLine("Day01Part1: " + zeroCounter);
    }

    public static void Day01Part2()
    {
        var dialAt = 50;
        var zeroCounter = 0;

        var lines = File.ReadLines("../../../Inputs/Day01.txt");
        foreach (var line in lines)
        {
            //Console.WriteLine(line);
            var rotations = Int32.Parse(line.Substring(1));

            if (line.StartsWith('R'))
            {
                zeroCounter += (dialAt + rotations) / 100;
                dialAt = (dialAt + rotations) % 100;
            }
            else
            {
                var temp = dialAt - rotations;

                if (dialAt == 0)
                {
                    if (temp <= -100)
                    {
                        zeroCounter = zeroCounter - (temp / 100);
                    }
                }
                else
                {
                    if (temp <= 0)
                    {
                        zeroCounter = zeroCounter + 1 - (temp / 100);
                    }
                }
                dialAt = (temp % 100 + 100) % 100;
            }
            //Console.WriteLine("DIAL AT: " + dialAt);
            //Console.WriteLine("ZERO COUNTER: " + zeroCounter);
            //Console.WriteLine("***");
        }

        Console.WriteLine("Day01Part2: " + zeroCounter);
    }
}
