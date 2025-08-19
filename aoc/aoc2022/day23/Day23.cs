public class Day23
{
    private class Elve
    {
        public Tuple<int, int> ProposedNorth { get; set; }
        public Tuple<int, int> ProposedSouth { get; set; }
        public Tuple<int, int> ProposedWest { get; set; }
        public Tuple<int, int> ProposedEast { get; set; }
        public Tuple<int, int> ProposedMove { get; set; }

        public void Clear()
        {
            ProposedNorth = null;
            ProposedSouth = null;
            ProposedWest = null;
            ProposedEast = null;
            ProposedMove = null;
        }
    }
    public static void Run()
    {
        var elves = new Dictionary<Tuple<int, int>, Elve>();

        using (StreamReader file = new StreamReader("day23/p.in"))
        {
            string ln;
            var r = 0;
            while ((ln = file.ReadLine()) != null)
            {
                for (var c = 0; c < ln.Length; c++)
                {
                    if (ln[c] == '#')
                    {
                        elves.Add(new Tuple<int, int>(c, r), new Elve());
                    }
                }
                r++;
            }

            file.Close();
        }

        var proposed = new Dictionary<Tuple<int, int>, int>();
        var consider = 0;
        for (var r = 0; r < int.MaxValue; r++)
        {
            // first half
            foreach (KeyValuePair<Tuple<int, int>, Elve> entry in elves)
            {
                Tuple<int, int> position = entry.Key;
                Elve elve = entry.Value;

                if (elves.ContainsKey(new Tuple<int, int>(position.Item1 - 1, position.Item2 - 1))
                    || elves.ContainsKey(new Tuple<int, int>(position.Item1, position.Item2 - 1))
                    || elves.ContainsKey(new Tuple<int, int>(position.Item1 + 1, position.Item2 - 1))

                    || elves.ContainsKey(new Tuple<int, int>(position.Item1 + 1, position.Item2))
                    || elves.ContainsKey(new Tuple<int, int>(position.Item1 - 1, position.Item2))

                    || elves.ContainsKey(new Tuple<int, int>(position.Item1 - 1, position.Item2 + 1))
                    || elves.ContainsKey(new Tuple<int, int>(position.Item1, position.Item2 + 1))
                    || elves.ContainsKey(new Tuple<int, int>(position.Item1 + 1, position.Item2 + 1)))
                {
                    if (!elves.ContainsKey(new Tuple<int, int>(position.Item1 - 1, position.Item2 - 1))
                    && !elves.ContainsKey(new Tuple<int, int>(position.Item1, position.Item2 - 1))
                    && !elves.ContainsKey(new Tuple<int, int>(position.Item1 + 1, position.Item2 - 1)))
                    {
                        elve.ProposedNorth = new Tuple<int, int>(position.Item1, position.Item2 - 1);
                    }

                    if (!elves.ContainsKey(new Tuple<int, int>(position.Item1 - 1, position.Item2 + 1))
                        && !elves.ContainsKey(new Tuple<int, int>(position.Item1, position.Item2 + 1))
                        && !elves.ContainsKey(new Tuple<int, int>(position.Item1 + 1, position.Item2 + 1)))
                    {
                        elve.ProposedSouth = new Tuple<int, int>(position.Item1, position.Item2 + 1);
                    }

                    if (!elves.ContainsKey(new Tuple<int, int>(position.Item1 - 1, position.Item2 - 1))
                        && !elves.ContainsKey(new Tuple<int, int>(position.Item1 - 1, position.Item2))
                        && !elves.ContainsKey(new Tuple<int, int>(position.Item1 - 1, position.Item2 + 1)))
                    {
                        elve.ProposedWest = new Tuple<int, int>(position.Item1 - 1, position.Item2);
                    }

                    if (!elves.ContainsKey(new Tuple<int, int>(position.Item1 + 1, position.Item2 - 1))
                        && !elves.ContainsKey(new Tuple<int, int>(position.Item1 + 1, position.Item2))
                        && !elves.ContainsKey(new Tuple<int, int>(position.Item1 + 1, position.Item2 + 1)))
                    {
                        elve.ProposedEast = new Tuple<int, int>(position.Item1 + 1, position.Item2);
                    }

                    Tuple<int, int> move = null;
                    if (consider == 0)
                    {
                        if (elve.ProposedNorth != null)
                        {
                            move = elve.ProposedNorth;
                        }
                        else if (elve.ProposedSouth != null)
                        {
                            move = elve.ProposedSouth;
                        }
                        else if (elve.ProposedWest != null)
                        {
                            move = elve.ProposedWest;
                        }
                        else if (elve.ProposedEast != null)
                        {
                            move = elve.ProposedEast;
                        }
                    }
                    else if (consider == 1)
                    {
                        if (elve.ProposedSouth != null)
                        {
                            move = elve.ProposedSouth;
                        }
                        else if (elve.ProposedWest != null)
                        {
                            move = elve.ProposedWest;
                        }
                        else if (elve.ProposedEast != null)
                        {
                            move = elve.ProposedEast;
                        }
                        else if (elve.ProposedNorth != null)
                        {
                            move = elve.ProposedNorth;
                        }
                    }
                    else if (consider == 2)
                    {
                        if (elve.ProposedWest != null)
                        {
                            move = elve.ProposedWest;
                        }
                        else if (elve.ProposedEast != null)
                        {
                            move = elve.ProposedEast;
                        }
                        else if (elve.ProposedNorth != null)
                        {
                            move = elve.ProposedNorth;
                        }
                        else if (elve.ProposedSouth != null)
                        {
                            move = elve.ProposedSouth;
                        }
                    }
                    else if (consider == 3)
                    {
                        if (elve.ProposedEast != null)
                        {
                            move = elve.ProposedEast;
                        }
                        else if (elve.ProposedNorth != null)
                        {
                            move = elve.ProposedNorth;
                        }
                        else if (elve.ProposedSouth != null)
                        {
                            move = elve.ProposedSouth;
                        }
                        else if (elve.ProposedWest != null)
                        {
                            move = elve.ProposedWest;
                        }
                    }

                    if (move != null)
                    {
                        elve.ProposedMove = move;

                        if (!proposed.ContainsKey(move))
                        {
                            proposed.Add(move, 1);
                        }
                        else
                        {
                            proposed[move]++;
                        }
                    }
                }
            }
            consider++;
            if (consider == 4)
            {
                consider = 0;
            }

            // second half
            var newElves = new Dictionary<Tuple<int, int>, Elve>();
            var elfMoved = false;
            foreach (KeyValuePair<Tuple<int, int>, Elve> entry in elves)
            {
                Tuple<int, int> position = entry.Key;
                Elve elve = entry.Value;
                if (elve.ProposedMove != null && proposed[elve.ProposedMove] == 1)
                {
                    newElves.Add(elve.ProposedMove, elve);
                    elfMoved = true;
                }
                else
                {
                    newElves.Add(position, elve);
                }
                elve.Clear();
            }
            proposed.Clear();
            elves = newElves;
            if (!elfMoved)
            {
                Console.WriteLine("No elf moved at round: " + (r + 1));
                break;
            }
        }

        var minx = int.MaxValue;
        var maxx = int.MinValue;
        var miny = int.MaxValue;
        var maxy = int.MinValue;
        foreach (KeyValuePair<Tuple<int, int>, Elve> entry in elves)
        {
            Tuple<int, int> position = entry.Key;
            if (position.Item1 < minx) minx = position.Item1;
            if (position.Item1 > maxx) maxx = position.Item1;
            if (position.Item2 < miny) miny = position.Item2;
            if (position.Item2 > maxy) maxy = position.Item2;
        }
        Console.WriteLine(minx + " " + miny + " to " + maxx + " " + maxy);
        var w = maxx - minx + 1;
        var h = maxy - miny + 1;
        Console.WriteLine("size: " + w + " " + h);
        var area = w * h;
        Console.WriteLine("area: " + area);
        Console.WriteLine("result: " + (area - elves.Count));
    }
}
