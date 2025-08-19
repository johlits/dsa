
public class Day18
{
    private static long GetArea(List<Tuple<long, long>> points, long borderLen)
    {
        long area = 0;
        points.Add(points[0]);

        for (var i = 0; i < points.Count - 1; ++i)
        {
            area += (points[i].Item2 + points[i + 1].Item2) * (points[i].Item1 - points[i + 1].Item1);
        }

        area += borderLen;
        area = area / 2 + 1;

        return area;
    }

    public static void Run()
    {
        using (StreamReader file = new StreamReader("day18/p.in"))
        {
            var points = new List<Tuple<long, long>>();
            var current = new Tuple<long, long>(0, 0);
            long borderLen = 0;

            string? ln;
            while ((ln = file.ReadLine()) != null)
            {
                var hexPart = ln.Split(' ')[2];
                var hexLen = hexPart.Substring(2, hexPart.Length - 4);
                var length = Convert.ToInt64(hexLen, 16);
                var op = hexPart.Substring(hexPart.Length - 2, 1);

                borderLen += length;

                if (op == "0")
                {
                    current = new Tuple<long, long>(current.Item1 + length, current.Item2);
                }
                if (op == "1")
                {
                    current = new Tuple<long, long>(current.Item1, current.Item2 + length);
                }
                if (op == "2")
                {
                    current = new Tuple<long, long>(current.Item1 - length, current.Item2);
                }
                if (op == "3")
                {
                    current = new Tuple<long, long>(current.Item1, current.Item2 - length);
                }

                points.Add(current);
            }

            var area = GetArea(points, borderLen);
            Console.WriteLine(area);

            file.Close();
        }
    }
}

