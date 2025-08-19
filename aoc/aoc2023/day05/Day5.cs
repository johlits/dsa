
public class Day5
{
    public static void Run()
    {
        using (StreamReader file = new StreamReader("day05/p.in"))
        {
            string? ln;
            var lineNo = 0;
            var seeds = new List<Tuple<ulong, ulong>>();
            var currentCategory = "";

            List<Tuple<ulong, ulong, ulong>> seedtosoil = new List<Tuple<ulong, ulong, ulong>>();
            List<Tuple<ulong, ulong, ulong>> soiltofertilizer = new List<Tuple<ulong, ulong, ulong>>();
            List<Tuple<ulong, ulong, ulong>> fertilizertowater = new List<Tuple<ulong, ulong, ulong>>();
            List<Tuple<ulong, ulong, ulong>> watertolight = new List<Tuple<ulong, ulong, ulong>>();
            List<Tuple<ulong, ulong, ulong>> lighttotemperature = new List<Tuple<ulong, ulong, ulong>>();
            List<Tuple<ulong, ulong, ulong>> temperaturetohumidity = new List<Tuple<ulong, ulong, ulong>>();
            List<Tuple<ulong, ulong, ulong>> humiditytolocation = new List<Tuple<ulong, ulong, ulong>>();

            while ((ln = file.ReadLine()) != null)
            {
                if (string.IsNullOrEmpty(ln))
                {
                    continue;
                }
                if (lineNo == 0)
                {
                    lineNo = 1;
                    var parts = ln.Split(" ");
                    for (var i = 1; i < parts.Length; i+=2)
                    {
                        seeds.Add(new Tuple<ulong, ulong>(ulong.Parse(parts[i]), ulong.Parse(parts[i]) + ulong.Parse(parts[i + 1])));
                    }
                }
                else
                {
                    if (ln.EndsWith("map:"))
                    {
                        var parts = ln.Split(" ");
                        currentCategory = parts[0];
                    }
                    else
                    {
                        var parts = ln.Split(" ");
                        var dest = ulong.Parse(parts[0]);
                        var src = ulong.Parse(parts[1]);
                        var range = ulong.Parse(parts[2]);
                        List<Tuple<ulong, ulong, ulong>>? dic = null;
                        if (currentCategory == "seed-to-soil") dic = seedtosoil;
                        if (currentCategory == "soil-to-fertilizer") dic = soiltofertilizer;
                        if (currentCategory == "fertilizer-to-water") dic = fertilizertowater;
                        if (currentCategory == "water-to-light") dic = watertolight;
                        if (currentCategory == "light-to-temperature") dic = lighttotemperature;
                        if (currentCategory == "temperature-to-humidity") dic = temperaturetohumidity;
                        if (currentCategory == "humidity-to-location") dic = humiditytolocation;
                        if (dic != null)
                        {
                            dic.Add(new Tuple<ulong, ulong, ulong>(src, src + range, dest - src));
                        }
                    }
                }
            }

            var lo = ulong.MaxValue;
            var sc = seeds.Count;
            for (var i = 0; i < seeds.Count; i++)
            {
                Console.WriteLine(i + " / " + sc);
                var seed = seeds[i];
                for (ulong j = seed.Item1; j < seed.Item2; j++) { 
                    var s7 = GetKey(humiditytolocation, GetKey(temperaturetohumidity, GetKey(lighttotemperature, GetKey(watertolight, GetKey(fertilizertowater, GetKey(soiltofertilizer, GetKey(seedtosoil, j)))))));
                    //Console.WriteLine(s7);
                    if (s7 < lo)
                    {
                        lo = s7;
                    }
                }
            }
            Console.WriteLine();
            Console.WriteLine(lo);

            file.Close();
        }
    }

    public static ulong GetKey(List<Tuple<ulong, ulong, ulong>> list, ulong val)
    {
        foreach (var tuple in list)
        {
            if (val >= tuple.Item1 && val < tuple.Item2)
            {
                return val + tuple.Item3;
            }
        }
        return val;
    }
}

