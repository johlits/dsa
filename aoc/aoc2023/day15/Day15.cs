using System.Reflection.Emit;
using System.Xml;

public class Day15
{
    public static void Run()
    {
        using (StreamReader file = new StreamReader("day15/p.in"))
        {
            long cnt = 0;
            string? ln;
            while ((ln = file.ReadLine()) != null)
            {
                var boxes = new List<List<Tuple<string, int>>>();
                var boxActivated = new List<bool>();
                for (var i = 0; i < 256; i++)
                {
                    boxes.Add(new List<Tuple<string, int>>());
                    boxActivated.Add(false);
                }

                var words = ln.Split(",");
                foreach (var word in words)
                {
                    if (word.Contains("-"))
                    {
                        var p = word.Split("-");
                        var label = p[0];

                        var boxId = 0;
                        for (var i = 0; i < label.Length; i++)
                        {
                            var c = label[i];
                            boxId += (int)c;
                            boxId = boxId * 17;
                            boxId = boxId % 256;
                        }
                        boxActivated[boxId] = true;

                        for (var i = 0; i < boxes[boxId].Count; i++)
                        {
                            if (boxes[boxId][i].Item1 == label)
                            {
                                boxes[boxId].RemoveAt(i);
                                break;
                            }
                        }
                    }
                    else if (word.Contains("="))
                    {
                        var p = word.Split("=");
                        var label = p[0];
                        var focal_length = int.Parse(p[1]);

                        var boxId = 0;
                        for (var i = 0; i < label.Length; i++)
                        {
                            var c = label[i];
                            boxId += (int)c;
                            boxId = boxId * 17;
                            boxId = boxId % 256;
                        }
                        boxActivated[boxId] = true;

                        var found = false;
                        for (var i = 0; i < boxes[boxId].Count; i++)
                        {
                            if (boxes[boxId][i].Item1 == label)
                            {
                                boxes[boxId].RemoveAt(i);
                                boxes[boxId].Insert(i, new Tuple<string, int>(label, focal_length));
                                found = true;
                                break;
                            }
                        }
                        if (!found)
                        {
                            boxes[boxId].Add(new Tuple<string, int>(label, focal_length));
                            
                        }
                    }
                }

                var idx = 0;
                for (var i = 0; i < 256; i++)
                {
                    if (boxActivated[i])
                    {
                        
                        Console.WriteLine(i + ": ");

                        for (var j = 0; j < boxes[i].Count; j++) 
                        {
                            cnt += (i + 1) * (j + 1) * boxes[i][j].Item2;
                            Console.WriteLine(boxes[i][j].Item1 + " " + boxes[i][j].Item2);
                        }
                        Console.WriteLine();
                        idx++;
                    }
                }
            }
            Console.WriteLine(cnt);

            file.Close();
        }
    }
}

