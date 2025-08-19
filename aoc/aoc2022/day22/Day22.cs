using System.Runtime.InteropServices;

public class Day22
{
    private static int w = 150;
    private static int h = 200;
    private static char[,] map = new char[150, 200];
    //private static int w = 16;
    //private static int h = 12;
    //private static char[,] map = new char[16, 12];
    private static int side = 1;
    private static char turn = 'R';
    private static int actualside = 1;
    private static char actualturn = 'R';
    private static char origturn = 'R';

    private static Tuple<int, int> MoveRight(int x, int y)
    {
        if (side == 1 || side == 2 || side == 3)
        {
            x++;
        }
        if (side == 4 || side == 5)
        {
            x--;
        }
        if (side == 6)
        {
            y++;
        }
        //x++;
        
        return new Tuple<int, int>(x, y);
    }

    private static Tuple<int, int> MoveLeft(int x, int y)
    {
        if (side == 1 || side == 2 || side == 3)
        {
            x--;
        }
        if (side == 4 || side == 5)
        {
            x++;
        }
        if (side == 6)
        {
            y--;
        }
        //x--;
        return new Tuple<int, int>(x, y);
    }

    private static Tuple<int, int> MoveUp(int x, int y)
    {
        if (side == 1 || side == 2 || side == 3 )
        {
            y--;
        }
        if (side == 4 || side == 5)
        {
            y++;
        }
        if (side == 6)
        {
            x++;
        }
        //y--;
        return new Tuple<int, int>(x, y);
    }

    private static Tuple<int, int> MoveDown(int x, int y)
    {
        if (side == 1 || side == 2 || side == 3)
        {
            y++;
        }
        if (side == 4 || side == 5)
        {
            y--;
        }
        if (side == 6)
        {
            x--;
        }
        //y++;

        return new Tuple<int, int>(x, y);
    }

    private static Tuple<int, int> Wrap(Tuple<int, int> pos)
    {
        var x = pos.Item1;
        var y = pos.Item2;

        if (side == 1)
        {
            if (x == 49)
            {
                side = 4;
                y = 150 - y - 1;
                x = 0;
            }
            else if (x == 100)
            {
                side = 2;
            }
            else if (y == -1)
            {
                side = 6;
                y = 150 + x - 50;
                x = 0;
            }
            else if (y == 50)
            {
                side = 3;
            }
        }
        else if (side == 2)
        {
            if (x == 99)
            {
                side = 1;
            }
            else if (x == 150)
            {
                side = 5;
                x = 99;
                y = 150 - y - 1;
            }
            else if (y == -1)
            {
                side = 6;
                y = 199;
                x = x - 100;
            }
            else if (y == 50)
            {
                side = 3;
                y = 50 + x - 100;
                x = 99;
            }
        }
        else if (side == 3)
        {
            if (y == 49)
            {
                side = 1;
            }
            else if (y == 100)
            {
                side = 5;
            }
            else if (x == 49)
            {
                side = 4;
                x = y - 50;
                y = 100;
            }
            else if (x == 100)
            {
                side = 2;
                x = 100 + y - 50;
                y = 49;
            }
        }
        else if (side == 4)
        {
            if (x == -1)
            {
                side = 1;
                y = 50 - (y - 100) - 1;
                x = 50;
            }
            else if (x == 50)
            {
                side = 5;
            }
            else if (y == 99)
            {
                side = 3;
                y = 50 + x;
                x = 50;
            }
            else if (y == 150)
            {
                side = 6;
            }
        }
        else if (side == 5)
        {
            if (x == 49)
            {
                side = 4;
            }
            else if (x == 100)
            {
                side = 2;
                y = 50 - (y - 100) - 1;
                x = 149;
            }
            else if (y == 99)
            {
                side = 3;
            }
            else if (y == 150)
            {
                side = 6;
                y = 150 + (x - 50);
                x = 49;
            }
        }
        else if (side == 6)
        {
            if (x == -1)
            {
                side = 1;
                x = 50 + (y - 150);
                y = 0;
            }
            else if (x == 50) 
            {
                side = 5;
                x = 50 + y - 150;
                y = 149;
            }
            else if (y == 149)
            {
                side = 4;
            }
            else if (y == 200)
            {
                side = 2;
                x = 100 + x;
                y = 0;
            }
        }

        //if (x == w) x = 0;
        //if (x == -1) x = w - 1;
        //if (y == -1) y = h - 1;
        //if (y == h) y = 0;
        return new Tuple<int, int>(x, y);
    }

