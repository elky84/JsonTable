﻿using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace JsonTable
{
    public class BaseDict<TK, T> : BaseTable, IReadOnlyDictionary<TK, T> where T : class where TK : notnull
    {
        private Dictionary<TK, T> _dictionary = new Dictionary<TK, T>();

        public T? this[TK key]
        {
#pragma warning disable CS8766 // 반환 형식에서 참조 형식의 null 허용 여부가 암시적으로 구현된 멤버와 일치하지 않음(null 허용 여부 특성 때문일 수 있음)
            get
#pragma warning restore CS8766 // 반환 형식에서 참조 형식의 null 허용 여부가 암시적으로 구현된 멤버와 일치하지 않음(null 허용 여부 특성 때문일 수 있음)
            {
                return _dictionary.TryGetValue(key, out var value) ? value : null;
            }
        }

        public IEnumerable<TK> Keys => _dictionary.Keys;

        public IEnumerable<T> Values => _dictionary.Values;

        public Dictionary<TK, T> Clone => new Dictionary<TK, T>(_dictionary);

        // ReSharper disable once ConvertToAutoPropertyWithPrivateSetter
        public Dictionary<TK, T> Container => _dictionary;

        public int Count => _dictionary.Count;

        public bool ContainsKey(TK key) => _dictionary.ContainsKey(key);

        public IEnumerator<KeyValuePair<TK, T>> GetEnumerator() => _dictionary.GetEnumerator();

        public bool TryGetValue(TK key, out T value) => _dictionary.TryGetValue(key, out value!);

        protected override void Load()
        {
            var json = Json();
            if (string.IsNullOrEmpty(json))
            {
                json = File.ReadAllText($"{FullPath}/{typeof(T).Name}.json");
            }

            var list = JsonConvert.DeserializeObject<List<T>>(json);
            if (list != null)
            {
                _dictionary = OnLoad(list);
            }
        }

        protected virtual Dictionary<TK, T> OnLoad(IEnumerable<T> list)
        {
            var property = typeof(T).GetProperties().FirstOrDefault(x => x.Name == Key);
            if (null == property)
            {
                throw new Exception($"Not found keyProperty from T. <KeyProperty:{Key}, T:{typeof(T).Name}>");
            }

            return list.ToDictionary(x => ((TK)property.GetValue(x))!, x => x);
        }

        IEnumerator IEnumerable.GetEnumerator() => _dictionary.GetEnumerator();
    }
}
