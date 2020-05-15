using System;

namespace Compact.Core
{
    [AttributeUsage(AttributeTargets.Class)]
    public class DatabaseTableAttribute : Attribute
    {
        public string TableName { get; set; }
    }
}
