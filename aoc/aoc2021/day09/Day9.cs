using Helper;
using System.Runtime.Intrinsics.X86;

public class Day9
{
    private static M2d map;
    private static bool[,] visited;

    public static void Run()
    {
        var maps = new ListOf2dMaps()
        {
            SaveInput = true
        };
        var bps = new List<(Blueprint, int)>
        {
            (maps, -1),
        };
        new Parser("p.in", bps, new Symbols()
        {

        });

        map = maps.Matrix2ds.First();
        visited = new bool[map.Width, map.Height];
        var poolSizes = new List<int>();

        for (var y = 0; y < map.Height; y++)
        {
            for (var x = 0; x < map.Width; x++)
            {
                var val = map.Map[x, y] - '0';
                if (x > 0 && val >= map.Map[x - 1, y] - '0') continue;
                if (x < map.Width - 1 && val >= map.Map[x + 1, y] - '0') continue;
                if (y > 0 && val >= map.Map[x, y - 1] - '0') continue;
                if (y < map.Height - 1 && val >= map.Map[x, y + 1] - '0') continue;

                var poolSize = 0;

                var q = new Queue<(int, int)>();
                q.Enqueue((x, y));

                while (q.Any())
                {
                    var current = q.Dequeue();
                    var cx = current.Item1;
                    var cy = current.Item2;

                    if (visited[cx, cy] || map.Map[cx, cy] == '9')
                    {
                        continue;
                    }
                    visited[cx, cy] = true;
                    poolSize++;

                    if (cx > 0 && visited[cx - 1, cy] == false) q.Enqueue((cx - 1, cy));
                    if (cx < map.Width - 1 && visited[cx + 1, cy] == false) q.Enqueue((cx + 1, cy));
                    if (cy > 0 && visited[cx, cy - 1] == false) q.Enqueue((cx, cy - 1));
                    if (cy < map.Height - 1 && visited[cx, cy + 1] == false) q.Enqueue((cx, cy + 1));
                }

                Console.WriteLine(poolSize);
                poolSizes.Add(poolSize);
            }
        }

        poolSizes.Sort();
        poolSizes.Reverse();

        Console.WriteLine(poolSizes[0] * poolSizes[1] * poolSizes[2]);
    }
}

