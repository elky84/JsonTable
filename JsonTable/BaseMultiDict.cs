using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace JsonTable
{
    public class BaseMultiDict<K, T> : BaseTable
        where T : class where K : notnull
    {
        protected Dictionary<K, List<T>> Dictionary = new Dictionary<K, List<T>>();

        public Dictionary<K, List<T>> Clone => new Dictionary<K, List<T>>(Dictionary);

        public Dictionary<K, List<T>> Container => Dictionary;

        protected void Add(K key, T value)
        {
            if (Dictionary.TryGetValue(key, out var list))
            {
                list.Add(value);
            }
            else
            {
                this.Dictionary[key] = new List<T>
                {
                    value
                };
            }
        }

        public IEnumerable<K> Keys => this.Dictionary.Keys;

        public string[] KeyDefines => Key.Split(",").Select(x => x.Trim()).ToArray();

        public List<T> this[K key]
        {
            get
            {
                if (this.Dictionary.TryGetValue(key, out var list)) return list;
                list = new List<T>();
                this.Dictionary[key] = list;
                return list;
            }
        }

        protected override void Load()
        {
            var json = Json();
            if (string.IsNullOrEmpty(json))
            {
                json = File.ReadAllText($"{FullPath}/{typeof(T).Name}.json");
            }

            var list = JsonConvert.DeserializeObject<List<T>>(json); if (list != null)
            {
                Dictionary = OnLoad(list);
            }
        }

        protected virtual Dictionary<K, List<T>> OnLoad(List<T> list)
        {
            if (KeyDefines == null || KeyDefines.Length == 0)
            {
                throw new Exception("Keys must contain at least one property name.");
            }

            var properties = KeyDefines.Select(key => typeof(T).GetProperties().FirstOrDefault(p => p.Name == key)).ToList();

            if (properties.Any(p => p == null))
            {
                throw new Exception($"Some key properties were not found in type {typeof(T).Name}. " +
                                    $"KeyDefines:{string.Join(",", KeyDefines)}");
            }

            foreach (var t in list)
            {
                var keyValues = properties.Select(p =>
                {
                    var value = p!.GetValue(t);
                    return p.ReflectedType!.IsEnum ? Enum.Parse(p.ReflectedType!, value!.ToString()!) : value;
                }).ToArray();
        
                if (keyValues.Any(v => v == null))
                {
                    throw new Exception("One or more key properties have null values. " +
                                        $"KeyDefines:{string.Join(",", KeyDefines)}");
                }

                K key;
                if (keyValues.Length == 1)
                {
                    key = (K)keyValues[0]!;
                }
                else
                {
                    key = CreateTypedValueTuple(keyValues);
                }

                Add(key, t!);
            }

            return Dictionary;
        }

        private static K CreateTypedValueTuple(object[] values)
        {
            Type tupleType = typeof(K);
    
            if (!tupleType.IsValueType || !tupleType.FullName!.StartsWith("System.ValueTuple"))
            {
                throw new Exception($"Invalid tuple type {tupleType}. Expected a System.ValueTuple.");
            }

            return (K)Activator.CreateInstance(tupleType, values)!;
        }


    }
}
