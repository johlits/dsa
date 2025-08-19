using System;
using System.Collections.Generic;
using System.Linq;

namespace Advent
{
    class Day15
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
            var vals = lines[0].Split(',');
            var dicIndex = new Dictionary<ulong, (ulong, ulong)>();
            var dicCnt = new Dictionary<ulong, ulong>();

            ulong lastSpokenNum = 0;
            ulong numsSpoken = 0;

            foreach (var val in vals)
            {
                dicIndex.Add(R.ToULong(val), (0, numsSpoken));
                dicCnt.Add(R.ToULong(val), 1);

                lastSpokenNum = R.ToULong(val);
                numsSpoken++;
            }

            while(numsSpoken < 30000000)
            {
                if (dicCnt[lastSpokenNum] == 1)
                {
                    if (!dicIndex.ContainsKey(0))
                    {
                        dicIndex.Add(0, (0, numsSpoken));
                        dicCnt.Add(0, 1);
                        lastSpokenNum = 0;
                        numsSpoken++;
                    }
                    else
                    {
                        dicIndex[0] = (dicIndex[0].Item2, numsSpoken);
                        dicCnt[0]++;
                        lastSpokenNum = 0;
                        numsSpoken++;
                    }
                }
                else if (dicCnt[lastSpokenNum] > 1)
                {
                    ulong next = dicIndex[lastSpokenNum].Item2 - dicIndex[lastSpokenNum].Item1;
                    if (!dicIndex.ContainsKey(next))
                    {
                        dicIndex.Add(next, (next, numsSpoken));
                        dicCnt.Add(next, 1);
                        lastSpokenNum = next;
                        numsSpoken++;
                    }
                    else
                    {
                        dicIndex[next] = (dicIndex[next].Item2, numsSpoken);
                        dicCnt[next]++;
                        lastSpokenNum = next;
                        numsSpoken++;
                    }
                }
                Console.WriteLine(numsSpoken + " " + lastSpokenNum);
            }

            Console.ReadLine();
        }
    }
}