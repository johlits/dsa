using Helper;

public class Day17
{
    private static bool WillHitTarget(int vx, int vy, int targetX1, int targetX2, int targetY1, int targetY2)
    {
        int x = 0, y = 0;
        while (x <= targetX2 && y >= targetY1) 
        {
            x += vx;
            y += vy;

            if (x >= targetX1 && x <= targetX2 && y >= targetY1 && y <= targetY2)
                return true;

            if (vx > 0) vx -= 1; 
            vy -= 1; 

            if ((vx == 0 && (x < targetX1 || x > targetX2)) || (vy < 0 && y < targetY1))
                break;
        }
        return false;
    }

    public static void Run()
    {
        int targetX1 = 281, targetX2 = 311;
        int targetY1 = -74, targetY2 = -54;

        int maxXVelocity = targetX2; 
        int minXVelocity = 1; 
        int maxYVelocity = -targetY1; 
        int minYVelocity = targetY1; 

        int successfulShots = 0;
        for (int vx = minXVelocity; vx <= maxXVelocity; vx++)
        {
            for (int vy = minYVelocity; vy <= maxYVelocity; vy++)
            {
                if (WillHitTarget(vx, vy, targetX1, targetX2, targetY1, targetY2))
                {
                    successfulShots++;
                }
            }
        }

        Console.WriteLine(successfulShots);
    }
}

