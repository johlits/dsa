public class Day2
{
    public static void Run()
    {
        using (StreamReader file = new StreamReader("day02/p.in"))
        {
            string? ln;
            int x = 0;
            int y = 0;
            int aim = 0;
            while ((ln = file.ReadLine()) != null)
            {
                var parts = ln.Split(" ");
                var direction = parts[0];
                var amount = int.Parse(parts[1]);
                if (direction == "forward")
                {
                    x += amount;
                    y += aim * amount;
                }
                if (direction == "down")
                {
                    aim += amount;
                }
                if (direction == "up")
                {
                    aim -= amount;
                }
            }
            file.Close();

            Console.WriteLine(x * y);
        }
    }
}

