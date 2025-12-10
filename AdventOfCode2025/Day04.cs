namespace AdventOfCode2025;

public class Day04
{
    public static void Day04Part1()
    {
        var total = 0;
        List<List<char>> grid = [];

        int currentRow = 0;
        var lines = File.ReadLines("../../../Inputs/Day04.txt");
        foreach (var line in lines)
        {
            grid.Add([]);
            grid[currentRow] = line.ToCharArray().ToList();
            currentRow++;
        }

        var width = grid[0].Count;
        var length = grid.Count;

        int nearbyRolls, nearbyRow, nearbyCol;
        for (int row = 0; row < length; row++) 
        {
            for (int col = 0; col < width; col++) 
            {
                //Console.Write(grid[row][col]);
                nearbyRolls = 0;
                if (grid[row][col] == '@')
                {
                    for (int r = -1; r <= 1; r++)
                    {
                        for (int c = -1; c <= 1; c++)
                        {
                            if (r == 0 && c == 0)
                            {
                                // do nothing as don't want to check central position in 3x3 grid
                            }
                            else
                            {
                                nearbyRow = row + r;
                                nearbyCol = col + c;
                                if (nearbyRow >= 0 && nearbyRow < length && nearbyCol >= 0 && nearbyCol < width
                                    && grid[nearbyRow][nearbyCol] == '@')
                                {
                                    nearbyRolls++;
                                }
                            }
                        }
                    }
                    if (nearbyRolls < 4)
                    {
                        total++;
                    }
                }
            }
            //Console.WriteLine();
        }

        Console.WriteLine($"Day04Part1: {total}");
    }

    public static void Day04Part2()
    {
        List<List<char>> grid = [];
        int currentRow = 0;

        var lines = File.ReadLines("../../../Inputs/Day04.txt");
        foreach (var line in lines)
        {
            grid.Add([]);
            grid[currentRow] = line.ToCharArray().ToList();
            currentRow++;
        }

        var totalRollsRemoved = 0;
        var rollsRemoved = 1;
        
        while (rollsRemoved > 0)
        {
            rollsRemoved = CountAndRemoveRolls(grid);
            totalRollsRemoved += rollsRemoved;
        }

        Console.WriteLine($"Day04Part2: {totalRollsRemoved}");
    }

    private static int CountAndRemoveRolls(List<List<char>> grid)
    {
        // the input grid will get modified in this method. no need to return it
        var total = 0;

        var width = grid[0].Count;
        var length = grid.Count;

        int nearbyRolls, nearbyRow, nearbyCol;
        for (int row = 0; row < length; row++)
        {
            for (int col = 0; col < width; col++)
            {
                //Console.Write(grid[row][col]);
                nearbyRolls = 0;
                if (grid[row][col] == '@' || grid[row][col] == 'x')
                {
                    for (int r = -1; r <= 1; r++)
                    {
                        for (int c = -1; c <= 1; c++)
                        {
                            if (r == 0 && c == 0)
                            {
                                // do nothing as don't want to check central position in 3x3 grid
                            }
                            else
                            {
                                nearbyRow = row + r;
                                nearbyCol = col + c;
                                if (nearbyRow >= 0 && nearbyRow < length && nearbyCol >= 0 && nearbyCol < width
                                    && (grid[nearbyRow][nearbyCol] == '@' || grid[nearbyRow][nearbyCol] == 'x'))
                                {
                                    nearbyRolls++;
                                }
                            }
                        }
                    }
                    if (nearbyRolls < 4)
                    {
                        total++;
                        grid[row][col] = 'x';
                    }
                }
            }
            //Console.WriteLine();
        }

        for (int row = 0; row < length; row++)
        {
            for (int col = 0; col < width; col++)
            {
                //Console.Write(grid[row][col]);
                nearbyRolls = 0;
                if (grid[row][col] == 'x')
                {
                    grid[row][col] = '.';
                }
            }
            //Console.WriteLine();
        }

        return total;
    }
}