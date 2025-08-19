public class Day17
{
    public static void Run()
    {
        using (StreamReader file = new StreamReader("day17/p.in"))
        {
            int w = 141;
            int h = 141;
            string? ln;
            int[,] map = new int[w, h];
            List<Tuple<char, int>>[,] visited = new List<Tuple<char, int>>[w, h];
            var r = 0;
            while ((ln = file.ReadLine()) != null)
            {
                for (var i = 0; i < ln.Length; i++)
                {
                    map[i, r] = ln[i] - '0';
                    visited[i, r] = new List<Tuple<char, int>>();
                }
                r++;
            }

            var cmax = 9;
            var cmin = 4;

            var q = new PriorityQueue<Tuple<int, int, int, int, char>, int>();
            q.Enqueue(new Tuple<int, int, int, int, char>(0, 0, 0, cmin, ' '), 0);
            while (q.Count > 0)
            {
                var e = q.Dequeue();
                var x = e.Item1;
                var y = e.Item2;
                var heat_loss = e.Item3;
                var combo_tmp = e.Item4;
                var combo_dir = e.Item5;

                if (x == w - 1 && y == h - 1)
                {
                    Console.WriteLine(heat_loss);
                }

                if (x > 0 && !(combo_dir != 'l' && combo_tmp < cmin))
                {
                    var combo = combo_dir == 'l' ? combo_tmp + 1 : 1;
                    
                    if (!visited[x - 1, y].Contains(new Tuple<char, int>(combo_dir, combo)) && !(combo_dir == 'l' && combo_tmp > cmax))
                    {
                        visited[x - 1, y].Add(new Tuple<char, int>(combo_dir, combo));
                        q.Enqueue(new Tuple<int, int, int, int, char>(x - 1, y, heat_loss + map[x - 1, y], combo, 'l'), heat_loss + map[x - 1, y]);
                    }
                }
                if (x < w - 1 && !(combo_dir != 'r' && combo_tmp < cmin))
                {
                    var combo = combo_dir == 'r' ? combo_tmp + 1 : 1;
                    if (!visited[x + 1, y].Contains(new Tuple<char, int>(combo_dir, combo)) && !(combo_dir == 'r' && combo_tmp > cmax))
                    {
                        visited[x + 1, y].Add(new Tuple<char, int>(combo_dir, combo));
                        q.Enqueue(new Tuple<int, int, int, int, char>(x + 1, y, heat_loss + map[x + 1, y], combo, 'r'), heat_loss + map[x + 1, y]);
                    }
                }
                if (y > 0 && !(combo_dir != 'u' && combo_tmp < cmin))
                {
                    var combo = combo_dir == 'u' ? combo_tmp + 1 : 1;
                    if (!visited[x, y - 1].Contains(new Tuple<char, int>(combo_dir, combo)) && !(combo_dir == 'u' && combo_tmp > cmax))
                    {
                        visited[x, y - 1].Add(new Tuple<char, int>(combo_dir, combo));
                        q.Enqueue(new Tuple<int, int, int, int, char>(x, y - 1, heat_loss + map[x, y - 1], combo, 'u'), heat_loss + map[x, y - 1]);
                    }
                }
                if (y < h - 1 && !(combo_dir != 'd' && combo_tmp < cmin))
                {
                    var combo = combo_dir == 'd' ? combo_tmp + 1 : 1;
                    if (!visited[x, y + 1].Contains(new Tuple<char, int>(combo_dir, combo)) && !(combo_dir == 'd' && combo_tmp > cmax))
                    {
                        visited[x, y + 1].Add(new Tuple<char, int>(combo_dir, combo));
                        q.Enqueue(new Tuple<int, int, int, int, char>(x, y + 1, heat_loss + map[x, y + 1], combo, 'd'), heat_loss + map[x, y + 1]);
                    }
                }
            }

            file.Close();
        }
    }
}

