public class Day3
{
    public static void Run()
    {
        using (StreamReader file = new StreamReader("day03/p.in"))
        {
            var prios = 0;
            var dic = new Dictionary<char, int>();
            var index = 0;

            string ln;
            while ((ln = file.ReadLine()) != null)
            {
                for (var i = 0; i < ln.Length; i++)
                {
                    if (dic.ContainsKey(ln[i]))
                    {
                        if (dic[ln[i]] == index - 1)
                        {
                            if (index == 2)
                            {
                                if (ln[i] >= 'a' && ln[i] <= 'z') prios += ln[i] - 'a' + 1;
                                if (ln[i] >= 'A' && ln[i] <= 'Z') prios += ln[i] - 'A' + 27;
                                dic.Clear();
                                break;
                            }
                            else
                            {
                                dic[ln[i]] = index;
                            }
                        }
                    }
                    else if (index == 0)
                    {
                        dic[ln[i]] = index;
                    }
                }
                index = (index + 1) % 3;
            }
            file.Close();

            Console.WriteLine(prios);
        }
    }
}

