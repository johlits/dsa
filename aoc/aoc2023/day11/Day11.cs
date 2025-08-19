using System.Numerics;

public class Day11
{
    public static void Run()
    {
        using (StreamReader file = new StreamReader("day11/p.in"))
        {
            string? ln;
            var map = new List<string>();

            var w = 0;
            var h = 0;
            while ((ln = file.ReadLine()) != null)
            {
                
                var allDots = true;
                w = 0;
                foreach (var c in ln)
                {
                    if (c != '.')
                    {
                        allDots = false;
                    }
                    w++;
                }
                if (allDots)
                {
                    for (var i = 0; i < ln.Length; i++)
                    {
                        ln = ln.Replace(".", "x");
                    }
                }
                map.Add(ln);
                h++;
            }

            for (var i = 0; i < w; i++)
            {
                var allDots = true;
                for (var j = 0; j < h; j++)
                {
                    if (map[j][i] != '.' && map[j][i] != 'x')
                    {
                        allDots = false;
                        break;
                    }
                }
                if (allDots)
                {

                    for (var j = 0; j < h; j++)
                    {
                        map[j] = map[j].Remove(i, 1);
                        map[j] = map[j].Insert(i, "x");
                    }
                    i++;
                }
            }

            for (var i = 0; i < h; i++)
            {
                for (var j = 0; j < w; j++)
                {
                    Console.Write(map[j][i]);
                }
                Console.WriteLine();
            }

            BigInteger acc = 0;
            for (var i = 0; i < h; i++)
            {
                for (var j = 0; j < w; j++)
                {
                    if (map[i][j] == '#')
                    {

                        Console.Write("x: " + j + " y: " + i + " = ");
                        var q = new Queue<Tuple<int, int, ulong>>();
                        q.Enqueue(new Tuple<int, int, ulong>(j, i, 0));
                        ulong dist = 0;
                        var visited = new List<Tuple<int, int>>();
                        visited.Add(new Tuple<int, int>(j, i));
                        while (q.Count > 0)
                        {
                            var current = q.Dequeue();
                            var d = current.Item3;

                            if (map[current.Item2][current.Item1] == '#' && d > 0)
                            {
                                dist += d;
                            }
                            if (map[current.Item2][current.Item1] == 'x')
                            {
                                d += 1000000 - 1;
                            }

                            if (current.Item1 > 0 && !visited.Contains(new Tuple<int, int>(current.Item1 - 1, current.Item2)))
                            {
                                visited.Add(new Tuple<int, int>(current.Item1 - 1, current.Item2));
                                q.Enqueue(new Tuple<int, int, ulong>(current.Item1 - 1, current.Item2, d + 1));
                            }
                            if (current.Item1 < w - 1 && !visited.Contains(new Tuple<int, int>(current.Item1 + 1, current.Item2)))
                            {
                                visited.Add(new Tuple<int, int>(current.Item1 + 1, current.Item2));
                                q.Enqueue(new Tuple<int, int, ulong>(current.Item1 + 1, current.Item2, d + 1));
                            }
                            if (current.Item2 > 0 && !visited.Contains(new Tuple<int, int>(current.Item1, current.Item2 - 1)))
                            {
                                visited.Add(new Tuple<int, int>(current.Item1, current.Item2 - 1));
                                q.Enqueue(new Tuple<int, int, ulong>(current.Item1, current.Item2 - 1, d + 1));
                            }
                            if (current.Item2 < h - 1 && !visited.Contains(new Tuple<int, int>(current.Item1, current.Item2 + 1)))
                            {
                                visited.Add(new Tuple<int, int>(current.Item1, current.Item2 + 1));
                                q.Enqueue(new Tuple<int, int, ulong>(current.Item1, current.Item2 + 1, d + 1));
                            }
                        }
                        Console.WriteLine(dist);
                        acc += dist;


                    }
                }
            }

            Console.WriteLine(acc / 2);

            file.Close();
        }
    }
}

