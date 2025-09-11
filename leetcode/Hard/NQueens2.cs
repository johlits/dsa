namespace LeetCode.NQueens2
{
    public class Solution
    {
        private static IList<IList<string>> solutions = new List<List<string>>()
                .Cast<IList<string>>()
                .ToList();

        private void CreateSolution(int n, char[,] board)
        {
            var result = new List<string>();
            for (var i = 0; i < n; i++)
            {
                var s = "";
                for (var j = 0; j < n; j++)
                {
                    s += board[i, j] == ' ' ? '.' : board[i, j];
                }
                result.Add(s);
            }
            solutions.Add(result);
        }

        private bool BoardOk(int n, char[,] board, int i, int j)
        {
            var queenCount = 0;
            var x = 0;
            var y = 0;

            for (x = 0; x < n; x++)
            {
                if (board[x, j] == 'Q')
                {
                    queenCount++;
                    if (queenCount > 1)
                        return false;
                }
            }
            if (queenCount != 1)
                return false;

            queenCount = 0;
            for (x = 0; x < n; x++)
            {
                if (board[i, x] == 'Q')
                {
                    queenCount++;
                    if (queenCount > 1)
                        return false;
                }
            }
            if (queenCount != 1)
                return false;

            x = i;
            y = j;
            while (x > 0 && y > 0)
            {
                x--;
                y--;
                if (board[x, y] == 'Q')
                {
                    return false;
                }
            }
            x = i;
            y = j;
            while (x < n - 1 && y > 0)
            {
                x++;
                y--;
                if (board[x, y] == 'Q')
                {
                    return false;
                }
            }
            x = i;
            y = j;
            while (x > 0 && y < n - 1)
            {
                x--;
                y++;
                if (board[x, y] == 'Q')
                {
                    return false;
                }
            }
            x = i;
            y = j;
            while (x < n - 1 && y < n - 1)
            {
                x++;
                y++;
                if (board[x, y] == 'Q')
                {
                    return false;
                }
            }

            return true;
        }

        public void Search(char[,] board, int n, int queens)
        {
            for (var i = 0; i < n; i++)
            {
                for (var j = 0; j < n; j++)
                {
                    if (board[i, j] == ' ')
                    {
                        board[i, j] = '.';
                        Search(board, n, queens);

                        board[i, j] = 'Q';
                        queens++;
                        if (BoardOk(n, board, i, j))
                        {
                            if (queens == n)
                            {
                                CreateSolution(n, board);
                            }
                            else if (queens < n)
                            {
                                Search(board, n, queens);
                            }
                        }

                        board[i, j] = ' ';

                        return;
                    }
                }
            }
            return;
        }

        public IList<IList<string>> SolveNQueens(int n)
        {
            solutions.Clear();
            char[,] board = new char[n, n];

            for (var i = 0; i < n; i++)
            {
                for (var j = 0; j < n; j++)
                {
                    board[i, j] = ' ';
                }
            }

            Search(board, n, 0);

            return solutions;
        }
    }
}