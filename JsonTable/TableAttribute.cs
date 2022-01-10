namespace JsonTable
{
    [AttributeUsage(AttributeTargets.Class)]
    public class TableAttribute : Attribute
    {
        public string Path { get; private set; }

        public string Key { get; private set; } = "Id";

        public TableAttribute(string path, string key = "Id")
        {
            Path = path;
            Key = key;
        }
    }
}
