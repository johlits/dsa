using System.Text;

public class Solution {
    public string ProcessStr(string s) {
        
        var sb = new StringBuilder("");
        for (var i = 0; i < s.Length; i++)
        {
            if (s[i] == '*')
            {
                sb.Remove(sb.Length - 1, 1);
            }
            else if (s[i] == '#')
            {
                sb.Append(sb.ToString());
            }
            else if (s[i] == '%')
            {
                sb = new StringBuilder(sb.ToString().Reverse());
            }
            else
            {
                sb.Append(s[i]);
            }
        }

        return sb.ToString();

    }
}