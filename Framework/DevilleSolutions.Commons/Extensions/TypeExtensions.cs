using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

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

        public static IEnumerable<Type> ExcludingTypesBasedOn(this IEnumerable<Type> types, Type baseType)
        {
            return types.Where(type => !baseType.IsAssignableFrom(type));
        }

        public static IEnumerable<Type> ExcludingTypesBasedOn<T>(this IEnumerable<Type> types)
        {
            return types.ExcludingTypesBasedOn(typeof (T));
        }

        public static IEnumerable<Type> InTheSameNamespaceAs(this IEnumerable<Type> types, Type type)
        {
            return types.Where(t => t.Namespace == type.Namespace);
        }

        public static IEnumerable<Type> InTheSameNamespaceAs<T>(this IEnumerable<Type> types)
        {
            return types.InTheSameNamespaceAs(typeof (T));
        }

        public static IEnumerable<PropertyInfo> Properties(this Type type)
        {
            var props = new List<PropertyInfo>();
            type.CollectProperties(ref props);
            return props;
        }

        private static void CollectProperties(this Type type, ref List<PropertyInfo> props)
        {
            props.AddRange(type.GetProperties());
            foreach(var i in type.GetInterfaces())
            {
                i.CollectProperties(ref props);
            }
        }
    }
}