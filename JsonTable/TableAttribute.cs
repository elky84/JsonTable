namespace JsonTable
{
    [AttributeUsage(AttributeTargets.Class)]
    public class TableAttribute : Attribute
    {
        public string Path { get; private set; }

        public TableAttribute(string path)
        {
            Path = path;
        }
    }
}
