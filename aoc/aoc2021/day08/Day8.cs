using Helper;

public class Day8
{
    private static Dictionary<string, int> DeduceDigitConfiguration(string[] patterns)
    {
        var sortedPatterns = patterns.Select(p => String.Concat(p.OrderBy(c => c))).ToList();
        var byLength = sortedPatterns.GroupBy(p => p.Length).ToDictionary(g => g.Key, g => g.ToList());

        var numMap = new Dictionary<int, string>();
        var strMap = new Dictionary<string, int>();

        numMap[1] = byLength[2].First(); 
        numMap[4] = byLength[4].First(); 
        numMap[7] = byLength[3].First(); 
        numMap[8] = byLength[7].First(); 

        foreach (var p in byLength[6]) 
        {
            if (!numMap[1].All(p.Contains)) numMap[6] = p; 
            else if (numMap[4].Count(p.Contains) == 4) numMap[9] = p; 
            else numMap[0] = p; 
        }

        foreach (var p in byLength[5]) 
        {
            if (numMap[1].All(p.Contains)) numMap[3] = p; 
            else if (numMap[6].Count(p.Contains) == 5) numMap[5] = p; 
            else numMap[2] = p; 
        }

        foreach (var pair in numMap) strMap[pair.Value] = pair.Key;

        return strMap;
    }

    public static void Run()
    {
        var words = new ListOfStrings();
        var bps = new List<(Blueprint, int)>
        {
            (words, -1),
        };
        new Parser("p.in", bps, new Symbols()
        {
            Delimiter = " "
        });

        int[] uniqueSegmentCounts = { 2, 4, 3, 7 };

        int totalOutputValueSum = 0;

        foreach (var l1d in words.lists)
        {
            var beforeList = new List<string>();
            var afterList = new List<string>();

            var bar = false;
            for (var i = 0; i < l1d.list.Count; i++)
            {
                if (l1d.list[i] == "|")
                {
                    bar = true;
                    continue;
                }
                if (!bar)
                {
                    beforeList.Add(l1d.list[i]);
                }
                else
                {
                    afterList.Add(l1d.list[i]);
                }
            }

            var digitMap = DeduceDigitConfiguration(beforeList.ToArray());

            string outputValue = "";
            foreach (string output in afterList)
            {
                var sortedOutput = String.Concat(output.OrderBy(c => c));
                outputValue += digitMap[sortedOutput].ToString();
            }

            totalOutputValueSum += int.Parse(outputValue);
        }

        Console.WriteLine(totalOutputValueSum);

    }
}

