using MiyGarden.Service.Interfaces;

namespace MiyGarden.Service.LeetCode
{
    /// <summary>
    /// Given an m x n integer matrix matrix, if an element is 0, set its entire row and column to 0's.
    /// You must do it in place.
    ///    Example 1:
    /// Input: matrix = [[1,1,1],[1,0,1],[1,1,1]]
    /// Output: [[1,0,1],[0,0,0],[1,0,1]]
    /// Example 2:
    /// Input: matrix = [[0,1,2,0],[3,4,5,2],[1,3,1,5]]
    /// Output: [[0,0,0,0],[0,4,5,0],[0,3,1,0]]
    /// Constraints:
    /// m == matrix.length
    /// n == matrix[0].length
    /// 1 <= m, n <= 200
    /// -231 <= matrix[i][j] <= 231 - 1
    /// Follow up:
    /// A straightforward solution using O(mn) space is probably a bad idea.
    /// A simple improvement uses O(m + n) space, but still not the best solution.
    /// Could you devise a constant space solution?
    /// </summary>
    public class _0073_Set_Matrix_Zeroes : ILeetCode
    {
        public int Number => 73;

        public string[] Main()
        {
            //SetZeroes(new int[][] { new int[] { 1, 1, 1 }, new int[] { 1, 0, 1 }, new int[] { 1, 1, 1 } });
            // [[1,0,1],[0,0,0],[1,0,1]]
            SetZeroes(new int[][] { new int[] { 0, 1, 2, 0 }, new int[] { 3, 4, 5, 2 }, new int[] { 1, 3, 1, 5 } });
            // [[0,0,0,0],[0,4,5,0],[0,3,1,0]]
            return new string[] { };
        }

        public void SetZeroes(int[][] matrix)
        {
            var hash = new int[matrix[0].Length];
            var setJ = -1;
            for (var i = 0; i < matrix.Length; i++)
            {
                for (var j = 0; j < matrix[i].Length; j++)
                {
                    if (matrix[i][j] == 0)
                    {
                        hash[j] = 1;
                        if (setJ != -1) setJ = j;
                    }
                    else if (setJ != -1) matrix[i][j] = 0;
                }

                if (setJ != -1)
                {
                    for (var j = setJ - 1; j >= 0; j--) matrix[i][j] = 0;
                    setJ = -1;
                }
            }

            for (var i = 0; i < matrix.Length; i++) for (var j = 0; j < hash.Length; j++) if (hash[j] == 1) matrix[i][j] = 0;
        }
    }
}
