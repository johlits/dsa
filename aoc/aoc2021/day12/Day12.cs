using Helper;

public class Day12
{
    public static void Run()
    {
        var dic = new Dictionary<string, List<string>>();
        var pairs = new ListOfStrings();
        var bps = new List<(Blueprint, int)>
        {
            (pairs, -1),
        };
        new Parser("p.in", bps, new Symbols()
        {
            Delimiter = "-"
        });

        foreach (var pair in pairs.lists)
        {
            var a = pair.list[0];
            var b = pair.list[1];

            if (dic.ContainsKey(a))
            {
                if (!dic[a].Contains(b))
                {
                    dic[a].Add(b);
                }
            }
            else
            {
                dic.Add(a, new List<string>() { b });
            }

            if (dic.ContainsKey(b))
            {
                if (!dic[b].Contains(a))
                {
                    dic[b].Add(a);
                }
            }
            else
            {
                dic.Add(b, new List<string>() { a });
            }
        }

        var q = new Queue<(string, HashSet<string>)>();
        q.Enqueue(("start", new HashSet<string>()));
        var cnt = 0;

        while (q.Any())
        {
            var current = q.Dequeue();
            var label = current.Item1;
            var visited = current.Item2;
            if (label == "end")
            {
                cnt++;
                continue;
            }
            if (label[0] < 'A' || label[0] > 'Z')
            {
                if (visited.Contains(label) && visited.Contains("adventofcode21"))
                {
                    continue;
                }
                else if (visited.Contains(label))
                {
                    visited.Add("adventofcode21");
                }
                else
                {
                    visited.Add(label);
                }
                
            }
            foreach (var adj in dic[label])
            {
                if (adj != "start")
                {
                    q.Enqueue((adj, new HashSet<string>(visited)));
                }
            }
        }

        Console.WriteLine(cnt);
    }
}

