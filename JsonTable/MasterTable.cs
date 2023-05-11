using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace JsonTable
{
    public static class MasterTable
    {
        private static readonly Dictionary<Type, BaseTable> LoadedTableDict = new Dictionary<Type, BaseTable>();

        static MasterTable()
        {
        }

        public static Dictionary<TK, TV> Merge<TK, TV>(IEnumerable<Dictionary<TK, TV>> dictionaries)
        {
            return dictionaries.SelectMany(x => x)
                            .ToDictionary(x => x.Key, y => y.Value);
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

            foreach (var table in assembly!.GetTypes()
                    .Where(x => x.IsSubclassOf(typeof(BaseTable)) && x.Namespace == assemblyName))
            {
                var instance = Activator.CreateInstance(table) as BaseTable;
                LoadedTableDict.Add(table, instance!);
            }
        }

        public static void Add<T>(BaseTable baseTable) where T : class
        {
            LoadedTableDict.Add(typeof(T), baseTable);
        }
        public static T From<T>() where T : BaseTable
        {
            if (LoadedTableDict!.TryGetValue(typeof(T), out var value) == false)
            {
                throw new Exception($"Not Found Table. <Table:{typeof(T)}>");
            }

            return (value as T)!;
        }
    }
}
