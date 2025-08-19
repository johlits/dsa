using Helper;

public class Day15
{
    private static int[] dx = { -1, 1, 0, 0 };
    private static int[] dy = { 0, 0, -1, 1 };
    private static int gridExpansion = 5;

    private static IEnumerable<(int, int)> GetNeighbors(int x, int y, long rows, long cols)
    {
        for (int i = 0; i < 4; i++)
        {
            int nx = x + dx[i];
            int ny = y + dy[i];

            if (nx >= 0 && nx < rows && ny >= 0 && ny < cols)
            {
                yield return (nx, ny);
            }
        }
    }

    private static int[,] ExpandGrid(char[,] originalGrid, long rows, long cols)
    {
        int[,] expanded = new int[rows * gridExpansion, cols * gridExpansion];
        for (int tileRow = 0; tileRow < gridExpansion; tileRow++)
        {
            for (int tileCol = 0; tileCol < gridExpansion; tileCol++)
            {
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        int risk = (originalGrid[i, j] - '0' + tileRow + tileCol) % 9;
                        risk = risk == 0 ? 9 : risk; 
                        expanded[tileRow * rows + i, tileCol * cols + j] = risk;
                    }
                }
            }
        }
        return expanded;
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

        var originalGrid = maps.Matrix2ds.First();
        var originalRows = originalGrid.Height;
        var originalCols = originalGrid.Width;

        var grid = ExpandGrid(originalGrid.Map, originalRows, originalCols);

        var rows = grid.GetLength(0);
        var cols = grid.GetLength(1);
        var risk = new long[rows, cols];
        for (int i = 0; i < rows; i++)
            for (int j = 0; j < cols; j++)
                risk[i, j] = long.MaxValue;

        var pq = new PriorityQueue<(int, int), long>();
        pq.Enqueue((0, 0), 0);
        risk[0, 0] = 0;

        while (pq.Count > 0)
        {
            var (x, y) = pq.Dequeue();
            foreach (var (dx, dy) in GetNeighbors(x, y, rows, cols))
            {
                long newRisk = risk[x, y] + grid[dx, dy];
                if (newRisk < risk[dx, dy])
                {
                    risk[dx, dy] = newRisk;
                    pq.Enqueue((dx, dy), newRisk);
                }
            }
        }

        Console.WriteLine(risk[rows - 1, cols - 1]);
    }
}

