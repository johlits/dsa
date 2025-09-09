using System.Linq;

namespace LeetCode.ReverseInteger
{
    public class Solution
    {
        public int Reverse(int x)
        {
            if (x == 0)
            {
                return 0;
            }
            string rev = new string(("" + x).Reverse().ToArray());
            rev = rev.TrimStart('0');
            if (rev.EndsWith("-"))
            {
                rev = "-" + rev.Substring(0, rev.Length - 1);
            }
            try
            {
                return int.Parse(rev);
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}
