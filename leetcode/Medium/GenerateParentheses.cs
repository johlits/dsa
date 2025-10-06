namespace LeetCode.GenerateParentheses
{
    public class Solution {
        private void Search(IList<string> result, string current, int open, int close, int max) {
            if (current.Length == max * 2) {
                result.Add(current);
                return;
            }

            if (open < max)
            {
                Search(result, current + "(", open + 1, close, max);
            }   
            if (close < open)
            {
                Search(result, current + ")", open, close + 1, max);
            }
        }
        public IList<string> GenerateParenthesis(int n)
        {
            var result = new List<string>();
            Search(result, "", 0, 0, n);
            return result;
        }
    }
}
