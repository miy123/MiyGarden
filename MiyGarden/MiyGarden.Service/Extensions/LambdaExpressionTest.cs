using System;
using System.Collections.Generic;
using System.Linq;

namespace MiyGarden.Service.Extensions
{
    public static class LambdaExpressionTest
    {
        public static IEnumerable<TSource> Distinct<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> predicate)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));

            HashSet<TKey> seenKeys = new HashSet<TKey>();
            foreach (TSource item in source)
            {
                TKey value = predicate(item);
                if (seenKeys.Add(value)) yield return item;
            }
        }
    }

    public static class EnumerableExtensions
    {
        // 向上尋訪所有父節點
        public static IEnumerable<T> Traverse<T>(this IEnumerable<T> items, Func<T, T> parentSelector)
        {
            var stack = new Stack<T>(items);
            while (stack.Any())
            {
                var item = stack.Pop();
                yield return item;

                // get item's parent
                var parent = parentSelector(item);
                if (parent != null)
                {
                    // push parent in stack
                    // find parent's parent later
                    stack.Push(parent);
                }
            }
        }

        // 向下尋訪所有子節點
        public static IEnumerable<T> Traverse<T>(this IEnumerable<T> items, Func<T, IEnumerable<T>> childSelector)
        {
            var stack = new Stack<T>(items);
            while (stack.Any())
            {
                var item = stack.Pop();
                yield return item;

                // get item's children
                foreach (var child in childSelector(item))
                {
                    // push each child in stack
                    // find child's children later
                    stack.Push(child);
                }
            }
        }

    }
}
