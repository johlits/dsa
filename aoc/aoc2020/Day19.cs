using System;
using System.Collections.Generic;
using System.Linq;

namespace Advent
{
    class Day19
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

        class Rule
        {
            public int id;
            public char? letter = null;

            public List<List<Rule>> subrules = new List<List<Rule>>();

            public List<int> Search(int from)
            {
                var ret = new List<int>();

                if (letter != null)
                {
                    if (from >= search.Length)
                    {
                        return ret;
                    }
                    if (search[from] == letter)
                    {
                        ret.Add(from + 1);
                    }
                    return ret;
                }

                foreach (var subrule in subrules)
                {
                    var possibleFroms = new List<int>();
                    var possibleFromsNew = new List<int>();
                    possibleFroms.Add(from);

                    var subruleOk = true;
                    foreach (var rule in subrule)
                    {
                        possibleFromsNew.Clear();
                        foreach (var possibleFrom in possibleFroms)
                        {
                            var s = rule.Search(possibleFrom);
                            foreach (var val in s)
                            {
                                possibleFromsNew.Add(val);
                            }
                        }
                        possibleFroms.Clear();
                        foreach (var val in possibleFromsNew)
                        {
                            possibleFroms.Add(val);
                        }

                        if (possibleFroms.Count == 0)
                        {
                            subruleOk = false;
                            break;
                        }
                    }

                    if (subruleOk)
                    {
                        foreach(var possibleFrom in possibleFroms)
                        {
                            ret.Add(possibleFrom);
                        }
                    }
                }

                return ret;
            }
        }

        Dictionary<int, Rule> rules = new Dictionary<int, Rule>();
        public static string search = "";

        public void Run()
        {
            var lines = R.ReadAllLines();
            foreach (var line in lines)
            {
                if (line.Contains(":"))
                {
                    var id = R.ToInt(line.Split(':')[0]);
                    rules.Add(id, new Rule() { id = id });
                }
            }

            foreach (var line in lines)
            {
                if (line.Contains(":"))
                {
                    var id = R.ToInt(line.Split(':')[0]);
                    var rule = line.Split(':')[1];

                    if (rule.Contains("\""))
                    {
                        rules[id].letter = rule.Trim().Split('\"')[1][0];
                        continue;
                    }

                    var pipes = rule.Split('|');
                    if (!rule.Contains('|'))
                    {
                        pipes = new string[1] { rule };
                    }

                    
                    foreach (var pipe in pipes)
                    {
                        var toAdd = new List<Rule>();
                        var subrules = pipe.Trim().Split(' ');
                        foreach (var subrule in subrules)
                        {
                            toAdd.Add(rules[Int32.Parse(subrule)]);
                        }
                        rules[id].subrules.Add(toAdd);
                    }
                }
            }

            var matches = 0;
            foreach (var line in lines)
            {
                if (!line.Contains(":") && line.Length > 0)
                {
                    Console.Write(line);
                    search = line;
                    var match = rules[0].Search(0);
                    if (match.Count > 0)
                    {
                        foreach (var m in match)
                        {
                            if (m == line.Length)
                            {
                                matches++;
                                Console.WriteLine(" YES");
                                break;
                            }
                        }
                        
                    }
                    else
                    {
                        Console.WriteLine(" NO");
                    }
                }
            }

            foreach (var kvp in rules)
            {
                Console.WriteLine("---");
                Console.WriteLine(kvp.Key + " = " + kvp.Value.id);
                Console.WriteLine("Letter: " + kvp.Value.letter);
                foreach (var sublist in kvp.Value.subrules)
                {
                    foreach (var rule in sublist)
                    {
                        Console.Write(rule.id + " ");
                    }
                    Console.Write(" OR ");
                }
                Console.WriteLine();
            }

            Console.WriteLine("ANSWER: " + matches);
            Console.WriteLine("DONE");
            Console.ReadLine();
        }
    }
}