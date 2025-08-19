using Helper;

public class Day14
{
    public static void Run()
    {
        var template = new ListOfStrings();
        var inserts = new ListOfStrings()
        {
            Delimiter = " "
        };
        var bps = new List<(Blueprint, int)>
        {
            (template, 1),
            (inserts, 1),
        };
        new Parser("p.in", bps, new Symbols()
        {

        });

        var rules = new Dictionary<string, string>();
        foreach (var insert in inserts.lists)
        {
            rules.Add(insert.list[0], insert.list[2]);
        }

        var polymerTemplate = template.lists.First().list.First();

        var pairCounts = new Dictionary<string, long>();
        for (int i = 0; i < polymerTemplate.Length - 1; i++)
        {
            string pair = polymerTemplate.Substring(i, 2);
            if (!pairCounts.ContainsKey(pair))
                pairCounts[pair] = 0;
            pairCounts[pair]++;
        }

        for (int step = 0; step < 40; step++)
        {
            var newPairCounts = new Dictionary<string, long>();
            foreach (var pair in pairCounts.Keys.ToList())
            {
                if (rules.ContainsKey(pair))
                {
                    string elementToInsert = rules[pair];
                    string newPair1 = pair[0] + elementToInsert;
                    string newPair2 = elementToInsert + pair[1];

                    if (!newPairCounts.ContainsKey(newPair1)) newPairCounts[newPair1] = 0;
                    if (!newPairCounts.ContainsKey(newPair2)) newPairCounts[newPair2] = 0;

                    newPairCounts[newPair1] += pairCounts[pair];
                    newPairCounts[newPair2] += pairCounts[pair];
                }
            }
            pairCounts = newPairCounts; 
        }

        var elementCounts = new Dictionary<char, long>();
        foreach (var pair in pairCounts)
        {
            if (!elementCounts.ContainsKey(pair.Key[0])) elementCounts[pair.Key[0]] = 0;
            elementCounts[pair.Key[0]] += pair.Value;
        }
        elementCounts[polymerTemplate.Last()]++;

        long max = elementCounts.Values.Max();
        long min = elementCounts.Values.Min();

        Console.WriteLine(max - min);
    }
}

