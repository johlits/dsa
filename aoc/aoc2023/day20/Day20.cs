public class Day20
{
    private static int lo = 0;
    private static int hi = 0;
    private static long iter = 0;
    private static Dictionary<string, List<long>> multiples = new Dictionary<string, List<long>>();


    public class Module
    {
        public string Name { get; set; }
        public int Type { get; set; }
        public List<string> Dest { get; set; }
        public virtual void Pulse(string sender, int pulse)
        {

        }

    }
    public class Broadcaster : Module
    {
        public override void Pulse(string sender, int pulse)
        {
            foreach (var d in Dest)
            {
                if (pulse == 1) hi++; else lo++;
                q.Enqueue(new Tuple<Module, string, int>(dic[d], Name, pulse));
            }
        }
    }
    public class FlipFlop : Module
    {
        public bool onoff = false;
        public override void Pulse(string sender,int pulse)
        {
            if (pulse == 0)
            {
                onoff = !onoff;
                foreach (var d in Dest)
                {
                    if (onoff) hi++; else lo++;
                    q.Enqueue(new Tuple<Module, string, int>(dic[d], Name, onoff ? 1 : 0));
                }
            }
        }
    }
    public class Conjunction : Module
    {
        public Dictionary<string, int> mem = new Dictionary<string, int>();

        public override void Pulse(string sender, int pulse)
        {
            if (Name == "th")
            {
                foreach (var pair in mem)
                {
                    var key = pair.Key;
                    var value = pair.Value;
                    if (pair.Value == 1)
                    {
                        if (multiples.ContainsKey(key))
                        {
                            multiples[key].Add(iter);
                        }
                        else
                        {
                            multiples.Add(key, new List<long> { iter });
                        }
                    }
                    
                }
            }
            var allOn = true;
            mem[sender] = pulse;
            foreach (var pair in mem)
            {
                var key = pair.Key;
                var value = pair.Value;
                if (value != 1)
                {
                    allOn = false;
                }
            }
            foreach (var d in Dest)
            {
                if (allOn) lo++; else hi++;
                if (dic.ContainsKey(d))
                {
                    q.Enqueue(new Tuple<Module, string, int>(dic[d], Name, allOn ? 0 : 1));
                }
                else
                {
                    if (allOn)
                    {
                        Console.WriteLine(d);
                    }
                }
            }
        }
    }

    public static Dictionary<string, Module> dic = new Dictionary<string, Module>();
    public static Queue<Tuple<Module, string, int>> q = new Queue<Tuple<Module, string, int>>();

    public static void Run()
    {
        Broadcaster broadcast = null;
        using (StreamReader file = new StreamReader("day20/p.in"))
        {
            string? ln;
            while ((ln = file.ReadLine()) != null)
            {
                var parts = ln.Split(" ", 3);
                if (parts[0].StartsWith("%"))
                {
                    var module = new FlipFlop { Dest = new List<string>() };
                    var name = parts[0].Substring(1);
                    var destinations = parts[2].Split(" ");
                    foreach (var dest in destinations)
                    {
                        module.Dest.Add(dest.Replace(",", "").Trim());
                    }
                    module.Name = name;
                    module.Type = 2;
                    dic.Add(name, module);
                }
                else if (parts[0].StartsWith("&"))
                {
                    var module = new Conjunction { Dest = new List<string>() };
                    var name = parts[0].Substring(1);
                    var destinations = parts[2].Split(" ");
                    foreach (var dest in destinations)
                    {
                        module.Dest.Add(dest.Replace(",", "").Trim());
                    }
                    module.Name = name;
                    module.Type = 3;
                    dic.Add(name, module);
                }
                else
                {
                    var module = new Broadcaster { Dest = new List<string>() };
                    var name = parts[0].Substring(1);
                    var destinations = parts[2].Split(" ");
                    foreach (var dest in destinations)
                    {
                        module.Dest.Add(dest.Replace(",", "").Trim());
                    }
                    module.Name = name;
                    module.Type = 1;
                    dic.Add(name, module);
                    broadcast = module;
                }
            }

            foreach (var pair in dic)
            {
                var key = pair.Key;
                var value = pair.Value;

                if (value.Type == 1 || value.Type == 2)
                {
                    foreach (var dest in value.Dest)
                    {
                        if (dic[dest].Type == 3)
                        {
                            if (!((Conjunction)dic[dest]).mem.ContainsKey(key))
                            {
                                ((Conjunction)dic[dest]).mem.Add(key, 0);
                            }
                        }
                    }
                }
            }

            long hi_tot = 0;
            long lo_tot = 0;

            for (var i = 0; i < 1000000; i++)
            {
                iter++;
                hi = 0;
                lo = 0;

                q.Clear();
                q.Enqueue(new Tuple<Module, string, int>(broadcast, "", 0));
                lo++;

                while (q.Count > 0)
                {
                    var current = q.Dequeue();
                    if (current.Item1.Name == "rx" && current.Item3 == 0)
                    {
                        Console.WriteLine("found rx");
                    }
                    current.Item1.Pulse(current.Item2, current.Item3);
                }


                hi_tot += hi;
                lo_tot += lo;
            }

            foreach (var m in multiples)
            {
                var key = m.Key;
                var value = m.Value;
                Console.Write(key + ": ");
                for (var i = 0; i < 10; i++)
                {
                    Console.Write(" " + value[i]);
                }
                Console.WriteLine();
            }

            // calculate LCM from these

            Console.WriteLine(hi_tot);
            Console.WriteLine(lo_tot);
            Console.WriteLine(hi_tot * lo_tot);

            file.Close();
        }
    }
}

