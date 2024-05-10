using MiyGarden.Service.Interfaces;
using System;

namespace MiyGarden.Service.LeetCode
{
    /// <summary>
    /// Given a linked list, swap every two adjacent nodes and return its head.
    /// You must solve the problem without modifying the values in the list's nodes (i.e., only nodes themselves may be changed.)
    /// Input: head = [1,2,3,4]
    /// Output: [2,1,4,3]
    /// Constraints:
    /// The number of nodes in the list is in the range[0, 100].
    ///0 <= Node.val <= 100
    /// </summary>
    /// <returns></returns>
    public class _0024_Swap_Nodes_in_Pairs : ILeetCode
    {
        int ILeetCode.Number => 24;

        public string[] Main()
        {
            var result = SwapPairs(new ListNode(1, new ListNode(2, new ListNode(3, new ListNode(4)))));
            result = SwapPairs(new ListNode(1, null));
            result = SwapPairs(null);
            return Array.Empty<string>();
        }

        public ListNode SwapPairs(ListNode head)
        {
            if (head == null || head.next == null) return head;
            var nextGroupHead = head.next.next;
            var newHead = head.next;
            var newLast = head;
            newHead.next = newLast;
            newLast.next = SwapPairs(nextGroupHead);
            return newHead;
        }
    }

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
}
