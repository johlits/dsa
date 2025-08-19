using Helper;
using System;

public class Day11
{
    static int gridSize = 10;
    static int[] dx = { -1, -1, -1, 0, 1, 1, 1, 0 };
    static int[] dy = { -1, 0, 1, 1, 1, 0, -1, -1 };

    public static int Flash(int x, int y, char[,] grid, HashSet<(int, int)> flashed)
    {
        if (x < 0 || x >= gridSize || y < 0 || y >= gridSize || flashed.Contains((x, y))) return 0;

        if (++grid[x,y] > '9')
        {
            flashed.Add((x, y));
            int flashCount = 1;
            for (int dir = 0; dir < 8; dir++)
            {
                flashCount += Flash(x + dx[dir], y + dy[dir], grid, flashed);
            }
            return flashCount;
        }
        return 0;
    }

    public static void ResetFlashedOctopuses(char[,] grid, HashSet<(int, int)> flashed)
    {
        foreach (var (x, y) in flashed)
        {
            grid[x,y] = '0';
        }
    }

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

        var map = maps.Matrix2ds.First();

        int step = 0;
        while (true)
        {
            step++;
            var flashed = new HashSet<(int, int)>();
            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    Flash(i, j, map.Map, flashed);
                }
            }

            if (flashed.Count == gridSize * gridSize)
            {
                Console.WriteLine("All octopuses flash simultaneously at step: " + step);
                break;
            }

            ResetFlashedOctopuses(map.Map, flashed);
        }
    }
}

