using System;
using System.Collections.Generic;

namespace Advent
{
    class Day12
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
            var commands = R.ReadAllLines();

            var x = 10;
            var y = 1;
            var sx = 0;
            var sy = 0;

            foreach (var command in commands)
            {
                var dir = command.Substring(0, 1);
                var amount = R.ToInt(command.Substring(1));

                if (dir == "N")
                {
                    y += amount;
                    Console.WriteLine("Waypoint: " + x + " " + y);
                }
                if (dir == "S")
                {
                    y -= amount;
                    Console.WriteLine("Waypoint: " + x + " " + y);
                }
                if (dir == "W")
                {
                    x -= amount;
                    Console.WriteLine("Waypoint: " + x + " " + y);
                }
                if (dir == "E")
                {
                    x += amount;
                    Console.WriteLine("Waypoint: " + x + " " + y);
                }
                if (dir == "F")
                {
                        sx += x * amount;
                        sy += y * amount;
                    Console.WriteLine("new ship: " + sx + " " + sy);
                }
                if (dir == "R")
                {
                    if (amount == 90)
                    {
                        var tmp = y;
                        y = -x;
                        x = tmp;
                    }
                    else if (amount == 180)
                    {
                        x = -x;
                        y = -y;

                    }
                    else if (amount == 270)
                    {
                        var tmp = y;
                        y = x;
                        x = -tmp;
                    }
                    Console.WriteLine("Waypoint: " + x + " " + y);
                }
                if (dir == "L")
                {
                    if (amount == 90)
                    {
                        var tmp = y;
                        y = x;
                        x = -tmp;
                    }
                    else if (amount == 180)
                    {
                        x = -x;
                        y = -y;
                    }
                    else if (amount == 270)
                    {
                        var tmp = x;
                        x = y;
                        y = -tmp;
                    }
                    Console.WriteLine("Waypoint: " + x + " " + y);
                }
            }

            if (sx < 0)
            {
                sx = -sx;
            }
            if (sy < 0)
            {
                sy = -sy;
            }

            Console.WriteLine(sx + " " + sy + " = " + (sx+sy));

        }
    }
}