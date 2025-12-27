namespace AdventOfCode2025;

public class Day06
{
    public static void Day06Part1()
    {
        // read each line
        // split by blank space or multiple spaces
        // store numbers in 2D grid
        // when on + or *, do the calculation for that column

        var lines = File.ReadLines("../../../Inputs/Day06.txt");
        List<List<string>> grid = [];
        
        foreach (var line in lines)
        {
            var values = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            grid.Add(values.ToList());
        }

        int rowCount = grid.Count;
        int colCount = grid[0].Count;

        // answers are very big when multiplied so use a long for the totals
        // the individual inputs are small enough to be parsed as integers though
        long total = 0;
        long colTotal;

        for (int col=0; col < colCount; col++)
        {
            if (grid[rowCount - 1][col] == "+")
            {
                colTotal = 0;
                for(int row=0; row < rowCount - 1; row++)
                {
                    colTotal += int.Parse(grid[row][col]);
                }
            }
            else
            {
                colTotal = 1;
                for (int row = 0; row < rowCount - 1; row++)
                {
                    colTotal *= int.Parse(grid[row][col]);
                }
            }
            total += colTotal;
        }

        Console.WriteLine("Day06Part1: " + total);
    }

    public static void Day06Part2()
    {
        // read each line
        // turn to char array
        // store chars in 2D grid
        // then go through column by column, finding a + or *
        // calculating each column, until another + or *
        // and adding that total to the final total

        var lines = File.ReadLines("../../../Inputs/Day06.txt");
        List<List<char>> grid = [];

        foreach (var line in lines)
        {
            grid.Add(line.ToCharArray().ToList());
        }

        int rowCount = grid.Count;
        int colCount = grid[0].Count;

        // answers are very big when multiplied so use a long for the totals
        // the individual inputs are small enough to be parsed as integers though
        long allProblemsTotal = 0;
        long currentProblemTotal;
        int currentNum;
        int currentDigit;
        bool entirelyBlankCol;

        List<int> currentNums = [];

        // read right to left and find the numbers involved
        for (int col = colCount-1; col >= 0; col--)
        {
            currentNum = 0;
            entirelyBlankCol = true;
            for (int row = 0; row < rowCount - 1; row++)
            {
                currentDigit = grid[row][col] - '0';
                // ignore spaces in the grid
                if (currentDigit >= 0 && currentDigit <= 9) 
                {
                    currentNum = currentNum * 10 + currentDigit;
                    entirelyBlankCol = false;
                }
            }

            if (!entirelyBlankCol)
            {
                //Console.WriteLine($"Current num: {currentNum}");
                currentNums.Add(currentNum);
            }

            if (grid[rowCount - 1][col] == '+' || grid[rowCount - 1][col] == '*')
            {
                if (grid[rowCount - 1][col] == '+')
                {
                    currentProblemTotal = 0;
                    foreach (int num in currentNums)
                    {
                        currentProblemTotal += num;
                    }
                }
                else
                {
                    currentProblemTotal = 1;
                    foreach (int num in currentNums)
                    {
                        currentProblemTotal *= num;
                    }
                }
                allProblemsTotal += currentProblemTotal;
                currentNums = [];
            }
        }

        Console.WriteLine("Day06Part2: " + allProblemsTotal);
    }
}
