public class Solution {
    public IList<IList<int>> FourSum(int[] nums, int target) {
        var result = new List<IList<int>>();

        Array.Sort(nums);

        for (int a = 0; a < nums.Length - 3; a++) {
            if (a > 0 && nums[a] == nums[a - 1]) {
                continue;
            }

            for (int d = nums.Length - 1; d > a + 2; d--) {
                if (d < nums.Length - 1 && nums[d] == nums[d + 1]) {
                    continue;
                }

                int b = a + 1;
                int c = d - 1;

                while (b < c) {
                    long sum = (long)nums[a] + nums[b] + nums[c] + nums[d];

                    if (sum == target) {
                        result.Add(new List<int> {
                            nums[a],
                            nums[b],
                            nums[c],
                            nums[d]
                        });

                        b++;
                        c--;

                        while (b < c && nums[b] == nums[b - 1]) {
                            b++;
                        }

                        while (b < c && nums[c] == nums[c + 1]) {
                            c--;
                        }
                    }
                    else if (sum < target) {
                        b++;
                    }
                    else {
                        c--;
                    }
                }
            }
        }

        return result;
    }
}