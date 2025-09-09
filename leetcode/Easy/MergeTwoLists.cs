#pragma warning disable
namespace LeetCode.MergeTwoLists
{
    public class Solution
    {

        public class ListNode
        {
            public int val;
            public ListNode next;
            public ListNode(int val = 0, ListNode next = null)
            {
                this.val = val;
                this.next = next;
            }
        }

        public ListNode MergeTwoLists(ListNode list1, ListNode list2)
        {
            var n1 = list1;
            var n2 = list2;
            ListNode last = null;
            ListNode first = null;

            while (n1 != null || n2 != null)
            {
                int? v1 = null;
                int? v2 = null;

                if (n1 != null)
                {
                    v1 = n1.val;
                }
                if (n2 != null)
                {
                    v2 = n2.val;
                }
                if (v1 != null && v2 != null)
                {
                    if ((int)v1 < (int)v2)
                    {
                        if (last == null)
                        {
                            last = new ListNode((int)v1);
                            first = last;
                        }
                        else
                        {
                            last.next = new ListNode((int)v1);
                            last = last.next;
                        }
                        n1 = n1.next;
                    }
                    else
                    {
                        if (last == null)
                        {
                            last = new ListNode((int)v2);
                            first = last;
                        }
                        else
                        {
                            last.next = new ListNode((int)v2);
                            last = last.next;
                        }
                        n2 = n2.next;
                    }
                }
                else if (v1 == null && v2 != null)
                {
                    if (last == null)
                    {
                        last = new ListNode((int)v2);
                        first = last;
                    }
                    else
                    {
                        last.next = new ListNode((int)v2);
                        last = last.next;
                    }
                    n2 = n2.next;
                }
                else if (v1 != null && v2 == null)
                {
                    if (last == null)
                    {
                        last = new ListNode((int)v1);
                        first = last;
                    }
                    else
                    {
                        last.next = new ListNode((int)v1);
                        last = last.next;
                    }
                    n1 = n1.next;
                }
            }

            return first;
        }
    }
}
#pragma warning restore