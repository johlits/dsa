public class Day17
{
    public static List<Tuple<int, long>> GetBlock(long type, long h) {
        var w = 1;
        var block = new List<Tuple<int, long>>();
        if (type == 0)
        {
            block.Add(new Tuple<int, long>(2 + w, 0 + h));
            block.Add(new Tuple<int, long>(3 + w, 0 + h));
            block.Add(new Tuple<int, long>(4 + w, 0 + h));
            block.Add(new Tuple<int, long>(5 + w, 0 + h));
        }
        if (type == 1)
        {
            block.Add(new Tuple<int, long>(3 + w, 0 + h - 2));
            block.Add(new Tuple<int, long>(2 + w, 1 + h - 2));
            block.Add(new Tuple<int, long>(3 + w, 1 + h - 2));
            block.Add(new Tuple<int, long>(4 + w, 1 + h - 2));
            block.Add(new Tuple<int, long>(3 + w, 2 + h - 2));
        }
        if (type == 2)
        {
            block.Add(new Tuple<int, long>(4 + w, 0 + h-2));
            block.Add(new Tuple<int, long>(4 + w, 1 + h-2));
            block.Add(new Tuple<int, long>(4 + w, 2 + h-2));
            block.Add(new Tuple<int, long>(3 + w, 2 + h-2));
            block.Add(new Tuple<int, long>(2 + w, 2 + h -2));
        }
        if (type == 3)
        {
            block.Add(new Tuple<int, long>(2 + w, 0 + h-3));
            block.Add(new Tuple<int, long>(2 + w, 1 + h-3));
            block.Add(new Tuple<int, long>(2 + w, 2 + h-3));
            block.Add(new Tuple<int, long>(2 + w, 3 + h-3));
        }
        if (type == 4)
        {
            block.Add(new Tuple<int, long>(2 + w, 0 + h-1));
            block.Add(new Tuple<int, long>(3 + w, 0 + h-1));
            block.Add(new Tuple<int, long>(2 + w, 1 + h-1));
            block.Add(new Tuple<int, long>(3 + w, 1 + h-1));
        }
        return block;
    }

    public static string ArrToStr(bool[,] arr, long block, long wind)
    {
        var s = "";
        for (var i = 0; i < 9; i++)
        {
            for (var j = 0; j < 4000; j++)
            {
                s += arr[i, j] ? '1' : '0';
            }
        }
        return s + block + "," + wind;
    }

    public static void Run()
    {
        var h = 4000;
        bool[,] arr = new bool[9, h];
        long[] tops = new long[7];

        for (var i = 0; i < h; i++)
        {
            for (var j = 0; j < 9; j++)
            {
                arr[j, i] = false;
            }
        }

        for (var i = 0; i < h; i++)
        {
            arr[0, i] = true;
            arr[8, i] = true;
        }
        for (var i = 0; i < 9; i++)
        {
            arr[i, h - 1] = true;
            if (i > 0 && i < 8)
            {
                tops[i-1] = h - 1;
            }
        }

        long lo = h;
        var dic = new Dictionary<string, long>();
        var firstOcc = 27; // first occured at
        long firstOccScore = 0;
        long secondOccScore = 0;

        using (StreamReader file = new StreamReader("day17/p.in"))
        {
            string wind = null;
            string ln;
            while ((ln = file.ReadLine()) != null)
            {
                wind = ln;
            }
            var step = 0;
            var truncatedTotal = 0;
            long save = 0;
            

            for (long r = 0; r < 1000000000000; r++)
            {
                var isFalling = true;
                var block = GetBlock(r % 5, lo - 5);
                while (isFalling)
                {
                    var push = wind[(step) % wind.Length];
                    //Console.WriteLine(push);
                    step++;
                    if (push == '<')
                    {
                        var newPositions = new List<Tuple<int, long>>();
                        foreach (var position in block)
                        {
                            newPositions.Add(new Tuple<int, long>(position.Item1 - 1, position.Item2));
                        }
                        var okToMove = true;
                        foreach (var position in newPositions)
                        {
                            if (arr[position.Item1, position.Item2] == true)
                            {
                                okToMove = false;
                            }
                        }
                        if (okToMove)
                        {
                            block = newPositions;
                        }
                    }
                    if (push == '>')
                    {
                        var newPositions = new List<Tuple<int, long>>();
                        foreach (var position in block)
                        {
                            newPositions.Add(new Tuple<int, long>(position.Item1 + 1, position.Item2));
                        }
                        var okToMove = true;
                        foreach (var position in newPositions)
                        {
                            if (arr[position.Item1, position.Item2])
                            {
                                okToMove = false;
                            }
                        }
                        if (okToMove)
                        {
                            block = newPositions;
                        }
                    }


                    var fallPositions = new List<Tuple<int, long>>();
                    foreach (var position in block)
                    {
                        fallPositions.Add(new Tuple<int, long>(position.Item1, position.Item2 + 1));
                    }
                    var okToFall = true;
                    foreach (var position in fallPositions)
                    {
                        if (arr[position.Item1, position.Item2])
                        {
                            okToFall = false;
                        }
                        if (arr[position.Item1, position.Item2])
                        {
                            okToFall = false;
                        }
                    }

                    if (okToFall)
                    {
                        block = fallPositions;
                    }
                    else
                    {

                        


                        foreach (var pos in block)
                        {
                            arr[pos.Item1, pos.Item2] = true;
                            if ((pos.Item2 + 1) < lo)
                            {
                                lo = pos.Item2 + 1;
                                //Console.WriteLine(lo);
                            }

                            if (pos.Item2 < tops[pos.Item1 - 1])
                            {
                                tops[pos.Item1 - 1] = pos.Item2;
                                //if (tops[6] < 3999)
                                //{
                                //    Console.WriteLine("fds");
                                //}
                            }
                            
                            isFalling = false;
                        }

                        //for (var j = lo - 1 - 10; j < 4000; j++)
                        //{
                        //    for (var i = 0; i < 9; i++)
                        //    {
                        //        Console.Write(arr[i, j] ? '#' : ' ');
                        //    }
                        //    Console.WriteLine();
                        //}
                        //Console.WriteLine();

                        var trunc = true;
                        long truncAmount = long.MaxValue;
                        for (var i = 0; i < 7; i++)
                        {
                            if (tops[i] >= h - 1)
                            {
                                trunc = false;
                                break;
                            }
                            else if ((h - 1) - tops[i] < truncAmount)
                            {
                                truncAmount = (h - 1) - tops[i];
                            }
                        }

                        if (trunc)
                        {
                            Console.WriteLine(truncAmount);
                        }
                        while (trunc && truncAmount > 0) 
                        {
                            //Console.WriteLine("truncing");
                            for (var i = 0; i < 7; i++)
                            {
                                for (var j = h - 1; j > 0; j--)
                                {
                                    arr[i + 1, j] = arr[i + 1, j - 1];
                                }
                                tops[i]++;
                            }
                            lo++;
                            truncAmount--;
                            truncatedTotal++;
                        }




                    }

                }
                var fp = ArrToStr(arr, r % 5, (step) % wind.Length);
                if (dic.ContainsKey(fp))
                {
                    Console.WriteLine("Divide: " + (1000000000000 - firstOcc));
                    var by = (r - firstOcc);
                    Console.WriteLine("By: " + by);
                    var rd = ((1000000000000 - firstOcc) / (r - firstOcc));
                    Console.WriteLine("Round down: " + rd);
                    Console.WriteLine("Extras to calculate: " + (1000000000000 - (firstOcc + rd * by)));
                    var perrot = (((h - lo) + truncatedTotal)) - firstOccScore;
                    Console.WriteLine("Per rot " + perrot);
                    Console.WriteLine("step 1 " + firstOccScore);
                    Console.WriteLine("step 2 " + (perrot * rd));
                    Console.WriteLine("step 3 " + secondOccScore);
                    Console.WriteLine("answer: " + (firstOccScore + perrot * rd + secondOccScore - 1));
                    Console.WriteLine("--");
                    Console.WriteLine("footprint at " + r);
                    Console.WriteLine("first occured at: " + dic[fp]);
                    Console.WriteLine("current score: " + ((h - lo) + truncatedTotal));
                    break;
                }
                else
                {
                    dic.Add(fp, r);
                }


                if (r == firstOcc + 10)
                {
                    Console.WriteLine("score at fo + 10: " + ((h - lo) + truncatedTotal));
                }
                if (r == firstOcc) 
                {
                    Console.WriteLine("score at first occ: " + ((h - lo) + truncatedTotal));
                     firstOccScore = ((h - lo) + truncatedTotal);
                }

                if (r == firstOcc + 23) // extras
                {
                    Console.WriteLine("score at 17 + 23: " + ((h - lo) + truncatedTotal - firstOccScore));
                    secondOccScore = ((h - lo) + truncatedTotal - firstOccScore);
                }
                //if (r == 200000)
                //{
                //    Console.WriteLine("Second 100k");
                //    Console.WriteLine(h + " " + lo + " " + truncatedTotal);
                //    Console.WriteLine("best: " + (((h - lo) + truncatedTotal - save) * 10 * 1000000));
                //    break;
                //}
            }

            file.Close();
            //Console.WriteLine(h + " " + lo + " " + truncatedTotal);
            //Console.WriteLine("best: " + ((h - lo) + truncatedTotal) * 10 * 1000000);
        }
    }
}

