
public class Day4
{
    static Dictionary<int, List<int>> dic = new Dictionary<int, List<int>>();

    public static int CountValues(int key)
    {
        var sum = 0;
        foreach (var entry in dic[key])
        {
            sum += 1;
            sum += CountValues(entry);
        }
        return sum;
    }

    public static void Run()
    {
        using (StreamReader file = new StreamReader("day04/p.in"))
        {
            string? ln;
            var sum = 0;
            
            while ((ln = file.ReadLine()) != null)
            {
                var words = ln.Split(" ").Where(x => !string.IsNullOrEmpty(x)).ToArray();

                var cardNo = int.Parse(words[1].Split(":")[0]);
                var cardNumbers = new List<int>();
                var playerNumbers = new List<int>();
                var matches = new List<int>();
                var part = 1;
                for (var i = 2; i < words.Length; i++)
                {

                    if (words[i] == "|")
                    {
                        part++;
                    }
                    else
                    {
                        var no = int.Parse(words[i]);
                        if (part == 1)
                        {
                            cardNumbers.Add(no);
                        }
                        else
                        {
                            playerNumbers.Add(no);
                            if (cardNumbers.Contains(no))
                            {
                                matches.Add(no);
                            }
                        }
                    }
                }

                dic.Add(cardNo, new List<int>());
                for (var i = cardNo + 1; i < cardNo+matches.Count + 1; i++)
                {
                    dic[cardNo].Add(i);
                }
            }

            foreach (KeyValuePair<int, List<int>> entry in dic)
            {
                sum += 1;
                sum += CountValues(entry.Key);
            }

            Console.WriteLine(sum);
            file.Close();
        }
    }
}

