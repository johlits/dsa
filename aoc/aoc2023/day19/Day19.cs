
public class Day19
{
    public class RangeComparer : IEqualityComparer<Range>
    {
        public bool Equals(Range x, Range y)
        {
            return x.minX == y.minX && x.maxX == y.maxX &&
                   x.minM == y.minM && x.maxM == y.maxM &&
                   x.minA == y.minA && x.maxA == y.maxA &&
                   x.minS == y.minS && x.maxS == y.maxS;
        }

        public int GetHashCode(Range obj)
        {
            return HashCode.Combine(obj.minX, obj.maxX, obj.minM, obj.maxM, obj.minA, obj.maxA, obj.minS, obj.maxS);
        }
    }

    public class Range
    {
        public int minX = 1;
        public int maxX = 4000;
        public int minM = 1;
        public int maxM = 4000;
        public int minA = 1;
        public int maxA = 4000;
        public int minS = 1;
        public int maxS = 4000;
        public Range() { }
        public Range(Range r)
        {
            this.minX = r.minX;
            this.maxX = r.maxX;
            this.minM = r.minM;
            this.maxM = r.maxM;
            this.minA = r.minA;
            this.maxA = r.maxA;
            this.minS = r.minS;
            this.maxS = r.maxS;
        }
    }
    public class Condition

    {
        public Condition() { }

        public Condition(string s)
        {
            // a<2006:qkq,m>2090:A,rfg
            var buff = "";
            for (var i = 0; i < s.Length; i++)
            {
                if (s[i] == '<')
                {
                    Operator = "<";
                    LHV = buff;
                    buff = "";
                    continue;
                }
                if (s[i] == '>')
                {
                    Operator = ">";
                    LHV = buff;
                    buff = "";
                    continue;
                }
                if (s[i] == ':')
                {
                    RHV = int.Parse(buff);
                    buff = "";
                    continue;
                }
                if (s[i] == ',')
                {
                    TrueCondition = new Condition() { Label = buff };
                    buff = "";
                    FalseCondition = new Condition(s.Substring(i + 1));
                    return;
                }
                buff += s[i];
            }
            Label = buff;
        }

        public string Label { get; set; }
        public string LHV { get; set; }
        public int RHV { get; set; }
        public string Operator { get; set; }
        public Condition TrueCondition { get; set; }
        public Condition FalseCondition { get; set; }
        public List<Range> GetRanges(Range range)
        {
            if (Label != null)
            {
                if (Label == "A")
                {
                    return new List<Range>() { range };
                }
                if (Label == "R")
                {
                    return new List<Range>() { };
                }
            }

            var t = TrueCondition.Label != null && TrueCondition.Label != "A" && TrueCondition.Label != "R" ? dic[TrueCondition.Label] : TrueCondition;
            var f = FalseCondition.Label != null && FalseCondition.Label != "A" && FalseCondition.Label != "R" ? dic[FalseCondition.Label] : FalseCondition; 
            
            if (Operator == "<")
            {
                if (LHV == "x")
                {
                    var r1 = t.GetRanges(new Range(range) { maxX = RHV - 1 });
                    var r2 = f.GetRanges(new Range(range) { minX = RHV });
                    r1.AddRange(r2);
                    return r1;
                }
                if (LHV == "m")
                {
                    var r1 = t.GetRanges(new Range(range) { maxM = RHV - 1 });
                    var r2 = f.GetRanges(new Range(range) { minM = RHV });
                    r1.AddRange(r2);
                    return r1;
                }
                if (LHV == "a")
                {
                    var r1 = t.GetRanges(new Range(range) { maxA = RHV - 1 });
                    var r2 = f.GetRanges(new Range(range) { minA = RHV });
                    r1.AddRange(r2);
                    return r1;
                }
                if (LHV == "s")
                {
                    var r1 = t.GetRanges(new Range(range) { maxS = RHV - 1 });
                    var r2 = f.GetRanges(new Range(range) { minS = RHV });
                    r1.AddRange(r2);
                    return r1;
                }
            }
            if (Operator == ">")
            {
                if (LHV == "x")
                {
                    var r1 = t.GetRanges(new Range(range) { minX = RHV + 1 });
                    var r2 = f.GetRanges(new Range(range) { maxX = RHV });
                    r1.AddRange(r2);
                    return r1;
                }
                if (LHV == "m")
                {
                    var r1 = t.GetRanges(new Range(range) { minM = RHV + 1 });
                    var r2 = f.GetRanges(new Range(range) { maxM = RHV });
                    r1.AddRange(r2);
                    return r1;
                }
                if (LHV == "a")
                {
                    var r1 = t.GetRanges(new Range(range) { minA = RHV + 1 });
                    var r2 = f.GetRanges(new Range(range) { maxA = RHV });
                    r1.AddRange(r2);
                    return r1;
                }
                if (LHV == "s")
                {
                    var r1 = t.GetRanges(new Range(range) { minS = RHV + 1 });
                    var r2 = f.GetRanges(new Range(range) { maxS = RHV });
                    r1.AddRange(r2);
                    return r1;
                }
            }
            throw new Exception();
        }
        public Condition Validate(int x, int m, int a, int s)
        {
            if (Label != null)
            {
                return this;
            }
            var lhv = -1;
            if (LHV == "x") lhv = x;
            if (LHV == "m") lhv = m;
            if (LHV == "a") lhv = a;
            if (LHV == "s") lhv = s;
            if (Operator == "<")
            {
                if (lhv < RHV)
                {
                    return TrueCondition;
                }
                return FalseCondition;
            }
            if (Operator == ">")
            {
                if (lhv > RHV)
                {
                    return TrueCondition;
                }
                return FalseCondition;
            }
            throw new Exception();
        }
    }

