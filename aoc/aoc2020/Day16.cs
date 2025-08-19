using System;
using System.Collections.Generic;
using System.Linq;

namespace Advent
{
    class Day16
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
            var segment = 0;
            var nums = new List<int>();
            var nearby = new List<List<int>>();
            var RANGES = new List<(string, int, int, int, int)>();
            var MYTICKET = new List<int>();

            foreach (var line in lines)
            {
                if (line == "")
                {
                    segment++;
                }
                else if (segment == 0)
                {
                    var ranges = line.Split(' ');
                    var r1 = ranges[1].Split('-');
                    var r2 = ranges[3].Split('-');
                    RANGES.Add((ranges[0], R.ToInt(r1[0]), R.ToInt(r1[1]), R.ToInt(r2[0]), R.ToInt(r2[1])));
                    for (var i = R.ToInt(r1[0]); i <= R.ToInt(r1[1]); i++)
                    {
                        nums.Add(i);
                    }
                    for (var i = R.ToInt(r2[0]); i <= R.ToInt(r2[1]); i++)
                    {
                        nums.Add(i);
                    }
                }
                else if (segment == 2)
                {
                    var numz = line.Split(',');
                    foreach (var nz in numz)
                    {
                        MYTICKET.Add(R.ToInt(nz));
                    }
                }
                else if (segment == 4)
                {
                    var numz = line.Split(',');
                    var tmp = new List<int>();
                    var discard = false;
                    foreach (var nz in numz)
                    {
                        if (nums.Contains(R.ToInt(nz)))
                        {
                            tmp.Add(R.ToInt(nz));
                        }
                        else
                        {
                            discard = true;
                        }

                    }
                    if (!discard)
                    {
                        nearby.Add(tmp);
                    }

                }
                else
                {
                    segment++;
                }
            }

            nearby.Add(MYTICKET);


            var DIC = new Dictionary<string, List<int>>();

            Console.WriteLine("---");
            for (var i = 0; i < RANGES.Count; i++)
            {
                Console.WriteLine(RANGES[i].Item1);
                DIC.Add(RANGES[i].Item1, new List<int>());

                for (var k = 0; k < nearby[0].Count; k++)
                {

                    var isInRange = true;

                    for (var j = 0; j < nearby.Count; j++)
                    {


                        if ((nearby[j][k] >= RANGES[i].Item2 && nearby[j][k] <= RANGES[i].Item3) || (nearby[j][k] >= RANGES[i].Item4 && nearby[j][k] <= RANGES[i].Item5))
                        {

                        }
                        else
                        {
                            isInRange = false;
                            break;
                        }

                    }

                    if (isInRange)
                    {
                        Console.Write(k + " is " + RANGES[i].Item1);
                        if (!DIC[RANGES[i].Item1].Contains(k))
                        {
                            DIC[RANGES[i].Item1].Add(k);
                        }

                    }

                }
                Console.WriteLine();
            }

            var cont = true;
            while (cont)
            {
                cont = false;

                foreach (var kvp in DIC)
                {
                    if (kvp.Value.Count == 1)
                    {
                        var val = kvp.Value[0];

                        foreach (var kvp2 in DIC)
                        {
                            if (kvp2.Key != kvp.Key)
                            {
                                kvp2.Value.Remove(val);
                            }
                        }

                    }
                }


                Console.WriteLine("---");
                foreach (var kvp in DIC)
                {
                    Console.Write(kvp.Key + ": ");
                    foreach (var num in kvp.Value)
                    {
                        Console.Write(num + " ");
                    }
                    if (kvp.Value.Count > 1)
                    {
                        cont = true;
                    }
                    Console.WriteLine();
                }
            }

            foreach (var kvp in DIC)
            {
                Console.Write(kvp.Key + ": ");
                foreach (var num in kvp.Value)
                {
                    Console.Write(num + " ");
                }
                Console.WriteLine();
            }

            Console.WriteLine("--- ok");
            ulong mult = 1;

            for (var i = 0; i < MYTICKET.Count; i++)
            {
                var tmp = MYTICKET[i];

                Console.Write(tmp);

                foreach (var kvp in DIC)
                {
                    if (kvp.Value.Contains(i))
                    {
                        Console.Write(" " + kvp.Key);
                        if (kvp.Key.StartsWith("departure"))
                        {
                            mult = mult * (ulong)tmp;
                        }
                        break;
                    }
                }

                Console.WriteLine();
            }

            Console.WriteLine(mult);

            Console.ReadLine();
        }
    }
}