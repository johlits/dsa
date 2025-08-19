
public class Day21
{
    public static int Modulo(int dividend, int divisor)
    {
        if (divisor == 0)
        {
            throw new ArgumentException("Divisor cannot be zero.");
        }

        int remainder = dividend % divisor;
        return remainder < 0 ? remainder + divisor : remainder;
    }

    public static void Run()
    {
        using (StreamReader file = new StreamReader("day21/p.in"))
        {
            string? ln;
            var input = new List<string>();
            Tuple<int, int> start = null;
            var r = 0;
            while ((ln = file.ReadLine()) != null)
            {
                input.Add(ln);
                for (var i = 0; i < ln.Length; i++)
                {
                    if (ln[i] == 'S')
                    {
                        start = new Tuple<int, int>(i, r);
                    }
                }
                r++;
            }

            var size = 131;
            var mod = 26501365 % size;
            var vc = new List<int>();

            for (var i = 0; i < 3; i++)
            {
                var visited = new HashSet<Tuple<int, int>> { start };

                for (var steps = 0; steps < i * size + mod; steps++)
                {
                    HashSet<Tuple<int, int>> next = new HashSet<Tuple<int, int>>();

                    foreach (var coord in visited)
                    {
                        var right = new Tuple<int, int>(coord.Item1 + 1, coord.Item2);
                        if (input[Modulo(right.Item1, size)][Modulo(right.Item2, size)] != '#')
                        {
                            next.Add(right);
                        }
                        var left = new Tuple<int, int>(coord.Item1 - 1, coord.Item2);
                        if (input[Modulo(left.Item1, size)][Modulo(left.Item2, size)] != '#')
                        {
                            next.Add(left);
                        }
                        var up = new Tuple<int, int>(coord.Item1, coord.Item2 - 1);
                        if (input[Modulo(up.Item1, size)][Modulo(up.Item2, size)] != '#')
                        {
                            next.Add(up);
                        }
                        var down = new Tuple<int, int>(coord.Item1, coord.Item2 + 1);
                        if (input[Modulo(down.Item1, size)][Modulo(down.Item2, size)] != '#')
                        {
                            next.Add(down);
                        }
                    }

                    visited = next;
                }

                vc.Add(visited.Count);
            }

            long F(long n) => 
                ((vc[2] - vc[0]) - 2 * (vc[1] - vc[0])) / 2 
                * n * n 
                + (vc[1] - vc[0] - 
                    ((vc[2] - vc[0]) - 2 * (vc[1] - vc[0])) / 2) 
                * n + vc[0];

            Console.WriteLine(F(26501365 / size));
        }
    }
}

