namespace LeetCode.PoorPigs
{
    public class Solution {
        public int PoorPigs(int buckets,
            int minutesToDie,
            int minutesToTest)
        {

            int rounds = minutesToTest / minutesToDie;
            int pigs = 0;
            while (Math.Pow(rounds + 1, pigs) < buckets)
            {
                pigs++;
            }
            return pigs;

        }
    }
}
