namespace LeetCode.SwapPairs
{
    public class ListNode {
        public int val;
        public ListNode next;
        public ListNode(int val=0, ListNode next=null) {
            this.val = val;
            this.next = next;
        }
    }
    
    public class Solution
    {
        public ListNode SwapPairs(ListNode head)
        {
            var current = head;
            var first = head;
            var firstSwap = true;
            ListNode prev = null;
            
            while (true)
            {
                if (current == null)
                {
                    break;
                }
                ListNode next = current.next;
                if (next == null)
                {
                    break;
                }
                current.next = next.next;
                next.next = current;
                if (firstSwap)
                {
                    firstSwap = false;
                    first = next;
                }
                if (prev != null)
                {
                    prev.next = next;
                }
                prev = current;
                current = current.next;
            }
            return first;
        }
    }
}
