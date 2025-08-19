using System.Linq;
using System.Threading.Tasks.Sources;

public class Day9
{
    public static void Run()
    {
        using (StreamReader file = new StreamReader("day09/p.in"))
        {
            string? ln;
            long cnt = 0;
            while ((ln = file.ReadLine()) != null)
            {
                var parts = ln.Split(" ");
                var lines = new List<List<long>>();
                lines.Add(new List<long>());
                foreach (var part in parts)
                {
                    lines[0].Add(long.Parse(part));
                }

                var fin = false;
                var currentLine = 0;
                while(!fin)
                {
                    var prev = lines[currentLine][0];
                    lines.Add(new List<long>());
                    fin = true;
                    for (var i = 1; i < lines[currentLine].Count; i++)
                    {
                        var diff = lines[currentLine][i] - prev;
                        lines[currentLine + 1].Add(diff);
                        prev = lines[currentLine][i];
                        if (diff != 0)
                        {
                            fin = false;
                        }
                    }
                    currentLine++;
                }

                lines[currentLine].Insert(0, 0);
                while (currentLine > 0)
                {
                    currentLine--;
                    lines[currentLine].Insert(0, lines[currentLine].First() - lines[currentLine + 1].First());
                }
                cnt += lines[0].First();
                Console.WriteLine();

            }
            Console.WriteLine(cnt);

            file.Close();
        }
    }
}

