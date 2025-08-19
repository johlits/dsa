using System.Data.SqlTypes;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

public class Day15
{
    private class Polygon
    {
        public Tuple<int, int> center;
        public List<Tuple<int, int>> polygon;

        public Polygon(Tuple<int,int> center,Tuple<int, int> left, Tuple<int, int> right, Tuple<int, int> up, Tuple<int, int> down)
        {
            this.center = center;
            polygon = new List<Tuple<int, int>>();
            polygon.Add(left);
            polygon.Add(up);
            polygon.Add(right);
            polygon.Add(down);
            polygon.Add(left);
        }
    }
    public static void Run()
    {
        var ss = 4000000;
        var areas = new List<Polygon>();
        var signals = new List<Tuple<int, int, int>>();
        using (StreamReader file = new StreamReader("day15/p.in"))
        {
            string ln;
            while ((ln = file.ReadLine()) != null)
            {
                int? sx = null, sy = null, bx = null, by = null;
                var words = ln.Split(" ");
                foreach (var word in words)
                {
                    if (word.StartsWith("x"))
                    {
                        if (sx == null)
                        {
                            sx = int.Parse(word.Replace("x=", "").Replace("y=", "").Replace(",", "").Replace(":", ""));
                        }
                        else
                        {
                            bx = int.Parse(word.Replace("x=", "").Replace("y=", "").Replace(",", "").Replace(":", ""));
                        }
                    }
                    if (word.StartsWith("y"))
                    {
                        if (bx == null)
                        {
                            sy = int.Parse(word.Replace("x=", "").Replace("y=", "").Replace(",", "").Replace(":", ""));
                        }
                        else
                        {
                            by = int.Parse(word.Replace("x=", "").Replace("y=", "").Replace(",", "").Replace(":", ""));
                        }
                    }
                }

                var d = Math.Abs((int)sx - (int)bx) + Math.Abs((int)sy - (int)by);
                var x = (int)sx;
                var y = (int)sy;

                signals.Add(new Tuple<int, int, int>(x, y, d));

                areas.Add(new Polygon(new Tuple<int, int>(x, y),
                    new Tuple<int, int>(x - d, y),
                    new Tuple<int, int>(x + d, y),
                    new Tuple<int, int>(x, y - d),
                    new Tuple<int, int>(x, y + d)
                ));
                
                sx = sy = bx = by = null;
            }

            var found = false;
            var ox = 0;
            var oy = 0;
            foreach (var area in areas)
            {
                for (var i = 0; i < area.polygon.Count() - 1; i++)
                {
                    var from = new Tuple<int, int>(area.polygon[i].Item1, area.polygon[i].Item2);
                    var to = new Tuple<int, int>(area.polygon[i + 1].Item1, area.polygon[i + 1].Item2);
                    while (true)
                    {
                        var x = from.Item1;
                        var y = from.Item2;
                        from = new Tuple<int, int>(x, y);

                        if (i == 0)
                        {
                            ox = -1;
                            oy = 0;
                        }
                        else if (i == 1)
                        {
                            ox = 0;
                            oy = -1;
                        }
                        else if (i == 2)
                        {
                            ox = 1;
                            oy = 0;
                        }
                        else if (i == 3)
                        {
                            ox = 0;
                            oy = 1;
                        }

                        if (from.Item1 + ox < 0 || from.Item1 + ox > ss)
                        {
                            break;
                        }
                        if (from.Item2 + oy < 0 || from.Item2 + oy > ss)
                        {
                            break;
                        }

                        var ok = true;
                        foreach (var signal in signals)
                        {
                            var m = Math.Abs(from.Item1 - signal.Item1 + ox) + Math.Abs(from.Item2 - signal.Item2 + oy);
                            if (m <= signal.Item3)
                            {
                                ok = false;
                                break;
                            }
                        }
                        if (ok)
                        {
                            ulong tuning = (ulong)(from.Item1 + ox) * (ulong)4000000 + (ulong)(from.Item2 + oy);
                            Console.WriteLine((from.Item1 + ox) + ", " + (from.Item2 + oy) + ": " + tuning);
                            found = true;
                            break;
                        }

                        if (from.Item1 == to.Item1 && from.Item2 == to.Item2)
                        {
                            break;
                        }

                        if (from.Item1 < to.Item1)
                        {
                            x++;
                        }
                        if (from.Item1 > to.Item1)
                        {
                            x--;
                        }
                        if (from.Item2 < to.Item2)
                        {
                            y++;
                        }
                        if (from.Item2 > to.Item2)
                        {
                            y--;
                        }
                        from = new Tuple<int, int>(x, y);
                    }
                    if (found)
                    {
                        break;
                    }
                }
                if (found)
                {
                    break;
                }
            }

            file.Close();
        }
    }
}

