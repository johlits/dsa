using System;
using System.Collections.Generic;
using System.Linq;

class Day23
{
    private static Tuple<int, int> Up = Tuple.Create(0, -1);
    private static Tuple<int, int> Down = Tuple.Create(0, 1);
    private static Tuple<int, int> Left = Tuple.Create(-1, 0);
    private static Tuple<int, int> Right = Tuple.Create(1, 0);
    private static Tuple<int, int>[] Directions = { Up, Down, Left, Right };

    private static Tuple<int, int>[] GetDirections(char c)
    {
        switch (c)
        {
            case '<': return [Left];
            case '>': return [Right];
            case 'v': return [Down];
            case '^': return [Up];
            case '.': return Directions;
            default: return [];
        }
    }

    private static string RemoveSlopes(string input)
    {
        return input.Replace("<", ".")
                    .Replace(">", ".")
                    .Replace("^", ".")
                    .Replace("v", ".");
    }

    private static int CalculateDistance(Dictionary<Tuple<int, int>, char> map, Tuple<int, int> start, Tuple<int, int> end)
    {
        var queue = new Queue<(Tuple<int, int>, int)>();
        queue.Enqueue((start, 0));

        var visited = new HashSet<Tuple<int, int>> { start };
        while (queue.Any())
        {
            var (position, distance) = queue.Dequeue();
            foreach (var direction in GetDirections(map[position]))
            {
                var newPosition = Tuple.Create(position.Item1 + direction.Item1, position.Item2 + direction.Item2);
                if (newPosition.Equals(end))
                {
                    return distance + 1;
                }
                else if (!Crossroad(map, newPosition) && !visited.Contains(newPosition))
                {
                    visited.Add(newPosition);
                    queue.Enqueue((newPosition, distance + 1));
                }
            }
        }
        return -1;
    }


    private static bool Free(Dictionary<Tuple<int, int>, char> map, Tuple<int, int> point) =>
        map.ContainsKey(point) && map[point] != '#';

    private static bool Crossroad(Dictionary<Tuple<int, int>, char> map, Tuple<int, int> point)
    {
        var count = 0;
        foreach (var direction in Directions)
        {
            var newPosition = Tuple.Create(point.Item1 + direction.Item1, point.Item2 + direction.Item2);
            if (Free(map, newPosition))
            {
                count++;
            }
        }
        return count != 2;
    }

    public static void Run()
    {
        using (var file = new System.IO.StreamReader("day23/p.in"))
        {
            var lines = new List<string>();
            string? line;
            while ((line = file.ReadLine()) != null)
            {
                lines.Add(RemoveSlopes(line));
            }

            var map = new Dictionary<Tuple<int, int>, char>();
            for (var rowIndex = 0; rowIndex < lines.Count; rowIndex++)
            {
                for (var colIndex = 0; colIndex < lines[0].Length; colIndex++)
                {
                    var position = Tuple.Create(colIndex, rowIndex);
                    map[position] = lines[rowIndex][colIndex];
                }
            }

            var nodePositions = new List<Tuple<int, int>>();
            foreach (var position in map.Keys.OrderBy(p => p.Item2).ThenBy(p => p.Item1))
            {
                if (Free(map, position) && Crossroad(map, position))
                {
                    nodePositions.Add(position);
                }
            }

            var nodes = new List<long>();
            for (var i = 0; i < nodePositions.Count; i++)
            {
                nodes.Add((long)Math.Pow(2, i));
            }

            var edges = new List<Tuple<long, long, int>>();
            for (var i = 0; i < nodePositions.Count; i++)
            {
                for (var j = 0; j < nodePositions.Count; j++)
                {
                    if (i != j)
                    {
                        var distance = CalculateDistance(map, nodePositions[i], nodePositions[j]);
                        if (distance > 0)
                        {
                            edges.Add(new Tuple<long, long, int>(nodes[i], nodes[j], distance));
                        }
                    }
                }
            }

            var startNode = nodes.First();
            var goalNode = nodes.Last();

            var cache = new Dictionary<Tuple<long, long>, int>();
            int GetLongestPath(long currentNode, long visited)
            {
                if (currentNode == goalNode)
                {
                    return 0;
                }
                else if (visited == (visited | currentNode))
                {
                    return int.MinValue;
                }
                var key = Tuple.Create(currentNode, visited);
                if (!cache.ContainsKey(key))
                {
                    var maxDistance = int.MinValue;
                    foreach (var edge in edges.Where(e => e.Item1 == currentNode))
                    {
                        var distance = edge.Item3 + GetLongestPath(edge.Item2, visited | currentNode);
                        if (distance > maxDistance)
                        {
                            maxDistance = distance;
                        }
                    }
                    cache[key] = maxDistance;
                }
                return cache[key];
            }

            var longestPath = GetLongestPath(startNode, 0);
            Console.WriteLine(longestPath);
        }
    }
}
