public class Day8
{
    public static void Run()
    {
        using (StreamReader file = new StreamReader("day08/p.in"))
        {
            string ln;
            int w = 99;
            int[,] map = new int[w,w];
            bool[,] visible = new bool[w,w];
            var line = 0;
            while ((ln = file.ReadLine()) != null)
            {
                for (var i = 0; i < ln.Length; i++)
                {
                    map[i, line] = int.Parse(ln[i].ToString());
                    if (line == 0 || line == w - 1 || i == 0 || i == w - 1)
                    {
                        visible[i, line] = true;
                    }
                }
                line++;
            }

            for (var i = 1; i < w - 1; i++)
            {
                var hi = map[i, 0];
                for (var j = 1; j < w - 1; j++)
                {
                    if (map[i,j] > hi)
                    {
                        hi = map[i, j];
                        visible[i,j] = true;
                    }
                }

                hi = map[i, w - 1];
                for (var j = w - 2; j > 0; j--)
                {
                    if (map[i, j] > hi)
                    {
                        hi = map[i, j];
                        visible[i, j] = true;
                    }
                }
            }
            for (var j = 1; j < w - 1; j++)
            {
                var hi = map[0, j];
                for (var i = 1; i < w - 1; i++)
                {
                    if (map[i, j] > hi)
                    {
                        hi = map[i, j];
                        visible[i, j] = true;
                    }
                }

                hi = map[w - 1, j];
                for (var i = w - 2; i > 0; i--)
                {
                    if (map[i, j] > hi)
                    {
                        hi = map[i, j];
                        visible[i, j] = true;
                    }
                }
            }

            var best = -1;
            for (var i = 0; i < w; i++)
            {
                for (var j = 0; j < w; j++)
                {
                    var scenicA = 0;
                    var scenicB = 0;
                    var scenicC = 0;
                    var scenicD = 0;
                    var current = map[i, j];

                    for (var x = i-1; x >= 0; x--)
                    {
                        if (map[x,j] <= current)
                        {
                            scenicA++;
                            if (map[x,j] == current)
                            {
                                break;
                            }
                        }
                        else
                        {
                            scenicA++;
                            break;
                        }
                    }
                    for (var x = i+1; x < w; x++)
                    {
                        if (map[x, j] <= current)
                        {
                            scenicB++;
                            if (map[x, j] == current)
                            {
                                break;
                            }
                        }
                        else
                        {
                            scenicB++;
                            break;
                        }
                    }
                    for (var y = j-1; y >= 0; y--)
                    {
                        if (map[i, y] <= current)
                        {
                            scenicC++;
                            if (map[i, y] == current)
                            {
                                break;
                            }
                        }
                        else
                        {
                            scenicC++;
                            break;
                        }
                    }
                    for (var y = j+1; y < w; y++)
                    {
                        if (map[i, y] <= current)
                        {
                            scenicD++;
                            if (map[i, y] == current)
                            {
                                break;
                            }
                        }
                        else
                        {
                            scenicD++;
                            break;
                        }
                    }
                    if (scenicA * scenicB * scenicC * scenicD > best)
                    {

                        best = scenicA * scenicB * scenicC * scenicD;
                    }
                }
            }

            Console.WriteLine(best);

            file.Close();
        }
    }
}

