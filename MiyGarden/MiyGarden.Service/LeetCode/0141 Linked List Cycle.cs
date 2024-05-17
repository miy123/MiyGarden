using MiyGarden.Service.Interfaces;
using System.Collections.Generic;

namespace MiyGarden.Service.LeetCode
{
    /// <summary>
    /// Given head, the head of a linked list, determine if the linked list has a cycle in it.
    /// There is a cycle in a linked list if there is some node in the list that can be reached again by continuously following the next pointer.Internally, 
    /// pos is used to denote the index of the node that tail's next pointer is connected to. Note that pos is not passed as a parameter.
    /// Return true if there is a cycle in the linked list.Otherwise, return false.
    /// Input: head = [3,2,0,-4], pos = 1
    ///Output: true
    ///Explanation: There is a cycle in the linked list, where the tail connects to the 1st node(0-indexed).
    /// </summary>
    internal class _0141_Linked_List_Cycle : ILeetCode
    {
        public int Number => 141;

        public string[] Main()
        {
            return new string[] { };
        }

        public bool HasCycle(ListNode head)
        {
            if (head == null) return false;
            var hash = new HashSet<ListNode>();
            var cur = head;
            do
            {
                if (!hash.Add(cur)) return true;
                cur = cur.next;
            } while (cur != null);
            return false;
        }

        public class ListNode
        {
            public int val;
            public ListNode next;
            public ListNode(int x)
            {
                val = x;
                next = null;
            }
        }
    }
}
