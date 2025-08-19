public class Day9
{
    private static bool IsConnected(int x, int y, int tx, int ty)
    {
        if (y == ty && Math.Abs(x - tx) > 1)
        {
            return false;
        }
        if (x == tx && Math.Abs(y - ty) > 1)
        {
            return false;
        }
        var dx = Math.Abs(x - tx);
        var dy = Math.Abs(y - ty);
        if (dx + dy > 2)
        {
            return false;
        }
        return true;
    }

    private class Knot
    {
        public int x;
        public int y;
        public Knot(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

    public static void Run()
    {
        using (StreamReader file = new StreamReader("day09/p.in"))
        {
            string ln;
            var head = new Knot(0, 0);
            var visited = new HashSet<Tuple<int, int>>();
            var cnt = 1;
            visited.Add(new Tuple<int, int>(0, 0));
            var rope = new List<Knot>();
            rope.Add(new Knot(0, 0));
            rope.Add(new Knot(0, 0));
            rope.Add(new Knot(0, 0));
            rope.Add(new Knot(0, 0));
            rope.Add(new Knot(0, 0));
            rope.Add(new Knot(0, 0));
            rope.Add(new Knot(0, 0));
            rope.Add(new Knot(0, 0));
            rope.Add(new Knot(0, 0));

            while ((ln = file.ReadLine()) != null)
            {
                var words = ln.Split(" ");
                var direction = words[0];
                var steps = int.Parse(words[1]);
                for (var i = 0; i < steps; i++)
                {
                    if (direction == "U")
                    {
                        head.y--;
                    }
                    if (direction == "D")
                    {
                        head.y++;
                    }
                    if (direction == "L")
                    {
                        head.x--;
                    }
                    if (direction == "R")
                    {
                        head.x++;
                    }

                    var lastKnot = head;
                    var knotid = 1;
                    foreach (var knot in rope)
                    {
                        if (!IsConnected(lastKnot.x, lastKnot.y, knot.x, knot.y))
                        {
                            var mx = lastKnot.x - knot.x;
                            var my = lastKnot.y - knot.y;
                            if (my < -1) my = -1;
                            if (my > 1) my = 1;
                            if (mx < -1) mx = -1;
                            if (mx > 1) mx = 1;
                            knot.y += my;
                            knot.x += mx;

                            if (knotid == 9 && !visited.Contains(new Tuple<int, int>(knot.x, knot.y)))
                            {
                                visited.Add(new Tuple<int, int>(knot.x, knot.y));
                                cnt++;
                            }
                        }
                        lastKnot = knot;
                        knotid++;
                    }
                }
            }

            file.Close();
            Console.WriteLine(cnt);
        }
    }
}

