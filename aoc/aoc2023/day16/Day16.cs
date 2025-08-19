public class Day16
{
    public static void Run()
    {
        using (StreamReader file = new StreamReader("day16/p.in"))
        {
            int w = 110;
            int h = 110;
            string? ln;
            char[,] map = new char[w, h];

            var r = 0;
            while ((ln = file.ReadLine()) != null)
            {
                for (var i = 0; i < w; i++)
                {
                    map[i, r] = ln[i];
                }
                r++;
            }
            var best = 0;

            for (var xx = 0; xx < w; xx++)
            {
                for (var yy = 0; yy < h; yy++)
                {
                    if ((xx == 0 || xx == w - 1 || yy == 0 || yy == h - 1))
                    {
                        Console.WriteLine(xx + " " + yy);
                        var q = new Queue<Tuple<int, int, char>>();

                        if (xx == 0)
                        {
                            q.Enqueue(new Tuple<int, int, char>(xx, yy, 'r'));
                        }
                        else if (xx == w - 1)
                        {
                            q.Enqueue(new Tuple<int, int, char>(xx, yy, 'l'));
                        }
                        else if (yy == 0)
                        {
                            q.Enqueue(new Tuple<int, int, char>(xx, yy, 'd'));
                        }
                        else if (yy == w - 1)
                        {
                            q.Enqueue(new Tuple<int, int, char>(xx, yy, 'u'));
                        }

                        var cnt = 0;
                        var nonew = 0;
                        bool[,] energized = new bool[w, h];

                        while (q.Count > 0)
                        {
                            var current = q.Dequeue();
                            var x = current.Item1;
                            var y = current.Item2;
                            var dir = current.Item3;

                            if (!energized[x, y])
                            {
                                energized[x, y] = true;
                                cnt++;
                                nonew = 0;
                            }
                            else
                            {
                                nonew++;
                                if (nonew > 10000000)
                                {
                                    break;
                                }
                            }

                            if (map[x, y] == '|' && dir == 'r')
                            {
                                if (y > 0)
                                {
                                    q.Enqueue(new Tuple<int, int, char>(x, y - 1, 'u'));
                                }
                                if (y < h - 1)
                                {
                                    q.Enqueue(new Tuple<int, int, char>(x, y + 1, 'd'));
                                }
                            }
                            else if (map[x, y] == '|' && dir == 'l')
                            {
                                if (y > 0)
                                {
                                    q.Enqueue(new Tuple<int, int, char>(x, y - 1, 'u'));
                                }
                                if (y < h - 1)
                                {
                                    q.Enqueue(new Tuple<int, int, char>(x, y + 1, 'd'));
                                }
                            }
                            else if (map[x, y] == '-' && dir == 'd')
                            {
                                if (x > 0)
                                {
                                    q.Enqueue(new Tuple<int, int, char>(x - 1, y, 'l'));
                                }
                                if (x < w - 1)
                                {
                                    q.Enqueue(new Tuple<int, int, char>(x + 1, y, 'r'));
                                }
                            }
                            else if (map[x, y] == '-' && dir == 'u')
                            {
                                if (x > 0)
                                {
                                    q.Enqueue(new Tuple<int, int, char>(x - 1, y, 'l'));
                                }
                                if (x < w - 1)
                                {
                                    q.Enqueue(new Tuple<int, int, char>(x + 1, y, 'r'));
                                }
                            }
                            else if (map[x, y] == '/' && dir == 'u')
                            {
                                if (x < w - 1)
                                {
                                    q.Enqueue(new Tuple<int, int, char>(x + 1, y, 'r'));
                                }
                            }
                            else if (map[x, y] == '/' && dir == 'd')
                            {
                                if (x > 0)
                                {
                                    q.Enqueue(new Tuple<int, int, char>(x - 1, y, 'l'));
                                }
                            }
                            else if (map[x, y] == '/' && dir == 'l')
                            {
                                if (y < h - 1)
                                {
                                    q.Enqueue(new Tuple<int, int, char>(x, y + 1, 'd'));
                                }
                            }
                            else if (map[x, y] == '/' && dir == 'r')
                            {
                                if (y > 0)
                                {
                                    q.Enqueue(new Tuple<int, int, char>(x, y - 1, 'u'));
                                }
                            }
                            else if (map[x, y] == '\\' && dir == 'u')
                            {
                                if (x > 0)
                                {
                                    q.Enqueue(new Tuple<int, int, char>(x - 1, y, 'l'));
                                }
                            }
                            else if (map[x, y] == '\\' && dir == 'd')
                            {
                                if (x < w - 1)
                                {
                                    q.Enqueue(new Tuple<int, int, char>(x + 1, y, 'r'));
                                }
                            }
                            else if (map[x, y] == '\\' && dir == 'l')
                            {
                                if (y > 0)
                                {
                                    q.Enqueue(new Tuple<int, int, char>(x, y - 1, 'u'));
                                }
                            }
                            else if (map[x, y] == '\\' && dir == 'r')
                            {
                                if (y < h - 1)
                                {
                                    q.Enqueue(new Tuple<int, int, char>(x, y + 1, 'd'));
                                }
                            }
                            else if (dir == 'l' && x > 0)
                            {
                                q.Enqueue(new Tuple<int, int, char>(x - 1, y, 'l'));
                            }
                            else if (dir == 'r' && x < w - 1)
                            {
                                q.Enqueue(new Tuple<int, int, char>(x + 1, y, 'r'));
                            }
                            else if (dir == 'u' && y > 0)
                            {
                                q.Enqueue(new Tuple<int, int, char>(x, y - 1, 'u'));
                            }
                            else if (dir == 'd' && y < h - 1)
                            {
                                q.Enqueue(new Tuple<int, int, char>(x, y + 1, 'd'));
                            }
                        }

                        Console.WriteLine(cnt + " best: " + best);
                        if (cnt > best)
                        {
                            best = cnt;

                        }

                    }
                }
            }

            Console.WriteLine(best);
            file.Close();
        }
    }
}

