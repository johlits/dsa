public class Day6
{
    public static void Run()
    {
        using (StreamReader file = new StreamReader("day06/p.in"))
        {
            var times = new List<ulong>();
            var distances = new List<ulong>();
            times.Add(56717999);
            distances.Add(334113513502430);
            //while ((ln = file.ReadLine()) != null)
            //{
            //    var parts = ln.Split(" ");
            //    var col = 0;
            //    for (var i = 0; i < parts.Length; i++)
            //    {
            //        if (!string.IsNullOrEmpty(parts[i]))
            //        {
            //            if (col > 0)
            //            {
            //                if (row == 0)
            //                {
            //                    times.Add(int.Parse(parts[i]));
            //                }
            //                if (row == 1)
            //                {
            //                    distances.Add(int.Parse(parts[i]));
            //                }
            //            }
            //            col++;
            //        }
            //    }
            //    row++;
            //}

            var mult = 1;
            for (var i = 0; i < times.Count; i++)
            {
                var cnt = 0;
                var time = times[i];
                var distance = distances[i];
                for (ulong j = 0; j <= time; j++)
                {
                    var speed = j;
                    var distanceTravelled = speed * (time - j);
                    if (distanceTravelled > distance)
                    {
                        //Console.Write(distanceTravelled + " " + speed + " ");
                        //Console.WriteLine(j);
                        cnt++;
                    }
                }
                mult *= cnt;
                Console.WriteLine(cnt);
                //Console.WriteLine();
            }
            Console.WriteLine(mult);
            

            file.Close();
        }
    }
}

