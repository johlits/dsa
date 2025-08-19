public class Day5
{
    public static void Run()
    {
        using (StreamReader file = new StreamReader("day05/p.in"))
        {
            string ln;
            var step = 0;
            var stacks = new List<List<char>>();
            while ((ln = file.ReadLine()) != null)
            {
                if (ln == "" || ln.StartsWith(" 1"))
                {
                    step++;
                    continue;
                }
                if (step == 0)
                {
                    for (var i = 0; i < ln.Length/3; i++)
                    {
                        stacks.Add(new List<char>());
                    }
                    step++;
                }

                if (step == 1)
                {
                    var j = 0;
                    for (var i = 0; i < ln.Length; i += 4)
                    {
                        if (ln[i+1] != ' ')
                        {
                            stacks[j].Add(ln[i + 1]);
                        }
                        j++;
                    }
                }
                else if (step == 3)
                {
                    string[] words = ln.Split(" ");
                    var move = int.Parse(words[1]);
                    var from = int.Parse(words[3]);
                    var to = int.Parse(words[5]);
                    var crates = stacks[from - 1].Take(move);
                    stacks[to - 1].InsertRange(0, crates);
                    stacks[from - 1].RemoveRange(0, move);
                }
            }

            file.Close();

            var str = "";
            foreach (var stack in stacks)
            {
                if (stack.Count > 0)
                {
                    str += stack[0];
                }
            }
            Console.WriteLine(str);
        }
    }
}

