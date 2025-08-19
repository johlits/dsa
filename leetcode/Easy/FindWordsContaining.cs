namespace LeetCode.FindWordsContaining
{
    public class Solution
    {
        public IList<int> FindWordsContaining(string[] words, char x)
        {
            var l = new List<int>();
            for (var i = 0; i < words.Length; i++) {
                if (words[i].Contains(x)) {
                    l.Add(i);
                }
            }
            return l;
        }
    }
}