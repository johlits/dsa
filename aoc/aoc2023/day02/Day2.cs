using System.Runtime.ExceptionServices;

public class Day2
{
    public static void Run()
    {
        using (StreamReader file = new StreamReader("day02/p.in"))
        {
            string? ln;
            var cnt = 0;

            while ((ln = file.ReadLine()) != null)
            {
                var words = ln.Split(" ");
                var blue = 0;
                var red = 0;
                var green = 0;
                var maxBlue = -1;
                var maxRed = -1;
                var maxGreen = -1;
                var p = 0;
                var no = -1;
                for (var i = 1; i < words.Length; i++)
                {
                    if (i == 1)
                    {
                        continue;
                    }
                    if (p == 0)
                    {
                        no = int.Parse(words[i]);
                        p = 1;
                    }
                    else if (p == 1)
                    {
                        if (words[i].StartsWith("blue"))
                        {
                            blue = no;
                        }
                        if (words[i].StartsWith("red"))
                        {
                            red = no;
                        }
                        if (words[i].StartsWith("green"))
                        {
                            green = no;
                        }
                        if (words[i].EndsWith(";"))
                        {
                            maxBlue = Math.Max(maxBlue, blue);
                            maxRed = Math.Max(maxRed, red);
                            maxGreen = Math.Max(maxGreen, green);
                            blue = 0;
                            red = 0;
                            green = 0;
                        }
                        p = 0;
                    }
                }
                maxBlue = Math.Max(maxBlue, blue);
                maxRed = Math.Max(maxRed, red);
                maxGreen = Math.Max(maxGreen, green);

                Console.WriteLine(maxBlue*maxRed*maxGreen);
                cnt += maxBlue * maxRed * maxGreen;
            }

            Console.WriteLine(cnt);
            file.Close();
        }
    }
}

