namespace LeetCode.TextJustification
{
    public class Solution
    {
        public IList<string> FullJustify(string[] words, int maxWidth)
        {
            var result = new List<string>();
            var bag = new List<string>();
            var bagLen = 0;
            var current = "";

            for (var i = 0; i < words.Length + 1; i++)
            {
                if (i < words.Length && bagLen + words[i].Length < maxWidth + 1)
                {
                    bag.Add(words[i]);
                    bagLen += words[i].Length + 1;
                }
                else
                {
                    if (i < words.Length)
                    {
                        bagLen -= bag.Count();
                        var j = 0;
                        while (bagLen < maxWidth)
                        {
                            bag[j++] += " ";
                            bagLen++;
                            if (j >= bag.Count() - 1)
                            {
                                j = 0;
                            }
                        }
                        for (j = 0; j < bag.Count(); j++)
                        {
                            current += bag[j];
                        }
                        result.Add(current);
                        bag = new List<string>();
                        bagLen = 0;
                        current = "";

                        i--;
                    }
                    else
                    {
                        for (var j = 0; j < bag.Count() - 1; j++)
                        {
                            bag[j] += " ";
                            bagLen++;
                        }
                        while (bagLen < maxWidth + bag.Count())
                        {
                            bag[bag.Count() - 1] += " ";
                            bagLen++;
                        }
                        for (var j = 0; j < bag.Count(); j++)
                        {
                            current += bag[j];
                        }
                        result.Add(current);
                        break;
                    }
                    
                }
            }

            return result;
        }
    }
}
