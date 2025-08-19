using System.Runtime.ExceptionServices;

public class Day1
{
    public static void Run()
    {
        using (StreamReader file = new StreamReader("day01/p.in"))
        {
            string? ln;
            var cnt = 0;
            while ((ln = file.ReadLine()) != null)
            {
                string? first = null;
                string? last = null;
                ln = ln.Replace("one", "one1one");
                ln = ln.Replace("two", "two2two");
                ln = ln.Replace("three", "three3three");
                ln = ln.Replace("four", "four4four");
                ln = ln.Replace("five", "five5five");
                ln = ln.Replace("six", "six6six");
                ln = ln.Replace("seven", "seven7seven");
                ln = ln.Replace("eight", "eight8eight");
                ln = ln.Replace("nine", "nine9nine");
                for (var i = 0; i < ln.Length; i++)
                {
                    if (ln[i] >= '0' && ln[i] <= '9')
                    {
                        if (first == null)
                        {
                            first = "" + ln[i];
                        }
                        last = "" + ln[i];
                    }
                }
                cnt += int.Parse("" + first + last);
            }
            Console.WriteLine(cnt);

            file.Close();
        }
    }
}

