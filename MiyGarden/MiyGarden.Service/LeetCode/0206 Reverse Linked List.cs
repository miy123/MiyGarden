using MiyGarden.Service.Interfaces;

namespace MiyGarden.Service.LeetCode
{
    /// <summary>
    /// Given the head of a singly linked list, reverse the list, and return the reversed list.
    /// Input: head = [1,2,3,4,5]
    /// Output: [5,4,3,2,1]
    /// </summary>
    public class _0206_Reverse_Linked_List : ILeetCode
    {
        public int Number => 206;

        public string[] Main()
        {
            var a = ReverseList(new ListNode(1, new ListNode(2, new ListNode(3, new ListNode(4, new ListNode(5, null))))));
            return new string[] { };
        }

        public ListNode ReverseList(ListNode head)
        {
            if (head == null) return head;
            ListNode past = null;
            do
            {
                var next = head.next;
                head.next = past;
                past = head;
                head = next;
            } while (head != null);
            return past;
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
