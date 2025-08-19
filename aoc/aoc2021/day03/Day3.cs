public class Day3
{
    public static void Run()
    {
        using (StreamReader file = new StreamReader("day03/p.in"))
        {
            string? ln;
            List<List<int>> numbers = null;
            var first = true;
            while ((ln = file.ReadLine()) != null)
            {
                if (first)
                {
                    first = false;
                    numbers = new List<List<int>>();
                    for (var i = 0; i < ln.Length; i++)
                    {
                        numbers.Add(new List<int>());
                    }
                }
                for (var i = 0; i < ln.Length; i++)
                {
                    var bit = ln[i] - '0';
                    numbers[i].Add(bit);
                }
            }
            file.Close();

            var keep = new List<string>();
            var active = new List<bool>();

            var keep2 = new List<string>();
            var active2 = new List<bool>();

            var found = "";
            var found2 = "";

            for (var i = 0; i < numbers.Count; i++)
            {
                if (i == 0)
                {
                    for (var j = 0; j < numbers[i].Count; j++)
                    {
                        keep.Add("");
                        active.Add(true);
                        keep2.Add("");
                        active2.Add(true);
                    }
                }
                for (var j = 0; j < numbers[i].Count; j++)
                {
                    keep[j] += numbers[i][j];
                    keep2[j] += numbers[i][j];
                }
            }

            for (var i = 0; i < numbers.Count; i++)
            {
                var ones = 0;
                var zeros = 0;
                var ones2 = 0;
                var zeros2 = 0;
                for (var j = 0; j < numbers[i].Count; j++)
                {
                    if (active[j])
                    {
                        var bit = numbers[i][j];
                        if (bit == 0)
                        {
                            zeros++;
                        }
                        else if (bit == 1)
                        {
                            ones++;
                        }
                    }
                    if (active2[j])
                    {
                        var bit = numbers[i][j];
                        if (bit == 0)
                        {
                            zeros2++;
                        }
                        else if (bit == 1)
                        {
                            ones2++;
                        }
                    }
                }
                var mostCommon = 1;
                if (ones > zeros)
                {
                    mostCommon = 1;
                }
                else if (zeros > ones)
                {
                    mostCommon = 0;
                }

                var leastCommon = 0;
                if (ones2 > zeros2)
                {
                    leastCommon = 0;
                }
                else if (zeros2 > ones2)
                {
                    leastCommon = 1;
                }

                for (var j = 0; j < numbers[i].Count; j++)
                {
                    var bit = numbers[i][j];
                    if (bit != mostCommon)
                    {
                        if (active[j])
                        {
                            active[j] = false;
                        }
                    }
                    if (bit != leastCommon)
                    {
                        if (active2[j])
                        {
                            active2[j] = false;
                        }
                    }
                }

                var cnt = 0;
                var cnt2 = 0;
                
                for (var j = 0; j < keep.Count; j++)
                {
                    if (active[j])
                    {
                        cnt++;
                        //Console.WriteLine("keep: " + keep[j]);
                        found = keep[j];
                    }
                    if (active2[j])
                    {
                        cnt2++;
                        //Console.WriteLine("keep2: " + keep2[j]);
                        found2 = keep2[j];
                    }
                }
                //Console.WriteLine();
                if (cnt == 1)
                {
                    Console.WriteLine("Found: " + found);
                }
                if (cnt2 == 1)
                {
                    Console.WriteLine("Found2: " + found2);
                }
            }

            int gammaDec = Convert.ToInt32(found, 2);
            int epsilonDec = Convert.ToInt32(found2, 2);
            Console.WriteLine(gammaDec);
            Console.WriteLine(epsilonDec);
            Console.WriteLine(gammaDec * epsilonDec);
        }
    }
}

