using System;
using System.Collections.Generic;
using System.Linq;

namespace DevilleSolutions.Commons.Extensions
{
    public static class TypeExtensions
    {
         public static IEnumerable<Type> BasedOn(this IEnumerable<Type> types, Type baseType)
         {
             return types.Where(baseType.IsAssignableFrom);
         }

        public static IEnumerable<Type> BasedOn<T>(this IEnumerable<Type> types)
            where T : class
        {
            return types.BasedOn(typeof (T));
        }
    }
}