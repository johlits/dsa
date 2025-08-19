public class Day13
{
    private class Packet
    {
        public int? integer;
        public List<Packet> packets;
        public string text;
        public Packet(int integer)
        {
            this.integer = integer;
        }
        public Packet(List<Packet> packets)
        {
            this.packets = packets;
        }
        public Packet(string line)
        {
            this.text = line;
            packets = new List<Packet>();
            string no = "";
            for (var i = 1; i < line.Length; i++)
            {
                if (line[i] == '[')
                {
                    var d = 1;
                    for (var j = i; j < line.Length - 1; j++)
                    {
                        if (line[j] == '[')
                        {
                            d++;
                        }
                        if (line[j] == ']')
                        {
                            d--;
                        }
                        if (line[j] == ']' && d == 1)
                        {
                            var s = line.Substring(i, j - i + 1);
                            packets.Add(new Packet(s));
                            i = j + 1;
                            break;
                        }
                    }
                }
                if (line[i] == ']')
                {
                    if (no != "")
                    {
                        packets.Add(new Packet(int.Parse(no)));
                        no = "";
                    }
                }
                else if (line[i] == ',')
                {
                    if (no != "")
                    {
                        packets.Add(new Packet(int.Parse(no)));
                        no = "";
                    }
                }
                else
                {
                    no += line[i];
                }
            }
        }
    }

    private static int Compare(int a, int b)
    {
        if (a < b)
        {
            return 1;
        }
        if (a > b)
        {
            return -1;
        }
        return 0;
    }

    private static int Compare(int a, Packet b)
    {
        return Compare(new Packet(new List<Packet> { new Packet(a) } ), b);
    }

    private static int Compare(Packet a, int b)
    {
        return Compare(a, new Packet(new List<Packet> { new Packet(b) }));
    }

    private static int Compare(Packet a, Packet b)
    {
        int? i1 = null;
        int? i2 = null;

        if (a.integer != null)
        {
            i1 = a.integer;
        }
        if (b.integer != null)
        {
            i2 = b.integer;
        }

        if (i1 != null && i2 != null)
        {
            return Compare((int)i1, (int)i2);
        }
        else if (i1 != null)
        {
            return Compare((int)i1, b);
        }
        else if (i2 != null)
        {
            return Compare(a, (int)i2);
        }

        Packet p1;
        Packet p2;

        for (var i = 0; i < Math.Max(a.packets.Count, b.packets.Count); i++)
        {
            if (i >= a.packets.Count)
            {
                // left side ran out
                return 1;
            }
            else
            {
                p1 = a.packets[i];
            }

            if (i >= b.packets.Count)
            {
                // right side ran out
                return -1;
            }
            else
            {
                p2 = b.packets[i];
            }

            var cmp = Compare(p1, p2);

            if (cmp == 1)
            {
                return 1;
            }
            else if (cmp == -1)
            {
                return -1;
            }
        }
        return 0;
    }

    public static void Run()
    {
        var packets = new List<Packet>();
        using (StreamReader file = new StreamReader("day13/p.in"))
        {
            string ln;
            string[] lines = new string[2];
            var index = 1;
            var soi = 0;
            while ((ln = file.ReadLine()) != null)
            {
                if (lines[0] == null)
                {
                    lines[0] = ln;
                }
                else if (lines[1] == null)
                {
                    lines[1] = ln;
                    var p1 = new Packet(lines[0]);
                    var p2 = new Packet(lines[1]);
                    packets.Add(p1);
                    packets.Add(p2);
                    if (Compare(p1, p2) == 1)
                    {
                        soi += index;
                    }
                    index++;
                }
                else
                {
                    lines[0] = null;
                    lines[1] = null;
                }
            }

            file.Close();
            Console.WriteLine(soi);
        }

        packets.Add(new Packet("[[2]]"));
        packets.Add(new Packet("[[6]]"));
        packets.Sort(delegate (Packet c1, Packet c2) { return -Compare(c1, c2); });
        var two = -1;
        var six = -1;
        for (var i = 0; i < packets.Count; i++)
        {
            if (packets[i].text == "[[2]]")
            {
                two = i + 1;
            }
            if (packets[i].text == "[[6]]")
            {
                six = i + 1;
            }
        }
        Console.WriteLine(two*six);
    }
}

