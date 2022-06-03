using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace JsonTable
{
    public class BaseDict<K, T> : BaseTable, IReadOnlyDictionary<K, T> where T : class where K : notnull
    {
        private Dictionary<K, T> _dictionary = new Dictionary<K, T>();

        public T? this[K key]
        {
#pragma warning disable CS8766 // 반환 형식에서 참조 형식의 null 허용 여부가 암시적으로 구현된 멤버와 일치하지 않음(null 허용 여부 특성 때문일 수 있음)
            get
#pragma warning restore CS8766 // 반환 형식에서 참조 형식의 null 허용 여부가 암시적으로 구현된 멤버와 일치하지 않음(null 허용 여부 특성 때문일 수 있음)
            {
                return _dictionary.TryGetValue(key, out var value) ? value : null;
            }
        }

        public IEnumerable<K> Keys => _dictionary.Keys;

        public IEnumerable<T> Values => _dictionary.Values;

        public Dictionary<K, T> Clone => new Dictionary<K, T>(_dictionary);

        public int Count => _dictionary.Count;

        public bool ContainsKey(K key) => _dictionary.ContainsKey(key);

        public IEnumerator<KeyValuePair<K, T>> GetEnumerator() => _dictionary.GetEnumerator();

        public bool TryGetValue(K key, out T value) => _dictionary.TryGetValue(key, out value!);

        protected override void Load()
        {
            var list = JsonConvert.DeserializeObject<List<T>>(File.ReadAllText($"{FullPath}/{typeof(T).Name}.json"));
            if (list != null)
            {
                _dictionary = OnLoad(list);
            }
        }

        protected virtual Dictionary<K, T> OnLoad(List<T> list)
        {
            var property = typeof(T).GetProperties().FirstOrDefault(x => x.Name == Key);
            if (null == property)
            {
                throw new Exception($"Not found keyProperty from T. <KeyProperty:{Key}, T:{typeof(T).Name}>");
            }

            return list.ToDictionary(x => ((K)property.GetValue(x))!, x => x);
        }

        IEnumerator IEnumerable.GetEnumerator() => _dictionary.GetEnumerator();
    }
}
