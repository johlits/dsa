public class Solution {
    public int SearchInsert(int[] nums, int target) {
        var len = nums.Length;
        int id = len / 2;
        int dir = 0;
        while (true)
        {
            if (id == -1)
            {
                return 0;
            }
            if (id >= len)
            {
                return len;
            }
            if (nums[id] == target)
            {
                return id;
            }
            if (nums[id] > target)
            {
                if (dir == 1)
                {
                    return id + 1;
                }
                id--;
                dir = -1;
            }
            if (nums[id] < target)
            {
                if (dir == -1)
                {
                    return id + 1;
                }
                id++;
                dir = 1;
            }
        }
    }
}