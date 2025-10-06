namespace LeetCode.AddBinary
{
    public class Solution {
        public string ReverseString(string s) {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
        public string RemoveLeadingZeros(string s)
        {
            var foundOne = false;
            string result = "";
            for (var i = 0; i < s.Length; i++)
            {
                if (foundOne == false)
                {
                    if (s[i] == '1')
                    {
                        foundOne = true;
                    }
                }
                if (foundOne)
                {
                    result += s[i];
                }
            }

            return result;
        }

        public string AddBinary(string a, string b)
        {
            if (a == "0" && b == "0")
            {
                return "0";
            }
            a = ReverseString(a);
            b = ReverseString(b);
            string c = "";
            var carry = 0;

            for (var i = 0; i < 10000; i++)
            {
                var aDigit = i >= a.Length ? 0 : a[i] - '0';
                var bDigit = i >= b.Length ? 0 : b[i] - '0';
                var sum = aDigit + bDigit + carry;
                if (sum == 0)
                {
                    c = c + "0";
                    carry = 0;
                }
                if (sum == 1)
                {
                    c = c + "1";
                    carry = 0;
                }
                if (sum == 2)
                {
                    c = c + "0";
                    carry = 1;
                }
                if (sum == 3)
                {
                    c = c + "1";
                    carry = 1;
                }
            }

            return RemoveLeadingZeros(ReverseString(c));
        }
    }
}
