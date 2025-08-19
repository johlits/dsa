using Helper;

public class Day10
{
    private static (bool isIncomplete, Stack<char> stack) CheckLine(string line)
    {
        Stack<char> stack = new Stack<char>();
        var openingChars = new Dictionary<char, char>
        {
            {')', '('},
            {']', '['},
            {'}', '{'},
            {'>', '<'}
        };
        var closingChars = openingChars.ToDictionary(kp => kp.Value, kp => kp.Key);

        foreach (char c in line)
        {
            if (openingChars.ContainsValue(c))
            {
                stack.Push(c);
            }
            else if (openingChars.ContainsKey(c))
            {
                if (stack.Count == 0 || stack.Pop() != openingChars[c])
                {
                    return (false, null); 
                }
            }
        }

        return (stack.Count > 0, stack);
    }

    private static long ScoreCompletion(Stack<char> stack)
    {
        long score = 0;
        var scoreTable = new Dictionary<char, int>
        {
            {')', 1},
            {']', 2},
            {'}', 3},
            {'>', 4}
        };
        var closingChars = new Dictionary<char, char>
        {
            {'(', ')'},
            {'[', ']'},
            {'{', '}'},
            {'<', '>'}
        };

        while (stack.Count > 0)
        {
            char c = stack.Pop();
            score = score * 5 + scoreTable[closingChars[c]];
        }

        return score;
    }

    public static void Run()
    {
        var lines = new ListOfStrings();
        var bps = new List<(Blueprint, int)>
        {
            (lines, -1),
        };
        new Parser("p.in", bps, new Symbols()
        {

        });

        var incompleteScores = new List<long>();

        foreach (var line in lines.lists)
        {
            var result = CheckLine(line.list.First());
            if (result.isIncomplete)
            {
                incompleteScores.Add(ScoreCompletion(result.stack));
            }
        }

        incompleteScores.Sort();
        long middleScore = incompleteScores[incompleteScores.Count / 2];

        Console.WriteLine("Middle score: " + middleScore);
    }
}

