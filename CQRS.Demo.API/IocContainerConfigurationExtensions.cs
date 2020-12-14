using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace CQRS.Demo.API
{
    public static class IocContainerConfigurationExtensions
    {
        private static readonly object AssemblyScanResultLock = new object();
        private static Dictionary<Type, Type> _assemblyScanResult;

        private static readonly string[] DefaultContainerControlledTypes = {
            "Service",
            "ServiceComponent",
            "Repository",
            "ServiceClient",
            "Mapping",
            "Factory",
            "Helper"
        };

        private static readonly string[] DefaultTransientTypes = {
            "Mapper",
            "Builder",
            "Query",
            "Command"
        };

        private static readonly string[] DefaultSingletonTypes = { };

        public static void RegisterTypesContainerControlledByConvention(Action<Type, Type> registerAction, Assembly[] assemblies, string[] typeNameEndsToRegister = null)
        {
            var assemblyScanResult = GetAssemblyScanResult(assemblies);

            foreach (var interfaceInstanceCombination in assemblyScanResult)
            {
                if (!interfaceInstanceCombination.Key.IsInterface)
                {
                    var configureIocScopeAttr = interfaceInstanceCombination.Key.GetCustomAttribute<ConfigureIocAttribute>();
                    if (configureIocScopeAttr.ScopeType == ScopeType.Scoped)
                    {
                        registerAction(interfaceInstanceCombination.Key, interfaceInstanceCombination.Value);
                    }
                }
                else if (DoesTypeNameEndsWithConvention(interfaceInstanceCombination.Key, typeNameEndsToRegister ?? DefaultContainerControlledTypes))
                {
                    registerAction(interfaceInstanceCombination.Key, interfaceInstanceCombination.Value);
                }
            }
        }

        public static void RegisterTypesTransientByConvention(Action<Type, Type> registerAction, Assembly[] assemblies, string[] typeNameEndsToRegister = null)
        {
            var assemblyScanResult = GetAssemblyScanResult(assemblies);

            foreach (var interfaceInstanceCombination in assemblyScanResult)
            {
                if (!interfaceInstanceCombination.Key.IsInterface)
                {
                    var configureIocScopeAttr = interfaceInstanceCombination.Key.GetCustomAttribute<ConfigureIocAttribute>();
                    if (configureIocScopeAttr.ScopeType == ScopeType.Transient)
                    {
                        registerAction(interfaceInstanceCombination.Key, interfaceInstanceCombination.Value);
                    }
                }
                else if (DoesTypeNameEndsWithConvention(interfaceInstanceCombination.Key, typeNameEndsToRegister ?? DefaultTransientTypes))
                {
                    registerAction(interfaceInstanceCombination.Key, interfaceInstanceCombination.Value);
                }
            }
        }

        public static void RegisterTypesSingletonByConvention(Action<Type, Type> registerAction, Assembly[] assemblies, string[] typeNameEndsToRegister = null)
        {
            var assemblyScanResult = GetAssemblyScanResult(assemblies);

            foreach (var interfaceInstanceCombination in assemblyScanResult)
            {
                if (!interfaceInstanceCombination.Key.IsInterface)
                {
                    var configureIocScopeAttr = interfaceInstanceCombination.Key.GetCustomAttribute<ConfigureIocAttribute>();
                    if (configureIocScopeAttr.ScopeType == ScopeType.Singleton)
                    {
                        registerAction(interfaceInstanceCombination.Key, interfaceInstanceCombination.Value);
                    }
                }
                else if (DoesTypeNameEndsWithConvention(interfaceInstanceCombination.Key, typeNameEndsToRegister ?? DefaultSingletonTypes))
                {
                    registerAction(interfaceInstanceCombination.Key, interfaceInstanceCombination.Value);
                }
            }
        }

        private static bool DoesTypeNameEndsWithConvention(Type interfaceType, string[] typeNameEndsToRegister)
        {
            foreach (var nameToCheck in typeNameEndsToRegister)
            {
                if (interfaceType.Name.EndsWith(nameToCheck))
                {
                    return true;
                }
            }

            return false;
        }

        private static Dictionary<Type, Type> GetAssemblyScanResult(Assembly[] assemblies)
        {
            if (_assemblyScanResult == null)
            {
                lock (AssemblyScanResultLock)
                {
                    if (_assemblyScanResult == null)
                    {
                        var interfaceInstanceCombinations = new Dictionary<Type, Type>();

                        var types = assemblies.SelectMany(a => a.GetTypes()).ToArray();

                        // Find all interfaces
                        var interfaces = types.Where(t => t.IsInterface).ToArray();
                        Parallel.ForEach(interfaces, interfaceType =>
                        {
                            // Exclude all types that are not managed by Unity
                            if (!IsTypeManagableByIocContainer(interfaceType))
                            {
                                return;
                            }

                            // If the interface has 1 possible implementation, configure this combination in Unity
                            var instances = types.Where(t => t.Namespace != null && t.Namespace.StartsWith("CQRS", StringComparison.InvariantCultureIgnoreCase)
                                                             && interfaceType.IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract).ToArray();
                            if (instances.Length == 1)
                            {
                                var instanceType = instances[0];
                                lock (interfaceInstanceCombinations)
                                {
                                    interfaceInstanceCombinations.Add(interfaceType, instanceType);
                                }
                            }
                        });

                        var transientTypes = types.Where(t => t.GetCustomAttributes<ConfigureIocAttribute>().Any());
                        foreach (var transientType in transientTypes)
                        {
                            interfaceInstanceCombinations.Add(transientType, transientType);
                        }

                        _assemblyScanResult = interfaceInstanceCombinations;
                    }
                }
            }

            return _assemblyScanResult;
        }

        private static bool IsTypeManagableByIocContainer(Type interfaceType)
        {
            // Only include Agrovision interfaces
            return interfaceType.Namespace != null && interfaceType.Namespace.StartsWith("CQRS", StringComparison.InvariantCultureIgnoreCase)
                   // Exclude types that have a generic argument in the interface
                   && interfaceType.GetGenericArguments().Length == 0;
        }
    }
}