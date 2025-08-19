namespace LeetCode.MinInsertions
{
    public class Solution
    {
        public int MinInsertions(string s)
        {
            var n = s.Length;
            var mem = new Dictionary<(int, int), int>();

            for (var i = 0; i < n; i++)
            {
                // x
                mem.Add((i, i), 1);
            }

            for (var length = 2; length <= n; length++)
            {
                for (int start = 0; start <= n - length; start++)
                {
                    var end = start + length - 1;

                    if (s[start] == s[end])
                    {
                        // xyyx -> yy
                        mem.Add((start, end), 
                            length == 2 ? 2 : 2 + mem[(start + 1, end - 1)]);
                    }
                    else
                    {
                        // xyyyz -> yyyz xyyy
                        mem.Add((start, end), 
                            Math.Max(
                                mem[(start + 1, end)], 
                                mem[(start, end - 1)]));
                    }
                }
            }

            var longestPalindromicSubsequence = mem[(0, n - 1)];
            return n - longestPalindromicSubsequence;
        }
    }
}
