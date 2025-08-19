public class Day12
{
    public static void Run()
    {
        int w = 172;
        int h = 41;
        char[,] map = new char[w, h];
        int[,] visited = new int[w, h];
        Tuple<int, int> start = new Tuple<int, int>(0, 0);
        Tuple<int, int> end = new Tuple<int, int>(0, 0);

        using (StreamReader file = new StreamReader("day12/p.in"))
        {
            string ln;
            var j = 0;
            while ((ln = file.ReadLine()) != null)
            {
                for (var i = 0; i < w; i++)
                {
                    
                    if (ln[i] == 'S')
                    {
                        start = new Tuple<int, int>(i, j);
                        map[i, j] = 'a';
                    }
                    else if (ln[i] == 'E')
                    {
                        end = new Tuple<int, int>(i, j);
                        map[i, j] = 'z';
                    }
                    else
                    {
                        map[i, j] = ln[i];
                    }
                }
                j++;
            }

            file.Close();
        }

        var queue = new Queue<Tuple<int, int>>();
        queue.Enqueue(end);
        visited[end.Item1, end.Item2] = 1;
        var lo = int.MaxValue;

        while(queue.Count > 0)
        {
            var current = queue.Dequeue();
            var cx = current.Item1;
            var cy = current.Item2;

            if (map[cx, cy] == 'a')
            {
                Console.WriteLine(cx + " " + cy);
                Console.WriteLine(visited[cx, cy]);
                if (visited[cx, cy] < lo)
                {
                    lo = visited[cx, cy];
                }
            }

            if (cx > 0 && map[cx - 1, cy] >= map[cx, cy] - 1 && visited[cx - 1, cy] == 0)
            {
                visited[cx - 1, cy] = visited[cx, cy] + 1;
                queue.Enqueue(new Tuple<int, int>(cx - 1, cy));
            }
            if (cy > 0 && map[cx, cy - 1] >= map[cx, cy] - 1 && visited[cx, cy - 1] == 0)
            {
                visited[cx, cy - 1] = visited[cx, cy] + 1;
                queue.Enqueue(new Tuple<int, int>(cx, cy - 1));
            }
            if (cx < w - 1 && map[cx + 1, cy] >= map[cx, cy] - 1 && visited[cx + 1, cy] == 0)
            {
                visited[cx + 1, cy] = visited[cx, cy] + 1;
                queue.Enqueue(new Tuple<int, int>(cx + 1, cy));
            }
            if (cy < h - 1 && map[cx, cy + 1] >= map[cx, cy] - 1 && visited[cx, cy + 1] == 0)
            {
                visited[cx, cy + 1] = visited[cx, cy] + 1;
                queue.Enqueue(new Tuple<int, int>(cx, cy + 1));
            }
        }
        Console.WriteLine("best: " + (lo - 1));
    }
}

