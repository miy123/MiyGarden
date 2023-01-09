using MiyGarden.Service.Interfaces;
using Newtonsoft.Json;
using System;

namespace MiyGarden.Service.LeetCode
{
    public class _0019_Remove_Nth_Node_From_End_of_List : ILeetCode
    {
        public int Number => 19;

        public string[] Main()
        {
            var result = new string[]
            {
               JsonConvert.SerializeObject(RemoveNthFromEnd(new ListNode() { val = 1, next = new ListNode() { val = 2, next = new ListNode() { val = 3, next = new ListNode() { val = 4, next = new ListNode() { val = 5 } } } } }, 2))
            };
            foreach (var x in result)
                Console.WriteLine(x);
            return result;
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

        private int count = 2;
        private bool hasRov = false;

        public ListNode RemoveNthFromEnd(ListNode head, int n)
        {
            ListNode result;
            if (head.next != null)
                result = this.RemoveNthFromEnd(head.next, n);
            else
            {
                if (n == 1)
                    return null;
                else
                    return head;
            }

            if (count == n && !hasRov)
            {
                hasRov = true;
                return result;
            }
            else
            {
                this.count++;
                head.next = result;
                return head;
            }
        }
    }
}
