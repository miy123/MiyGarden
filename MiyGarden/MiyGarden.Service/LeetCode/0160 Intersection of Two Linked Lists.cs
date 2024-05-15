using MiyGarden.Service.Interfaces;
using System.Collections.Generic;

namespace MiyGarden.Service.LeetCode
{
    public class _0160_Intersection_of_Two_Linked_Lists : ILeetCode
    {
        public int Number => 160;

        public string[] Main()
        {
            var a = new ListNode(1) { next = new ListNode(2) };
            var b = GetIntersectionNode(new ListNode(5) { next = a }, new ListNode(7) { next = a });
            return null;
        }

        /// <summary>
        /// 利用hash特性 m+n
        /// </summary>
        /// <param name="headA"></param>
        /// <param name="headB"></param>
        /// <returns></returns>
        public ListNode GetIntersectionNode(ListNode headA, ListNode headB)
        {
            var hash = new HashSet<ListNode>();
            var cur = headA;
            do
            {
                if (!hash.Add(cur)) return cur;
                cur = cur.next;
            } while (cur != null);
            cur = headB;
            do
            {
                if (!hash.Add(cur)) return cur;
                cur = cur.next;
            } while (cur != null);
            return null;
        }

        /// <summary>
        /// 雙loop n^2
        /// </summary>
        /// <param name="headA"></param>
        /// <param name="headB"></param>
        /// <returns></returns>
        public ListNode GetIntersectionNode2(ListNode headA, ListNode headB)
        {
            var curA = headA;
            do
            {
                var curB = headB;
                do
                {
                    if (curA == curB) return curA;
                    curB = curB.next;
                } while (curB != null);
                curA = curA.next;
            } while (curA != null);
            return null;
        }

        public class ListNode
        {
            public int val;
            public ListNode next;
            public ListNode(int x) { val = x; }
        }
    }
}
