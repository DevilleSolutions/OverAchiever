using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using DevilleSolutions.Commons.Extensions;

namespace DevilleSolutions.Commons.Proxies
{
    public class ProxyBuilder : IProxyBuilder
    {
        private ICollection<Type> _interfaces;
        private readonly IDictionary<string, AssemblyBuilder> _assemblyBuilders;
        private bool _includeDefaultConstructor;

        private Type Base
        {
            get
            {
                return _interfaces.First() ?? typeof (object);
            }
        }

        public ProxyBuilder()
        {
            _interfaces = new List<Type>();
            _assemblyBuilders = new Dictionary<string, AssemblyBuilder>();
        }

        public IProxyBuilder Implementing(Type t)
        {
            if (!t.IsInterface)
                throw new InvalidOperationException(t.FullName + " must be an interface.");

            _interfaces.Add(t);

            return this;
        }

        public IProxyBuilder IncludeADefaultConstructor()
        {
            _includeDefaultConstructor = true;

            return this;
        }

        public Type Build()
        {
            var module = GetModule();
            var type = module.GetType(_interfaces.First().Name + "Proxy");

            if (type == null)
            {
                var typeBuilder = module.DefineType(_interfaces.First().Name + "Proxy", TypeAttributes.Public);

                _interfaces.ForEach(typeBuilder.AddInterfaceImplementation);
                Dictionary<PropertyInfo, FieldBuilder> properties = _interfaces.SelectManyInParallel(i => i.Properties())
                    .ToDictionary(prop => prop, prop => typeBuilder.DefineProperty(prop));

                if (_includeDefaultConstructor) typeBuilder.DefineDefaultConstructor();

                IEnumerable<FieldBuilder> constructorParams =
                    properties.Where(prop => prop.Key.CanRead && !prop.Key.CanWrite).Select(prop => prop.Value);

                if (constructorParams.Any()) typeBuilder.DefineConstructor(constructorParams);

                type = typeBuilder.CreateType();
                _interfaces = new List<Type>();
            }

            return type;
        }

        public void SaveProxyAssemblies()
        {
            _assemblyBuilders.Values.ForEach(builder => builder.Save("TEST"));
        }

        private ModuleBuilder GetModule()
        {
            var assName = _interfaces.First().Assembly.GetName().Name;
            var assemblyName = new AssemblyName(assName + ".Proxies")
                                   {Version = _interfaces.First().Assembly.GetName().Version};

            if (!_assemblyBuilders.ContainsKey(assemblyName.Name))
            {
                _assemblyBuilders.Add(assemblyName.Name,
                                      AppDomain.CurrentDomain.DefineDynamicAssembly(
                                          assemblyName,
                                          AssemblyBuilderAccess.RunAndSave,
                                          Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin")));
            }

            return _assemblyBuilders[assemblyName.Name].GetDynamicModule(_interfaces.First().Namespace) ?? _assemblyBuilders[assemblyName.Name].DefineDynamicModule(_interfaces.First().Namespace, assemblyName.Name + ".dll");

        }
    }
}