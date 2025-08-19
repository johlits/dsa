using System;
using System.Collections.Generic;
using System.Linq;

namespace Advent
{
    class Day14
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
            var initial = "000000000000000000000000000000000000".ToArray();
            var mask = new char[36];

            var dic = new Dictionary<long, long>();

            foreach (var line in lines)
            {
                if (line.StartsWith("mask"))
                {
                    mask = line.Split(' ')[2].ToArray();
                }
                else
                {
                    var parts = line.Split('=');
                    var location = R.ToInt(parts[0].Remove(0, 4).Split(']')[0]);
                    var value = R.ToInt(parts[1].Trim());

                    var bin = Convert.ToString(location, 2);
                    
                    while (bin.Length < 36)
                    {
                        bin = "0" + bin;
                    }
                    R.Out("value " + location);
                    R.Out("bin " + bin);

                    

                    var masked = bin.ToArray();
                    var exes = "";
                    var exesLen = 0;

                    for (var i = 0; i < 36; i++)
                    {
                        if (mask[i] == '1')
                        {
                            masked[i] = '1';
                        }
                        if (mask[i] == 'X')
                        {
                            masked[i] = 'X';
                            exes += "1";
                            exesLen++;
                        }
                    }

                    var toAdd = new List<char[]>();

                    var max = Convert.ToInt64(exes, 2);
                    for (var i = 0; i <= max; i++)
                    {
                        var exBin = Convert.ToString(i, 2);
                        while(exBin.Length < exesLen)
                        {
                            exBin = "0" + exBin;
                        }

                        var exi = 0;
                        var maskedEx = new char[masked.Length];
                        for (var j = 0; j < masked.Length; j++)
                        {
                            if (masked[j] == 'X')
                            {
                                maskedEx[j] = exBin[exi++];
                            }
                            else
                            {
                                maskedEx[j] = masked[j];
                            }
                        }

                        
                        var dec = Convert.ToInt64(new string(maskedEx), 2);
                        R.Out("add " + dec + " " + new string(maskedEx));
                        if (dic.ContainsKey(dec))
                        {
                            dic[dec] = value;
                        }
                        else
                        {
                            dic.Add(dec, value);
                        }
                    }

                    
                }
            }

            long sum = 0;
            foreach (var kvp in dic)
            {
                sum += kvp.Value;
            }
            R.Out(sum);

            Console.ReadLine();
        }
    }
}