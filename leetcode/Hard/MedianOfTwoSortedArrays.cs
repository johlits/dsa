namespace LeetCode.MedianOfTwoSortedArrays
{
    public class Solution
    {
        public double FindMedianSortedArrays(int[] nums1, int[] nums2)
        {
            var len = nums1.Length + nums2.Length;
            var n1p = 0;
            var n2p = 0;
            var last = 0.0;
            var nextToLast = 0.0;

            for (var i = 0; i < len / 2 + 1; i++)
            {
                if (n1p < nums1.Length && (n2p >= nums2.Length || nums1[n1p] <= nums2[n2p]))
                {
                    nextToLast = last;
                    last = nums1[n1p];
                    n1p++;
                }
                else if (n2p < nums2.Length)
                {
                    nextToLast = last;
                    last = nums2[n2p];
                    n2p++;
                }
            }

            if ((len % 2) == 0)
            {
                return (last + nextToLast) / 2;
            }

            return last;
        }
    }
}
