
using System.ComponentModel.DataAnnotations;

public class Day10
{
    public static void Run()
    {
        using (StreamReader file = new StreamReader("day10/p.in"))
        {
            int w = 140;
            int h = 140;
            int bw = w * 2;
            int bh = h * 2;
            char[,] map = new char[w, h];
            int[,] dist = new int[w, h];
            char[,] big = new char[bw, bh];

            for (int y = 0; y < bh; y++)
            {
                for (int x = 0; x < bw; x++)
                {
                    big[x, y] = '.';
                }
            }

            string? ln;
            var j = 0;
            var q = new Queue<Tuple<int, int, int>>();
            var q2 = new Queue<Tuple<int, int, int>>();
            while ((ln = file.ReadLine()) != null)
            {
                for (var i = 0; i < ln.Length; i++)
                {
                    map[i, j] = ln[i];
                    if (ln[i] == 'S')
                    {
                        q.Enqueue(new Tuple<int, int, int>(i, j, 0));
                    }
                }
                j++;
            }


            var hi = 0;
            while (q.Count > 0)
            {
                var c = q.Dequeue();
                var x = c.Item1;
                var y = c.Item2;
                var d = c.Item3;
                var m = map[x, y];
                big[x * 2, y * 2] = '#';

                if (dist[x, y] == 0)
                {
                    dist[x, y] = d;

                    if (d > hi)
                    {
                        hi = d;
                    }
                }
                else
                {
                    continue;
                }

                if (m == 'S')
                {
                    if (x > 0)
                    {
                        if (map[x - 1, y] == '-') q.Enqueue(new Tuple<int, int, int>(x - 1, y, d + 1));
                        if (map[x - 1, y] == 'L') q.Enqueue(new Tuple<int, int, int>(x - 1, y, d + 1));
                        if (map[x - 1, y] == 'F') q.Enqueue(new Tuple<int, int, int>(x - 1, y, d + 1));

                        if (map[x - 1, y] == '-') big[x * 2 - 1, y * 2] = '#';
                        if (map[x - 1, y] == 'L') big[x * 2 - 1, y * 2] = '#';
                        if (map[x - 1, y] == 'F') big[x * 2 - 1, y * 2] = '#';
                    }
                    if (x < w - 1)
                    {
                        if (map[x + 1, y] == '-') q.Enqueue(new Tuple<int, int, int>(x + 1, y, d + 1));
                        if (map[x + 1, y] == 'J') q.Enqueue(new Tuple<int, int, int>(x + 1, y, d + 1));
                        if (map[x + 1, y] == '7') q.Enqueue(new Tuple<int, int, int>(x + 1, y, d + 1));

                        if (map[x + 1, y] == '-') big[x * 2 + 1, y * 2] = '#';
                        if (map[x + 1, y] == 'J') big[x * 2 + 1, y * 2] = '#';
                        if (map[x + 1, y] == '7') big[x * 2 + 1, y * 2] = '#';
                    }
                    if (y > 0)
                    {
                        if (map[x, y - 1] == '|') q.Enqueue(new Tuple<int, int, int>(x, y - 1, d + 1));
                        if (map[x, y - 1] == '7') q.Enqueue(new Tuple<int, int, int>(x, y - 1, d + 1));
                        if (map[x, y - 1] == 'F') q.Enqueue(new Tuple<int, int, int>(x, y - 1, d + 1));

                        if (map[x, y - 1] == '|') big[x * 2, y * 2 - 1] = '#';
                        if (map[x, y - 1] == '7') big[x * 2, y * 2 - 1] = '#';
                        if (map[x, y - 1] == 'F') big[x * 2, y * 2 - 1] = '#';
                    }
                    if (y < h -1)
                    {
                        if (map[x, y + 1] == '|') q.Enqueue(new Tuple<int, int, int>(x, y + 1, d + 1));
                        if (map[x, y + 1] == 'L') q.Enqueue(new Tuple<int, int, int>(x, y + 1, d + 1));
                        if (map[x, y + 1] == 'J') q.Enqueue(new Tuple<int, int, int>(x, y + 1, d + 1));

                        if (map[x, y + 1] == '|') big[x * 2, y * 2 + 1] = '#';
                        if (map[x, y + 1] == 'L') big[x * 2, y * 2 + 1] = '#';
                        if (map[x, y + 1] == 'J') big[x * 2, y * 2 + 1] = '#';
                    }
                }

                if (m == 'F')
                {
                    if (x < w - 1)
                    {
                        if (map[x + 1, y] == '-') q.Enqueue(new Tuple<int, int, int>(x + 1, y, d + 1));
                        if (map[x + 1, y] == 'J') q.Enqueue(new Tuple<int, int, int>(x + 1, y, d + 1));
                        if (map[x + 1, y] == '7') q.Enqueue(new Tuple<int, int, int>(x + 1, y, d + 1));

                        if (map[x + 1, y] == '-') big[x * 2 + 1, y * 2] = '#';
                        if (map[x + 1, y] == 'J') big[x * 2 + 1, y * 2] = '#';
                        if (map[x + 1, y] == '7') big[x * 2 + 1, y * 2] = '#';
                    }
                    if (y < h - 1)
                    {
                        if (map[x, y + 1] == '|') q.Enqueue(new Tuple<int, int, int>(x, y + 1, d + 1));
                        if (map[x, y + 1] == 'L') q.Enqueue(new Tuple<int, int, int>(x, y + 1, d + 1));
                        if (map[x, y + 1] == 'J') q.Enqueue(new Tuple<int, int, int>(x, y + 1, d + 1));

                        if (map[x, y + 1] == '|') big[x * 2, y * 2 + 1] = '#';
                        if (map[x, y + 1] == 'L') big[x * 2, y * 2 + 1] = '#';
                        if (map[x, y + 1] == 'J') big[x * 2, y * 2 + 1] = '#';
                    }
                }

                if (m == '7')
                {
                    if (x > 0)
                    {
                        if (map[x - 1, y] == '-') q.Enqueue(new Tuple<int, int, int>(x - 1, y, d + 1));
                        if (map[x - 1, y] == 'L') q.Enqueue(new Tuple<int, int, int>(x - 1, y, d + 1));
                        if (map[x - 1, y] == 'F') q.Enqueue(new Tuple<int, int, int>(x - 1, y, d + 1));

                        if (map[x - 1, y] == '-') big[x * 2 - 1, y * 2] = '#';
                        if (map[x - 1, y] == 'L') big[x * 2 - 1, y * 2] = '#';
                        if (map[x - 1, y] == 'F') big[x * 2 - 1, y * 2] = '#';
                    }
                    if (y < h - 1)
                    {
                        if (map[x, y + 1] == '|') q.Enqueue(new Tuple<int, int, int>(x, y + 1, d + 1));
                        if (map[x, y + 1] == 'L') q.Enqueue(new Tuple<int, int, int>(x, y + 1, d + 1));
                        if (map[x, y + 1] == 'J') q.Enqueue(new Tuple<int, int, int>(x, y + 1, d + 1));

                        if (map[x, y + 1] == '|') big[x * 2, y * 2 + 1] = '#';
                        if (map[x, y + 1] == 'L') big[x * 2, y * 2 + 1] = '#';
                        if (map[x, y + 1] == 'J') big[x * 2, y * 2 + 1] = '#';
                    }
                }

                if (m == 'J')
                {
                    if (x > 0)
                    {
                        if (map[x - 1, y] == '-') q.Enqueue(new Tuple<int, int, int>(x - 1, y, d + 1));
                        if (map[x - 1, y] == 'L') q.Enqueue(new Tuple<int, int, int>(x - 1, y, d + 1));
                        if (map[x - 1, y] == 'F') q.Enqueue(new Tuple<int, int, int>(x - 1, y, d + 1));

                        if (map[x - 1, y] == '-') big[x * 2 - 1, y * 2] = '#';
                        if (map[x - 1, y] == 'L') big[x * 2 - 1, y * 2] = '#';
                        if (map[x - 1, y] == 'F') big[x * 2 - 1, y * 2] = '#';
                    }
                    if (y > 0)
                    {
                        if (map[x, y - 1] == '|') q.Enqueue(new Tuple<int, int, int>(x, y - 1, d + 1));
                        if (map[x, y - 1] == '7') q.Enqueue(new Tuple<int, int, int>(x, y - 1, d + 1));
                        if (map[x, y - 1] == 'F') q.Enqueue(new Tuple<int, int, int>(x, y - 1, d + 1));

                        if (map[x, y - 1] == '|') big[x * 2, y * 2 - 1] = '#';
                        if (map[x, y - 1] == '7') big[x * 2, y * 2 - 1] = '#';
                        if (map[x, y - 1] == 'F') big[x * 2, y * 2 - 1] = '#';
                    }
                }

                if (m == 'L')
                {
                    if (x < w - 1)
                    {
                        if (map[x + 1, y] == '-') q.Enqueue(new Tuple<int, int, int>(x + 1, y, d + 1));
                        if (map[x + 1, y] == 'J') q.Enqueue(new Tuple<int, int, int>(x + 1, y, d + 1));
                        if (map[x + 1, y] == '7') q.Enqueue(new Tuple<int, int, int>(x + 1, y, d + 1));

                        if (map[x + 1, y] == '-') big[x * 2 + 1, y * 2] = '#';
                        if (map[x + 1, y] == 'J') big[x * 2 + 1, y * 2] = '#';
                        if (map[x + 1, y] == '7') big[x * 2 + 1, y * 2] = '#';
                    }
                    if (y > 0)
                    {
                        if (map[x, y - 1] == '|') q.Enqueue(new Tuple<int, int, int>(x, y - 1, d + 1));
                        if (map[x, y - 1] == '7') q.Enqueue(new Tuple<int, int, int>(x, y - 1, d + 1));
                        if (map[x, y - 1] == 'F') q.Enqueue(new Tuple<int, int, int>(x, y - 1, d + 1));

                        if (map[x, y - 1] == '|') big[x * 2, y * 2 - 1] = '#';
                        if (map[x, y - 1] == '7') big[x * 2, y * 2 - 1] = '#';
                        if (map[x, y - 1] == 'F') big[x * 2, y * 2 - 1] = '#';
                    }
                }

                if (m == '-')
                {
                    if (x > 0)
                    {
                        if (map[x - 1, y] == '-') q.Enqueue(new Tuple<int, int, int>(x - 1, y, d + 1));
                        if (map[x - 1, y] == 'L') q.Enqueue(new Tuple<int, int, int>(x - 1, y, d + 1));
                        if (map[x - 1, y] == 'F') q.Enqueue(new Tuple<int, int, int>(x - 1, y, d + 1));

                        if (map[x - 1, y] == '-') big[x * 2 - 1, y * 2] = '#';
                        if (map[x - 1, y] == 'L') big[x * 2 - 1, y * 2] = '#';
                        if (map[x - 1, y] == 'F') big[x * 2 - 1, y * 2] = '#';
                    }
                    if (x < w - 1)
                    {
                        if (map[x + 1, y] == '-') q.Enqueue(new Tuple<int, int, int>(x + 1, y, d + 1));
                        if (map[x + 1, y] == 'J') q.Enqueue(new Tuple<int, int, int>(x + 1, y, d + 1));
                        if (map[x + 1, y] == '7') q.Enqueue(new Tuple<int, int, int>(x + 1, y, d + 1));

                        if (map[x + 1, y] == '-') big[x * 2 + 1, y * 2] = '#';
                        if (map[x + 1, y] == 'J') big[x * 2 + 1, y * 2] = '#';
                        if (map[x + 1, y] == '7') big[x * 2 + 1, y * 2] = '#';
                    }
                }

                if (m == '|')
                {
                    if (y > 0)
                    {
                        if (map[x, y - 1] == '|') q.Enqueue(new Tuple<int, int, int>(x, y - 1, d + 1));
                        if (map[x, y - 1] == '7') q.Enqueue(new Tuple<int, int, int>(x, y - 1, d + 1));
                        if (map[x, y - 1] == 'F') q.Enqueue(new Tuple<int, int, int>(x, y - 1, d + 1));

                        if (map[x, y - 1] == '|') big[x * 2, y * 2 - 1] = '#';
                        if (map[x, y - 1] == '7') big[x * 2, y * 2 - 1] = '#';
                        if (map[x, y - 1] == 'F') big[x * 2, y * 2 - 1] = '#';
                    }
                    if (y < h - 1)
                    {
                        if (map[x, y + 1] == '|') q.Enqueue(new Tuple<int, int, int>(x, y + 1, d + 1));
                        if (map[x, y + 1] == 'L') q.Enqueue(new Tuple<int, int, int>(x, y + 1, d + 1));
                        if (map[x, y + 1] == 'J') q.Enqueue(new Tuple<int, int, int>(x, y + 1, d + 1));

                        if (map[x, y + 1] == '|') big[x * 2, y * 2 + 1] = '#';
                        if (map[x, y + 1] == 'L') big[x * 2, y * 2 + 1] = '#';
                        if (map[x, y + 1] == 'J') big[x * 2, y * 2 + 1] = '#';
                    }
                }
            }

            Console.WriteLine(hi);

            for (int y = 0; y < bh; y++)
            {
                for (int x = 0; x < bw; x++)
                {
                    Console.Write(big[x, y]);
                }
                Console.WriteLine();
            }

            for (int y = 0; y < bh; y++)
            {
                for (int x = 0; x < bw; x++)
                {
                    if (x == 0 || x == bw - 1 || y == 0 || y == bh - 1)
                    {
                        if (big[x, y] == '.')
                        {
                            q2.Enqueue(new Tuple<int, int, int>(x, y, 1));
                        }
                    }
                }
            }

            while (q2.Count > 0)
            {
                var c = q2.Dequeue();
                var x = c.Item1;
                var y = c.Item2;
                var m = big[x, y];
                if (m != '.')
                {
                    continue;
                }
                big[x, y] = ' ';
                if (x > 0)
                {
                    q2.Enqueue(new Tuple<int, int, int>(x - 1, y, 0));
                }
                if (x < bw - 1)
                {
                    q2.Enqueue(new Tuple<int, int, int>(x + 1, y, 0));
                }
                if (y > 0)
                {
                    q2.Enqueue(new Tuple<int, int, int>(x, y - 1, 0));
                }
                if (y < bh - 1)
                {
                    q2.Enqueue(new Tuple<int, int, int>(x, y + 1, 0));
                }
            }

            
            for (int y = 0; y < bh; y++)
            {
                for (int x = 0; x < bw; x++)
                {
                    if (x % 2 == 0 && y % 2 == 0)
                    {
                        map[x / 2, y / 2] = big[x, y];
                    }
                    
                    Console.Write(big[x, y]);
                }
                Console.WriteLine();
            }

            Console.WriteLine();

            var dots = 0;
            for (int y = 0; y < h; y++)
            {
                for (int x = 0; x < w; x++)
                {
                    if (map[x, y] == '.')
                    {
                        dots++;
                    }
                    Console.Write(map[x, y]);
                }
                Console.WriteLine();
            }

            Console.WriteLine();
            Console.WriteLine(dots);

            file.Close();
        }
    }
}

