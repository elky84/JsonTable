using Newtonsoft.Json;

namespace JsonTable
{
    public class BaseMultiDict<K, T> : BaseTable
        where T : class where K : notnull
    {
        protected Dictionary<K, List<T>> _dictionary = new();

        public void Add(K key, T value)
        {
            if (_dictionary.TryGetValue(key, out var list))
            {
                list.Add(value);
            }
            else
            {
                this._dictionary[key] = new List<T>
                {
                    value
                };
            }
        }

        public IEnumerable<K> Keys
        {
            get
            {
                return this._dictionary.Keys;
            }
        }

        public List<T> this[K key]
        {
            get
            {
                if (!this._dictionary.TryGetValue(key, out var list))
                {
                    list = new List<T>();
                    this._dictionary[key] = list;
                }
                return list;
            }
        }

        protected override void Load()
        {
            var list = JsonConvert.DeserializeObject<List<T>>(File.ReadAllText($"{FullPath}/{typeof(T).Name}.json"));
            if (list != null)
            {
                _dictionary = OnLoad(list);
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
                    ((K?)value)!;

                Add(key, t!);
            }

            return _dictionary;
        }
    }
}
