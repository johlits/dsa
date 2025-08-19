public class Day14
{
    public static void Run()
    {
        using (StreamReader file = new StreamReader("day14/p.in"))
        {
            var w = 100;
            var h = 100;
            char[,] map = new char[w, h];
            string? ln;

            var r = 0;
            while ((ln = file.ReadLine()) != null)
            {
                for (var c = 0; c < w; c++)
                {
                    map[c, r] = ln[c];
                }
                r++;
            }

            var dic = new Dictionary<int, List<int>>();

            for (var i = 1; i <= 1000000; i++)
            {

                var moving = true;
                while (moving)
                {
                    moving = false;

                    for (var y = 0; y < h; y++)
                    {
                        for (var x = 0; x < w; x++)
                        {
                            if (map[x, y] == 'O' && y > 0 && map[x, y - 1] == '.')
                            {
                                map[x, y - 1] = 'O';
                                map[x, y] = '.';
                                moving = true;
                            }
                        }
                    }
                }

                moving = true;
                while (moving)
                {
                    moving = false;

                    for (var x = 0; x < w; x++)
                    {
                        for (var y = 0; y < h; y++)
                        {
                            if (map[x, y] == 'O' && x > 0 && map[x - 1, y] == '.')
                            {
                                map[x - 1, y] = 'O';
                                map[x, y] = '.';
                                moving = true;
                            }
                        }
                    }
                }

                moving = true;
                while (moving)
                {
                    moving = false;

                    for (var y = 0; y < h; y++)
                    {
                        for (var x = 0; x < w; x++)
                        {
                            if (map[x, y] == 'O' && y < h - 1 && map[x, y + 1] == '.')
                            {
                                map[x, y + 1] = 'O';
                                map[x, y] = '.';
                                moving = true;
                            }
                        }
                    }
                }
                
                moving = true;
                while (moving)
                {
                    moving = false;

                    for (var x = 0; x < w; x++)
                    {
                        for (var y = 0; y < h; y++)
                        {
                            if (map[x, y] == 'O' && x < w - 1 && map[x + 1, y] == '.')
                            {
                                map[x + 1, y] = 'O';
                                map[x, y] = '.';
                                moving = true;
                            }
                        }
                    }
                }

                var score = 0;
                for (var y = 0; y < h; y++)
                {
                    for (var x = 0; x < w; x++)
                    {
                        if (map[x, y] == 'O')
                        {

                            score += h - y;
                        }
                    }
                }

                if (dic.ContainsKey(score))
                {
                    if (i == 1000)
                    {
                        Console.WriteLine(score);
                    }
                    dic[score].Add(i);
                }
                else
                {
                    dic.Add(score, new List<int>() { i });
                }
            }
            file.Close();
        }
    }
}

