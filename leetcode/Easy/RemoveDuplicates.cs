/**
 * Definition for singly-linked list.
 * public class ListNode {
 *     public int val;
 *     public ListNode next;
 *     public ListNode(int val=0, ListNode next=null) {
 *         this.val = val;
 *         this.next = next;
 *     }
 * }
 */
public class Solution {
    public ListNode DeleteDuplicates(ListNode head) {
        var current = head;
        if (current == null)
        {
            return head;
        }
        while (current.next != null)
        {
            var next = current.next;
            if (next.val == current.val)
            {
                if (next.next != null)
                {
                    current.next = next.next;
                }
                else
                {
                    current.next = null;
                    break;
                }
            }
            else
            {
                current = current.next;
            }
        }
        return head;
    }
}