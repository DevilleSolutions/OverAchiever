using System;
using System.Collections.Generic;

namespace OverAchiever.Web.Extensions
{
    public static class IntegerExtensions
    {
        public static IEnumerable<T> Times<T>(this int count, Func<T> func)
        {
            var results = new List<T>();

            for(var i=0; i<count; i++)
            {
                results.Add(func());
            }

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