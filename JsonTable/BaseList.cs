using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace JsonTable
{
    public class BaseList<T> : BaseTable, IReadOnlyList<T> where T : class
    {
        private List<T> _values = new List<T>();

        public T? this[int index]
        {
#pragma warning disable CS8766 // 반환 형식에서 참조 형식의 null 허용 여부가 암시적으로 구현된 멤버와 일치하지 않음(null 허용 여부 특성 때문일 수 있음)
            get
#pragma warning restore CS8766 // 반환 형식에서 참조 형식의 null 허용 여부가 암시적으로 구현된 멤버와 일치하지 않음(null 허용 여부 특성 때문일 수 있음)
            {
                return index > _values!.Count - 1 ? null : _values[index];
            }
        }

        public int Count => _values!.Count;

        public List<T> Clone => new List<T>(_values);

        // ReSharper disable once ConvertToAutoPropertyWithPrivateSetter
        public List<T> Container => _values;


        public IEnumerator<T> GetEnumerator() => _values!.GetEnumerator();

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
                _values = OnLoad(list);
            }
        }

        protected virtual List<T> OnLoad(List<T> list)
        {
            return list;
        }

        IEnumerator IEnumerable.GetEnumerator() => _values!.GetEnumerator();
    }
}
