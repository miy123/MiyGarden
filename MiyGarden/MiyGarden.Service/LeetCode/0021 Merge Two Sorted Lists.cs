using MiyGarden.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiyGarden.Service.LeetCode
{
    public class _0021_Merge_Two_Sorted_Lists : ILeetCode
    {
        public int Number => 21;

        public string[] Main()
        {
            Show(MergeTwoLists(new ListNode(1, new ListNode(2, new ListNode(4, null))), new ListNode(1, new ListNode(3, new ListNode(4, null)))));
            Show(MergeTwoLists(null, null));
            Show(MergeTwoLists(null, new ListNode(0, null)));
            Show(MergeTwoLists(new ListNode(2, null), new ListNode(1, null)));
            Show(MergeTwoLists(new ListNode(5, null), new ListNode(1, new ListNode(2, new ListNode(4, null)))));

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="l1"></param>
        /// <param name="l2"></param>
        /// <returns></returns>
        public ListNode MergeTwoLists(ListNode l1, ListNode l2)
        {
            // 紀錄目前欲往後接的位置(head的尾部)
            var dummy = new ListNode(-1);
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
