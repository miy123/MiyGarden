using MiyGarden.Service.Interfaces;
using System;
using System.Collections.Generic;

namespace MiyGarden.Service.LeetCode
{
    public class _0094_Binary_Tree_Inorder_Traversal : ILeetCode
    {
        public int Number => 94;

        public string[] Main()
        {
            var result = new string[]
            {
               string.Join(',',InorderTraversal(new TreeNode(1,null, new TreeNode(2, new TreeNode(3))))),
               string.Join(',',InorderTraversal(null)),
               string.Join(',',InorderTraversal(new TreeNode(1)))
            };
            foreach (var x in result)
                Console.WriteLine(x);
            return result;
        }

        //Definition for a binary tree node.
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

        // 前序 (preorder), 中序 (inorder) 和後序 (postorder) 是指遍歷二元樹 (binary tree) 時，父節點相對於左右節點的順序。假設二元樹如下：
        // Given the root of a binary tree, return the inorder traversal of its nodes' values.
        public IList<int> InorderTraversal(TreeNode root)
        {
            var result = new List<int>();
            if (root == null) return result;
            GetInorderTraversal(root, result);
            return result;
        }

        private void GetInorderTraversal(TreeNode root, List<int> result)
        {
            if (root.left != null) GetInorderTraversal(root.left, result);
            result.Add(root.val);
            if (root.right != null) GetInorderTraversal(root.right, result);
        }

        //public IList<int> InorderTraversal2(TreeNode root)
        //{
        //    var currenctNode = root;
        //    while (root.left != null)
        //    {
        //        currenctNode = currenctNode.left;

        //    };
        //}
    }
}
