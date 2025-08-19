using Helper;

public class Day5
{
    public static void Run()
    {
        var arr = new ListOfStrings();
        var bps = new List<(Blueprint, int)>
        {
            (arr, -1),
        };
        new Parser("day05/p.in", bps, new Symbols()
        {
            Delimiter = " "
        });

        var grid = new int[1000, 1000];

        foreach (var item in arr.lists)
        {
            var p1 = J.Point(item.list[0]);
            var p2 = J.Point(item.list[2]);

            long x0 = p1.Item1, y0 = p1.Item2, x1 = p2.Item1, y1 = p2.Item2;
            long dx = x1 - x0, dy = y1 - y0;
            long steps = Math.Max(Math.Abs(dx), Math.Abs(dy));
            long xStep = dx == 0 ? 0 : dx / Math.Abs(dx);
            long yStep = dy == 0 ? 0 : dy / Math.Abs(dy);

            for (int i = 0; i <= steps; i++)
            {
                grid[x0 + i * xStep, y0 + i * yStep]++;
            }
        }

        int overlapCount = 0;

        for (int i = 0; i < 1000; i++)
        {
            for (int j = 0; j < 1000; j++)
            {
                if (grid[i, j] > 1)
                {
                    overlapCount++;
                }
            }
        }

        Console.WriteLine(overlapCount);
    }
}

