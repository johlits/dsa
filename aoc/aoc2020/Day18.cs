using System;
using System.Collections.Generic;
using System.Linq;

namespace Advent
{
    class Day18
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

        List<string> ops = new List<string>();

        public (ulong, int) Calculate(int from, int depth)
        {
            ulong? result = null;
            char? op = null;
            int i = from;
            for (; i < ops.Count; i++)
            {
                ulong number;
                if (UInt64.TryParse(ops[i], out number))
                {
                    if (op == null)
                    {
                        result = number;
                    }
                    else if (op == '+')
                    {
                        Console.WriteLine("add " + number + " to " + result + " (no)");
                        result += number;
                        op = null;
                    }
                    else if (op == '*')
                    {
                        Console.WriteLine("mult " + number + " to " + result + " (no)");
                        result *= number;
                        op = null;
                    }
                }

                else if (ops[i] == "+")
                {
                    op = '+';
                }

                else if (ops[i] == "*")
                {
                    op = '*';

                    var calc = Calculate(i + 1, depth + 1);
                    Console.WriteLine("mult " + calc.Item1 + " to " + result + " (op)");
                    result *= calc.Item1;
                    i = calc.Item2;
                    op = null;
                    return ((ulong)result, i);
                }

                else if (ops[i] == ")")
                {
                    Console.WriteLine(")");
                    return ((ulong)result, i);
                }

                else if (ops[i] == "(")
                {
                    if (op == '+')
                    {
                        var calc = Calculate(i + 1, depth + 1);
                        Console.WriteLine("add " + calc.Item1 + " to " + result);
                        result += calc.Item1;
                        
                        i = calc.Item2;
                        op = null;
                    }
                    else if (op == '*')
                    {
                        var calc = Calculate(i + 1, depth + 1);
                        Console.WriteLine("mult " + calc.Item1 + " to " + result);
                        result *= calc.Item1;
                        i = calc.Item2;
                        op = null;
                    }
                    else if (op == null)
                    {
                        var calc = Calculate(i + 1, depth + 1);
                        result = calc.Item1;
                        i = calc.Item2;
                        op = null;
                    }
                }
            }

            return ((ulong)result, i);

        }

        public void Run()
        {
            var lines = R.ReadAllLines();
            ulong sum = 0;

            foreach (var line in lines)
            {
                ops = new List<string>();
                var operators = line.Replace("(", " ( ").Replace(")", " ) ").Split(' ');
                foreach (var op in operators) {
                    if (op == "")
                    {
                        continue;
                    }
                    ops.Add(op);
                }

                var result = Calculate(0, 0).Item1;
                Console.WriteLine(result);
                sum += result;
                
            }


            R.Out(sum);


            Console.WriteLine("DONE");
            Console.ReadLine();
        }
    }
}