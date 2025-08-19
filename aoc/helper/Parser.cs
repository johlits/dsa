namespace Helper
{
    public enum Type
    {
        Integer,
        Long,
        ULong,
        String
    }

    public class Symbols
    {
        public string? Delimiter { get; set; }
        public char? StartSymbol { get; set; }
        public char? GoalSymbol { get; set; }
        public char? ObstacleSymbol { get; set; }
    }

    public class Parser
    {
        public Parser(string path, List<(Blueprint, int)> blueprints, Symbols symbols)
        {
            using (StreamReader file = new StreamReader(path))
            {
                var lines = new List<string>();
                string? ln;

                var bluePrintIndex = 0;
                var blueprint = blueprints[bluePrintIndex].Item1;
                var bluePrintIteration = 1;
                var bluePrintIterations = blueprints[bluePrintIndex].Item2 > 0 ? blueprints[bluePrintIndex].Item2 : int.MaxValue;
                var end = false;

                while ((ln = file.ReadLine()) != null)
                {
                    if (ln.Trim() == "")
                    {
                        if (bluePrintIteration < bluePrintIterations && bluePrintIterations != int.MaxValue)
                        {
                            blueprint.Process(lines, symbols);
                            lines = new List<string>();
                            bluePrintIteration++;
                        }
                        else
                        {
                            blueprint.Process(lines, symbols);
                            lines = new List<string>();
                            bluePrintIndex++;
                            if (bluePrintIndex >= blueprints.Count)
                            {
                                end = true;
                                break;
                            }
                            blueprint = blueprints[bluePrintIndex].Item1;
                            bluePrintIteration = 1;
                            bluePrintIterations = blueprints[bluePrintIndex].Item2 > 0 ? blueprints[bluePrintIndex].Item2 : int.MaxValue;
                        }
                    }
                    else
                    {
                        lines.Add(ln);
                    }
                }
                if (!end)
                {
                    blueprint.Process(lines, symbols);
                }
            }
        }
    }

    public interface Blueprint
    {
        public void Process(List<string> list, Symbols symbols);
    }

    public class ListOfIntegerBingos : Blueprint
    {
        public List<Bingo> bingos = new List<Bingo>();
        public bool SaveInput;
        public void Process(List<string> list, Symbols symbols)
        {
            bingos.Add(new Bingo(list));
        }
    }

    public class Bingo
    {
        public int width = 0;
        public int height = 0;
        public List<List<int>> board = new List<List<int>>();
        public List<List<bool>> marked = new List<List<bool>>();
        public Bingo(List<string> rows)
        {
            height = rows.Count;
            for (var i = 0; i < rows.Count; i++)
            {
                board.Add(new List<int>());
                marked.Add(new List<bool>());
                var numbers = rows[i].Split(" ");
                for (var j = 0; j < numbers.Count(); j++)
                {
                    if (string.IsNullOrEmpty(numbers[j]))
                    {
                        continue;
                    }
                    else
                    {
                        board[i].Add(J.I(numbers[j]));
                        marked[i].Add(false);
                    }
                }
                width = board[i].Count;
            }
        }
        public void Mark(int val)
        {
            for (var i = 0; i < height; i++)
            {
                for (var j = 0; j < width; j++)
                {
                    if (board[i][j] == val)
                    {
                        marked[i][j] = true;
                    }
                }
            }
        }

        public bool IsBingo()
        {
            for (var i = 0; i < height; i++)
            {
                var bingo = true;
                for (var j = 0; j < width; j++)
                {
                    if (marked[j][i] != true)
                    {
                        bingo = false;
                        break;
                    }
                }
                if (bingo)
                {
                    return true;
                }
            }

            for (var i = 0; i < width; i++)
            {
                var bingo = true;
                for (var j = 0; j < height; j++)
                {
                    if (marked[i][j] != true)
                    {
                        bingo = false;
                        break;
                    }
                }
                if (bingo)
                {
                    return true;
                }
            }
            return false;
        }
    }

    public class ListOfIntegers : Blueprint
    {
        public List<L1d<int>> lists = new List<L1d<int>>();
        public string Delimiter;

        public void Process(List<string> list, Symbols symbols)
        {
            if (Delimiter != null)
            {
                symbols.Delimiter = Delimiter;
            }
            for (var i = 0; i < list.Count; i++)
            {
                lists.Add(new L1d<int>(list[i], symbols));
            }
        }
    }

    public class ListOfStrings : Blueprint
    {
        public List<L1d<string>> lists = new List<L1d<string>>();
        public string Delimiter;

        public void Process(List<string> list, Symbols symbols)
        {
            if (Delimiter != null)
            {
                symbols.Delimiter = Delimiter;
            }
            for (var i = 0; i < list.Count; i++)
            {
                lists.Add(new L1d<string>(list[i], symbols));
            }
        }
    }

    public class ListOf2dMaps : Blueprint
    {
        public List<M2d> Matrix2ds = new List<M2d>();
        public bool SaveInput;

        public int Count()
        {
            return Matrix2ds.Count;
        }

        public M2d Get(int id)
        {
            return Matrix2ds[id];
        }

        public void Process(List<string> lines, Symbols symbols)
        {
            int width = lines[0].Length;
            Matrix2ds.Add(new M2d(lines, width, lines.Count, SaveInput, symbols));
        }
    }

    public class L1d<T>
    {
        public List<T> list = new List<T>();
        public Type type = Type.String;

        public L1d(string line, Symbols? symbol = null)
        {
            var splittingSymbol = ",";
            if (symbol != null && symbol.Delimiter != null)
            {
                splittingSymbol = symbol.Delimiter;
            }
            var parts = line.Split(splittingSymbol);
            foreach (var part in parts)
            {
                list.Add((T)Convert.ChangeType(part, typeof(T)));
            }
        }
    }

    public class M2d
    {
        private (long, long) dimensions;
        public char[,] Map;
        public long Width { get { return dimensions.Item1; } }
        public long Height { get { return dimensions.Item2; } }
        public List<(long, long)> Starts = new List<(long, long)>();
        public List<(long, long)> Goals = new List<(long, long)>();
        public List<(long, long)> Obstacles = new List<(long, long)>();

        private static long[] dx = { -1, 1, 0, 0 };
        private static long[] dy = { 0, 0, -1, 1 };

        private static IEnumerable<(long, long)> GetNeighbors(long x, long y, long rows, long cols)
        {
            for (int i = 0; i < 4; i++)
            {
                var nx = x + dx[i];
                var ny = y + dy[i];

                if (nx >= 0 && nx < rows && ny >= 0 && ny < cols)
                {
                    yield return (nx, ny);
                }
            }
        }

        public long Dijkstra((long, long) start, (long, long) goal)
        {
            var rows = Height;
            var cols = Width;
            var risk = new long[rows, cols];
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    risk[i, j] = long.MaxValue;

            var pq = new PriorityQueue<(long, long), long>();
            pq.Enqueue((start.Item1, start.Item2), 0);
            risk[start.Item1, start.Item2] = 0;

            while (pq.Count > 0)
            {
                var (x, y) = pq.Dequeue();
                foreach (var (dx, dy) in GetNeighbors(x, y, rows, cols))
                {
                    long newRisk = risk[x, y] + Map[dx, dy] - '0';
                    if (newRisk < risk[dx, dy])
                    {
                        risk[dx, dy] = newRisk;
                        pq.Enqueue((dx, dy), newRisk);
                    }
                }
            }
            return risk[goal.Item1, goal.Item2];
        }

        public long? BFS((long, long) start, (long, long) goal)
        {
            long? shortestDistance = null;

            var current = ((start.Item1, start.Item2), 0);
            var q = new Queue<((long, long), long)>();
            var v = new HashSet<(long, long)>();
            q.Enqueue(current);

            while (q.Count > 0)
            {
                current = (((long, long) start, int))q.Dequeue();
                var position = current.Item1;
                var distance = current.Item2;

                if (position == goal)
                {
                    shortestDistance = distance;
                    break;
                }

                (long, long)? w = GoWest(position);
                (long, long)? e = GoEast(position);
                (long, long)? n = GoNorth(position);
                (long, long)? s = GoSouth(position);

                if (w != null && !v.Contains(w.Value) && !Obstacles.Contains(w.Value))
                {
                    q.Enqueue((w.Value, distance + 1));
                }
                if (e != null && !v.Contains(e.Value) && !Obstacles.Contains(e.Value))
                {
                    q.Enqueue((e.Value, distance + 1));
                }
                if (n != null && !v.Contains(n.Value) && !Obstacles.Contains(n.Value))
                {
                    q.Enqueue((n.Value, distance + 1));
                }
                if (s != null && !v.Contains(s.Value) && !Obstacles.Contains(s.Value))
                {
                    q.Enqueue((s.Value, distance + 1));
                }
            }

            return shortestDistance;
        }

        public (long, long)? GoNorth((long, long) position, bool wrap = false)
        {
            if (position.Item2 > 0)
            {
                return (position.Item1, position.Item2 - 1);
            }
            else if (wrap)
            {
                return (position.Item1, Height - 1);
            }
            return null;
        }

        public (long, long)? GoSouth((long, long) position, bool wrap = false)
        {
            if (position.Item2 < Height - 1)
            {
                return (position.Item1, position.Item2 + 1);
            }
            else if (wrap)
            {
                return (position.Item1, 0);
            }
            return null;
        }

        public (long, long)? GoWest((long, long) position, bool wrap = false)
        {
            if (position.Item1 > 0)
            {
                return (position.Item1 - 1, position.Item2);
            }
            else if (wrap)
            {
                return (Width - 1, position.Item2);
            }
            return null;
        }

        public (long, long)? GoEast((long, long) position, bool wrap = false)
        {
            if (position.Item1 < Width - 1)
            {
                return (position.Item1 + 1, position.Item2);
            }
            else if (wrap)
            {
                return (0, position.Item2);
            }
            return null;
        }

        public M2d(List<string> lines, int width, int height, bool saveMap, Symbols? symbols)
        {
            if (saveMap)
            {
                Map = new char[width, height];
            }

            if (symbols != null)
            {
                for (var i = 0; i < height; i++)
                {
                    for (var j = 0; j < width; j++)
                    {
                        if (symbols.StartSymbol != null && lines[i][j] == symbols.StartSymbol)
                        {
                            Starts.Add((j, i));
                        }
                        if (symbols.GoalSymbol != null && lines[i][j] == symbols.GoalSymbol)
                        {
                            Goals.Add((j, i));
                        }
                        if (symbols.ObstacleSymbol != null && lines[i][j] == symbols.ObstacleSymbol)
                        {
                            Obstacles.Add((j, i));
                        }
                        if (saveMap)
                        {
                            Map[j, i] = lines[i][j];
                        }
                    }
                }
            }

            dimensions = (width, height);
        }
    }
}
