using Microsoft.Z3;
using Helper;

public class Day24
{
    public class Hailstone
    {
        public long X { get; set; }
        public long Y { get; set; }
        public long Z { get; set; }
        public long DX { get; set; }
        public long DY { get; set; }
        public long DZ { get; set; }
    }

    private static bool IsWithinRange((double, double, double)? intersection)
    {
        const long minRange = 200000000000000;
        const long maxRange = 400000000000000;
        //const long minRange = 7;
        //const long maxRange = 27;

        return intersection.Value.Item1 >= minRange && intersection.Value.Item1 <= maxRange &&
               intersection.Value.Item2 >= minRange && intersection.Value.Item2 <= maxRange;
    }

    static long Solve(List<Hailstone> hailstones)
    {
        var context = new Context();
        var solver = context.MkSolver();

        var position = new[] { context.MkIntConst("x"), context.MkIntConst("y"), context.MkIntConst("z") };
        var velocity = new[] { context.MkIntConst("vx"), context.MkIntConst("vy"), context.MkIntConst("vz") };

        for (int i = 0; i < hailstones.Count; i++)
        {
            var time = context.MkIntConst($"t{i}");
            var hailstone = hailstones[i];

            var hailstonePosition = new[] {
            context.MkInt(hailstone.X),
            context.MkInt(hailstone.Y),
            context.MkInt(hailstone.Z)
        };
            var hailstoneVelocity = new[] {
            context.MkInt(hailstone.DX),
            context.MkInt(hailstone.DY),
            context.MkInt(hailstone.DZ)
        };

            for (int dim = 0; dim < 3; dim++)
            {
                var predictedPosition = context.MkAdd(position[dim], context.MkMul(time, velocity[dim]));
                var actualPosition = context.MkAdd(hailstonePosition[dim], context.MkMul(time, hailstoneVelocity[dim]));

                solver.Add(time >= 0);
                solver.Add(context.MkEq(predictedPosition, actualPosition));
            }
        }

        if (solver.Check() == Status.SATISFIABLE)
        {
            var model = solver.Model;
            var result = position.Select(coord => Convert.ToInt64(model.Eval(coord).ToString())).Sum();
            return result;
        }

        throw new Exception("No solution found");
    }

    public static void Run()
    {
        using (StreamReader file = new StreamReader("day24/p.in"))
        {
            var hailstones = new List<Hailstone>();

            string? ln;
            while ((ln = file.ReadLine()) != null)
            {
                string[] parts = ln.Replace(" ", "").Split(new char[] { ',', '@' });

                hailstones.Add(new Hailstone()
                {
                    X = long.Parse(parts[0]),
                    Y = long.Parse(parts[1]),
                    Z = long.Parse(parts[2]),
                    DX = long.Parse(parts[3]),
                    DY = long.Parse(parts[4]),
                    DZ = long.Parse(parts[5])
                });
            }

            var lines = new List<J.Line3D>();
            foreach (var hailstone in hailstones)
            {
                lines.Add(new J.Line3D(
                    new J.Vector3D(hailstone.X, hailstone.Y, 1),
                    new J.Vector3D(hailstone.X + hailstone.DX, hailstone.Y + hailstone.DY, 1)));
            }

            int cnt = 0;
            for (int i = 0; i < lines.Count; i++)
            {
                for (int j = i + 1; j < lines.Count; j++)
                {
                    (double, double, double)? intersection = J.LineIntersection(lines[i], lines[j], true, true);
                    if (intersection != null)
                    {
                        var r = (lines[i].Point2.X - lines[i].Point1.X, lines[i].Point2.Y - lines[i].Point1.Y);
                        var s = (lines[j].Point2.X - lines[j].Point1.X, lines[j].Point2.Y - lines[j].Point1.Y);
                        var q_p = (lines[j].Point1.X - lines[i].Point1.X, lines[j].Point1.Y - lines[i].Point1.Y);
                        var rxs = J.CrossProduct(r, s);

                        var t = J.CrossProduct(q_p, s) / (double)rxs;
                        var u = J.CrossProduct(q_p, r) / (double)rxs;

                        if (t > 0 && u > 0 && IsWithinRange(intersection))
                        {
                            cnt++;
                        }
                    }
                }
            }

            Console.WriteLine(cnt);
            Console.WriteLine(Solve(hailstones));

            file.Close();
        }
    }
}

