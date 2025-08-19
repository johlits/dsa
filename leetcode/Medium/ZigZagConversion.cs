namespace LeetCode.ZigZagConversion
{
    public class Solution
    {
        public string Convert(string s, int numRows)
        {
            List<char>[] chars = new List<char>[numRows];
            for (var i = 0; i < numRows; i++)
            {
                chars[i] = new List<char>();
            }
            var inBetween = numRows - 2;

            var state = 0;
            var k = 0;
            for (var i = 0; i < s.Length; i++)
            {
                if (state == 0)
                {
                    chars[k].Add(s[i]);
                    k++;
                    if (k >= numRows)
                    {
                        state = 1;
                        k = 0;
                    }
                }
                else
                {
                    for (var j = 0; j < inBetween; j++)
                    {
                        chars[numRows - j - 2].Add(s[i++]);
                        if (i >= s.Length)
                        {
                            break;
                        }
                    }
                    i--;
                    state = 0;
                }
            }

            return string.Join("", chars.Select(x => string.Join("", x)));
        }
    }
}
