namespace AdventOfCode2025;

public class Day07
{
    private static readonly List<List<char>> _grid = [];
    private static readonly Dictionary<Point, long> _seenPointsWithTimelines = [];

    public static void Day07Part1()
    {
        // store input in 2D grid of chars
        // find row & col location of beam 'S'
        // check same col of the next row
        // if '.' change value to '|'
        // if '^', place '|' on either side on current row
        // move to next row
        // after grid is completed, iterate over grid again
        // if '^' with a '|' above, increment split beam counter

        var lines = File.ReadLines("../../../Inputs/Day07.txt");
        List<List<char>> grid = [];

        foreach (var line in lines)
        {
            grid.Add(line.ToCharArray().ToList());
        }

        var rowCount = grid.Count;
        var colCount = grid[0].Count;
        char currentCell;
        char belowCell;

        for (int row = 0; row < rowCount; row++)
        {
            for (int col = 0; col < colCount; col++)
            {
                currentCell = grid[row][col];
                if (currentCell == 'S' || currentCell == '|')
                {
                    if (row + 1 < rowCount)
                    {
                        belowCell = grid[row + 1][col];
                        if (belowCell == '.')
                        {
                            grid[row + 1][col] = '|';
                        }
                        else if (belowCell == '^')
                        {
                            if (col - 1 >= 0 && grid[row + 1][col - 1] == '.')
                            {
                                grid[row + 1][col - 1] = '|';
                            }
                            if (col + 1 < colCount && grid[row + 1][col + 1] == '.')
                            {
                                grid[row + 1][col + 1] = '|';
                            }
                        }
                    }
                }
            }
        }

        var beamSplits = 0;
        for (int row = 1; row < rowCount; row++)
        {
            for (int col = 0; col < colCount; col++)
            {
                if (grid[row][col] == '^' && grid[row - 1][col] == '|')
                {
                    beamSplits++;
                }
            }
        }

        Console.WriteLine($"Day07Part1: {beamSplits}");
    }

    public static void Day07Part2()
    {
        // store input in 2D grid of chars
        // find S location
        // count paths

        var lines = File.ReadLines("../../../Inputs/Day07.txt");
        
        foreach (var line in lines)
        {
            _grid.Add(line.ToCharArray().ToList());
        }

        var startCol = 0;
        for (int col=0; col < _grid[0].Count; col++)
        {
            if (_grid[0][col] == 'S')
            {
                startCol = col;
                break;
            }
        }

        var timelinesTotal = CountPaths(0, startCol);

        Console.WriteLine($"Day07Part2: {timelinesTotal}");
    }

    // if S, move down (increase row)
    // if ., move down (increase row)
    // if ^, return FindPath sum with each of the new path coordinates
    private static long CountPaths(int row, int col)
    {
        // check if coordinate has already been calculated and cached
        if (_seenPointsWithTimelines.TryGetValue(new Point(row, col), out var cachedTimelines))
        {
            return cachedTimelines;
        }

        // check if last row and return 1
        if (row == _grid.Count - 1)
        {
            return 1;
        }

        if (row >= _grid.Count || col < 0 || col > _grid[0].Count)
        {
            return 0;
        }

        long timelines = 0;

        if (_grid[row][col] == 'S' || _grid[row][col] == '.')
        {
            timelines = CountPaths(row + 1, col);
        }
        else // if '^'
        {
            timelines = CountPaths(row+1, col-1) + CountPaths(row + 1, col + 1);
        }

        // cache values to avoid recalculating routes already seen
        _seenPointsWithTimelines[new Point(row, col)] = timelines;

        return timelines;
    }

    public struct Point(int row, int col)
    {
        public int Row { get; set; } = row;
        public int Col { get; set; } = col;
    }
}
