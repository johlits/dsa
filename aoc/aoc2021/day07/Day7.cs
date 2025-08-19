using Helper;
using System.Reflection.Metadata;

public class Day7
{
    private static Dictionary<(int, int), long> dic = new Dictionary<(int, int), long>();
    public static long GetCost(int a, int b)
    {
        if (a > b)
        {
            return GetCost(b, a);
        }
        if (dic.ContainsKey((a, b)))
        {
            return dic[(a, b)];
        }
        else
        {
            long cost = 0;
            long cnt = 0;
            for (var i = a + 1; i <= b; i++)
            {
                cost += ++cnt;
                if (!dic.ContainsKey((a, i)))
                {
                    dic.Add((a, i), cost);
                }
            }
            return cost;
        }

    }
    public static void Run()
    {
        var numbers = new ListOfIntegers();
        var bps = new List<(Blueprint, int)>
        {
            (numbers, -1),
        };
        new Parser("p.in", bps, new Symbols()
        {

        });

        var crabs = numbers.lists.First().list;
        long best = long.MaxValue;
        for (var i = 0; i < 1000; i++)
        {
            long cost = 0;
            for (var j = 0; j < crabs.Count; j++)
            {
                cost += GetCost(crabs[j], i);
            }
            if (cost < best)
            {
                best = cost;
                Console.WriteLine(i + ": " + best);
            }
        }
    }
}

