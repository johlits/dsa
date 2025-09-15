namespace LeetCode.MyAtoi
{
    public class Solution {
        public int MyAtoi(string s)
        {
            s = s.Trim();
            var signed = "";
            var leadingZeros = true;
            var num = "";
            for (var i = 0; i < s.Length; i++)
            {
                if (i == 0 && s[i] == '-')
                {
                    signed = "-";
                }
                else if (i == 0 && s[i] == '+')
                {
                    signed = "";
                }
                else if (leadingZeros)
                {
                    if (s[i] >= '1' && s[i] <= '9')
                    {
                        leadingZeros = false;
                    }
                    else if (s[i] == '0')
                    {
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }
                if (!leadingZeros)
                {
                    if (s[i] >= '0' && s[i] <= '9')
                    {
                        num += s[i];
                    }
                    else
                    {
                        break;
                    }
                }
            }

            if (num == "")
            {
                num = "0";
            }

            var result = 0;
            if (num.Length > 12)
            {
                if (signed == "-")
                {
                    result = int.MinValue;
                }
                else
                {
                    result = int.MaxValue;
                }
            }
            else
            {   
                if (long.TryParse(signed + num, out long value))
                {
                    if (value > int.MaxValue)
                    {
                        result = int.MaxValue;
                    }
                    else if (value < int.MinValue)
                    {
                        result = int.MinValue;
                    }
                    else
                    {
                        result = (int)value;
                    }
                }
            }

            return result;
        }
    }
}
