namespace AdventOfCode2025;

public class Day08
{
    public struct Point3D(int x, int y, int z)
    {
        public int X { get; set; } = x;
        public int Y { get; set; } = y;
        public int Z { get; set; } = z;

        public override readonly string ToString() 
        {
            return $"({X}, {Y}, {Z})";
        }
    }

    public static void Day08Part1()
    {
        // find the distances between every pair of points, store them
        // iterate over the lowest 10 or 1000 distances
        //   add points for lowest distance to an existing or new circuit (set)
        //   AND remove them from the unconnected points set
        // then find the 3 largest circuits and multiply the sizes together

        var lines = File.ReadLines("../../../Inputs/Day08.txt");
        var points = new List<Point3D>();
        var distanceToPointPairs = new Dictionary<double, List<(Point3D, Point3D)>>();
        var distances = new List<double>();

        foreach (var line in lines)
        {
            var coords = line.Split(',');
            var newPoint = new Point3D(
                int.Parse(coords[0]),
                int.Parse(coords[1]),
                int.Parse(coords[2]));
                      
            if (points.Count >= 1)
            {
                // find distance with all the points already read
                // store the distances in an ordered list
                // store the distances in a dictionary of distance => (point, point)
                foreach (var point in points) { 
                    var distance = StraightLineDistance3D(point, newPoint);

                    if (distanceToPointPairs.ContainsKey(distance))
                    {
                        // update list
                        var pointPairList = distanceToPointPairs[distance];
                        pointPairList.Add((point, newPoint));
                        distanceToPointPairs[distance] = pointPairList;
                    }
                    else
                    {
                        distances.Add(distance);
                        distanceToPointPairs.Add(distance, [(point, newPoint)]);
                    }
                }
            }

            points.Add(newPoint);
        }

        distances.Sort();

        var unconnectedPoints = points.ToHashSet();
        var listOfCircuits = new List<HashSet<Point3D>>();
        var count = 0;
        var maxClosestPairs = 1000;

        while (count < maxClosestPairs)
        {
            var distance = distances[count];
            if (distanceToPointPairs.TryGetValue(distance, out var pointPairList))
            {
                // iterate over points
                // if either point is in any circuits already
                //   set current circuit to first instance, include the new point as well
                //   remove first instance from list
                //   check for other instances with either point, combine with current circuit
                //   also remove them from the list
                // finally, add current circuit to list
                foreach (var pointPair in pointPairList)
                {
                    Console.WriteLine($"{count}: {pointPair.Item1} & {pointPair.Item2}");

                    var newCircuit = new HashSet<Point3D>() { pointPair.Item1, pointPair.Item2 };
                    // backwards iteration over listOfCircuits as it will get updated
                    for (int i = listOfCircuits.Count - 1; i >= 0; i--)
                    {
                        var circuit = listOfCircuits[i];
                        if (circuit.Contains(pointPair.Item1) || circuit.Contains(pointPair.Item2))
                        {
                            newCircuit.UnionWith(circuit);
                            listOfCircuits.RemoveAt(i);
                        }
                    }

                    listOfCircuits.Add(newCircuit);
                    unconnectedPoints.Remove(pointPair.Item1);
                    unconnectedPoints.Remove(pointPair.Item2);
                    
                    if (count >= maxClosestPairs) 
                        break;
                    count++;
                }
            }
        }

        // sort list of circuits by descending length/count
        // multiply 3 largest
        listOfCircuits.Sort(delegate (HashSet<Point3D> hs1, HashSet<Point3D> hs2) { return hs2.Count.CompareTo(hs1.Count); });

        long answer = listOfCircuits[0].Count * listOfCircuits[1].Count * listOfCircuits[2].Count;

        Console.WriteLine($"Day08Part1: {answer}");
    }

    private static double StraightLineDistance3D(Point3D p1,  Point3D p2)
    {
        var sqX = Math.Pow(p1.X - p2.X, 2);
        var sqY = Math.Pow(p1.Y - p2.Y, 2);
        var sqZ = Math.Pow(p1.Z - p2.Z, 2);

        return Math.Sqrt(sqX + sqY + sqZ);
    }

    public static void Day08Part2()
    {
        // find the distances between every pair of points, store them
        // iterate over the lowest 10 or 1000 distances
        //   add points for lowest distance to an existing or new circuit (set)
        //   AND remove them from the unconnected points set
        // then find the 3 largest circuits and multiply the sizes together

        var lines = File.ReadLines("../../../Inputs/Day08.txt");
        var points = new List<Point3D>();
        var distanceToPointPairs = new Dictionary<double, List<(Point3D, Point3D)>>();
        var distances = new List<double>();

        foreach (var line in lines)
        {
            var coords = line.Split(',');
            var newPoint = new Point3D(
                int.Parse(coords[0]),
                int.Parse(coords[1]),
                int.Parse(coords[2]));

            if (points.Count >= 1)
            {
                // find distance with all the points already read
                // store the distances in an ordered list
                // store the distances in a dictionary of distance => (point, point)
                foreach (var point in points)
                {
                    var distance = StraightLineDistance3D(point, newPoint);

                    if (distanceToPointPairs.ContainsKey(distance))
                    {
                        // update list
                        var pointPairList = distanceToPointPairs[distance];
                        pointPairList.Add((point, newPoint));
                        distanceToPointPairs[distance] = pointPairList;
                    }
                    else
                    {
                        distances.Add(distance);
                        distanceToPointPairs.Add(distance, [(point, newPoint)]);
                    }
                }
            }

            points.Add(newPoint);
        }

        distances.Sort();

        var unconnectedPoints = points.ToHashSet();
        var listOfCircuits = new List<HashSet<Point3D>>();
        var count = 0;
        long answer = 0;

        while (unconnectedPoints.Count > 0)
        {
            var distance = distances[count];
            if (distanceToPointPairs.TryGetValue(distance, out var pointPairList))
            {
                // iterate over points
                // if either point is in any circuits already
                //   set current circuit to first instance, include the new point as well
                //   remove first instance from list
                //   check for other instances with either point, combine with current circuit
                //   also remove them from the list
                // finally, add current circuit to list
                foreach (var pointPair in pointPairList)
                {
                    //Console.WriteLine($"{count}: {pointPair.Item1} & {pointPair.Item2}");

                    var newCircuit = new HashSet<Point3D>() { pointPair.Item1, pointPair.Item2 };
                    // backwards iteration over listOfCircuits as it will get updated
                    for (int i = listOfCircuits.Count - 1; i >= 0; i--)
                    {
                        var circuit = listOfCircuits[i];
                        if (circuit.Contains(pointPair.Item1) || circuit.Contains(pointPair.Item2))
                        {
                            newCircuit.UnionWith(circuit);
                            listOfCircuits.RemoveAt(i);
                        }
                    }

                    listOfCircuits.Add(newCircuit);
                    unconnectedPoints.Remove(pointPair.Item1);
                    unconnectedPoints.Remove(pointPair.Item2);

                    if (unconnectedPoints.Count == 0)
                    {
                        answer = pointPair.Item1.X * pointPair.Item2.X;
                        break;
                    }
                    count++;
                }
            }
        }

        Console.WriteLine($"Day08Part2: {answer}");
    }
}
