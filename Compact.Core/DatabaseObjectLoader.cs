using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Compact.Core
{
    internal class DatabaseObjectLoader
    {
        internal void LoadAll()
        {
            var assembly = Assembly.GetEntryAssembly();
            var types = assembly.GetTypes().Where(t => t.IsSubclassOf(typeof(DatabaseObject)));

            foreach (var type in types)
            {
                DatabaseObject.TableSchemaMap.Add(type, new DatabaseObjectMap
                {
                    TableName = GetTableName(type),
                    PropertyMap = GetPropertyMap(type)
                });
            }
        }

        private Dictionary<string, DatabaseColumn> GetPropertyMap(Type type)
        {
            var propertyMap = new Dictionary<string, DatabaseColumn>();

            foreach (var property in type.GetProperties())
            {
                var columnAttribute = (DatabaseColumnAttribute)property.GetCustomAttribute(typeof(DatabaseColumnAttribute));
                if (columnAttribute != null)
                    AddToPropertyMap(propertyMap, property.Name, columnAttribute.ColumnName, columnAttribute.DataType, false, columnAttribute.Nullable);
            }
            AddToPropertyMap(propertyMap, DatabaseObject.IdColumnName, DatabaseObject.IdColumnName, "INTEGER", true, false);

            return propertyMap;
        }

        private void AddToPropertyMap(Dictionary<string, DatabaseColumn> propertyMap, string propertyName, string columnName, string dataType, bool primaryKey, bool nullable)
        {
            propertyMap.Add(propertyName, new DatabaseColumn
            {
                Name = columnName,
                DataType = dataType,
                PrimaryKey = primaryKey,
                Nullable = nullable
            });
        }

        private string GetTableName(Type type)
        {
            var tableName = type.Name;

            var tableAttribute = (DatabaseTableAttribute)type.GetCustomAttribute(typeof(DatabaseTableAttribute));
            if (tableAttribute != null)
                tableName = tableAttribute.TableName;

            return tableName;
        }
    }
}
