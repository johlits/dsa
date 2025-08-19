public class Day4
{
    public static void Run()
    {
        using (StreamReader file = new StreamReader("day04/p.in"))
        {
            var cnt = 0;
            string ln;
            while ((ln = file.ReadLine()) != null)
            {
                string[] words = ln.Split(',');
                var p1f = int.Parse(words[0].Split('-')[0]);
                var p1t = int.Parse(words[0].Split('-')[1]);
                var p2f = int.Parse(words[1].Split('-')[0]);
                var p2t = int.Parse(words[1].Split('-')[1]);

                if (p1f >= p2f && p1f <= p2t)
                {
                    cnt++;
                }
                else if (p1t >= p2f && p1t <= p2t)
                {
                    cnt++;
                }
                else if (p1f < p2f && p1t > p2t)
                {
                    cnt++;
                }
            }

            file.Close();
            Console.WriteLine(cnt);
        }
    }
}

