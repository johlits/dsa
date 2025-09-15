namespace LeetCode.TrappingRainWater
{
    public class Solution
    {
        public int Trap(int[] height)
        {
            var top = -1;
            for (var i = 0; i < height.Length; i++)
            {
                if (height[i] > top)
                {
                    top = height[i];
                }
            }

            var trappings = 0;
            for (var i = 1; i <= top; i++)
            {
                //Console.WriteLine(i);
                var from = -1;
                var to = -1;
                for (var j = 0; j < height.Length; j++)
                {
                    if (height[j] >= i)
                    {
                        if (from == -1)
                        {
                            from = j;
                        }
                        else if (to == -1)
                        {
                            to = j;
                        }
                        if (from != -1 && to != -1)
                        {
                            //Console.WriteLine("from " + from + " to " + to);
                            if (to - from > 1)
                            {
                                trappings += to - from - 1;
                                //Console.WriteLine("Trapping " + (to - from - 1));
                                from = to;
                                to = -1;
                            }
                            else if (to - from == 1)
                            {
                                from = to;
                                to = -1;
                            }
                        }
                    }
                }
            }

            return trappings;
        }
    }
}
