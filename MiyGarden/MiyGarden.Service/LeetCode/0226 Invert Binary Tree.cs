using MiyGarden.Service.Interfaces;

namespace MiyGarden.Service.LeetCode
{
    /// <summary>
    /// Given the root of a binary tree, invert the tree, and return its root.
    /// Input: root = [4,2,7,1,3,6,9]
    /// Output: [4,7,2,9,6,3,1]
    /// </summary>
    public class _0226_Invert_Binary_Tree : ILeetCode
    {
        public int Number => 226;

        public string[] Main()
        {
            var a = InvertTree(new TreeNode(4, new TreeNode(2, new TreeNode(1), new TreeNode(3)), new TreeNode(7, new TreeNode(6), new TreeNode(9))));
            return new string[] { };
        }

        public TreeNode InvertTree(TreeNode root)
        {
            if (root == null) return root;
            (root.right, root.left) = (root.left, root.right);
            InvertTree(root.left);
            InvertTree(root.right);
            return root;
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
