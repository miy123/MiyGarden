using MiyGarden.Service.Interfaces;
using System;

namespace MiyGarden.Service.LeetCode
{
    /// <summary>
    /// Given the root of a binary tree, return the length of the diameter of the tree.
    /// The diameter of a binary tree is the length of the longest path between any two nodes in a tree.This path may or may not pass through the root.
    /// The length of a path between two nodes is represented by the number of edges between them.
    /// Input: root = [1,2,3,4,5]
    ///Output: 3
    ///Explanation: 3 is the length of the path[4, 2, 1, 3] or[5, 2, 1, 3].
    ///Example 2:
    ///Input: root = [1,2]
    ///    Output: 1
    ///Constraints:
    ///The number of nodes in the tree is in the range[1, 104].
    ///-100 <= Node.val <= 100
    /// </summary>
    public class _0543_Diameter_of_Binary_Tree : ILeetCode
    {
        public int Number => 543;

        public string[] Main()
        {
            var a = DiameterOfBinaryTree(new TreeNode(1, new TreeNode(2, new TreeNode(4), new TreeNode(5)), new TreeNode(3)));
            return new string[] { };
        }

        public int DiameterOfBinaryTree(TreeNode root)
        {
            var max = 0;
            DiameterOfBinaryTree(root, ref max);
            return max;
        }

        public int DiameterOfBinaryTree(TreeNode root, ref int max)
        {
            if (root.left == null && root.right == null) return 1;
            int p1 = 0;
            int p2 = 0;
            if (root.left != null)
            {
                p1 = DiameterOfBinaryTree(root.left, ref max);
            }
            if (root.right != null)
            {
                p2 = DiameterOfBinaryTree(root.right, ref max);
            }
            if (p1 + p2 > max) max = p1 + p2;
            return Math.Max(p1, p2) + 1;
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
