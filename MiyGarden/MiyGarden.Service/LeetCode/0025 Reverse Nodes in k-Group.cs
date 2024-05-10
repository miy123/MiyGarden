using MiyGarden.Service.Interfaces;
using System;

namespace MiyGarden.Service.LeetCode
{
    /// <summary>
    /// Given the head of a linked list, reverse the nodes of the list k at a time, and return the modified list.
    /// k is a positive integer and is less than or equal to the length of the linked list.
    /// If the number of nodes is not a multiple of k then left-out nodes, in the end, should remain as it is.
    /// You may not alter the values in the list's nodes, only nodes themselves may be changed.
    /// Input: head = [1,2,3,4,5], k = 2
    /// Output: [2,1,4,3,5]
    /// Input: head = [1,2,3,4,5], k = 3
    /// Output: [3,2,1,4,5]
    /// Constraints:
    /// The number of nodes in the list is n.
    /// 1 <= k <= n <= 5000
    /// 0 <= Node.val <= 1000
    /// </summary>
    public class _0025_Reverse_Nodes_in_k_Group : ILeetCode
    {
        public int Number => 25;

        public string[] Main()
        {
            var r = ReverseKGroup(new ListNode(1, new ListNode(2, new ListNode(3, new(4, new ListNode(5, null))))), 2);
            var r3 = ReverseKGroup(new ListNode(1, new ListNode(2, new ListNode(3, new(4, new ListNode(5, null))))), 3);
            var r2 = ReverseKGroup(new ListNode(1, new ListNode(2, null)), 2);
            var r1 = ReverseKGroup(new ListNode(3, new ListNode(9, new ListNode(6, new(1, new ListNode(1, new ListNode(4, new ListNode(7, null))))))), 4);
            return Array.Empty<string>();
        }

        public ListNode ReverseKGroup(ListNode head, int k)
        {
            if (head == null) return null;
            var first = head;
            var result = Swap(head, head.next, k);
            if (result.swap)
            {
                var nextGroupHead = result.nextGroupHead;
                var truthLast = result.head;
                result = Swap(result.head, result.head.next, k - result.k + 1);
                truthLast.next = nextGroupHead;
            }
            else
            {
                first.next = ReverseKGroup(result.nextGroupHead, k);
            }
            return result.head;
        }

        private (ListNode head, ListNode nextGroupHead, bool swap, int k) Swap(ListNode past, ListNode current, int k)
        {
            if (k == 1)
            {
                return (past, current, false, k);
            }
            else if (current == null)
            {
                // 1,2,3,4,5;k=2;past=5,current=null;該Swap
                return (past, current, true, k);
            }
            var next = current.next;
            current.next = past;
            return Swap(current, next, k - 1);
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
}
