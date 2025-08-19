#pragma warning disable
namespace LeetCode.LongestPalindromicSubstring
{
    public class Solution
    {
        public string Palindrome(int i, int j, string s)
        {
            var len = 0;
            var y = 0;

            if (i == j)
            {
                len = 1;
            }
            else
            {
                for (var x = 0; x < j - i; x++)
                {
                    if (s[i + x] == s[j - x])
                    {
                        len++;
                    }
                    else
                    {
                        return null;
                    }
                }
            }

            var palindrome = "";
            for (var x = i; x <= j; x++)
            {
                palindrome += s[x];
            }

            return palindrome;
        }
        public string LongestPalindrome(string s)
        {
            var best = 0;
            var bestPalindrome = "";
            for (var i = 0; i < s.Length; i++)
            {
                for (var j = i; j < s.Length; j++)
                {
                    if (j - i >= best)
                    {
                        var pal = Palindrome(i, j, s);
                        if (pal != null && pal.Length > best)
                        {
                            best = pal.Length;
                            bestPalindrome = pal;
                        }
                    }
                }
            }
            return bestPalindrome;

        }
    }
}
#pragma warning restore