    private static Tuple<int, int> DoMove(Tuple<int, int> pos)
    {
        side = actualside;
        turn = actualturn;

        Tuple<int, int> newPos = null;
        if (turn == 'R') newPos = Wrap(MoveRight(pos.Item1, pos.Item2));
        else if (turn == 'L') newPos = Wrap(MoveLeft(pos.Item1, pos.Item2));
        else if (turn == 'U') newPos = Wrap(MoveUp(pos.Item1, pos.Item2));
        else if (turn == 'D') newPos = Wrap(MoveDown(pos.Item1, pos.Item2));


        //while (map[newPos.Item1, newPos.Item2] == ' ')
        //{
        //    if (turn == 'R') newPos = Wrap(MoveRight(newPos.Item1, newPos.Item2));
        //    if (turn == 'L') newPos = Wrap(MoveLeft(newPos.Item1, newPos.Item2));
        //    if (turn == 'U') newPos = Wrap(MoveUp(newPos.Item1, newPos.Item2));
        //    if (turn == 'D') newPos = Wrap(MoveDown(newPos.Item1, newPos.Item2));
        //}

        Console.WriteLine("f " + pos.Item1 + " " + pos.Item2 + " (side " + actualside + ", dir " + turn + ")");
        Console.WriteLine("t " + newPos.Item1 + " " + newPos.Item2 + " (side " + side + ")");

        if (map[newPos.Item1, newPos.Item2] == '#')
        {
            //Console.WriteLine("blocked");
            return new Tuple<int, int>(pos.Item1, pos.Item2);
        }
        else if (map[newPos.Item1, newPos.Item2] == '.')
        {

            if (actualside != side)
            {
                Console.WriteLine("f " + pos.Item1 + " " + pos.Item2 + " (side " + actualside + ", dir " + turn + ")");
                Console.WriteLine("t " + newPos.Item1 + " " + newPos.Item2 + " (side " + side + ")");
                Console.WriteLine();

                
                if (actualside == 2 && side == 3) turn = 'L';
                if (actualside == 2 && side == 6) turn = 'L';

                if (actualside == 3 && side == 2) turn = 'U';
                if (actualside == 3 && side == 4) turn = 'U';
                if (actualside == 3 && side == 5) turn = 'U';

                if (actualside == 4 && side == 3) turn = 'R';
                if (actualside == 4 && side == 6) turn = 'R';

                if (actualside == 5 && side == 3) turn = 'U';
                if (actualside == 5 && side == 6) turn = 'D';
                
                if (actualside == 6 && side == 2) turn = 'D';
                if (actualside == 6 && side == 4) turn = 'D';
                if (actualside == 6 && side == 5) turn = 'D';
                
                
            }
            else
            {
                //Console.WriteLine(newPos.Item1 + " " + newPos.Item2 + " (side " + side + ")");
            }
            //Console.WriteLine("OK");
            actualside = side;
            actualturn = turn;
            return newPos;
        }

        Console.WriteLine("WARNING");
        Console.WriteLine("f " + pos.Item1 + " " + pos.Item2 + " (side " + actualside + ", dir " + turn + ")");
        Console.WriteLine("t " + newPos.Item1 + " " + newPos.Item2 + " (side " + side + ")");
        Console.WriteLine();
        throw new Exception();
    }
    public static void Run()
    {
        using (StreamReader file = new StreamReader("day22/p.in"))
        {
            string ln;
            var index = 0;
            string movement = "";
            Tuple<int, int> pos = null;

            while ((ln = file.ReadLine()) != null)
            {
                if (index <= h - 1)
                {
                    for (var i = 0; i < w; i++)
                    {
                        if (i < ln.Length)
                        {
                            map[i, index] = ln[i];
                        }
                        else
                        {
                            map[i, index] = ' ';
                        }
                        if (pos == null && map[i, index] == '.')
                        {
                            pos = new Tuple<int, int>(i, index);
                        }
                    }
                }
                else if (index == h + 1)
                {
                    movement = ln;
                }
                index++;
            }

            var str = "";
            for(var c = 0; c < movement.Length; c++)
            {
                if (movement[c] == 'R')
                {
                    if (str.Length != 0) {
                        var m = int.Parse(str);
                        for (var move = 0; move < m; move++)
                        {
                            pos = DoMove(pos);
                            //Console.WriteLine(pos.Item1 + " " + pos.Item2);
                        }
                        str = "";
                    }
                    // clockwise
                    if (actualturn == 'R') { actualturn = 'D'; }
                    else if (actualturn == 'D') { actualturn = 'L'; }
                    else if (actualturn == 'L') { actualturn = 'U'; }
                    else if (actualturn == 'U') { actualturn = 'R'; }

                }
                else if (movement[c] == 'L')
                {
                    if (str.Length != 0)
                    {
                        var m = int.Parse(str);
                        for (var move = 0; move < m; move++)
                        {
                            pos = DoMove(pos);
                        }
                        str = "";
                    }
                    // clockwise
                    if (actualturn == 'R') { actualturn = 'U'; }
                    else if (actualturn == 'D') { actualturn = 'R'; }
                    else if (actualturn == 'L') { actualturn = 'D'; }
                    else if (actualturn == 'U') { actualturn = 'L'; }
                }
                else
                {
                    str += movement[c];
                }
            }

            if (str.Length != 0)
            {
                var m = int.Parse(str);
                for (var move = 0; move < m; move++)
                {
                    pos = DoMove(pos);
                }
                str = "";
            }

            var row = pos.Item2 + 1;
            var col = pos.Item1 + 1;
            var tot = row * 1000 + 4 * col + 2; // moving left in the end
            Console.WriteLine(row + " " + col);
            Console.WriteLine(tot);

            file.Close();
        }
    }
}

