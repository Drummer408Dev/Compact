using System;

namespace Compact.Core
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DatabaseColumnAttribute : Attribute
    {
        public string ColumnName { get; set; }
        public string DataType { get; set; }
        public bool Nullable { get; set; }
    }
}
