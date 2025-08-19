using System.Diagnostics.CodeAnalysis;

public class Day19
{
    public static void Run()
    {
        using (StreamReader file = new StreamReader("day19/p.in"))
        {
            string ln;
            var index = 1;
            var best = -1;
            var sum = 1;
            while ((ln = file.ReadLine()) != null)
            {
                Console.WriteLine(index);
                best = -1;
                var words = ln.Split(' ');
                var orerobotcost = int.Parse(words[6]); // in ore
                var clayrobotcost = int.Parse(words[12]); // in ore
                var obsidianrobotcost1 = int.Parse(words[18]); // in ore
                var obsidianrobotcost2 = int.Parse(words[21]); // in clay
                var geoderobotcost1 = int.Parse(words[27]); // in ore
                var geoderobotcost2 = int.Parse(words[30]); // in obsidian

                var maxore = orerobotcost + clayrobotcost + obsidianrobotcost1 + geoderobotcost1;
                var maxclay = obsidianrobotcost2;
                var maxobsidian = geoderobotcost2;

                var q = new Queue<Mem>();
                var m = new Mem(1, 0, 0, 0, 0, 0, 0, 0, 1);
                q.Enqueue(m);
                var hs = new HashSet<string>();
                hs.Add(m.ToString());

                while (q.Count != 0)
                {
                    var current = q.Dequeue();

                    var orerobots = current.v1;
                    var clayrobots = current.v2;
                    var obsidianrobots = current.v3;
                    var geoderobots = current.v4;

                    var ores = current.v5;
                    var clays = current.v6;
                    var obsidians = current.v7;
                    var geodes = current.v8;

                    var time = current.v9;


                    if (time > 33)
                    {
                        continue;
                    }

                    if (geodes > best)
                    {
                        best = geodes;
                        //Console.WriteLine(time + ": " + orerobots + " " + clayrobots + " " + obsidianrobots + " " + geoderobots + " - " + ores + " " + clays + " " + obsidians + " " + geodes);
                        //Console.WriteLine(best);
                    }

                    if (geodes < best - 1) 
                    {
                        continue;
                    }

                    //Console.WriteLine(time + ": " + orerobots + " " + clayrobots + " " + obsidianrobots + " " + geoderobots + " - " + ores + " " + clays + " " + obsidians + " " + geodes);


                    for (var a = 0; a < 2; a++)
                    {
                        for (var b = 0; b < 2; b++)
                        {
                            for (var c = 0; c < 2; c++)
                            {
                                for (var d = 0; d < 2; d++)
                                {
                                    var t_geoderobots = geoderobots;
                                    var t_obsidianrobots = obsidianrobots;
                                    var t_clayrobots = clayrobots;
                                    var t_orerobots = orerobots;
                                    var t_geodes = geodes;
                                    var t_obsidians = obsidians;
                                    var t_clays = clays;
                                    var t_ores = ores;

                                    if (a == 1)
                                    {
                                        t_geoderobots++;
                                        t_ores -= geoderobotcost1;
                                        t_obsidians -= geoderobotcost2;
                                    }
                                    else if (b == 1)
                                    {
                                        t_obsidianrobots++;
                                        t_ores -= obsidianrobotcost1;
                                        t_clays -= obsidianrobotcost2;
                                    }
                                    else if (c == 1)
                                    {
                                        t_clayrobots++;
                                        t_ores -= clayrobotcost;
                                    }
                                    else if (d == 1)
                                    {
                                        t_orerobots++;
                                        t_ores -= orerobotcost;
                                    }

                                    if (t_ores < 0 || t_clays < 0 || t_obsidians < 0)
                                    {
                                        continue;
                                    }

                                    //if (t_ores > 300 || t_clays > 300 || t_obsidians > 300)
                                    //{
                                    //    Console.WriteLine("A");
                                    //    continue;
                                    //}

                                    //if (t_orerobots > 30 || t_clayrobots > 30  || t_obsidianrobots > 30)
                                    //{
                                    //    Console.WriteLine("B")
                                    //    continue;
                                    //}


                                    t_ores += orerobots;
                                    t_clays += clayrobots;
                                    t_obsidians += obsidianrobots;
                                    t_geodes += geoderobots;

                                    var temp = new Mem(
                                       t_orerobots, t_clayrobots, t_obsidianrobots, t_geoderobots, t_ores, t_clays, t_obsidians, t_geodes, time + 1);
                                    if (!hs.Contains(temp.ToString()))
                                    {
                                        q.Enqueue(temp);
                                        hs.Add(temp.ToString());
                                    }
                                    
                                }
                            }
                        }
                    }



                }
                Console.WriteLine(index + " " + best + " = " + (index * best));
                sum *= best;
                index++;
                if (index == 4)
                {
                    break;
                }

            }

            file.Close();
            Console.WriteLine(sum);
        }
    }
}

internal class Mem { 

    public int v1;
    public int v2;
    public int v3;
    public int v4;
    public int v5;
    public int v6;
    public int v7;
    public int v8;
    public int v9;

    public Mem(int v1, int v2, int v3, int v4, int v5, int v6, int v7, int v8, int v9)
    {
        this.v1 = v1;
        this.v2 = v2;
        this.v3 = v3;
        this.v4 = v4;
        this.v5 = v5;
        this.v6 = v6;
        this.v7 = v7;
        this.v8 = v8;
        this.v9 = v9;
    }

    public override string ToString()
    {
        return String.Join(',', v1, v2, v3, v4, v5, v6, v7, v8, v9);
    }
}