public class Day18
{
    public static bool CanEscape(Tuple<int, int, int> from)
    {
        var q = new Queue<Tuple<int, int, int>>();
        q.Enqueue(from);
        while (q.Count != 0)
        {
            var c = q.Dequeue();
            var x = c.Item1;
            var y = c.Item2;
            var z = c.Item3;
            if (!visited.Contains(new Tuple<int, int, int>(x - 1, y, z)) && !cubes.Contains(new Tuple<int, int, int>(x - 1, y, z))) { q.Enqueue(new Tuple<int, int, int>(x - 1, y, z)); visited.Add(new Tuple<int, int, int>(x - 1, y, z)); }
            if (!visited.Contains(new Tuple<int, int, int>(x + 1, y, z)) && !cubes.Contains(new Tuple<int, int, int>(x + 1, y, z))) { q.Enqueue(new Tuple<int, int, int>(x + 1, y, z)); visited.Add(new Tuple<int, int, int>(x + 1, y, z)); }
            if (!visited.Contains(new Tuple<int, int, int>(x, y + 1, z)) && !cubes.Contains(new Tuple<int, int, int>(x, y + 1, z))) { q.Enqueue(new Tuple<int, int, int>(x, y + 1, z)); visited.Add(new Tuple<int, int, int>(x, y + 1, z)); }
            if (!visited.Contains(new Tuple<int, int, int>(x, y - 1, z)) && !cubes.Contains(new Tuple<int, int, int>(x, y - 1, z))) { q.Enqueue(new Tuple<int, int, int>(x, y - 1, z)); visited.Add(new Tuple<int, int, int>(x, y - 1, z)); }
            if (!visited.Contains(new Tuple<int, int, int>(x, y, z - 1)) && !cubes.Contains(new Tuple<int, int, int>(x, y, z - 1))) { q.Enqueue(new Tuple<int, int, int>(x, y, z - 1)); visited.Add(new Tuple<int, int, int>(x, y, z - 1)); }
            if (!visited.Contains(new Tuple<int, int, int>(x, y, z + 1)) && !cubes.Contains(new Tuple<int, int, int>(x, y, z + 1))) { q.Enqueue(new Tuple<int, int, int>(x, y, z + 1)); visited.Add(new Tuple<int, int, int>(x, y, z + 1)); }
            if (q.Count > 200)
            {
                return true;
            }
        }
        return false;
    }

    private static List<Tuple<int, int, int>> cubes = new List<Tuple<int, int, int>>();
    private static HashSet<Tuple<int, int, int>> visited = new HashSet<Tuple<int, int, int>>();

    public static void Run()
    {
        
        using (StreamReader file = new StreamReader("day18/p.in"))
        {
            string ln;
            while ((ln = file.ReadLine()) != null)
            {
                var words = ln.Split(',');
                var x = int.Parse(words[0]);
                var y = int.Parse(words[1]);
                var z = int.Parse(words[2]);
                cubes.Add(new Tuple<int, int, int>(x, y, z));
            }

            file.Close();
        }

        var sides = 0;
        var index = 0;
        foreach (var cube in cubes)
        {
            Console.WriteLine(++index + " / " + cubes.Count + " (" + sides + ")");
            var x = cube.Item1;
            var y = cube.Item2;
            var z = cube.Item3;
            visited.Clear();
            if (!cubes.Contains(new Tuple<int, int, int>(x - 1, y, z))) sides += CanEscape(new Tuple<int, int, int>(x - 1, y, z)) ? 1 : 0;
            visited.Clear();
            if (!cubes.Contains(new Tuple<int, int, int>(x + 1, y, z))) sides += CanEscape(new Tuple<int, int, int>(x + 1, y, z)) ? 1 : 0;
            visited.Clear();
            if (!cubes.Contains(new Tuple<int, int, int>(x, y + 1, z))) sides += CanEscape(new Tuple<int, int, int>(x, y + 1, z)) ? 1 : 0;
            visited.Clear();
            if (!cubes.Contains(new Tuple<int, int, int>(x, y - 1, z))) sides += CanEscape(new Tuple<int, int, int>(x, y - 1, z)) ? 1 : 0;
            visited.Clear();
            if (!cubes.Contains(new Tuple<int, int, int>(x, y, z - 1))) sides += CanEscape(new Tuple<int, int, int>(x, y, z - 1)) ? 1 : 0;
            visited.Clear();
            if (!cubes.Contains(new Tuple<int, int, int>(x, y, z + 1))) sides += CanEscape(new Tuple<int, int, int>(x, y, z + 1)) ? 1 : 0;
        }
        Console.WriteLine(sides);
    }
}

