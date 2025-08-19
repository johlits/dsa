
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.X86;
using System.Text.RegularExpressions;

public class Day12
{
    static Dictionary<string, long> cache = new Dictionary<string, long>();

    public static long GetFromCache(string p, List<int> n)
    {
        var key = p + "," + string.Join(',', n);

        if (cache.ContainsKey(key))
        {
            return cache[key]; ;
        }

        var cnt = GetCount(p, n);
        cache[key] = cnt;

        return cnt;
    }

    public static long GetCount(string p, List<int> n)
    {
        while (true)
        {
            if (n.Count == 0)
            {
                return p.Contains('#') ? 0 : 1;
            }
            else if (p == "")
            {
                return 0;
            }
            else if (p.StartsWith('.'))
            {
                p = p.Trim('.');
                continue;
            }
            else if (p.StartsWith('?'))
            {
                return
                    GetFromCache("." + p.Substring(1), n) +
                    GetFromCache("#" + p.Substring(1), n);
            }
            else if (p.StartsWith('#'))
            {
                if (n.Count == 0)
                {
                    return 0;
                }

                if (p.Length < n[0])
                {
                    return 0;
                }

                if (p.Substring(0, n[0]).Contains('.'))
                {
                    return 0;
                }

                if (n.Count > 1)
                {
                    if (p.Length < n[0] + 1 || p[n[0]] == '#')
                    {
                        return 0;
                    }

                    p = p.Substring(n[0] + 1);
                    n = n.Skip(1).ToList();
                    continue;
                }

                p = p.Substring(n[0]);
                n = n.Skip(1).ToList();
                continue;
            }
        }
    }

    public static void Run()
    {
        using (StreamReader file = new StreamReader("day12/p.in"))
        {
            long cnt = 0;
            
            string? ln;
            while ((ln = file.ReadLine()) != null) {

                var line = ln.Split(' ');
                var p = string.Join('?', Enumerable.Repeat(line[0], 5));
                var n = Enumerable.Repeat(line[1].Split(',')
                    .Select(int.Parse).ToList(), 5)
                    .SelectMany(x => x).ToList();

                cache.Clear();
                cnt += GetFromCache(p, n);
            }

            Console.WriteLine(cnt);
            file.Close();
        }
    }
}

