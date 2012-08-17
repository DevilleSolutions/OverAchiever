using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace OverAchiever.Web.Plumbing
{
    public static class ProxyGenerator
    {
        private const string ProxyAssemblyName = "OverAchiever.Proxies";

        private static readonly IDictionary<Type, Type> _cachedProxyTypes = new Dictionary<Type, Type>();

        public static Type CreateTypeImplementing<T>()
        {
            return CreateTypeImplementing(typeof (T));
        }

        public static Type CreateTypeImplementing(Type iface)
        {
            if (!_cachedProxyTypes.ContainsKey(iface))
            {
                Type proxy = BuildTypeFromInterface(iface);
                _cachedProxyTypes.Add(iface, proxy);
            }

            return _cachedProxyTypes[iface];
        }

        private static Type BuildTypeFromInterface(Type typeOfT)
        {
            var propInfos = typeOfT.GetProperties();


            var methodInfos = typeOfT.GetMethods();
            var assName = new AssemblyName(ProxyAssemblyName);
            var assBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(assName, AssemblyBuilderAccess.RunAndSave);
            var moduleBuilder = assBuilder.DefineDynamicModule("Proxies", "Overachiever.Proxies.dll");
            var typeBuilder = moduleBuilder.DefineType(typeOfT.Name + "Proxy", TypeAttributes.Public);

            typeBuilder.AddInterfaceImplementation(typeOfT);
            var ctorBuilder = typeBuilder.DefineConstructor(
                MethodAttributes.Public,
                CallingConventions.Standard,
                new Type[] {});
            var ilGenerator = ctorBuilder.GetILGenerator();
            ilGenerator.EmitWriteLine("Creating Proxy instance");
            ilGenerator.Emit(OpCodes.Ret);
            foreach (var methodInfo in methodInfos)
            {
                var methodBuilder = typeBuilder.DefineMethod(
                    methodInfo.Name,
                    MethodAttributes.Public | MethodAttributes.Virtual,
                    methodInfo.ReturnType,
                    methodInfo.GetParameters().Select(p => p.GetType()).ToArray()
                    );
                var methodILGen = methodBuilder.GetILGenerator();
                if (methodInfo.ReturnType == typeof (void))
                {
                    methodILGen.Emit(OpCodes.Ret);
                }
                else
                {
                    if (methodInfo.ReturnType.IsValueType || methodInfo.ReturnType.IsEnum)
                    {
                        var getMethod = typeof (Activator).GetMethod("CreateInstance", new[] {typeof ((Type)});
                        var lb = methodILGen.DeclareLocal(methodInfo.ReturnType);
                        methodILGen.Emit(OpCodes.Ldtoken, lb.LocalType);
                        methodILGen.Emit(OpCodes.Call, typeofype).GetMethod("GetTypeFromHandle"))
                        ;
                    ))
                        ;
                        methodILGen.Emit(OpCodes.Callvirt, getMethod);
                        methodILGen.Emit(OpCodes.Unbox_Any, lb.LocalType);
                    }
                    else
                    {
                        methodILGen.Emit(OpCodes.Ldnull);
                    }
                    methodILGen.Emit(OpCodes.Ret);
                }
                typeBuilder.DefineMethodOverride(methodBuilder, methodInfo);
            }

            var constructedType = typeBuilder.CreateType();
            return constructedType;
        }
    }
}