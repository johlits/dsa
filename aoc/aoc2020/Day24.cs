using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Advent
{
    class Day24
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

            var map = new char[1000, 1000];
            var flip = new bool[1000, 1000];

            for (var i = 0; i < 1000; i++)
            {
                for (var j = 0; j < 1000; j++)
                {
                    map[i, j] = 'w';
                }
            }

            foreach (var line in lines)
            {
                var j = 0;
                var acc = "";
                var x = 500;
                var y = 500;
                for (var i = 0; i < line.Length; i++)
                {
                    if (j == 0)
                    {
                        if (line[i] == 'w')
                        {
                            x--;
                        }
                        else if (line[i] == 'e')
                        {
                            x++;
                        }
                        else
                        {
                            acc += line[i];
                            j++;
                        }
                    }
                    else
                    {
                        if (acc + line[i] == "nw")
                        {
                            if (y % 2 == 0)
                            {
                                x--;
                                y--;
                            }
                            else
                            {
                                y--;
                            }
                        }
                        else if (acc + line[i] == "ne")
                        {
                            if (y % 2 == 0)
                            {
                                y--;
                            }
                            else
                            {
                                x++;
                                y--;
                            }
                        }
                        else if (acc + line[i] == "sw")
                        {
                            if (y % 2 == 0)
                            {
                                x--;
                                y++;
                            }
                            else
                            {
                                y++;
                            }
                        }
                        else if (acc + line[i] == "se")
                        {
                            if (y % 2 == 0)
                            {
                                y++;
                            }
                            else
                            {
                                x++;
                                y++;
                            }
                        }
                        else
                        {
                            throw new Exception();
                        }
                        acc = "";
                        j = 0;
                    }
                }
                if (map[x, y] == 'w')
                {
                    map[x, y] = 'b';
                }
                else
                {
                    map[x, y] = 'w';
                }
            }




            for (var day = 0; day < 100; day++)
            {
                for (var i = 1; i < 999; i++)
                {
                    for (var j = 1; j < 999; j++)
                    {
                        flip[i, j] = false;
                        var adj = 0;
                        if (j % 2 == 0)
                        {
                            if (map[i - 1, j] == 'b') adj++;
                            if (map[i + 1, j] == 'b') adj++;
                            if (map[i - 1, j - 1] == 'b') adj++;
                            if (map[i, j - 1] == 'b') adj++;
                            if (map[i - 1, j + 1] == 'b') adj++;
                            if (map[i, j + 1] == 'b') adj++;
                        }
                        else
                        {
                            if (map[i - 1, j] == 'b') adj++;
                            if (map[i + 1, j] == 'b') adj++;
                            if (map[i, j - 1] == 'b') adj++;
                            if (map[i + 1, j - 1] == 'b') adj++;
                            if (map[i, j + 1] == 'b') adj++;
                            if (map[i + 1, j + 1] == 'b') adj++;
                        }

                        /*
Any black tile with zero or more than 2 black tiles immediately adjacent to it is flipped to white.
Any white tile with exactly 2 black tiles immediately adjacent to it is flipped to black.
                        */

                        if (map[i, j] == 'b' && (adj == 0 || adj > 2))
                        {
                            flip[i, j] = true;
                        }
                        else if (map[i, j] == 'w' && adj == 2)
                        {
                            flip[i, j] = true;
                        }
                    }
                }

                for (var i = 0; i < 1000; i++)
                {
                    for (var j = 0; j < 1000; j++)
                    {
                        if (flip[i, j])
                        {
                            if (map[i, j] == 'b')
                            {
                                map[i, j] = 'w';
                            }
                            else
                            {
                                map[i, j] = 'b';
                            }
                        }
                    }
                }
            }

            var blk = 0;
            for (var i = 0; i < 1000; i++)
            {
                for (var j = 0; j < 1000; j++)
                {
                    if (map[i, j] == 'b')
                    {
                        blk++;
                    }
                }
            }

            Console.WriteLine(blk);

            Console.WriteLine("DONE");
            Console.ReadLine();
        }
    }
}