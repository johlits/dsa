
namespace LeetCode.SudokuSolver
{
    public class Solution
    {
        public bool CheckRow(ref int[,] values, int y)
        {
            bool[] check = new bool[9];
            for (var x = 0; x < 9; x++)
            {
                if (values[x, y] == 0)
                {
                    continue;
                }
                if (check[values[x, y] - 1])
                {
                    return false;
                }
                else {
                    check[values[x, y] - 1] = true;
                }
            }
            return true;
        }

        public bool CheckCol(ref int[,] values, int x)
        {
            bool[] check = new bool[9];
            for (var y = 0; y < 9; y++)
            {
                if (values[x, y] == 0)
                {
                    continue;
                }
                if (check[values[x, y] - 1])
                {
                    return false;
                }
                else
                {
                    check[values[x, y] - 1] = true;
                }
            }
            return true;
        }

        public bool CheckBox(ref int[,] values, int x, int y)
        {
            bool[] check = new bool[9];

            var xbox = x / 3 * 3;
            var ybox = y / 3 * 3;

            for (var i = xbox; i < xbox + 3; i++)
            {
                for (var j = ybox; j < ybox + 3; j++)
                {
                    if (values[i, j] == 0)
                    {
                        continue;
                    }
                    if (check[values[i, j] - 1])
                    {
                        return false;
                    }
                    else
                    {
                        check[values[i, j] - 1] = true;
                    }
                }
            }

            return true;
        }

        private bool Solve(int[,] values)
        {
            for (int x = 0; x < 9; x++)
            {
                for (int y = 0; y < 9; y++)
                {
                    if (values[x, y] == 0)
                    {
                        for (int num = 1; num <= 9; num++)
                        {
                            values[x, y] = num;
                            if (CheckRow(ref values, y) &&
                                CheckCol(ref values, x) &&
                                CheckBox(ref values, x, y))
                            {
                                if (Solve(values))
                                {
                                    return true;
                                }
                            }
                        }
                        values[x, y] = 0;
                        return false;
                    }
                }
            }
            return true;
        }

        public void SolveSudoku(char[][] board)
        {
            int[,] values = new int[9, 9];

            for (var x = 0; x < 9; x++)
            {
                for (var y = 0; y < 9; y++)
                {
                    if (board[x][y] != '.')
                    {
                        values[x, y] = board[x][y] - '0';
                    }
                }
            }

            Solve(values);

            for (var x = 0; x < 9; x++)
            {
                for (var y = 0; y < 9; y++)
                {
                    board[x][y] = (char)('0' + values[x, y]);
                }
            }
        }
    }
}
