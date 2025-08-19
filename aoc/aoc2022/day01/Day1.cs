public class Day1
{
    public static void Run()
    {
        using (StreamReader file = new StreamReader("day01/p.in"))
        {
            string ln;
            var elves = new List<int>();
            var elve = 0;
            while ((ln = file.ReadLine()) != null)
            {
                if (ln != "")
                {
                    elve += int.Parse(ln);
                }
                else
                {
                    elves.Add(elve);
                    elve = 0;
                }
            }
            elves.Add(elve);
            elves.Sort();
            var cnt = elves.Count();
            var topElves = elves[cnt - 1] + elves[cnt - 2] + elves[cnt - 3];
            Console.WriteLine(topElves);

            file.Close();
        }
    }
}

