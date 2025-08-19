using Helper;

public class Day13
{
    static void PrintDots(HashSet<(int x, int y)> dots)
    {
        int maxX = dots.Max(d => d.x) + 1;
        int maxY = dots.Max(d => d.y) + 1;

        for (int y = 0; y < maxY; y++)
        {
            for (int x = 0; x < maxX; x++)
            {
                Console.Write(dots.Contains((x, y)) ? "#" : ".");
            }
            Console.WriteLine();
        }
    }

    public static void Run()
    {
        var numbers = new ListOfIntegers()
        {
            Delimiter = ","
        };
        var texts = new ListOfStrings()
        {
            Delimiter = " "
        };

        var bps = new List<(Blueprint, int)>
        {
            (numbers, -1),
            (texts, -1),
        };

        new Parser("p.in", bps, new Symbols()
        {
        });

        var dots = new HashSet<(int x, int y)>();
        var folds = new List<(char axis, int value)>();

        foreach (var line in numbers.lists)
        {
            dots.Add((line.list[0], line.list[1]));
        }

        foreach (var line in texts.lists)
        {
            var parts = line.list[2].Split('=');
            folds.Add((parts[0][0], int.Parse(parts[1])));
        }

        foreach (var (axis, value) in folds)
        {
            var newDots = new HashSet<(int x, int y)>();
            foreach (var (x, y) in dots)
            {
                if (axis == 'x')
                {
                    newDots.Add(x > value ? (2 * value - x, y) : (x, y));
                }
                else 
                {
                    newDots.Add(y > value ? (x, 2 * value - y) : (x, y));
                }
            }
            dots = newDots; 
        }

        PrintDots(dots);

    }
}

