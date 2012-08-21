using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DevilleSolutions.Commons
{
    public static class Types
    {
        public static IEnumerable<Type> FromAssemblies(params Assembly[] assemblies)
        {
            return
                assemblies.SelectMany(a => a.GetExportedTypes());
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