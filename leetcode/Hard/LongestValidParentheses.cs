namespace LeetCode.LongestValidParentheses
{
    public class Solution
    {
        public int LongestValidParentheses(string s)
        {
            var stack = new Stack<int> ();
            var lastValidIndex = -1;
            var best = 0;

            for (var i = 0; i < s.Length; i++)
            {
                if (s[i] == '(')
                {
                    stack.Push(i);
                }
                else
                {
                    if (stack.Count == 0)
                    {
                        lastValidIndex = i;
                    }
                    else
                    {
                        stack.Pop();

                        int length;
                        if (stack.Count == 0)
                        {
                            length = i - lastValidIndex;
                        }
                        else
                        {
                            length = i - stack.Peek();
                        }

                        if (length > best)
                        {
                            best = length;
                        }
                    }
                    
                }
            }

            return best;
        }
    }
}
