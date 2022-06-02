using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace JsonTable
{
    public static class MasterTable
    {
        private static Dictionary<Type, BaseTable>? _loadedTableDict = new Dictionary<Type, BaseTable>();

        static MasterTable()
        {
        }

        public static void Load(string assemblyName)
        {
            var assembly = string.IsNullOrEmpty(assemblyName) ?
                Assembly.GetEntryAssembly() :
                AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(x =>
                {
                    var name = x.GetName().Name;
                    return name != null && assemblyName.StartsWith(name);
                });

            _loadedTableDict = assembly!.GetTypes()
                .Where(x => x.IsSubclassOf(typeof(BaseTable)) && x.Namespace == assemblyName)
                .ToDictionary(x => x, x =>
                {
                    var instance = Activator.CreateInstance(x);
                    return (instance as BaseTable)!;
                });
        }

        public static T? From<T>() where T : BaseTable
        {
            if (_loadedTableDict!.TryGetValue(typeof(T), out var value) == false)
                return null;

            return value as T;
        }
    }
}
