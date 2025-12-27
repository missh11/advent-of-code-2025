namespace AdventOfCode2025;

public class Day05
{
    public struct Range(long start, long end)
    {
        public long Start { get; set; } = start;
        public long End { get; set; } = end;
    }

    public static void Day05Part1()
    {
        // store each range
        // iterate over ingredients and check if in range

        var freshTotal = 0;
        var freshRangeMode = true;
        List<Range> freshIdRanges = [];

        var lines = File.ReadLines("../../../Inputs/Day05.txt");
        foreach (var line in lines)
        {
            Console.WriteLine(line);
            if (freshRangeMode)
            {
                if (line == "")
                {
                    freshRangeMode = false;
                }
                else
                {
                    // read in fresh ingredient ID ranges
                    var ids = line.Split('-');
                    var startId = long.Parse(ids[0]);
                    var endId = long.Parse(ids[1]);
                    
                    freshIdRanges.Add(new Range(startId, endId));
                }
            }
            else
            {
                // read in available ingredient IDs
                var id = long.Parse(line);
                foreach (Range range in freshIdRanges)
                {
                    if (id >= range.Start && id <= range.End)
                    {
                        freshTotal++;
                        break;
                    }
                }
            }
        }

        Console.WriteLine($"Day05Part1: {freshTotal}");
    }

    public static void Day05Part2()
    {
        // store each range
        // with each new range compare against all existing ranges and edit modify

        var freshRangeMode = true;
        List<Range> freshIdRanges = [];

        var lines = File.ReadLines("../../../Inputs/Day05.txt");
        foreach (var line in lines)
        {
            //Console.WriteLine(line);
            if (freshRangeMode)
            {
                if (line == "")
                {
                    freshRangeMode = false;
                }
                else
                {
                    // read in fresh ingredient ID ranges
                    var ids = line.Split('-');
                    var startId = long.Parse(ids[0]);
                    var endId = long.Parse(ids[1]);
                    var newRange = new Range(startId, endId);

                    // iterate backwards because we may modify the list by removing Ranges
                    for (int i = freshIdRanges.Count - 1; i >= 0; i--)
                    {
                        var range = freshIdRanges[i];

                        // newRange overlaps with a range we already have
                        // remove the existing range from the list
                        // update the new range and add to the list

                        // https://stackoverflow.com/questions/325933/determine-whether-two-date-ranges-overlap/325964#325964
                        if (Math.Max(newRange.Start, range.Start) <= Math.Min(newRange.End, range.End))
                        {
                            newRange.Start = Math.Min(newRange.Start, range.Start);
                            newRange.End = Math.Max(newRange.End, range.End);

                            freshIdRanges.Remove(range);
                        }
                    }
                    freshIdRanges.Add(newRange);
                }
            }
        }

        long totalFreshIds = 0;
        for (int i = 0; i < freshIdRanges.Count; i++) 
        {
            var range = freshIdRanges[i];
            totalFreshIds += range.End - range.Start + 1;
            //Console.WriteLine($"{range.Start}-{range.End}");
        }

        Console.WriteLine($"Day05Part2: {totalFreshIds}");
    }
}