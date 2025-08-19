using System.Globalization;

namespace LeetCode.ThreeSum {
    public class Solution {
        public IList<IList<int>> ThreeSum(int[] nums) {
            IList<IList<int>> triplets = new List<IList<int>>();
            var hs = new HashSet<(int, int, int)>();
            var mem = new HashSet<int>[nums.Length];
            var len = nums.Length;

            for (var i = 0; i < len; i++)
            {
                mem[i] = new HashSet<int>();
                for (var j = i + 1; j < len; j++)
                {
                    mem[i].Add(nums[j]);
                }
            }
            
            
            for (var i = 0; i < len; i++) {
                for (var j = i + 1; j < len; j++) {
                    if (mem[j].Contains(-(nums[i] + nums[j])))
                    {
                        var k = -(nums[i] + nums[j]);

                        if (nums[i] + nums[j] + k == 0)
                        {

                            var ts1 = (nums[i], nums[j], k);
                            if (hs.Contains(ts1)) continue;
                            var ts2 = (nums[i], k, nums[j]);
                            if (hs.Contains(ts2)) continue;
                            var ts3 = (nums[j], nums[i], k);
                            if (hs.Contains(ts3)) continue;
                            var ts4 = (nums[j], k, nums[i]);
                            if (hs.Contains(ts4)) continue;
                            var ts5 = (k, nums[i], nums[j]);
                            if (hs.Contains(ts5)) continue;
                            var ts6 = (k, nums[j], nums[i]);
                            if (hs.Contains(ts6)) continue;

                            triplets.Add(new List<int>() { nums[i], nums[j], k });
                            hs.Add(ts1);
                        }
                    }
                }
            }
            return triplets;
        }
    }
}