public class Day1
{
    public static void Run()
    {
        var l = new List<int>();
        using (StreamReader file = new StreamReader("day01/p.in"))
        {
            string? ln;
            while ((ln = file.ReadLine()) != null)
            {
                l.Add(int.Parse(ln));
            }

            file.Close();
        }

        var first = true;
        var prev = 0;
        var cnt = 0;
        for (var i = 2; i < l.Count; i++)
        {
            var a = l[i - 2];
            var b = l[i - 1];
            var c = l[i - 0];
            var sum = a + b + c;
            if (first)
            {
                first = false;
                prev = sum;
            }
            else
            {
                if (sum > prev)
                {
                    cnt++;
                }
                prev = sum;
            }
        }
        Console.WriteLine(cnt);
    }
}

