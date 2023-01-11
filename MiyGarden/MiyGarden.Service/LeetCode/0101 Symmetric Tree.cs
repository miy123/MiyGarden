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
               IsSymmetric(new TreeNode(1, new TreeNode(2, new TreeNode(2)), new TreeNode(2, new TreeNode(2)))).ToString(),
            };
            foreach (var x in result)
                Console.WriteLine(x);
            return result;
        }

        // Given the root of a binary tree, check whether it is a mirror of itself (i.e., symmetric around its center).
        public bool IsSymmetric(TreeNode root)
        {
            var depth = GetDepth(root);
            var result = new List<int?>((2 ^ depth) - 1);
            GetInorderTraversal(root, result, depth);
            if (result.Count % 2 == 0) return false;
            else
            {
                for (var i = 0; i < (result.Count - 1) / 2; i++)
                {
                    if (result[i] != result[result.Count - 1 - i]) return false;
                }
            }
            return true;
        }

        private void GetInorderTraversal(TreeNode root, List<int?> result, int depth, int currentDepth = 1)
        {
            if (depth >= currentDepth)
            {
                var nextDepth = currentDepth + 1;
                if (root == null)
                {
                    GetInorderTraversal(null, result, depth, nextDepth);
                    result.Add(null);
                    GetInorderTraversal(null, result, depth, nextDepth);
                }
                else
                {
                    GetInorderTraversal(root.left, result, depth, nextDepth);
                    result.Add(root.val);
                    GetInorderTraversal(root.right, result, depth, nextDepth);
                }
            }
            else
            {
                return;
            }
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
