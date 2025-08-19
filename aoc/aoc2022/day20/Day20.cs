public class Day20
{
    public class No
    {
        public long Value { get; set; }
        public int RotIdx { get; set; }
        public long ActualValue { get; set; }

        public override string ToString()
        {
            return ActualValue.ToString();
        }
    }
    public static void Run()
    {
        using (StreamReader file = new StreamReader("day20/p.in"))
        {
            var l = new List<No>();
            var rot = new List<No>();
            string ln;
            var index = 0;
            while ((ln = file.ReadLine()) != null)
            {
                // 4
                // 4153
                var no = new No { Value = long.Parse(ln), RotIdx = index, ActualValue = long.Parse(ln) * 811589153 };
                if (no.Value < 0)
                {
                    no.Value = -((no.ActualValue) % 4999);
                }
                if (no.Value > 0)
                {
                    no.Value = ((no.ActualValue) % 4999);
                }
                index++;
                l.Add(no);
                rot.Add(no);
            }

            var len = l.Count();
            var mixings = 10;

            Console.WriteLine(string.Join(',', rot));

            for (var m = 0; m < mixings; m++)
            {
                for (var i = 0; i < l.Count; i++)
                {
                    var no = l[i];
                    var idx = l[i].RotIdx;
                    var move = no.Value;

                    //Console.WriteLine(no + " " + move + " " + idx);
                    while (move != 0)
                    {
                        if (move > 0)
                        {
                            var a = rot[idx];
                            var b = idx == len - 1 ? rot[0] : rot[idx + 1];

                            //Console.WriteLine("swap " + a + " and " + b);
                            //Console.WriteLine(a + " has rotidx " + a.RotIdx);
                            //Console.WriteLine(b + " has rotidx " + b.RotIdx);
                            var tmp = a;

                            rot[idx] = b;
                            if (idx == len - 1)
                            {
                                rot[0] = tmp;
                            }
                            else
                            {
                                rot[idx + 1] = tmp;
                            }
                            var tmpIdx = a.RotIdx;
                            a.RotIdx = b.RotIdx;
                            b.RotIdx = tmpIdx;
                            //Console.WriteLine(a + " has rotidx " + a.RotIdx);
                            //Console.WriteLine(b + " has rotidx " + b.RotIdx);
                            move--;
                            idx++;
                            if (idx == len) idx = 0;
                        }

                        if (move < 0)
                        {
                            var a = rot[idx];
                            var b = idx == 0 ? rot[len - 1] : rot[idx - 1];

                            //Console.WriteLine("swap " + a + " and " + b);

                            var tmp = a;

                            rot[idx] = b;
                            if (idx == 0)
                            {
                                rot[len - 1] = tmp;
                            }
                            else
                            {
                                rot[idx - 1] = tmp;
                            }
                            var tmpIdx = a.RotIdx;
                            a.RotIdx = b.RotIdx;
                            b.RotIdx = tmpIdx;
                            move++;
                            idx--;
                            if (idx == -1) idx = len - 1;
                        }
                        //for (var j = 0; j < len; j++)
                        //{
                        //    Console.Write(l[j].RotIdx + " ");
                        //}
                        //Console.WriteLine();
                    }

                    
                }
                Console.WriteLine("Round " + (m + 1));
                Console.WriteLine(string.Join(',', rot));
            }
            Console.WriteLine();

            file.Close();

            var pos = -1;
            for (var i = 0; i < rot.Count; i++)
            {
                if (rot[i].Value == 0)
                {
                    pos = i;
                }
            }

            var id = 0;
            long sum = 0;
            for (var i = 0; i < 3005; i++)
            {
                if (i == 1000)
                {
                    Console.WriteLine(rot[pos].ActualValue);
                    sum += rot[pos].ActualValue;
                }
                if (i == 2000)
                {
                    Console.WriteLine(rot[pos].ActualValue);
                    sum += rot[pos].ActualValue;
                }
                if (i == 3000)
                {
                    Console.WriteLine(rot[pos].ActualValue);
                    sum += rot[pos].ActualValue;
                }
                pos++;
                if (pos == len)
                {
                    pos = 0;
                }
            }
            Console.WriteLine("sum: " + sum);
        }
    }
}

