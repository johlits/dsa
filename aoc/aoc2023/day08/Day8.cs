using System.Linq;
using System.Threading.Tasks.Sources;

public class Day8
{
    public static void Run()
    {
        using (StreamReader file = new StreamReader("day08/p.in"))
        {
            string? ln;
            var path = "";
            var lnno = 0;

            var dic = new Dictionary<string, Tuple<string, string>>();
            var starts = new List<string>();

            while ((ln = file.ReadLine()) != null)
            {
                if (lnno == 0)
                {
                    path = ln;
                }
                else if (lnno > 1)
                {
                    var parts = ln.Split(" ");
                    var key = parts[0];
                    var left = parts[2].Substring(1, parts[2].Length - 2);
                    var right = parts[3].Substring(0, parts[3].Length - 1);
                    dic.Add(key, Tuple.Create(left, right));
                    if (key[2] == 'A')
                    {
                        starts.Add(key);
                    }
                    
                }
                lnno++;
            }

            var fin = false;
            var idx = 0;
            ulong cnt = 0;
            var wheels = new List<int>[starts.Count];
            for (var i = 0; i < starts.Count; i++)
            {
                wheels[i] = new List<int>();
            }

            while (!fin)
            {
                if (path[idx] == 'L')
                {
                    for (var i = 0; i < starts.Count; i++)
                    {
                        starts[i] = dic[starts[i]].Item1;
                    }
                }
                else if (path[idx] == 'R')
                {
                    for (var i = 0; i < starts.Count; i++)
                    {
                        starts[i] = dic[starts[i]].Item2;
                    }
                }
                fin = true;
                for (var i = 0; i < starts.Count; i++)
                {
                    if (starts[i][2] != 'Z')
                    {
                        fin = false;
                        break;
                    }
                    else
                    {
                        //if (wheels[i].Count < 5)
                        //{
                        //    wheels[i].Add(idx);
                        //    Console.WriteLine(i + ": " + string.Join(", ", wheels[i]));
                        //}
                    }
                }
                idx++;
                if (idx >= path.Length)
                {
                    idx = 0;
                }
                cnt++;
            }

            Console.WriteLine(cnt);
            /*
0: 13938, 27877, 41816, 55755, 69694
1: 11308, 22617, 33926, 45235, 56544
2: 20776, 41553, 62330, 83107, 103884
3: 15516, 31033, 46550, 62067, 77584
4: 17620, 35241, 52862, 70483, 88104
5: 18672, 37345, 56018, 74691, 93364

LCM(13939, 11309, 20777, 15517, 17621, 18673) = 13289612809129
            */

            file.Close();
        }
    }
}

