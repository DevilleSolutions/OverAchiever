using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using DevilleSolutions.Commons.Extensions;

namespace DevilleSolutions.Commons.Proxies
{
    public static class TypeBuilderExtensions
    {
         public static FieldBuilder DefineProperty(this TypeBuilder typeBuilder, PropertyInfo property)
         {
             var field = typeBuilder.DefineField("_" + property.Name.ToLower(), property.PropertyType,
                                                 FieldAttributes.Private);

             const MethodAttributes getSetAttr =
                MethodAttributes.Public | MethodAttributes.Virtual;

             var getMethod = property.GetGetMethod();
             var propertyBuilder = typeBuilder.DefineProperty(property.Name,
                                                              PropertyAttributes.HasDefault,
                                                              property.PropertyType, null);
             if (getMethod != null)
             {
                 var getter = typeBuilder.DefineMethod(getMethod.Name, getSetAttr,
                                                       property.PropertyType, Type.EmptyTypes);

                 var il = getter.GetILGenerator();
                 il.Emit(OpCodes.Ldarg_0);
                 il.Emit(OpCodes.Ldfld, field);
                 il.Emit(OpCodes.Ret);

                 propertyBuilder.SetGetMethod(getter);
             }

             var setMethod = property.GetSetMethod();
             if (setMethod != null)
             {
                 var setter = typeBuilder.DefineMethod(setMethod.Name, getSetAttr,
                                                       typeof(void), new[] { property.PropertyType });
                 var il = setter.GetILGenerator();
                 il.Emit(OpCodes.Ldarg_0);
                 il.Emit(OpCodes.Ldarg_1);
                 il.Emit(OpCodes.Stfld, field);
                 il.Emit(OpCodes.Ret);

                 propertyBuilder.SetSetMethod(setter);
             }

             return field;
         }

        public static void DefineDefaultConstructor(this TypeBuilder typeBuilder)
        {
            var defaultConstructorInfo = typeof(object).GetConstructor(Type.EmptyTypes);

            var defaultConstructor = typeBuilder.DefineConstructor(MethodAttributes.Public,
                                                                   CallingConventions.Standard,
                                                                   Type.EmptyTypes);

            var defaultConstructorGen = defaultConstructor.GetILGenerator();
            defaultConstructorGen.Emit(OpCodes.Ldarg_0);
            defaultConstructorGen.Emit(OpCodes.Call, defaultConstructorInfo);
            defaultConstructorGen.Emit(OpCodes.Ret);
        }

        public static void DefineConstructor(this TypeBuilder typeBuilder, IEnumerable<FieldBuilder> parameters)
        {
            var defaultConstructorInfo = typeof(object).GetConstructor(Type.EmptyTypes);
            var constructor = typeBuilder.DefineConstructor(MethodAttributes.Public,
                                                            CallingConventions.Standard,
                                                            parameters.Select(fb => fb.FieldType).ToArray());


            var il = constructor.GetILGenerator();
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Call, defaultConstructorInfo);

            parameters.ForEach((index, param) =>
            {
                constructor.DefineParameter(index + 1, ParameterAttributes.None,
                                            param.Name.Replace("_", String.Empty));
                il.Emit(OpCodes.Ldarg_0);
                il.Emit(OpCodes.Ldarg, index + 1);
                il.Emit(OpCodes.Stfld, param);
            });

            il.Emit(OpCodes.Ret);
        }
    }
}