namespace LeetCode.UniquePathsIII
{
    public class Solution
    {
        public int UniquePathsIII(int[][] grid)
        {
            var m = grid.Length;
            var n = grid.First().Length;
            var s = (-1, -1);
            var e = (-1, -1);
            var o = 0;

            for (var i = 0; i < m; i++)
            {
                for (var j = 0; j < n; j++)
                {
                    if (grid[i][j] == 1)
                    {
                        s = (i, j);
                    }
                    else if (grid[i][j] == 2)
                    {
                        e = (i, j);
                    }
                    else if (grid[i][j] == -1)
                    {
                        o++;
                    }
                }
            }

            var q = new Queue<((int, int), HashSet<(int, int)>)>();
            q.Enqueue((s, new HashSet<(int, int)>()));
            var successes = 0;

            while (q.Count > 0)
            {
                var current = q.Dequeue();
                var pos = current.Item1;
                var x = pos.Item1;
                var y = pos.Item2;
                var visited = current.Item2;
                visited.Add(pos);

                if (pos == e)
                {
                    if (visited.Count == m * n - o)
                    {
                        successes++;
                        continue;
                    }
                }

                if (x > 0 && grid[x - 1][y] != -1 && !visited.Contains((x - 1, y)))
                {
                    q.Enqueue(((x - 1, y), new HashSet<(int, int)>(visited)));
                }
                if (x < m - 1 && grid[x + 1][y] != -1 && !visited.Contains((x + 1, y)))
                {
                    q.Enqueue(((x + 1, y), new HashSet<(int, int)>(visited)));
                }
                if (y > 0 && grid[x][y - 1] != -1 && !visited.Contains((x, y - 1)))
                {
                    q.Enqueue(((x, y - 1), new HashSet<(int, int)>(visited)));
                }
                if (y < n - 1 && grid[x][y + 1] != -1 && !visited.Contains((x, y + 1)))
                {
                    q.Enqueue(((x, y + 1), new HashSet<(int, int)>(visited)));
                }
            }

            return successes;
        }
    }
}
