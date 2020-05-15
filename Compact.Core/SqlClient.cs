using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Compact.Core
{
    internal class SqlClient
    {
        private string connectionString;

        internal SqlClient(string connectionString)
        {
            this.connectionString = connectionString;
        }

        internal void ExecuteNonQuery(string sql)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand(sql, connection))
            {
                command.CommandType = CommandType.Text;
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        internal List<T> Query<T>(string sql) where T : DatabaseObject
        {
            using (var connection = new SqlConnection(connectionString))
            {
                return connection.Query<T>(sql).ToList();
            }
        }
    }
}
