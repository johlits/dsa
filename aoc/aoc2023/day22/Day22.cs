using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class Day22
{
    public class Block
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
    }

    public class Brick
    {
        public List<Block> Blocks { get; set; } = new List<Block>();
        public List<int> Supporting { get; set; } = new List<int>();
        public List<int> SupportedBy { get; set; } = new List<int>();
        public bool Grounded { get; set; }
        public bool Removed { get; set; }
    }

    private static void CreateBlocks(Brick brick, Tuple<int, int, int> start, Tuple<int, int, int> end)
    {
        for (int x = Math.Min(start.Item1, end.Item1); x <= Math.Max(start.Item1, end.Item1); x++)
        {
            for (int y = Math.Min(start.Item2, end.Item2); y <= Math.Max(start.Item2, end.Item2); y++)
            {
                for (int z = Math.Min(start.Item3, end.Item3); z <= Math.Max(start.Item3, end.Item3); z++)
                {
                    brick.Blocks.Add(new Block { X = x, Y = y, Z = z });
                }
            }
        }
    }

    private static void Fall(List<Brick> bricks)
    {
        bool brickMoved;
        var groundedCnt = 0;
        do
        {
            brickMoved = false;

            foreach (Brick fallingBrick in bricks.Where(x => x.Grounded == false))
            {
                int lowestZ = fallingBrick.Blocks.Min(b => b.Z);
                if (lowestZ == 1)
                {
                    if (!fallingBrick.Grounded)
                    {
                        fallingBrick.Grounded = true;
                        groundedCnt++;
                        Console.WriteLine(groundedCnt);
                    }
                    
                    continue;
                }

                var supported = false;
                foreach (var checkBrick in bricks)
                {
                    if (checkBrick != fallingBrick)
                    {
                        if (IsConnected(checkBrick, fallingBrick))
                        {
                            if (checkBrick.Grounded)
                            {
                                fallingBrick.Grounded = true;
                            }
                            supported = true;
                            break;
                        }
                    }
                }
                if (supported)
                {
                    continue;
                }

                foreach (Block block in fallingBrick.Blocks)
                {
                    block.Z--;
                }
                brickMoved = true; 
            }
        } while (brickMoved); 

        SetSupporting(bricks); 
    }

    private static void SetSupporting(List<Brick> bricks)
    {
        foreach (Brick brick in bricks)
        {
            brick.Supporting.Clear();
            brick.SupportedBy.Clear();
        }

        for (int i = 0; i < bricks.Count; i++)
        {
            for (int j = 0; j < bricks.Count; j++)
            {
                if (i == j) continue; 

                if (IsConnected(bricks[i], bricks[j]))
                {
                    bricks[i].Supporting.Add(j); 
                    bricks[j].SupportedBy.Add(i);
                }
            }
        }
    }

    private static bool IsConnected(Brick lower, Brick upper)
    {
        foreach (Block lowerBlock in lower.Blocks)
        {
            foreach (Block upperBlock in upper.Blocks)
            {
                if (lowerBlock.X == upperBlock.X && lowerBlock.Y == upperBlock.Y && upperBlock.Z - 1 == lowerBlock.Z)
                {
                    return true; 
                }
            }
        }
        return false; 
    }

    private static List<Brick> CanBeRemoved(List<Brick> bricks)
    {
        List<Brick> safeToDisintegrate = new List<Brick>();
        foreach (Brick brick in bricks)
        {
            bool canBeRemoved = true;
            foreach (var supportedBrickIndex in brick.Supporting)
            {
                var supportedBrick = bricks[supportedBrickIndex];
                if (supportedBrick.SupportedBy.Count <= 1) 
                {
                    canBeRemoved = false;
                    break;
                }
            }
            if (canBeRemoved)
            {
                safeToDisintegrate.Add(brick);
            }
        }
        return safeToDisintegrate;
    }

    private static void GetImpacts(List<Brick> bricks)
    {
        var cnt = 0;
        for (int i = 0; i < bricks.Count; i++)
        {
            bricks.ForEach(brick => brick.Removed = false);
            bricks[i].Removed = true;

            bool anyFell;
            do
            {
                anyFell = false;
                foreach (var brick in bricks.Where(b => !b.Removed && b.SupportedBy.Any()))
                {
                    if (brick.SupportedBy.All(supportIndex => bricks[supportIndex].Removed))
                    {
                        brick.Removed = true; 
                        anyFell = true;
                    }
                }
            } while (anyFell); 

            int fallenBricksCount = bricks.Count(b => b.Removed) - 1; 
            cnt += fallenBricksCount;
        }
        Console.WriteLine(cnt);
    }


    public static void Run()
    {
        var bricks = new List<Brick>();

        using (StreamReader file = new StreamReader("day22/p.in"))
        {
            string line;
            while ((line = file.ReadLine()) != null)
            {
                var parts = line.Split('~');
                var startCoords = parts[0].Split(',').Select(int.Parse).ToArray();
                var endCoords = parts[1].Split(',').Select(int.Parse).ToArray();

                var newBrick = new Brick();
                CreateBlocks(newBrick,
                    Tuple.Create(startCoords[0], startCoords[1], startCoords[2]),
                    Tuple.Create(endCoords[0], endCoords[1], endCoords[2]));
                bricks.Add(newBrick);
            }
        }

        Fall(bricks);
        var safeToRemove = CanBeRemoved(bricks);
        Console.WriteLine(safeToRemove.Count);
        GetImpacts(bricks);
    }
}

