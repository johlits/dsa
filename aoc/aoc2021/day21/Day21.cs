using Helper;

public class Day21
{
    private record GameState(int Player1Position, int Player1Score, int Player2Position, int Player2Score);

    public static void Run()
    {
        int player1StartingPosition = 4; 
        int player2StartingPosition = 10; 

        var initialGameState = new GameState(player1StartingPosition, 0, player2StartingPosition, 0);
        var (player1Wins, player2Wins) = PlayGame(initialGameState);

        Console.WriteLine(player1Wins);
        Console.WriteLine(player2Wins);
    }

    private static (long, long) PlayGame(GameState gameState, Dictionary<GameState, (long, long)> memo = null)
    {
        memo ??= new Dictionary<GameState, (long, long)>();

        if (memo.ContainsKey(gameState))
            return memo[gameState];

        if (gameState.Player1Score >= 21)
            return (1, 0);
        if (gameState.Player2Score >= 21)
            return (0, 1);

        long player1TotalWins = 0;
        long player2TotalWins = 0;

        foreach (int roll1 in new[] { 1, 2, 3 })
        {
            foreach (int roll2 in new[] { 1, 2, 3 })
            {
                foreach (int roll3 in new[] { 1, 2, 3 })
                {
                    int totalRoll = roll1 + roll2 + roll3;
                    int newPlayer1Position = (gameState.Player1Position - 1 + totalRoll) % 10 + 1;
                    int newPlayer1Score = gameState.Player1Score + newPlayer1Position;
                    var nextGameState = new GameState(gameState.Player2Position, gameState.Player2Score, newPlayer1Position, newPlayer1Score);
                    var (player2Wins, player1Wins) = PlayGame(nextGameState, memo);
                    player1TotalWins += player1Wins;
                    player2TotalWins += player2Wins;
                }
            }
        }

        var result = (player1TotalWins, player2TotalWins);
        memo[gameState] = result;
        return result;
    }
}

