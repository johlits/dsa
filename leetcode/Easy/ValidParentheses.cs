namespace LeetCode.ValidParenthesis
{
    public class Solution
    {
        public bool IsValid(string s)
        {
            var type1 = 0;
            var type2 = 0;
            var type3 = 0;
            var toClose = new Stack<int>();

            for (var i = 0; i < s.Length; i++)
            {
                if (s[i] == '(')
                {
                    type1++;
                    toClose.Push(1);
                }
                if (s[i] == ')')
                {
                    type1--;
                    if (toClose.Count > 0 && toClose.Pop() != 1)
                    {
                        return false;
                    }
                }
                if (s[i] == '{')
                {
                    type2++;
                    toClose.Push(2);
                }
                if (s[i] == '}')
                {
                    type2--;
                    if (toClose.Count > 0 && toClose.Pop() != 2)
                    {
                        return false;
                    }
                }
                if (s[i] == '[')
                {
                    type3++;
                    toClose.Push(3);
                }
                if (s[i] == ']')
                {
                    type3--;
                    if (toClose.Count > 0 && toClose.Pop() != 3)
                    {
                        return false;
                    }
                }
            }

            return type1 == 0 && type2 == 0 && type3 == 0 && toClose.Count == 0;
        }
    }
}
