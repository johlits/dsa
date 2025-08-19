using System;
using System.Collections.Generic;
using System.Linq;

namespace Advent
{
    class Day17
    {
        static class R
        {
            static object o;
            public static void Set(object result)
            {
                o = result;
            }
            public static void Out(object result = null)
            {
                object output = o;
                if (result != null)
                {
                    output = result;
                }
                Console.WriteLine(DateTime.Now + " Result -> " + output.ToString());
            }
            public static List<string> ReadAllLines(string from = @"in.txt")
            {
                string dataIn = System.IO.File.ReadAllText(from);
                var lines = dataIn.Split('\n');
                var ret = new List<string>();
                foreach (var line in lines)
                {
                    ret.Add(line.Trim());
                }
                return ret;
            }
            public static int ToInt(string s)
            {
                return Convert.ToInt32(s);
            }
            public static ulong ToULong(string s)
            {
                return Convert.ToUInt64(s);
            }
        }

        public void Run()
        {
            var lines = R.ReadAllLines();
            var dic = new Dictionary<(int, int, int, int), char>();

            for (var y = 0; y < lines.Count; y++)
            {
                for (var x = 0; x < lines[y].Length; x++)
                {
                    dic.Add((x, y, 0, 0), lines[y][x]);
                }
            }



            for (var i = 0; i < 6; i++)
            {
                var dic2 = new Dictionary<(int, int, int, int), char>();

                var toAdd = new List<(int, int, int, int)>();
                foreach (var kvp in dic)
                {
                    for (var x = kvp.Key.Item1 - 1; x <= kvp.Key.Item1 + 1; x++)
                    {
                        for (var y = kvp.Key.Item2 - 1; y <= kvp.Key.Item2 + 1; y++)
                        {
                            for (var z = kvp.Key.Item3 - 1; z <= kvp.Key.Item3 + 1; z++)
                            {
                                for (var w = kvp.Key.Item4 - 1; w <= kvp.Key.Item4 + 1; w++)
                                {
                                    if (!(x == kvp.Key.Item1 && y == kvp.Key.Item2 && z == kvp.Key.Item3 && w == kvp.Key.Item4))
                                    {
                                        toAdd.Add((x, y, z, w));
                                    }
                                }
                            }
                        }
                    }
                }

                foreach (var add in toAdd)
                {
                    if (!dic.ContainsKey(add))
                    {
                        dic.Add(add, '.');
                    }
                }

                foreach (var kvp in dic)
                {
                    var neighbors = 0;

                    for (var x = kvp.Key.Item1-1; x <= kvp.Key.Item1+1; x++)
                    {
                        for (var y = kvp.Key.Item2-1; y <= kvp.Key.Item2+1; y++)
                        {
                            for (var z = kvp.Key.Item3-1; z <= kvp.Key.Item3+1; z++)
                            {
                                for (var w = kvp.Key.Item4 - 1; w <= kvp.Key.Item4 + 1; w++)
                                {
                                    if (!(x == kvp.Key.Item1 && y == kvp.Key.Item2 && z == kvp.Key.Item3 && w == kvp.Key.Item4))
                                    {
                                        if (dic.ContainsKey((x, y, z, w)))
                                        {
                                            if (dic[(x, y, z, w)] == '#')
                                                neighbors++;
                                        }

                                    }
                                }
                            }
                        }
                    }

                    /*
        If a cube is active and exactly 2 or 3 of its neighbors are also active, the cube remains active. Otherwise, the cube becomes inactive.
        If a cube is inactive but exactly 3 of its neighbors are active, the cube becomes active. Otherwise, the cube remains inactive.
                    */

                    if (kvp.Value == '#')
                    {
                        if (neighbors == 2 || neighbors == 3)
                        {
                            dic2.Add(kvp.Key, '#');
                        }
                        else
                        {
                            dic2.Add(kvp.Key, '.');
                        }
                    }
                    else if (kvp.Value == '.')
                    {
                        if (neighbors == 3)
                        {
                            dic2.Add(kvp.Key, '#');
                        }
                        else
                        {
                            dic2.Add(kvp.Key, '.');
                        }
                    }
                }

                dic = new Dictionary<(int, int, int, int), char>();
                foreach (var kvp in dic2)
                {
                    dic.Add(kvp.Key, kvp.Value);
                }

                var active = 0;
                foreach (var kvp in dic)
                {
                    if (kvp.Value == '#')
                    {
                        active++;
                    }
                }

                R.Out(i + " " + active);

            }
            Console.WriteLine("DONE");
            Console.ReadLine();
        }
    }
}