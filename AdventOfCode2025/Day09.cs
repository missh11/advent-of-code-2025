namespace AdventOfCode2025;

public class Day09
{
    public struct Point2D(int x, int y)
    {
        // X = col, Y = row
        public int X { get; set; } = x;
        public int Y { get; set; } = y;

        public override readonly string ToString()
        {
            return $"({X}, {Y})";
        }
    }

    public static void Day09Part1()
    {
        // brute force it
        var points = new List<Point2D>();
        long maxArea = 0;

        var lines = File.ReadLines("../../../Inputs/Day09.txt");
        foreach (var line in lines)
        {
            var coords = line.Split(',');
            var newPoint = new Point2D(int.Parse(coords[0]), int.Parse(coords[1]));

            foreach (var point in points)
            {
                var area = CalculateArea(newPoint, point);
                if (area > maxArea)
                {
                    maxArea = area;
                }
            }

            points.Add(newPoint);
        }

        Console.WriteLine($"Day09Part1: {maxArea}");
    }

    private static long CalculateArea(Point2D p1, Point2D p2)
    {
        long num1 = Math.Abs(p1.X - p2.X) + 1;
        long num2 = Math.Abs(p1.Y - p2.Y) + 1;
        return num1 * num2;
    }

}