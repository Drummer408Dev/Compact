using System.Collections.Generic;

namespace Compact.Core
{
    internal class DatabaseObjectMap
    {
        internal string TableName { get; set; }
        internal Dictionary<string, DatabaseColumn> PropertyMap { get; set; }
    }
}
