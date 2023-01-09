using MiyGarden.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiyGarden.Service.LeetCode
{
    public class _02Add_Two_Numbers : ILeetCode
    {
        public int Number => 2;

        public string[] Main()
        {
            var result = new string[]
            {

            };
            foreach (var x in result)
                Console.WriteLine(x);
            return result;
        }

        public ListNode AddTwoNumbers(ListNode l1, ListNode l2)
        {
            var value = l1.val + l2.val;
            if (value >= 10)
            {
                var aa = new ListNode(value - 10);
                if (l1.next == null)
                {
                    l1.next = new ListNode(0);
                }

                if (l2.next == null)
                {
                    l2.next = new ListNode(0);
                }

                l1.next.val += 1;
                aa.next = this.AddTwoNumbers(l1.next, l2.next);

                return aa;
            }
            else
            {
                var aa = new ListNode(value);
                if (l1.next != null && l2.next != null)
                    aa.next = this.AddTwoNumbers(l1.next, l2.next);

                if (l1.next != null && l2.next == null)
                    aa.next = this.AddTwoNumbers(l1.next, new ListNode(0));

                if (l1.next == null && l2.next != null)
                    aa.next = this.AddTwoNumbers(new ListNode(0), l2.next);

                return aa;
            }
        }

        public class ListNode
        {
            public int val;
            public ListNode next;
            public ListNode(int x) { val = x; }
        }
    }
}
