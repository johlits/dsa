namespace LeetCode.TrappingRainWater
{
    public class Solution
    {
        public int Trap(int[] height)
        {
            var left = 0;
            var right = height.Length - 1;
            var leftMax = 0;
            var rightMax = 0;
            var trappings = 0;

            while (left < right)
            {
                if (height[left] < height[right])
                {
                    if (height[left] >= leftMax)
                    {
                        leftMax = height[left];
                    }
                    else
                    {
                        trappings += leftMax - height[left];
                    }
                    left++;
                }
                else
                {
                    if (height[right] >= rightMax)
                    {
                        rightMax = height[right];
                    }
                    else
                    {
                        trappings += rightMax - height[right];
                    }
                    right--;
                }
            }

            return trappings;
        }
    }
}
