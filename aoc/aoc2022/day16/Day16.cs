public class Day16
{
    public class Valve
    {
        public string name;
        public int rate;
        public List<Valve> connections;
        public Valve(string name)
        {
            this.name = name;
            connections = new List<Valve>();
        }

        public override string ToString()
        {
            return this.name;
        }
    }
    public static void Run()
    {
        var hi = -1;
        var dic = new Dictionary<string, Valve>();

        // delete opened valves for 2nd elephant
        using (StreamReader file = new StreamReader("day16/p.in"))
        {
            string ln;
            while ((ln = file.ReadLine()) != null)
            {
                var words = ln.Split(' ');
                var valve = words[1];
                if (!dic.ContainsKey(valve))
                {
                    dic.Add(valve, new Valve(valve));
                }
                var rate = int.Parse(words[4].Replace("rate=", "").Replace(";", ""));
                dic[valve].rate = rate;
                var toValves = new List<string>();
                for (var i = 9; i < words.Length; i++)
                {
                    toValves.Add(words[i].Replace(",", ""));
                }
                foreach (var toValve in toValves)
                {
                    if (!dic.ContainsKey(toValve))
                    {
                        dic.Add(toValve, new Valve(toValve));
                    }
                    dic[valve].connections.Add(dic[toValve]);
                }
                Console.WriteLine(ln);
            }

            file.Close();

            var q = new Queue<Tuple<Valve, HashSet<string>, int, int, bool>>();
            var mem = new Dictionary<Tuple<Valve, string, int>, int>();
            var mem2 = new Dictionary<Tuple<Valve, string, int>, int>();

            q.Enqueue(new Tuple<Valve, HashSet<string>, int, int, bool>(dic["AA"], new HashSet<string>(), 26, 0, false));
            var index = 0;
            while (q.Count != 0)
            {
                if (index++ % 1000000 == 0)
                {
                    Console.WriteLine(q.Count);
                    
                }
                var current = q.Dequeue();
                HashSet<string> clone = new HashSet<string>(current.Item2);
                if (current.Item5)
                {
                    clone.Add(current.Item1.name);
                }

                var opened = string.Join("", clone);
                var m = new Tuple<Valve, string, int>(
                    current.Item1,
                    opened,
                    current.Item3
                    //current.Item5
                    );

                var m2 = new Tuple<Valve, string, int>(
                    current.Item1,
                    opened,
                    current.Item4
                    //current.Item5
                    );

                //Console.WriteLine(m.ToString());

                if (current.Item4 > hi)
                {
                    hi = current.Item4;
                    foreach (var s in current.Item2)
                    {
                        Console.Write(s + ", ");
                    }
                    Console.WriteLine("new record: " + hi);
                }

                if (mem.ContainsKey(m))
                {
                    //Console.WriteLine("found mem!");
                    if (mem[m] < current.Item4)
                    {
                        mem[m] = current.Item4;
                    }
                    else
                    {
                        continue;
                    }
                }
                else
                {
                    mem.Add(m, current.Item4);
                }

                if (mem2.ContainsKey(m2))
                {
                    //Console.WriteLine("found mem!");
                    if (mem2[m2] < current.Item3)
                    {
                        mem2[m2] = current.Item3;
                    }
                    else
                    {
                        continue;
                    }
                }
                else
                {
                    mem2.Add(m2, current.Item3);
                }

                if (current.Item3 < 2)
                {
                    continue;
                }
                foreach (var conn in current.Item1.connections)
                {
                    q.Enqueue(new Tuple<Valve, HashSet<string>, int, int, bool>(dic[conn.name], clone, current.Item3 - 1, current.Item4, false));
                    if (!current.Item2.Contains(conn.name))
                    {
                        if (dic[conn.name].rate > 0) { 
                            q.Enqueue(new Tuple<Valve, HashSet<string>, int, int, bool>(dic[conn.name], clone, current.Item3 - 2, current.Item4 + dic[conn.name].rate * (current.Item3 - 2), true));
                        }
                    }
                }
            }

            Console.WriteLine("best: " + hi);
        }
    }
}

