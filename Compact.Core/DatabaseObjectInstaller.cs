using System.Collections.Generic;
using System.Text;

namespace Compact.Core
{
    internal class DatabaseObjectInstaller
    {
        private SqlClient sqlClient;

        internal DatabaseObjectInstaller()
        {
            sqlClient = new SqlClient(CompactConfiguration.ConnectionString);
        }

        internal void Install()
        {
            // Eventually we will check table existence and table schema changes
            // For now, just install the tables
            foreach (var databaseObjectMap in DatabaseObject.TableSchemaMap.Values)
            {
                var tableName = databaseObjectMap.TableName;

                var columnsList = new List<string>();
                foreach (var column in databaseObjectMap.PropertyMap.Values)
                    columnsList.Add(GetColumnDefinition(column));

                var columns = string.Join(", ", columnsList);
                var sql = $"CREATE TABLE {tableName} ({columns})";
                sqlClient.ExecuteNonQuery(sql);
            }
        }

        private string GetColumnDefinition(DatabaseColumn column)
        {
            var columnsBuilder = new StringBuilder();

            columnsBuilder.Append($"{column.Name} {column.DataType}");
            
            if (column.PrimaryKey)
                columnsBuilder.Append($" IDENTITY(1, 1)");

            if (!column.Nullable)
                columnsBuilder.Append(" NOT NULL");

            return columnsBuilder.ToString();
        }
    }
}
