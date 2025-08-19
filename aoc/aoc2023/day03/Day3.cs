public class Day3
{
    public static void Run()
    {
        using (StreamReader file = new StreamReader("day03/p.in"))
        {
            var w = 140;
            var h = 140;
            char[,] arr = new char[w, h];

            string? ln;
            var q = 0;
            while ((ln = file.ReadLine()) != null)
            {
                for (var j = 0; j < ln.Length; j++)
                {
                    arr[q, j] = ln[j];
                }
                q++;
            }

            
            var currentString = "";
            var validString = true;
            var cnt = 0;
            List<Tuple<int, int>> tempGears = new List<Tuple<int, int>>();
            Dictionary<Tuple<int, int>, List<int>> dic = new Dictionary<Tuple<int, int>, List<int>>();
            for (var i = 0; i < w; i++)
            {
                for (var j = 0; j < h; j++)
                {
                    if (arr[i, j] >= '0' && arr[i, j] <= '9')
                    {
                        
                        currentString += arr[i, j];
                        //Console.WriteLine(currentString);
                        if (i > 0)
                        {
                            if ((arr[i - 1, j] < '0' || arr[i - 1, j] > '9') && arr[i - 1, j] == '*')
                            {
                                tempGears.Add(new Tuple<int, int>(i - 1, j));
                                validString = false;
                            }
                        }
                        if (j > 0)
                        {
                            if ((arr[i, j - 1] < '0' || arr[i, j - 1] > '9') && arr[i, j - 1] == '*')
                            {
                                tempGears.Add(new Tuple<int, int>(i, j - 1));
                                validString = false;
                            }
                        }
                        if (i < w - 1)
                        {
                            if ((arr[i + 1, j] < '0' || arr[i + 1, j] > '9') && arr[i + 1, j] == '*')
                            {
                                tempGears.Add(new Tuple<int, int>(i + 1, j));
                                validString = false;
                            }
                        }
                        if (j < h - 1)
                        {
                            if ((arr[i, j + 1] < '0' || arr[i, j + 1] > '9') && arr[i, j + 1] == '*')
                            {
                                tempGears.Add(new Tuple<int, int>(i, j + 1));
                                validString = false;
                            }
                        }

                        if (i > 0 && j > 0)
                        {
                            if ((arr[i - 1, j - 1] < '0' || arr[i - 1, j - 1] > '9') && arr[i - 1, j - 1] == '*')
                            {
                                tempGears.Add(new Tuple<int, int>(i - 1, j - 1));
                                validString = false;
                            }
                        }
                        if (i < w - 1 && j > 0)
                        {
                            if ((arr[i + 1, j - 1] < '0' || arr[i + 1, j - 1] > '9') && arr[i + 1, j - 1] == '*')
                            {
                                tempGears.Add(new Tuple<int, int>(i + 1, j - 1));
                                validString = false;
                            }
                        }
                        if (i > 0 && j < h - 1)
                        {
                            if ((arr[i - 1, j + 1] < '0' || arr[i - 1, j + 1] > '9') && arr[i - 1, j + 1] == '*')
                            {
                                tempGears.Add(new Tuple<int, int>(i - 1, j + 1));
                                validString = false;
                            }
                        }
                        if (i < w - 1 && j < h - 1)
                        {
                            if ((arr[i + 1, j + 1] < '0' || arr[i + 1, j + 1] > '9') && arr[i + 1, j + 1] == '*')
                            {
                                tempGears.Add(new Tuple<int, int>(i + 1, j + 1));
                                validString = false;
                            }
                        }

                    }
                    else
                    {
                        if (!validString && !string.IsNullOrEmpty(currentString))
                        {
                            Console.WriteLine(int.Parse(currentString));
                            cnt += int.Parse(currentString);
                            foreach(var t in tempGears.Distinct().ToList())
                            {
                                if (dic.ContainsKey(t))
                                {
                                    dic[t].Add(int.Parse(currentString));
                                }
                                else
                                {
                                    dic.Add(t, new List<int> { int.Parse(currentString) });
                                }
                            }
                        }
                        tempGears.Clear();
                        currentString = "";
                        validString = true;
                    }
                    //Console.Write(arr[i, j]);
                }
                if (!validString && !string.IsNullOrEmpty(currentString))
                {
                    Console.WriteLine(int.Parse(currentString));
                    cnt += int.Parse(currentString);
                    foreach (var t in tempGears.Distinct().ToList())
                    {
                        if (dic.ContainsKey(t))
                        {
                            dic[t].Add(int.Parse(currentString));
                        }
                        else
                        {
                            dic.Add(t, new List<int> { int.Parse(currentString) });
                        }
                    }
                }
                currentString = "";
                validString = true;
                tempGears.Clear();
                //Console.WriteLine();
            }
            if (!validString && !string.IsNullOrEmpty(currentString))
            {
                Console.WriteLine(int.Parse(currentString));
                cnt += int.Parse(currentString);
                foreach (var t in tempGears.Distinct().ToList())
                {
                    if (dic.ContainsKey(t))
                    {
                        dic[t].Add(int.Parse(currentString));
                    }
                    else
                    {
                        dic.Add(t, new List<int> { int.Parse(currentString) });
                    }
                }
            }
            currentString = "";
            validString = true;
            tempGears.Clear();

            Console.WriteLine(cnt);
            Console.WriteLine("mult");

            var addup = 0;
            foreach (var entry in dic)
            {
                var mult = 1;
                if (entry.Value.Count > 1)
                {
                    for (var i = 0; i < entry.Value.Count; i++)
                    {
                        Console.Write(entry.Value[i] + " ");
                        mult = mult * entry.Value[i];
                    }
                    Console.WriteLine();
                    Console.WriteLine("result: " + mult);
                    addup += mult;
                }
                
            }
            Console.WriteLine(addup);
            file.Close();
        }
    }
}

