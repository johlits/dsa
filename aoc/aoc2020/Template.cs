using System;
using System.Collections.Generic;

namespace Advent
{
    class Template
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

            var sum = 0;

            for (var i = 0; i < lines.Count; i++)
            {
                sum += R.ToInt(lines[i]);
            }

            R.Out(sum);

        }
    }
}