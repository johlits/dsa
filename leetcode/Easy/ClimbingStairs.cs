public class Solution {
    private static Dictionary<int, int> dic = new Dictionary<int, int>();
    public int ClimbStairs(int n) {
        if (n == 1)
        {
            return 1;
        }
        else if (n == 2)
        {
            return 2;
        }
        else if (n == 3)
        {
            return 3;
        }
        else
        {
            if (dic.ContainsKey(n))
            {
                return dic[n];
            }
            else
            {
                var r = ClimbStairs(n - 1) + ClimbStairs(n - 2);
                dic.Add(n, r);
                return r;
            }
        }
    }
}