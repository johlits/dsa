public class Day6
{
    public static void Run()
    {
        using (StreamReader file = new StreamReader("day06/p.in"))
        {
            var queue = new Queue<char>();
            var hashset = new HashSet<char>();
            string ln;
            while ((ln = file.ReadLine()) != null)
            {
                for (var i = 0; i < ln.Length; i++)
                {
                    queue.Enqueue(ln[i]);
                    if (queue.Count >= 14)
                    {
                        if (queue.Count > 14)
                        {
                            queue.Dequeue();
                            hashset.Clear();
                        }
                        
                        var unique = true;
                        foreach (var c in queue)
                        {
                            if (hashset.Contains(c))
                            {
                                unique = false;
                                continue;
                            }
                            else
                            {
                                hashset.Add(c);
                            }
                        }
                        if (unique)
                        {
                            Console.WriteLine(i + 1);
                            break;
                        }
                    }
                }
            }

            file.Close();
        }
    }
}

