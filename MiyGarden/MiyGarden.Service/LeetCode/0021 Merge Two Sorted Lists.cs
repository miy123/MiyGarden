using MiyGarden.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiyGarden.Service.LeetCode
{
    public class _0021_Merge_Two_Sorted_Lists : ILeetCode
    {
        public int Number => 21;

        public void Main()
        {
            Show(MergeTwoLists(new ListNode(1, new ListNode(2, new ListNode(4, null))), new ListNode(1, new ListNode(3, new ListNode(4, null)))));
            Show(MergeTwoLists(null, null));
            Show(MergeTwoLists(null, new ListNode(0, null)));
            Show(MergeTwoLists(new ListNode(2, null), new ListNode(1, null)));

            Show(MergeTwoLists(new ListNode(5, null), new ListNode(1, new ListNode(2, new ListNode(4, null)))));
        }

        private void Show(ListNode listNode)
        {
            if (listNode == null) return;
            var result = new List<int>();
            do
            {
                result.Add(listNode.val);
                listNode = listNode.next;
            } while (listNode != null);
            Console.WriteLine(string.Join(',', result));
        }

        public ListNode MergeTwoLists(ListNode l1, ListNode l2)
        {
            if (l1 == null || (l1 != null && l2 != null && l1.val > l2.val))
            {
                var temp = l1;
                l1 = l2;
                l2 = temp;
            }

            return MergeTwoLists(l1, l2, 0);
        }

        private ListNode MergeTwoLists(ListNode l1, ListNode l2, int type)
        {
            if (l2 == null) return l1;
            if (l1 == null) return l2;

            if (l1.val == l2.val)
            {
                l1.next = new ListNode(l2.val, l1.next);
                l2 = l2.next;
                l1.next.next = MergeTwoLists(l1.next.next, l2, 0);
                return l1;
            }

            if (l1.val > l2.val)
            {
                if (type == 0)
                {
                    var result = MergeTwoLists(l1, l2.next, 1);
                    l1.next = new ListNode(result.val, l1.next);
                    l2 = l2.next;
                    l1.next.next = MergeTwoLists(l1.next.next, l2, 0);
                    return l1;
                }
                if (type == 1)
                    return MergeTwoLists(l1, l2.next, 1);
                if (type == 2)
                    return l2;
            }

            if (l1.val < l2.val)
            {
                if (type == 0)
                {
                    var result = MergeTwoLists(l1.next, l2, 2);
                    l1.next = new ListNode(result.val, l1.next);
                    l2 = l2.next;
                    l1.next.next = MergeTwoLists(l1.next.next, l2, 0);
                    return l1;
                }
                if (type == 2)
                    return MergeTwoLists(l1.next, l2, 2);
                if (type == 1)
                    return l1;
            }

            return null;
        }

        public class ListNode
        {
            public int val { get; set; }
            public ListNode next { get; set; }

            public ListNode(int val = 0, ListNode next = null)
            {
                this.val = val;
                this.next = next;
            }
        }
    }
}
