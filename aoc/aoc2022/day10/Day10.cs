public class Day10
{
    private class Instruction
    {
        public int ExecutesIn { get; set; }
        public int Amount { get; set; }
    }
    public static void Run()
    {
        var instructions = new List<Instruction>();

        using (StreamReader file = new StreamReader("day10/p.in"))
        {
            string ln;
            while ((ln = file.ReadLine()) != null)
            {
                // cycle starts
                var words = ln.Split(' ');
                if (words[0] == "addx")
                {
                    var v = int.Parse(words[1]);
                    instructions.Add(new Instruction() { ExecutesIn = 2, Amount = v });
                }
                else if (words[0] == "noop")
                {
                    instructions.Add(new Instruction() { ExecutesIn = 1, Amount = 0 });
                }
            }

            file.Close();
        }

        var x = 1;
        var p = 1;
        var index = 0;
        var calculateAt = 40;
        var signalStrength = 0;
        Console.Write("#");
        for (var i = 1; i <= 240; i++)
        {
            if (i == calculateAt)
            {
                Console.WriteLine();
                calculateAt += 40;
            }
            if (index >= instructions.Count)
            {
                break;
            }
            var instuction = instructions[index];
            if (instuction.ExecutesIn == 1)
            {
                x += instuction.Amount;
                index++;
            }
            else
            {
                instuction.ExecutesIn--;
            }
            if (p >= x - 1 && p <= x + 1)
            {
                Console.Write("#");
            }
            else
            {
                Console.Write(".");
            }
            p = (p + 1) % 40;

        }

        Console.WriteLine(signalStrength);
    }
}

