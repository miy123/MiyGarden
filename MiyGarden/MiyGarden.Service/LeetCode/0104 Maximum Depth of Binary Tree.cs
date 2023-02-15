using MiyGarden.Service.Interfaces;
using System;

namespace MiyGarden.Service.LeetCode
{
    public class _0104_Maximum_Depth_of_Binary_Tree : ILeetCode
    {
        public int Number => 104;

        public string[] Main()
        {
            var result = new string[]
            {
            };
            foreach (var x in result)
                Console.WriteLine(x);
            return result;
        }

        public int MaxDepth(TreeNode root)
        {
            if (root == null) return 0;
            return GetDepth(root);
        }

        private int GetDepth(TreeNode root, int depth = 1)
        {
            if (root.right == null && root.left == null) return depth;
            else
            {
                var r = root.right == null ? depth : GetDepth(root.right, depth + 1);
                var l = root.left == null ? depth : GetDepth(root.left, depth + 1);
                return Math.Max(l, r);
            }
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
