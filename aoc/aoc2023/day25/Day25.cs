
public class Day25
{
    private static int Cuts(List<List<string>> sets, List<Tuple<string, string>> edges)
    {
        var cuts = 0;
        for (var i = 0; i < edges.Count; i++)
        {
            var subset1 = sets.Where(s => s.Contains(edges[i].Item1)).First();
            var subset2 = sets.Where(s => s.Contains(edges[i].Item2)).First();

            if (subset1 != subset2)
            {
                cuts++;
            }
        }

        return cuts;
    }

    public static void Run()
    {
        using (StreamReader file = new StreamReader("day25/p.in"))
        {
            List<Tuple<string, string>> edges = new List<Tuple<string, string>>();
            List<string> nodes = new List<string>();

            List<string> input = new List<string>();

            string? ln;
            while ((ln = file.ReadLine()) != null)
            {
                var split = ln.Split(": ");
                string from = split[0];
                split = split[1].Split(' ');
                HashSet<string> to = new HashSet<string>(split);

                if (!nodes.Contains(from))
                {
                    nodes.Add(from);
                }

                foreach (var c in to)
                {
                    if (!nodes.Contains(c))
                    {
                        nodes.Add(c);
                    }

                    if (!edges.Contains(new Tuple<string, string>(from, c))
                        && !edges.Contains(new Tuple<string, string>(c, from)))
                    {
                        edges.Add(new Tuple<string, string>(from, c));
                    }
                }
            }

            List<List<string>> sets = new List<List<string>>();

            do
            {
                sets = new List<List<string>>();

                foreach (var node in nodes)
                {
                    sets.Add(new List<string>() { node });
                }

                while (sets.Count > 2)
                {
                    var i = new Random().Next() % edges.Count;

                    var s1 = sets.Where(s => s.Contains(edges[i].Item1)).First();
                    var s2 = sets.Where(s => s.Contains(edges[i].Item2)).First();

                    if (s1 == s2)
                    {
                        continue;
                    }

                    sets.Remove(s2);
                    s1.AddRange(s2);
                }
            } while (Cuts(sets, edges) != 3);

            int product = 1;
            foreach (var subset in sets)
            {
                product *= subset.Count;
            }

            Console.WriteLine(product);

            file.Close();
        }
    }
}

