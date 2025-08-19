using System.Drawing;

public class Day14
{
    public static void Run()
    {
        var w = 500;
        var h = 200;
        var offsetx = -200;
        char[,] map = new char[w, h];
        var hiy = -1;

        using (StreamReader file = new StreamReader("day14/p.in"))
        {
            for (var i = 0; i < w; i++)
            {
                for (var j = 0; j < h; j++)
                {
                    map[i, j] = '.';
                }
            }
            
            string ln;
            while ((ln = file.ReadLine()) != null)
            {
                var coords = new List<Tuple<int, int>>();
                var words = ln.Split(" -> ");

                foreach (var word in words)
                {
                    var coord = word.Split(",");
                    coords.Add(new Tuple<int, int>(int.Parse(coord[0]) + offsetx, int.Parse(coord[1])));
                    if (int.Parse(coord[1]) > hiy) {
                        hiy = int.Parse(coord[1]);
                    }
                }
                for (var i = 0; i < coords.Count - 1; i++)
                {
                    var p1 = coords[i];
                    var p2 = coords[i + 1];
                    var dx = p2.Item1 - p1.Item1;
                    var dy = p2.Item2 - p1.Item2;

                    var x = p1.Item1;
                    var y = p1.Item2;
                    map[x, y] = '#';
                    while (dx != 0 || dy != 0)
                    {
                        if (dx > 0)
                        {
                            x++;
                            dx--;
                        }
                        if (dx < 0)
                        {
                            x--;
                            dx++;
                        }
                        if (dy > 0)
                        {
                            y++;
                            dy--;
                        }
                        if (dy < 0)
                        {
                            y--;
                            dy++;
                        }
                        map[x, y] = '#';
                    }
                }
            }

            file.Close();
        }

        for (var i = 0; i < w; i++)
        {
            map[i, hiy + 2] = '#';
        }

        var sandx = 500 + offsetx;
        var sandy = 0;
        var cnt = 0;
        while (sandy < h - 1)
        {
            if (map[sandx, sandy + 1] == '.')
            {
                sandy++;
            }
            else if (map[sandx - 1, sandy + 1] == '.')
            {
                sandx--;
                sandy++;
            }
            else if (map[sandx + 1, sandy + 1] == '.')
            {
                sandx++;
                sandy++;
            }
            else
            {
                map[sandx, sandy] = 'o';
                cnt++;
                if (sandx == 500 + offsetx && sandy == 0)
                {
                    Console.WriteLine("done");
                    break;
                }
                sandx = 500 + offsetx;
                sandy = 0;
            }
        }
        Console.WriteLine(cnt);

        // visualize
        using (StreamWriter writer = new StreamWriter("day14/p.out"))
        {
            writer.WriteLine(w + "," + h);
            for (var i = 0; i < h; i++)
            {
                for (var j = 0; j < w; j++)
                {
                    writer.Write(map[j, i]);
                }
                writer.WriteLine("");
            }
        }
    }
}

