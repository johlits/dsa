namespace LeetCode.LongestSubstringWithoutRepeatingCharacters
{
    public class Solution
    {
        public int LengthOfLongestSubstring(string s)
        {
            var hs = new HashSet<char>();
            var best = 0;
            for (var i = 0; i < s.Length; i++)
            {
                hs.Clear();
                var len = 0;
                for (var j = i; j < s.Length; j++)
                {
                    if (hs.Contains(s[j]))
                    {
                        break;
                    }
                    len++;
                    hs.Add(s[j]);
                }
                if (len > best)
                {
                    best = len;
                }
            }
            return best;
        }
    }
}
