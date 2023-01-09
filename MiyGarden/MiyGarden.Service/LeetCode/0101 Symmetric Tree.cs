using MiyGarden.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiyGarden.Service.LeetCode
{
    public class _0101_Symmetric_Tree : ILeetCode
    {
        public int Number => 101;

        public string[] Main()
        {

            var result = new string[]
            {
            };
            foreach (var x in result)
                Console.WriteLine(x);
            return result;
        }

        public bool IsSymmetric(TreeNode root)
        {
            var nodes = new List<int?>();
            if (root != null)
            {

            }

            return false;
        }

        public class TreeNode
        {
            public int val;
            public TreeNode left;
            public TreeNode right;
            public TreeNode(int val = 0, TreeNode left = null, TreeNode right = null)
            {
                this.val = val;
                this.left = left;
                this.right = right;
            }
        }
    }
}
