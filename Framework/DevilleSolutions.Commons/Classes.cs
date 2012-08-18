using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DevilleSolutions.Commons
{
    public static class Classes
    {
         public static IEnumerable<Type> FromAssemblies(params Assembly[] assemblies)
         {
             return
                 assemblies.SelectMany(a => a.GetExportedTypes()).Where(
                     t => !t.IsInterface && !t.IsAbstract && !t.IsValueType);
         }

        public static IEnumerable<Type> FromAssembly(Assembly assembly)
        {
            return FromAssemblies(assembly);
        }

        public static IEnumerable<Type> FromThisAssembly()
        {
            return FromAssembly(Assembly.GetCallingAssembly());
        }
    }
}