using System;
using System.Collections.Generic;

namespace Compact.Core
{
    public class DatabaseObjectRepository<T> where T : DatabaseObject
    {
        private SqlQueryGenerator queryGenerator;
        private SqlClient sqlClient;

        public DatabaseObjectRepository()
        {
            queryGenerator = new SqlQueryGenerator();
            sqlClient = new SqlClient(CompactConfiguration.ConnectionString);
        }

        public void InsertOrUpdate(T databaseObject)
        {
            if (databaseObject.Id.HasValue)
                sqlClient.ExecuteNonQuery(queryGenerator.GenerateUpdateStatement(databaseObject));
            else
                sqlClient.ExecuteNonQuery(queryGenerator.GenerateInsertStatement(databaseObject));
        }

        public void Delete(T databaseObject)
        {
            if (!databaseObject.Id.HasValue)
                throw new Exception("Cannot delete an object of type DatabaseObject if no Id is set.");

            sqlClient.ExecuteNonQuery(queryGenerator.GenerateDeleteStatement(databaseObject));
        }

        public List<T> Get(string sql)
        {
            return sqlClient.Query<T>(sql);
        }
    }
}
