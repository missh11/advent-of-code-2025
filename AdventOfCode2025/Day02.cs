namespace AdventOfCode2025;

public class Day02
{
    public static void Day02Part1()
    {
        long invalidIdTotal = 0;

        var lines = File.ReadLines("../../../Inputs/Day02.txt");
        var ranges = lines.ElementAt(0).Split(',');

        foreach (var range in ranges)
        {
            var ids = range.Split('-');
            var id1 = long.Parse(ids[0]);
            var id2 = long.Parse(ids[1]);

            for (long id = id1; id <= id2; id++)
            {
                var idString = id.ToString();

                var idLength = idString.Length;
                if (idLength % 2 == 0)
                {
                    var l = idString.Substring(0, idLength / 2);
                    var r = idString.Substring(idLength / 2);
                    //Console.WriteLine("L: " + l + " R: " + r);
                    if (l.Equals(r))
                    {
                        invalidIdTotal += id;
                        //Console.WriteLine("total: " + invalidIdTotal);
                    }
                }
            }
        }

        Console.WriteLine("Day02Part1: " + invalidIdTotal);
    }

    public static void Day02Part2()
    {
        long invalidIdTotal = 0;

        var lines = File.ReadLines("../../../Inputs/Day02.txt");
        var ranges = lines.ElementAt(0).Split(',');

        foreach (var range in ranges)
        {
            var ids = range.Split('-');
            var id1 = long.Parse(ids[0]);
            var id2 = long.Parse(ids[1]);
            //Console.WriteLine(range);

            for (long id = id1; id <= id2; id++)
            {
                var idString = id.ToString();
                var idLength = idString.Length;
                //Console.WriteLine(id);

                for (int digits = 1; digits <= idLength / 2; digits++)
                {
                    //Console.WriteLine(digits);
                    if (idLength % digits == 0)
                    {
                        var l = idString.Substring(0, digits);

                        //Console.WriteLine(l);
                        var hasRepeats = true;

                        for (int i = digits; i < idLength; i+=digits)
                        {
                            //Console.WriteLine("i: "+ i);
                            var r = idString.Substring(i, digits);

                            if (!l.Equals(r))
                            {
                                hasRepeats = false;
                                break;
                            }
                        }

                        if (hasRepeats)
                        {
                            invalidIdTotal += id;
                            //Console.WriteLine("ID: "+ id + ", total: " + invalidIdTotal);
                            break;
                        }
                    }
                }
            }
        }

        Console.WriteLine("Day02Part2: " + invalidIdTotal);
    }
}
