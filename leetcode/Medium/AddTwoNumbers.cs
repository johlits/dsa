#pragma warning disable
namespace LeetCode.AddTwoNumbers
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

    public class Solution
    {
        public ListNode AddTwoNumbers(ListNode l1, ListNode l2)
        {
            var s1 = "";
            var s2 = "";
            while (l1 != null)
            {
                s1 += l1.val;
                l1 = l1.next;
            }
            while (l2 != null)
            {
                s2 += l2.val;
                l2 = l2.next;
            }
            var c1 = s1.ToCharArray();
            var c2 = s2.ToCharArray();
            Array.Reverse(c1);
            Array.Reverse(c2);
            s1 = new string(c1);
            s2 = new string(c2);

            while (s1.Length < s2.Length)
            {
                s1 = "0" + s1;
            }

            while (s2.Length < s1.Length)
            {
                s2 = "0" + s2;
            }

            var res = "";
            var carry = 0;
            for (var i = s1.Length - 1; i >= 0; i--)
            {
                var n1 = s1[i] - '0';
                var n2 = s2[i] - '0';

                var sum = n1 + n2 + carry;
                carry = sum / 10;
                res = (carry > 0 ? sum % 10 : sum) + res;
            }

            if (carry > 0)
            {
                res = carry + res;
            }

            ListNode current = null;
            ListNode ret = null;
            for (var i = 0; i < res.Length; i++)
            {
                if (current == null)
                {
                    current = new ListNode(res[i] - '0', null);
                    ret = current;
                }
                else
                {
                    current = new ListNode(res[i] - '0', current);
                }
            }


            return current;
        }
    }
}
#pragma warning restore