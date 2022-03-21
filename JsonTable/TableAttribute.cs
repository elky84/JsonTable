using System.Reflection;

namespace JsonTable
{
    [AttributeUsage(AttributeTargets.Class)]
    public class TableAttribute : Attribute
    {
        private string Path { get; set; }

        public string FullPath => $"{System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly()!.Location)}/{Path}";

        public string Key { get; private set; } = "Id";

        public TableAttribute(string path, string key = "Id")
        {
            Path = path;
            Key = key;
        }
    }
}
