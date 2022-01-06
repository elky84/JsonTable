using Newtonsoft.Json;
using System.Collections;

namespace JsonTable
{
    public class BaseDict<K, T> : BaseTable, IReadOnlyDictionary<K, T> where T : class where K : notnull
    {
        private Dictionary<K, T?>? _dictionary = new();

        public T? this[K key]
        {
#pragma warning disable CS8766 // 반환 형식에서 참조 형식의 null 허용 여부가 암시적으로 구현된 멤버와 일치하지 않음(null 허용 여부 특성 때문일 수 있음)
            get
#pragma warning restore CS8766 // 반환 형식에서 참조 형식의 null 허용 여부가 암시적으로 구현된 멤버와 일치하지 않음(null 허용 여부 특성 때문일 수 있음)
            {
                return _dictionary!.TryGetValue(key, out var value) ? value : null;
            }
        }

        public IEnumerable<K> Keys => _dictionary!.Keys;

#pragma warning disable CS8613 // 반환 형식에 있는 참조 형식 Null 허용 여부가 암시적으로 구현된 멤버와 일치하지 않습니다.
        public IEnumerable<T?> Values => _dictionary!.Values;
#pragma warning restore CS8613 // 반환 형식에 있는 참조 형식 Null 허용 여부가 암시적으로 구현된 멤버와 일치하지 않습니다.

        public int Count => _dictionary!.Count;

        public bool ContainsKey(K key) => _dictionary!.ContainsKey(key);

        public IEnumerator<KeyValuePair<K, T>> GetEnumerator() => _dictionary!.GetEnumerator();

#pragma warning disable CS8767 // 반환 형식에서 참조 형식의 null 허용 여부가 암시적으로 구현된 멤버와 일치하지 않음(null 허용 여부 특성 때문일 수 있음)
        public bool TryGetValue(K key, out T? value) => _dictionary!.TryGetValue(key, out value);
#pragma warning restore CS8767 // 반환 형식에서 참조 형식의 null 허용 여부가 암시적으로 구현된 멤버와 일치하지 않음(null 허용 여부 특성 때문일 수 있음)

        protected override void Load()
        {
            var property = typeof(T).GetProperties().FirstOrDefault(x => x.Name == "Id");

            var deserialized = JsonConvert.DeserializeObject<List<T?>>(File.ReadAllText(Path));
            if(deserialized != null)
            {
                _dictionary = deserialized.ToDictionary(x => ((K?)property!.GetValue(x))!, x => x);
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => _dictionary!.GetEnumerator();
    }
}
