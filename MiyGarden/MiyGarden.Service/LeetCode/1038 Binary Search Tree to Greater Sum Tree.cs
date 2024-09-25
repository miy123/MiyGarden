using MiyGarden.Service.Interfaces;

namespace MiyGarden.Service.LeetCode
{
    /// <summary>
    /// Given the root of a Binary Search Tree (BST), convert it to a Greater Tree such that every key of the original BST is changed to the original key plus the sum of all keys greater than the original key in BST.
    ///     As a reminder, a binary search tree is a tree that satisfies these constraints:
    /// The left subtree of a node contains only nodes with keys less than the node's key.
    /// The right subtree of a node contains only nodes with keys greater than the node's key.
    /// Both the left and right subtrees must also be binary search trees.
    /// Example 1:
    /// Input: root = [4, 1, 6, 0, 2, 5, 7, null, null, null, 3, null, null, null, 8]
    /// Output: [30,36,21,36,35,26,15,null,null,null,33,null,null,null,8]
    /// Example 2:
    /// Input: root = [0, null, 1]
    /// Output: [1,null,1]
    /// Constraints:
    /// The number of nodes in the tree is in the range [1, 100].
    /// 0 <= Node.val <= 100
    /// All the values in the tree are unique.
    /// </summary>
    public class _1038_Binary_Search_Tree_to_Greater_Sum_Tree : ILeetCode
    {
        public int Number => 1038;

        public string[] Main()
        {
            var tree = BstToGst(new TreeNode(4, new TreeNode(1, new TreeNode(0), new TreeNode(2, null, new TreeNode(3))), new TreeNode(6, new TreeNode(5), new TreeNode(7, null, new TreeNode(8)))));
            return System.Array.Empty<string>();
        }

        public TreeNode BstToGst(TreeNode root)
        {
            var newTreeParentNode = new TreeNode
            {
                val = root.val
            };
            var max = 0;
            if (root.right != null)
            {
                (newTreeParentNode, max) = BstToGstR(root.right, newTreeParentNode, 0);
                newTreeParentNode.val = max + root.val;
            }

            if (root.left != null)
            {
                (newTreeParentNode, _) = BstToGstL(root.left, newTreeParentNode, max + root.val);
            }
            return newTreeParentNode;
        }

        public (TreeNode newTreeParentNode, int max) BstToGstR(TreeNode rootTreeCurrentNode, TreeNode newTreeParentNode, int max)
        {
            if (rootTreeCurrentNode.right != null)
            {
                var newNode = new TreeNode();
                newTreeParentNode.right = newNode;
                newNode.val = rootTreeCurrentNode.val + BstToGstR(rootTreeCurrentNode.right, newNode, max).max;
                var mex = newNode.val;
                if (rootTreeCurrentNode.left != null)
                {
                    mex = BstToGstL(rootTreeCurrentNode.left, newNode, mex).Max;
                }

                return (newTreeParentNode, mex);
            }
            else
            {
                var newNode = new TreeNode
                {
                    val = rootTreeCurrentNode.val + max
                };
                newTreeParentNode.right = newNode;
                var mex = newNode.val;
                if (rootTreeCurrentNode.left != null)
                {
                    mex = BstToGstL(rootTreeCurrentNode.left, newNode, mex).Max;
                }

                return (newTreeParentNode, mex);
            }
        }

        public (TreeNode newTreeParentNode, int Max) BstToGstL(TreeNode rootTreeCurrentNode, TreeNode newTreeParentNode, int max)
        {
            if (rootTreeCurrentNode.right != null)
            {
                var newNode = new TreeNode();
                newTreeParentNode.left = newNode;
                newNode.val = rootTreeCurrentNode.val + BstToGstR(rootTreeCurrentNode.right, newNode, max).max;
                var mex = newNode.val;
                if (rootTreeCurrentNode.left != null)
                {
                    mex = BstToGstL(rootTreeCurrentNode.left, newNode, mex).Max;
                }

                return (newTreeParentNode, mex);
            }
            else
            {
                var newNode = new TreeNode
                {
                    val = rootTreeCurrentNode.val + max
                };
                newTreeParentNode.left = newNode;
                var mex = newNode.val;
                if (rootTreeCurrentNode.left != null)
                {
                    mex = BstToGstL(rootTreeCurrentNode.left, newNode, mex).Max;
                }

                return (newTreeParentNode, mex);
            }
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
