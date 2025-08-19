public class Day21
{
    public static Dictionary<string, Monkey> dic = new Dictionary<string, Monkey>();
    public static Dictionary<string, string> ddic = new Dictionary<string, string>();
    public static int stalen = 0;
    public static bool log = false;
    public class Monkey
    {
        public string Name { get; set; }
        public long? Yell = null;
        public string Yell1 { get; set; }
        public string Yell2 { get; set; }
        public string Op { get; set; }
        public int Stale = 1000;
        public long Ret = -1;
        public long GetValue()
        {
            if (Yell != null)
            {
                return (long)Yell;
            }
            else
            {
                if (log)
                {
                    Console.Write(Name + " is ");
                }

                var ret = DoOp(dic[Yell1], dic[Yell2]);
                if (ret == Ret)
                {
                    Stale--;
                }
                else
                {
                    Stale = 1000;
                    this.Ret = ret;
                }
                if (Stale < 0)
                {
                    this.Yell = Ret;
                    stalen++;
                    Console.WriteLine(Name + " is stale" + stalen);
                }
                return ret;
            }
        }
        public bool GetEqual(Monkey m1, Monkey m2)
        {
            if (m1.GetValue() == m2.GetValue())
            {
                return true;
            }
            return false;
        }
        public long DoOp(Monkey m1, Monkey m2)
        {
            if (Op == "+") {
                if (log)
                {
                    var s1 = m1.Yell == null ? m1.Name : "" + m1.Yell;
                    var s2 = m2.Yell == null ? m2.Name : "" + m2.Yell;
                    ddic.Add(Name, " ( " + s1 + " + " + s2 + " ) ");
                    Console.WriteLine(" ( " + s1 + " + " + s2 + " ) ");
                }
                return m1.GetValue() + m2.GetValue();
            }
            if (Op == "-") {
                if (log)
                {
                    var s1 = m1.Yell == null ? m1.Name : "" + m1.Yell;
                    var s2 = m2.Yell == null ? m2.Name : "" + m2.Yell;
                    ddic.Add(Name, " ( " + s1 + " - " + s2 + " ) ");
                    Console.WriteLine(" ( " + s1 + " - " + s2 + " ) ");
                }
                return m1.GetValue() - m2.GetValue();
            }
            if (Op == "*")
            {
                if (log)
                {
                    var s1 = m1.Yell == null ? m1.Name : "" + m1.Yell;
                    var s2 = m2.Yell == null ? m2.Name : "" + m2.Yell;
                    ddic.Add(Name, " ( " + s1 + " * " + s2 + " ) ");
                    Console.WriteLine(" ( " + s1 + " * " + s2 + " ) ");
                }
                return m1.GetValue() * m2.GetValue();
            }
            if (Op == "/")
            {
                if (log)
                {
                    var s1 = m1.Yell == null ? m1.Name : "" + m1.Yell;
                    var s2 = m2.Yell == null ? m2.Name : "" + m2.Yell;
                    ddic.Add(Name, " ( " + s1 + " / " + s2 + " ) ");
                    Console.WriteLine(" ( " + s1 + " / " + s2 + " ) ");
                }
                return m1.GetValue() / m2.GetValue();
            }
            throw new Exception();
        }
    }

    public static void Run()
    {
        using (StreamReader file = new StreamReader("day21/p.in"))
        {
            string ln;
            var monkeys = new List<Monkey>();
            Monkey root = null;
            while ((ln = file.ReadLine()) != null)
            {
                string[] words = ln.Split(' ');
                var m = new Monkey();
                if (words.Length == 2)
                {
                    m.Name = words[0].Replace(":", "");
                    m.Yell = long.Parse(words[1]);
                }
                if (words.Length == 4)
                {
                    m.Name = words[0].Replace(":", "");
                    m.Yell1 = words[1];
                    m.Op = words[2];
                    m.Yell2 = words[3];
                }
                dic.Add(m.Name, m);
                monkeys.Add(m);
                if (m.Name == "root")
                {
                    root = m;
                    root.Op = "=";
                }
            }
            var humn = 0;
            while (true)
            {
                dic["humn"].Yell = humn;
                var eq = root.GetEqual(dic[root.Yell1], dic[root.Yell2]);
                if (eq == true)
                {
                    Console.WriteLine(humn);
                    break;
                }
                humn++;

                if (humn == 1000000)
                {
                    log = true;
                    Console.WriteLine(dic["vqfc"].Yell);
                }
                if (humn == 1000000 + 1)
                {
                    var s = ddic["wrvq"];
                    var found = true;
                    while (found)
                    {
                        found = false;
                        var ss = s.Split(' ');
                        foreach (var sss in ss)
                        {
                            if (ddic.ContainsKey(sss))
                            {
                                s = s.Replace(sss, ddic[sss]);
                                found = true;
                            }
                        }
                    }
                    Console.WriteLine("94625185243550=" +s.Replace(" ", "").Replace("1000000", "x"));
                    Console.WriteLine("solve for x..");
                    break;
                }

                if (false && humn % 1000000 == 0)
                {
                    Console.WriteLine(humn);
                    var q = new Queue<Tuple<Monkey, long>>();

                    if (dic[root.Yell1].Yell == null)
                    {
                        q.Enqueue(new Tuple<Monkey, long>(root, 0));
                    }
                    else
                    {
                        q.Enqueue(new Tuple<Monkey, long>(root, 0));
                    }
                    
                    while (q.Count != 0)
                    {
                        var m = q.Dequeue();
                        var monkey = m.Item1;
                        var m1 = dic[monkey.Yell1];
                        var m2 = dic[monkey.Yell2];
                        var mustResultIn = m.Item2;
                        var op = monkey.Op;
                        long rhv;

                        if (monkey.Name == "humn")
                        {
                            Console.WriteLine("found");
                        }

                        Monkey nextMonkey = null;
                        
                        if (m1.Yell == null)
                        {
                            nextMonkey = m1;
                            rhv = (long)m2.Yell;
                        }
                        else if (m2.Yell == null) {
                            nextMonkey = m2;
                            rhv = (long)m1.Yell;
                        }
                        else
                        {
                            continue;
                        }

                        if (op == "+")
                        {
                            // nextMonkey + rhv = mustResultIn
                            q.Enqueue(new Tuple<Monkey, long>(nextMonkey, mustResultIn - rhv));
                        }
                        if (op == "-")
                        {
                            // nextMonkey - rhv = mustResultIn
                            q.Enqueue(new Tuple<Monkey, long>(nextMonkey, mustResultIn + rhv));
                        }
                        if (op == "*")
                        {
                            // nextMonkey * rhv = mustResultIn
                            q.Enqueue(new Tuple<Monkey, long>(nextMonkey, mustResultIn / rhv));
                        }
                        if (op == "/")
                        {
                            // nextMonkey / rhv = mustResultIn
                            q.Enqueue(new Tuple<Monkey, long>(nextMonkey, mustResultIn * rhv));
                        }
                        if (op == "=")
                        {
                            q.Enqueue(new Tuple<Monkey, long>(nextMonkey, rhv));
                        }
                    }
                    //Console.WriteLine(stalen);
                    //var notStale = new List<Monkey>();
                    //foreach (var m in monkeys)
                    //{
                    //    if (m.Yell == null)
                    //    {
                    //        notStale.Add(m);
                    //    }
                    //}
                }
            }
            
            file.Close();
        }
    }
}

