using MiyGarden.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiyGarden.Service.LeetCode
{
    public class _0023_Merge_k_Sorted_Lists : ILeetCode
    {
        public int Number => 23;

        public string[] Main()
        {
            Show(MergeKLists(new ListNode[] {
                new ListNode(1, new ListNode(4, new ListNode(5, null))),
                new ListNode(1, new ListNode(3, new ListNode(4, null))),
                new ListNode(2, new ListNode(6, null))
            }));

            Show(MergeKLists(new ListNode[] {
                new ListNode(-10, new ListNode(-9, new ListNode(-9,new ListNode(-3,new ListNode(-1,new ListNode(-1,new ListNode(0,null))))))),
                new ListNode(-5, null),
                new ListNode(4, null),
                new ListNode(-8, null),
                null,
                new ListNode(-9, new ListNode(-6, new ListNode(-5,new ListNode(-4,new ListNode(-2,new ListNode(2,new ListNode(3,null))))))),
                new ListNode(-3, new ListNode(-3, new ListNode(-2,new ListNode(-1,new ListNode(0,null)))))
            }));

            var result = new string[]
            {
            };
            foreach (var x in result)
                Console.WriteLine(x);
            return result;
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

        public ListNode MergeKLists(ListNode[] lists)
        {
            if (lists == null || !lists.Any()) return null;
            var count = lists.Count();
            if (count == 1) return lists[0];
            if (count == 2) return MergeTwoLists(lists[0], lists[1]);
            var split = lists
                .Select((x, i) => new { Index = i, Value = x })
                .GroupBy(x => x.Index <= lists.Length / 2)
                .Select(x => x.Select(v => v.Value).ToArray())
                .ToArray();

            return MergeKLists(new ListNode[] { MergeKLists(split[0]), MergeKLists(split[1]) });
        }

        private ListNode MergeTwoLists(ListNode l1, ListNode l2)
        {
            // 紀錄目前欲往後接的位置(head的尾部)
            var dummy = new ListNode(-(10 ^ 4) - 1);
            var head = dummy;

            while (l1 != null && l2 != null)
            {
                if (l1.val <= l2.val)
                {
                    dummy.next = l1;
                    l1 = l1.next;
                }
                else
                {
                    dummy.next = l2;
                    l2 = l2.next;
                }
                dummy = dummy.next;
            }

            if (l1 != null)
            {
                dummy.next = l1;
            }
            else
            {
                dummy.next = l2;
            }

            return head.next;
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
