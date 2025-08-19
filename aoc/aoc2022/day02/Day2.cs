using System.Threading.Tasks.Sources;

public class Day2
{
    public static void Run()
    {
        using (StreamReader file = new StreamReader("day02/p.in"))
        {
            string ln;
            var score = 0;
            while ((ln = file.ReadLine()) != null)
            {
                var words = ln.Split(" ");
                var p1 = words[0];
                var p2 = words[1];

                if (p1 == "A") // rock
                {
                    if (p2 == "X") // s
                    {
                        score += 3;
                        score += 0;
                    }
                    if (p2 == "Y") // r
                    {
                        score += 1;
                        score += 3;
                    }
                    if (p2 == "Z") // p
                    {
                        score += 2;
                        score += 6;
                    }
                }
                if (p1 == "B") // paper
                {
                    if (p2 == "X") // r
                    {
                        score += 1;
                        score += 0;
                    }
                    if (p2 == "Y") // p
                    {
                        score += 2;
                        score += 3;
                    }
                    if (p2 == "Z") // s
                    {
                        score += 3;
                        score += 6;
                    }
                }
                if (p1 == "C") // scissors
                {
                    if (p2 == "X") // p
                    {
                        score += 2;
                        score += 0;
                    }
                    if (p2 == "Y") // s
                    {
                        score += 3;
                        score += 3;
                    }
                    if (p2 == "Z") // r
                    {
                        score += 1;
                        score += 6;
                    }
                }
            }

            Console.WriteLine(score);
            file.Close();
        }
    }
}

