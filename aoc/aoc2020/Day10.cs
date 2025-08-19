using System;
using System.Collections.Generic;

namespace Advent
{
    class Day10
    {
        class Solutions
        {
            public static List<int> sorted = new List<int>();
            public static ulong cnt = (ulong)0;
        }

        class Connection
        {
            public Adapter from;
            public Adapter to;
            public int diff;
        }

        class Adapter
        {
            public int jolt;
            public bool isEnd;
            public ulong mem = 0;
            public bool full = false;
            public List<Connection> possible = new List<Connection>();
            public List<Adapter> back = new List<Adapter>();
            public bool HasPossible(Adapter from, Adapter to, int diff)
            {
                for (var i = 0; i < possible.Count; i++)
                {
                    if (possible[i].from == from && possible[i].to == to && possible[i].diff == diff)
                    {
                        return true;
                    }
                }
                return false;
            }

            public void Check(int depth)
            {
                
                if (!full)
                {
                    var isfull = true;
                    ulong tmp = 0;
                    foreach (var node in back)
                    {
                        if (!node.full)
                        {
                            isfull = false;
                            break;
                        }
                        else
                        {
                            tmp += node.mem;
                        }
                    }

                    if (isfull)
                    {
                        full = true;
                        mem = tmp;
                    }
                }

                if (isEnd || depth > 30)
                {
                    return;
                }

                foreach (var conn in possible)
                {
                    conn.to.Check(depth + 1);
                }
            }

            public ulong Cnt()
            {
                ulong sum = 0;
                foreach (var adp in back)
                {
                    if (adp.full)
                    {
                        sum += adp.mem;

                    }
                    else
                    {
                        sum += adp.Cnt();
                    }
                        
                }
                return sum;
            }
        }

        public void Run()
        {
            

            string dataIn = System.IO.File.ReadAllText(@"in.txt");

            var inputs = dataIn.Split('\n');

            var adapters = new List<Adapter>();

            var start = new Adapter() { jolt = 0 };
            Adapter hi = null;

            for (var i = 0; i < inputs.Length; i++)
            {
                var jolt = Int32.Parse(inputs[i]);
                Solutions.sorted.Add(jolt);
            }

            
            Solutions.sorted.Sort();

            for (var i = 0; i < Solutions.sorted.Count; i++)
            {
                var adapter = new Adapter() { jolt = Solutions.sorted[i] };
                adapters.Add(adapter);
                if (adapter.jolt <= 3)
                {
                    var conn = new Connection() { diff = adapter.jolt, from = start, to = adapter };
                    start.possible.Add(conn);
                    adapter.back.Add(start);
                }
                if (hi == null)
                {
                    hi = adapter;
                }
                else if (adapter.jolt > hi.jolt)
                {
                    hi = adapter;
                }
            }

            var end = new Adapter() { jolt = hi.jolt + 3, isEnd = true };
            hi.possible.Add(new Connection { diff = 3, from = hi, to = end });
            end.back.Add(hi);

            var connect = true;
            while (connect)
            {
                connect = false;
                for (var i = 0; i < adapters.Count; i++)
                {
                    var connectfrom = adapters[i];
                    for (var j = 0; j < adapters.Count; j++)
                    {
                        if (i != j)
                        {
                            var connectto = adapters[j];
                            if (connectfrom.jolt + 1 == connectto.jolt)
                            {
                                if (!connectfrom.HasPossible(connectfrom, connectto, 1))
                                {
                                    connectfrom.possible.Add(new Connection { diff = 1, from = connectfrom, to = connectto });
                                    connectto.back.Add(connectfrom);
                                    connect = true;
                                }
                            }
                            if (connectfrom.jolt + 2 == connectto.jolt)
                            {
                                if (!connectfrom.HasPossible(connectfrom, connectto, 2))
                                {
                                    connectfrom.possible.Add(new Connection { diff = 2, from = connectfrom, to = connectto });
                                    connectto.back.Add(connectfrom);
                                    connect = true;
                                }
                            }
                            if (connectfrom.jolt + 3 == connectto.jolt)
                            {
                                if (!connectfrom.HasPossible(connectfrom, connectto, 3))
                                {
                                    connectfrom.possible.Add(new Connection { diff = 3, from = connectfrom, to = connectto });
                                    connectto.back.Add(connectfrom);
                                    connect = true;
                                }
                            }
                        }
                    }
                }
            }

            start.full = true;
            start.mem = 1;
            start.Check(1);

            Console.WriteLine(end.Cnt());

        }
    }
}