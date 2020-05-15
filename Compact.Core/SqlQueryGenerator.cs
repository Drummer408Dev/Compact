using System.Collections.Generic;
using System.Linq;

namespace Compact.Core
{
    internal class SqlQueryGenerator
    {
        internal string GenerateInsertStatement(DatabaseObject databaseObject)
        {
            var tableName = databaseObject.GetTableName();
            var columns = databaseObject.GetDatabaseColumns();

            var columnNames = string.Join(", ", columns.Where(c => !c.Key.PrimaryKey).Select(c => c.Key.Name));
            var columnValues = string.Join(", ", columns.Where(c => !c.Key.PrimaryKey).Select(c => FormatValue(c.Key.DataType, c.Value.ToString())));

            return $"INSERT INTO {tableName} ({columnNames}) VALUES ({columnValues})";
        }

        internal string GenerateUpdateStatement(DatabaseObject databaseObject)
        {
            var tableName = databaseObject.GetTableName();
            var columns = databaseObject.GetDatabaseColumns();

            var settersList = new List<string>();
            foreach (var column in columns)
            {
                if (!column.Key.PrimaryKey)
                    settersList.Add($"{column.Key.Name} = {FormatValue(column.Key.DataType, column.Value.ToString())}");
            }

            var setters = string.Join(", ", settersList);
            var keyColumn = columns.Single(c => c.Key.PrimaryKey);

            return $"UPDATE {tableName} SET {setters} WHERE {keyColumn.Key.Name} = {keyColumn.Value}";
        }

        internal string GenerateDeleteStatement(DatabaseObject databaseObject)
        {
            var tableName = databaseObject.GetTableName();
            var keyColumn = databaseObject.GetDatabaseColumns().Single(c => c.Key.PrimaryKey);

            return $"DELETE FROM {tableName} WHERE {keyColumn.Key.Name} = {keyColumn.Value}";
        }

        private string FormatValue(string dataType, string value)
        {
            if (dataType.ToLower().StartsWith("integer") || dataType.ToLower().StartsWith("decimal"))
                return value;
            return $"'{value}'";
        }
    }
}
