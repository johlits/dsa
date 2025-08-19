using Helper;

public class DayX
{
    public static void Run()
    {
        var numbers = new ListOfIntegers();
        var bps = new List<(Blueprint, int)>
        {
            (numbers, -1),
        };
        new Parser("dayXX/p.in", bps, new Symbols()
        {

        });
    }
}

