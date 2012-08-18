using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using DevilleSolutions.Commons.Extensions;

namespace DevilleSolutions.Commons.Proxies
{
    public class ProxyBuilder
    {
        public const string ProxyAssemblyName = "OverAchiever.Proxies";

        private readonly ModuleBuilder _moduleBuilder;
        private readonly IDictionary<Type, Type> _proxyCache;
        private readonly AssemblyBuilder _assBuilder;

        public ProxyBuilder()
        {
            _proxyCache = new Dictionary<Type, Type>();

            _assBuilder =
                AppDomain.CurrentDomain.DefineDynamicAssembly(new AssemblyName(ProxyAssemblyName),
                                                              AssemblyBuilderAccess.RunAndSave, AppDomain.CurrentDomain.BaseDirectory + "bin/");
            _moduleBuilder = _assBuilder.DefineDynamicModule("Proxies", "OverAchiever.Proxies.dll");
        }

        public Type BuildTypeImplementing<T>()
            where T : class
        {
            return BuildTypeImplementing(typeof (T));
        }

        public Type BuildTypeImplementing(Type iface)
        {
            if (!iface.IsInterface)
                throw new ArgumentException(string.Format("{0} must be an interface in order to create proxy",
                                                          iface.Name));

            if (!_proxyCache.ContainsKey(iface))
            {
                _proxyCache.Add(iface, CreateProxyOf(iface));
            }

            return _proxyCache[iface];
        }

        private void GetPropertiesOfTRecursive(Type type, ref IList<PropertyInfo> properties)
        {
            foreach(var prop in type.GetProperties())
            {
                properties.Add(prop);
            }

            foreach(var subInterface in type.GetInterfaces())
            {
                GetPropertiesOfTRecursive(subInterface, ref properties);
            }
        }

        private void GetMethodsOfTRecursive(Type type, ref IList<MethodInfo> methods)
        {
            foreach(var method in type.GetMethods())
            {
                methods.Add(method);
            }

            foreach(var subInterface in type.GetInterfaces())
            {
                GetMethodsOfTRecursive(subInterface, ref methods);
            }
        }

        private Type CreateProxyOf(Type iface)
        {
            var typeBuilder = _moduleBuilder.DefineType(string.Format("{0}Proxy", iface.Name),
                                                                TypeAttributes.Public);
            typeBuilder.AddInterfaceImplementation(iface);

            IList<PropertyInfo> properties = new List<PropertyInfo>();
            GetPropertiesOfTRecursive(iface, ref properties);

            IList<MethodInfo> methods = new List<MethodInfo>();
            GetMethodsOfTRecursive(iface, ref methods);

            //return typeBuilder.CreateType();
            var fields = properties.ToDictionary(pi => pi.Name,
                                                                              prop =>
                                                                              typeBuilder.DefineField(
                                                                                  string.Format("_{0}", prop.Name.ToLower()),
                                                                                  prop.PropertyType,
                                                                                  FieldAttributes.Private));

            AddDefaultConstructor(typeBuilder);
            AddConstructorForReadOnlyProperties(typeBuilder, properties.Where(pi => pi.CanWrite == false && pi.CanRead), fields);
            
            const MethodAttributes getSetAttr =
                MethodAttributes.Public | MethodAttributes.Virtual;

            foreach(var property in properties)
            {
                var getMethod = property.GetGetMethod();

                if (getMethod != null)
                {
                    methods.Remove(getMethod);
                    var getter = typeBuilder.DefineMethod(getMethod.Name, getSetAttr,
                                                          property.PropertyType, Type.EmptyTypes);

                    var il = getter.GetILGenerator();
                    il.Emit(OpCodes.Ldarg_0);
                    il.Emit(OpCodes.Ldfld, fields[property.Name]);
                    il.Emit(OpCodes.Ret);

                    typeBuilder.DefineMethodOverride(getter, getMethod);
                }

                var setMethod = property.GetSetMethod();
                if (setMethod != null)
                {
                    methods.Remove(setMethod);
                    var setter = typeBuilder.DefineMethod(setMethod.Name, getSetAttr,
                                                          typeof(void), new [] { property.PropertyType });
                    var il = setter.GetILGenerator();
                    il.Emit(OpCodes.Ldarg_0);
                    il.Emit(OpCodes.Ldarg_1);
                    il.Emit(OpCodes.Stfld, fields[property.Name]);
                    il.Emit(OpCodes.Ret);

                    typeBuilder.DefineMethodOverride(setter, setMethod);
                }
            }

            return typeBuilder.CreateType();
        }

        private static void AddConstructorForReadOnlyProperties(TypeBuilder typeBuilder, IEnumerable<PropertyInfo> properties, IDictionary<string, FieldBuilder> fields)
        {
            var defaultConstructorInfo = typeof (object).GetConstructor(Type.EmptyTypes);
            var constructor = typeBuilder.DefineConstructor(MethodAttributes.Public,
                                                            CallingConventions.Standard, 
                                                            properties.Select(pi => pi.PropertyType).ToArray());


            var il = constructor.GetILGenerator();
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Call, defaultConstructorInfo);

            properties.ForEach((index, property) =>
                                   {
                                       constructor.DefineParameter(index + 1, ParameterAttributes.None,
                                                                   property.Name.ToLower());
                                       il.Emit(OpCodes.Ldarg_0);
                                       il.Emit(OpCodes.Ldarg, index + 1);
                                       il.Emit(OpCodes.Stfld, fields[property.Name]);
                                   });

            il.Emit(OpCodes.Ret);
        }

        private static void AddDefaultConstructor(TypeBuilder typeBuilder)
        {
            var defaultConstructorInfo = typeof (object).GetConstructor(Type.EmptyTypes);

            var defaultConstructor = typeBuilder.DefineConstructor(MethodAttributes.Public,
                                                                   CallingConventions.Standard,
                                                                   Type.EmptyTypes);

            var defaultConstructorGen = defaultConstructor.GetILGenerator();
            defaultConstructorGen.Emit(OpCodes.Ldarg_0);
            defaultConstructorGen.Emit(OpCodes.Call, defaultConstructorInfo);
            defaultConstructorGen.Emit(OpCodes.Ret);
        }
    }
}