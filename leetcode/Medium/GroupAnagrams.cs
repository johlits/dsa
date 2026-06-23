public class Solution {
    public IList<IList<string>> GroupAnagrams(string[] strs) {
        var dic = new Dictionary<string, List<string>>();
        foreach (var str in strs) {

            int[] count = new int[26];
            foreach (char c in str) {
                count[c - 'a']++;
            }

            var sb = new System.Text.StringBuilder();
            for (int i = 0; i < 26; i++) {
                sb.Append('#');
                sb.Append(count[i]);
            }

            if (dic.ContainsKey(sb.ToString())) {
                dic[sb.ToString()].Add(str);
            }
            else {
                dic.Add(sb.ToString(), new List<string>() { str });
            }
        }
        return dic.Values.ToList<IList<string>>();
    }
}