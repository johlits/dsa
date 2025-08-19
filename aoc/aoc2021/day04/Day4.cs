using Helper;

public class Day4
{
    public static void Run()
    {
        var numbers = new ListOfIntegers();
        var bingo = new ListOfIntegerBingos();
        var bps = new List<(Blueprint, int)>
        {
            (numbers, 1),
            (bingo, -1)
        };
        new Parser("day04/p.in", bps, new Symbols()
        {

        });

        var nos = numbers.lists[0].list;
        bool[] winners = new bool[bingo.bingos.Count];
        for (var i = 0; i < nos.Count; i++)
        {
            for (var j = 0; j < bingo.bingos.Count; j++)
            {
                if (!winners[j])
                {
                    var board = bingo.bingos[j];
                    board.Mark(nos[i]);
                    if (board.IsBingo())
                    {
                        var num = nos[i];
                        var cnt = 0;

                        for (var y = 0; y < board.height; y++)
                        {
                            for (var x = 0; x < board.width; x++)
                            {
                                if (!board.marked[x][y])
                                {
                                    cnt += board.board[x][y];
                                }
                            }
                        }

                        Console.WriteLine(num * cnt);
                        winners[j] = true;

                    }
                }
            }
        }
    }
}

