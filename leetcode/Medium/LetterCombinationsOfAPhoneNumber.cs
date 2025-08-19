namespace LeetCode.LetterCombinationsOfAPhoneNumber
{
    public class Solution
    {
        private static Dictionary<char, List<char>> dic = new()
        {
            { '2', new List<char>() { 'a', 'b', 'c' } },
            { '3' , new List<char>() { 'd', 'e', 'f' } },
            { '4' , new List<char>() { 'g', 'h', 'i' } },
            { '5' , new List<char>() { 'j', 'k', 'l' } },
            { '6' , new List<char>() { 'm', 'n', 'o' } },
            { '7' , new List<char>() { 'p', 'q', 'r', 's' } },
            { '8' , new List<char>() { 't', 'u', 'v' } },
            { '9' , new List<char>() { 'w', 'x', 'y', 'z' } },
        };

        public List<string> Search(string s, string digits, int pos)
        {
            if (pos >= digits.Length)
            {
                return s == "" ? new List<string>() : new List<string>() { s };
            }
            var combinations = dic[digits[pos]];
            var result = new List<string>();
            foreach (var combination in combinations)
            {
                result.AddRange(Search(s + combination, digits, pos + 1));
            }
            return result;
        }

        public IList<string> LetterCombinations(string digits)
        {
            return Search("", digits, 0);
        }
    }
}
