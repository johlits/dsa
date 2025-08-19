using Helper;
using static System.Runtime.InteropServices.JavaScript.JSType;

public class Day6
{
    public static void Run()
    {
        var numbers = new ListOfIntegers();
        var bps = new List<(Blueprint, int)>
        {
            (numbers, -1),
        };
        new Parser("day06/p.in", bps, new Symbols()
        {

        });

        long[] fish = new long[9];
        foreach (var f in numbers.lists.First().list)
        {
            fish[f]++;
        }

        for (int day = 0; day < 256; day++)
        {
            long newFish = fish[0];
            Array.Copy(fish, 1, fish, 0, fish.Length - 1);
            fish[6] += newFish; 
            fish[8] = newFish; 
        }

        long totalFish = fish.Sum();
        Console.WriteLine(totalFish);
    }
}

