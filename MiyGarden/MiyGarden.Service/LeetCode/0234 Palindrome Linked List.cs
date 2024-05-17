using MiyGarden.Service.Interfaces;
using System.Collections.Generic;

namespace MiyGarden.Service.LeetCode
{
    /// <summary>
    /// Given the head of a singly linked list, return true if it is a palindrome or false otherwise.
    /// </summary>
    public class _0234_Palindrome_Linked_List : ILeetCode
    {
        public int Number => 234;

        public string[] Main()
        {
            var a = IsPalindrome(new List<int>() { 1, 2, 2, 1 });
            a = IsPalindrome(new List<int>() { 1, 2, 3, 2, 1 });
            a = IsPalindrome(new List<int>() { 1, 2 });
            return new string[] { };
        }

        public bool IsPalindrome(ListNode head)
        {
            var list = new List<int>(10^5);
            while (head != null)
            {
                list.Add(head.val);
                head = head.next;
            }

            var half = list.Count / 2;
            for (var i = 0; i < half; i++)
            {
                if (list[i] != list[list.Count - i - 1])
                {
                    return false;
                }
            }

            return true;
        }

        private bool IsPalindrome(List<int> list)
        {
            var half = list.Count / 2;
            for (var i = 0; i < half; i++)
            {
                if (list[i] != list[list.Count - i - 1])
                {
                    return false;
                }
            }

            return true;
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
