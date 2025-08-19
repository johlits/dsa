namespace LeetCode.Test
{
    public class Test
    {
        private bool Check(int[] arr)
        {
            int[] combinationLock = { 7, 3, 5, 9 };
            for (var i = 0; i < combinationLock.Length; i++)
            {
                if (arr[i] != combinationLock[i])
                {
                    return false;
                }
            }
            return true;
        }

        private int[]? FindCombination(int[] arr, int idx)
        {
            for (var num = 1; num <= 9; num++)
            {
                arr[idx] = num;
                if (Check(arr))
                {
                    return arr;
                }
                
                if (idx < arr.Length - 1)
                {
                    var combination = FindCombination(arr, idx + 1);
                    if (combination != null)
                    {
                        return combination;
                    }
                }
                
            }
            return null;
        }

        public void Run()
        {
            var combination = FindCombination([0, 0, 0, 0], 0);
            Console.WriteLine(combination == null ? 
                "No combination found" : 
                string.Format("Found combination: {0}", 
                    string.Join(" ", combination).ToString()));
        }
    }
}