    private static Dictionary<string, Condition> dic = new Dictionary<string, Condition>();

    public static void Run()
    {
        using (StreamReader file = new StreamReader("day19/p.in"))
        {
            var cnt = 0;
            string? ln;
            var part = 1;

            while ((ln = file.ReadLine()) != null)
            {
                if (string.IsNullOrEmpty(ln))
                {
                    part = 2;
                    continue;
                }

                if (part == 1)
                {
                    var a = ln.Split("{");
                    var label = a[0];
                    var b = a[1].Substring(0, a[1].Length - 1);
                    dic.Add(label, new Condition(b));
                }
                else if (part == 2)
                {
                    var w = ln.Replace("{", "").Replace("}", "").Trim();
                    var parts = w.Split(",");
                    var x = 0;
                    var m = 0;
                    var a = 0;
                    var s = 0;
                    foreach (var p in parts)
                    {
                        var l = p.Split("=")[0];
                        var r = p.Split("=")[1];
                        if (l == "x") x = int.Parse(r);
                        if (l == "m") m = int.Parse(r);
                        if (l == "a") a = int.Parse(r);
                        if (l == "s") s = int.Parse(r);
                    }

                    var c = dic["in"];
                    while (true)
                    {
                        var n = c.Validate(x, m, a, s);
                        if (n.Label == "A")
                        {
                            cnt += x + m + a + s;
                            break;
                        }
                        if (n.Label == "R")
                        {
                            break;
                        }
                        if (n.Label != null)
                        {
                            c = dic[n.Label];
                        }
                        else
                        {
                            c = n;
                        }
                    }
                }
            }
            Console.WriteLine(cnt);

            var ranges = dic["in"].GetRanges(new Range());
            var uniqueRanges = new HashSet<Range>(new RangeComparer());

            ulong totalCount = 0;
            ulong duplicateCount = 0;

            foreach (var r in ranges)
            {
                int xDiff = r.maxX - r.minX + 1;
                int mDiff = r.maxM - r.minM + 1;
                int aDiff = r.maxA - r.minA + 1;
                int sDiff = r.maxS - r.minS + 1;

                var uniqueRange = new Range
                {
                    minX = r.minX,
                    maxX = r.minX + xDiff - 1,
                    minM = r.minM,
                    maxM = r.minM + mDiff - 1,
                    minA = r.minA,
                    maxA = r.minA + aDiff - 1,
                    minS = r.minS,
                    maxS = r.minS + sDiff - 1
                };

                if (!uniqueRanges.Contains(uniqueRange))
                {
                    uniqueRanges.Add(uniqueRange);
                    totalCount += (ulong)xDiff * (ulong)mDiff * (ulong)aDiff * (ulong)sDiff;
                }
                else
                {
                    duplicateCount += (ulong)xDiff * (ulong)mDiff * (ulong)aDiff * (ulong)sDiff;
                }
            }

            var cnt2 = totalCount - duplicateCount;
            Console.WriteLine(cnt2);

            file.Close();
        }
    }
}

