using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevilleSolutions.Commons.Extensions
{
    public static class EnumerableExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<int, T> action)
        {
            var i = 0;

            foreach (var item in enumerable)
            {
                action(i, item);
                i++;
            }
        }

        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            enumerable.ForEach((i, t) => action(t));
        }

        public static void InParallel<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            Parallel.ForEach(enumerable, action);
        }

        public static IEnumerable<TResult> SelectManyInParallel<TSource, TResult>(this IEnumerable<TSource> enumerable, Func<TSource, IEnumerable<TResult>> selector)
        {
            var results = new List<TResult>();
            Parallel.ForEach(enumerable, item => Parallel.ForEach(selector(item), results.Add));
            return results;
        }
    }
}