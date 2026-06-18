public class Solution {
    public int MaxArea(int[] height) {
        var a = 0;
        var b = height.Length - 1;
        var best = -1;
        while (a < b)
        {
            var area = height[a] > height[b] ? (b - a) * height[b] : (b - a) * height[a];
            if (area > best)
            {
                best = area;
            }
            if (height[a] < height[b])
            {
                a++;
            }
            else
            {
                b--;
            }
        }
        return best;
 
    }
}