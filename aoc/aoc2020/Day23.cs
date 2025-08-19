using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Advent
{
    class Day23
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

        public class Mug
        {
            public int val;
            public Mug previous;
            public Mug next;
        }

        public void Run()
        {
            var input = "186524973";
            var dic = new Dictionary<int, Mug>();
            int i;
            Mug prev = null;
            Mug first = null;

            foreach (var c in input)
            {
                var m = new Mug() { val = (int)(c - '0') };
                if (prev != null)
                {
                    m.previous = prev;
                    prev.next = m;
                }
                dic.Add((int)(c - '0'), m);
                prev = m;
                if (first == null)
                    first = m;
            }

            for (i = 10; i <= 1000000; i++)
            {
                var m = new Mug() { val = i };
                if (prev != null)
                {
                    m.previous = prev;
                    prev.next = m;
                }
                dic.Add(i, m);
                prev = m;
            }

            first.previous = dic[1000000];
            dic[1000000].next = first;

            var current = first;

            for (i = 0; i < 10000000; i++)
            {
                Console.WriteLine(i);
                var dest = current.val - 1;
                if (dest < 1) dest = 1000000;
                while (current.next.val == dest || current.next.next.val == dest || current.next.next.next.val == dest)
                {
                    dest--;
                    if (dest < 1) dest = 1000000;
                }
                var destA = dic[dest]; // destination A
                var destB = dic[dest].next; // destination B
                var mug = current;
                var a = mug.next;
                var c = mug.next.next.next;
                var d = mug.next.next.next.next;

                var destANext = a;
                var aPrev = destA;
                var destBPrev = c;
                var cNext = destB;
                var mugNext = d;
                var dPrev = mug;

                destA.next = destANext;
                a.previous = aPrev;
                destB.previous = destBPrev;
                c.next = cNext;
                mug.next = mugNext;
                d.previous = dPrev;

                current = d;

            }

            Console.WriteLine(dic[1].next.val + " " + dic[1].next.next.val);
            Console.WriteLine(((ulong)dic[1].next.val * (ulong)dic[1].next.next.val));

            Console.ReadLine();
            Console.WriteLine("DONE");
            Console.ReadLine();
        }
    }
}