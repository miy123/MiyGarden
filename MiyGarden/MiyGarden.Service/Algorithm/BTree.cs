using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace MiyGarden.Service.Algorithm
{
    public class BtreeValidator
    {
        public void Start1()
        {
            var root = new BTreeNode();
            root = Btree.Build(1, 1, root);
            root = Btree.Build(3, 3, root);
            root = Btree.Build(5, 5, root);
            root = Btree.Build(2, 2, root);
            root = Btree.Build(4, 4, root);
            root = Btree.Build(100, 100, root);
            root = Btree.Build(50, 50, root);
            root = Btree.Build(70, 70, root);
            PrintValidation(1, root);
            PrintValidation(2, root);
            PrintValidation(3, root);
            PrintValidation(4, root);
            PrintValidation(5, root);
            PrintValidation(6, root, true);
            PrintValidation(7, root, true);
            PrintValidation(10, root, true);
            PrintValidation(50, root);
            PrintValidation(70, root);
            PrintValidation(100, root);
            PrintValidation(200, root, true);
        }

        public void Start2()
        {
            var root = new BTreeNode();
            var hashset = new HashSet<int>();
            for (var i = 1000; i > 0; i /= 2)
            {
                root = Btree.Build(i, i, root);
                hashset.Add(i);
                if (!Check(root)) Console.WriteLine($"{i}");
            }

            foreach (var i in hashset)
            {
                PrintValidation(i, root, false);
            }

            for (var i = 1; i < 1000; i *= 2)
            {
                root = Btree.Build(i, i, root);
                hashset.Add(i);
                if (!Check(root)) Console.WriteLine($"{i}");
            }

            foreach (var i in hashset)
            {
                PrintValidation(i, root, false);
            }

            for (var i = 0; i < 1000; i += 3)
            {
                root = Btree.Build(i, i, root);
                hashset.Add(i);
                if (!Check(root)) Console.WriteLine($"{i}");
            }

            foreach (var i in hashset)
            {
                PrintValidation(i, root, false);
            }

            for (var i = 0; i < 1000; i += 5)
            {
                root = Btree.Build(i, i, root);
                hashset.Add(i);
                if (!Check(root)) Console.WriteLine($"{i}");
            }

            foreach (var i in hashset)
            {
                PrintValidation(i, root, false);
            }

            for (var i = 0; i < 1000; i++)
            {
                root = Btree.Build(i, i, root);
                hashset.Add(i);
                if (!Check(root)) Console.WriteLine($"{i}");
            }

            foreach (var i in hashset)
            {
                PrintValidation(i, root, false);
            }

            Console.WriteLine("Done.");
        }

        private void PrintValidation(int key, BTreeNode root, bool isNull = false)
        {
            var result = Btree.Search(key, root);
            if (result == (isNull ? null : key) == false) Console.WriteLine($"[{result == (isNull ? null : key)}] {key}:{result}");
        }

        private bool Check(BTreeNode root)
        {
            foreach (var el in root.BTreeElements)
            {
                if (el.RightNode != null)
                {
                    if (el.RightNode.ParentNode == root && Check(el.RightNode))
                    {
                        if (el.LeftNode != null)
                        {
                            if (el.LeftNode.ParentNode == root && Check(el.LeftNode))
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return true;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return true;
                }
            }
            return true;
        }
    }

    public static class Btree
    {
        // 欲加入的key在非葉節點上，大於則往右；小於等於則往左
        public static BTreeNode Build(int key, int rowID, BTreeNode root)
        {
            if (root.BTreeElements == null)
            {
                root.BTreeElements = new List<BTreeElement>() { new BTreeElement { Key = key,
                        LeftNode = new BTreeNode() { BTreeElements =  new List<BTreeElement>(){ new BTreeElement() { Key = key, RowID = rowID} } , ParentNode = root} } };
                return root;
            }
            else
            {
                var length = root.ContainLength;
                // 只有root節點，直接插入
                if (root.IsRoot && (root.BTreeElements.Any(x => x.LeftNode.IsLeaf) || root.BTreeElements.Any(x => x.RightNode.IsLeaf)))
                {
                    return TryInsert(key, rowID, root);
                }
                else // 開始尋找插入點
                {
                    return LookupInsert(key, rowID, root);
                }
            }
        }

        private static BTreeNode LookupInsert(int key, int rowID, BTreeNode root)
        {
            for (var i = 0; i < root.BTreeElements.Count; i++)
            {
                if (key > root.BTreeElements[i].Key)
                {
                    if (i + 1 == root.BTreeElements.Count)
                    {
                        if ((root.BTreeElements[i].LeftNode != null && root.BTreeElements[i].LeftNode.IsLeaf) ||
                            (root.BTreeElements[i].RightNode != null && root.BTreeElements[i].RightNode.IsLeaf)) // 插入root
                        {
                            return TryInsert(key, rowID, root);
                        }
                        else
                        {
                            var node = LookupInsert(key, rowID, root.BTreeElements[i].RightNode);
                            return node.IsRoot ? node : root;
                        }
                    }
                    else continue;
                }
                else
                {
                    if ((root.BTreeElements[i].LeftNode != null && root.BTreeElements[i].LeftNode.IsLeaf) ||
                        (root.BTreeElements[i].RightNode != null && root.BTreeElements[i].RightNode.IsLeaf)) // 插入root
                    {
                        return TryInsert(key, rowID, root);
                    }
                    else
                    {
                        var node = LookupInsert(key, rowID, root.BTreeElements[i].LeftNode);
                        return node.IsRoot ? node : root;
                    }
                }
            }
            throw new ArgumentException("Lookup failed.");
        }
        // 拿到子樹的root節點
        private static BTreeNode TryInsert(int key, int? rowID, BTreeNode node, BTreeElement insertEl = null)
        {
            if (node.IsFull && node.BTreeElements.Any(x => x.Key != key)) // 分裂
            {
                var left = new BTreeNode { BTreeElements = new List<BTreeElement>() };
                var right = new BTreeNode { BTreeElements = new List<BTreeElement>() };

                var array = node.BTreeElements.Select(x => x.Key).ToList();
                array.Add(key);
                var orderedArray = array.OrderBy(x => x).ToList();
                var halfIndex = orderedArray.Count % 2 != 0 ? orderedArray.Count / 2 : orderedArray.Count / 2 - 1;
                var basisValue = orderedArray[halfIndex];
                var newPel = new BTreeElement { Key = basisValue, RightNode = right, LeftNode = left };
                if (insertEl == null) // 首次插入元素，須先創建其子葉(rowID)
                {
                    ManualInsertNodeAndLeaf(key, rowID, node);
                }
                else
                {
                    ManualInsertNode(insertEl, node);
                }

                for (var i = 0; i < node.BTreeElements.Count; i++)
                {
                    if (node.BTreeElements[i].Key < basisValue)
                    {
                        left.BTreeElements.Add(node.BTreeElements[i]);
                    }
                    else if (node.BTreeElements[i].Key > basisValue)
                    {
                        right.BTreeElements.Add(node.BTreeElements[i]);
                    }
                }

                foreach (var lel in left.BTreeElements)
                {
                    if (lel.LeftNode != null) { lel.LeftNode.ParentNode = left; }
                    if (lel.RightNode != null) { lel.RightNode.ParentNode = left; }
                }
                foreach (var rel in right.BTreeElements)
                {
                    if (rel.LeftNode != null) { rel.LeftNode.ParentNode = right; }
                    if (rel.RightNode != null) { rel.RightNode.ParentNode = right; }
                }

                var newNode = ProcessSplitNode(node.ParentNode, newPel);
                return newNode;
            }
            else
            {
                if (node.BTreeElements.Any(x => x.LeftNode.IsLeaf || x.RightNode.IsLeaf))
                {
                    ManualInsertNodeAndLeaf(key, rowID, node);
                    return node;
                }
                else
                {
                    ManualInsertNode(insertEl, node);
                    return node;
                }
            }
        }
        private static void ManualInsertNode(BTreeElement el, BTreeNode root)
        {
            var flag = 0;
            for (var i = 0; i < root.BTreeElements.Count; i++)
            {
                if (el.Key == root.BTreeElements[i].Key)
                {
                    throw new ArgumentException("非臨葉節點不可插入重複的key");
                }
                else if (el.Key > root.BTreeElements[i].Key)
                {
                    if (i + 1 == root.BTreeElements.Count)
                    {
                        root.BTreeElements[i].RightNode = el.LeftNode;
                        root.BTreeElements.Insert(i + 1, el);
                        break;
                    }
                    else flag = 1;
                }
                else if (el.Key < root.BTreeElements[i].Key)
                {
                    if (flag == 1 || i == 0) // insert
                    {
                        root.BTreeElements[i].LeftNode = el.RightNode;
                        if (i != 0) root.BTreeElements[i - 1].RightNode = el.LeftNode; // 夾中間
                        root.BTreeElements.Insert(i, el);
                        break;
                    }
                    else flag = -1;
                }
            }
        }
        private static void ManualInsertNodeAndLeaf(int key, int? rowID, BTreeNode root)
        {
            var flag = 0;
            for (var i = 0; i < root.BTreeElements.Count; i++)
            {
                if (key == root.BTreeElements[i].Key) // insert leaf
                {
                    root.BTreeElements[i].LeftNode.BTreeElements.Add(new BTreeElement { RowID = rowID, Key = key });
                }
                else if (key > root.BTreeElements[i].Key)
                {
                    if (flag == -1 || i + 1 == root.BTreeElements.Count) // insert
                    {
                        var leafEl = new BTreeElement { Key = key, RowID = rowID };
                        var leafNode = new BTreeNode { BTreeElements = new List<BTreeElement> { leafEl }, ParentNode = root };
                        var el = new BTreeElement
                        {
                            Key = key,
                            LeftNode = leafNode,
                            RightNode = i + 1 == root.BTreeElements.Count ? (root.BTreeElements[i].RightNode) : root.BTreeElements[i + 1].LeftNode
                            // 若插入位置為尾端，則right node is ?上個元素的右子樹，否則right node為下個el的left node
                        };
                        root.BTreeElements[i].RightNode = leafNode;
                        root.BTreeElements.Insert(i + 1, el);
                        break;
                    }
                    else flag = 1;
                }
                else if (key < root.BTreeElements[i].Key)
                {
                    if (flag == 1 || i == 0) // insert
                    {
                        var leafEl = new BTreeElement { Key = key, RowID = rowID };
                        var leafNode = new BTreeNode { BTreeElements = new List<BTreeElement> { leafEl }, ParentNode = root };
                        var el = new BTreeElement { Key = key, LeftNode = leafNode, RightNode = root.BTreeElements[i].LeftNode };

                        if (i != 0) root.BTreeElements[i - 1].RightNode = leafNode;
                        root.BTreeElements.Insert(i, el);
                        break;
                    }
                    else flag = -1;
                }
            }
        }
        private static BTreeNode ProcessSplitNode(BTreeNode parentNode, BTreeElement overedEl)
        {
            if (parentNode == null)
            {
                var parent = new BTreeNode { BTreeElements = new List<BTreeElement>() };
                overedEl.LeftNode.ParentNode = parent;
                overedEl.RightNode.ParentNode = parent;
                parent.BTreeElements.Add(overedEl);
                return parent;
            }
            else
            {
                var parent = parentNode;
                overedEl.LeftNode.ParentNode = parent;
                overedEl.RightNode.ParentNode = parent;
                return TryInsert(overedEl.Key, overedEl.RowID, parentNode, overedEl);
            }
        }

        public static int? Search(int key, BTreeNode root)
        {
            var flag = 0;
            for (var i = 0; i < root.BTreeElements.Count; i++)
            {
                if (key > root.BTreeElements[i].Key)
                {
                    if (i + 1 == root.BTreeElements.Count) // 比最右邊的元素大，往右子樹找
                    {
                        var nextNode = root.BTreeElements[i].RightNode;
                        if (nextNode == null) return null;
                        return Search(key, nextNode);
                    }
                    else flag = 1;
                }
                else if (root.BTreeElements[i].Key == key)
                {
                    var nextNode = root.BTreeElements[i].LeftNode;
                    if (root.IsLeaf) return root.BTreeElements[i].RowID.Value;
                    if (nextNode == null) return null;
                    return Search(key, nextNode);
                }
                else
                {
                    if (i == 0) // 比最左邊的元素小，往左子樹找
                    {
                        var nextNode = root.BTreeElements[i].LeftNode;
                        if (nextNode == null) return null;
                        return Search(key, nextNode);
                    }
                    else if (flag == 1)
                    {
                        var nextNode = root.BTreeElements[i - 1].RightNode;
                        if (nextNode == null) return null;
                        return Search(key, nextNode);
                    }
                    else throw new ArgumentException($"key {key} not found.");
                }
            }
            return null;
        }
    }

    public class BTreeNode
    {
        public int ContainLength => 4;
        public bool IsRoot => ParentNode == null;
        public bool IsLeaf => BTreeElements != null && BTreeElements.Any(x => x.RowID.HasValue);
        public bool IsFull => !IsLeaf && BTreeElements.Count == ContainLength;
        public List<BTreeElement> BTreeElements { get; set; }
        [JsonIgnore]
        public BTreeNode ParentNode { get; set; }
    }

    public class BTreeElement
    {
        public int Key { get; set; }

        public int? RowID { get; set; }

        public BTreeNode LeftNode { get; set; }

        public BTreeNode RightNode { get; set; }
    }
}
