using System;
using System.Collections.Generic;

namespace Compact.Core
{
    public abstract class DatabaseObject
    {
        internal const string IdColumnName = nameof(Id);

        internal static Dictionary<Type, DatabaseObjectMap> TableSchemaMap = new Dictionary<Type, DatabaseObjectMap>();

        public int? Id { get; internal set; }

        internal string GetTableName()
        {
            return TableSchemaMap[GetType()].TableName;
        }

        internal Dictionary<DatabaseColumn, object> GetDatabaseColumns()
        {
            var columns = new Dictionary<DatabaseColumn, object>();
            var propertyMap = TableSchemaMap[GetType()].PropertyMap;

            foreach (var property in propertyMap)
            {
                var value = GetType().GetProperty(property.Key).GetValue(this);
                columns.Add(property.Value, value);
            }

            return columns;
        }
    }
}
