using System;
using System.Reflection;

namespace JsonTable
{
    public abstract class BaseTable
    {
        protected string FullPath => GetType().GetCustomAttribute<TableAttribute>()?.FullPath ??
            throw new Exception("cannot find TableAttribute.");

        protected string Key => GetType().GetCustomAttribute<TableAttribute>()?.Key ??
                        throw new Exception("cannot find TableAttribute.");

        protected BaseTable()
        {
            // ReSharper disable once VirtualMemberCallInConstructor
            Load();
        }

        protected abstract void Load();

        protected virtual string Json() { return string.Empty; }
    }
}
