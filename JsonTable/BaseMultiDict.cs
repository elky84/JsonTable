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
            var property = typeof(T).GetProperties().FirstOrDefault(x => x.Name == Key);
            if (null == property)
            {
                throw new Exception($"Not found keyProperty from T. <KeyProperty:{Key}, T:{typeof(T).Name}>");
            }

            foreach (var t in list)
            {
                var value = property.GetValue(t!);
                var key = property.ReflectedType!.IsEnum ?
                    (K)Enum.Parse(property.ReflectedType!, (string)value!) :
                    ((K)value)!;

                Add(key, t!);
            }

            return Dictionary;
        }
    }
}
