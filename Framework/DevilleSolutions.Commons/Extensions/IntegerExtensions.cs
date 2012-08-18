using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevilleSolutions.Commons.Extensions
{
    public static class IntegerExtensions
    {
        public static IEnumerable<T> Times<T>(this int count, Func<T> func)
        {
            return count.Times(i => func());
        }

        public static IEnumerable<T> Times<T>(this int count, Func<int, T> func)
        {
            var results = new List<T>();

            for(var i=0; i<count; i++)
            {
                results.Add(func(i));
            }

            return results;
        }

        public static IEnumerable<T> TimesInParallel<T>(this int count, Func<int, T> func)
        {
            var results = new List<T>();

            Parallel.For(0, count, i => results.Add(func(i)));

            return results;
        }

        public static void Times(this int count, Action action)
        {
            for(var i=0; i<count; i++)
            {
                action();
            }
        }
         
    }
}