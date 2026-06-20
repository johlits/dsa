public class Solution {
    public int calculateDistance(int val, int target) {
        return Math.Abs(val - target);
    }
    public int ThreeSumClosest(int[] nums, int target) {
        var len = nums.Length;
        var bestSum = Int32.MaxValue;
        var bestDistance = Int32.MaxValue;
        for (var i = 0; i < len; i++) {
            for (var j = i + 1; j < len; j++) {
                for (var k = j + 1; k < len; k++) {
                    var sum = nums[i] + nums[j] + nums[k];
                    var dist = calculateDistance(sum, target);
                    if (dist < bestDistance) {
                        bestSum = sum;
                        bestDistance = dist;
                    }
                }
            }
        }
        return bestSum;
    }
}