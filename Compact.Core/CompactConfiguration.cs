namespace Compact.Core
{
    public static class CompactConfiguration
    {
        internal static string ConnectionString;

        public static void RegisterDatabase(string connectionString, bool installTables)
        {
            ConnectionString = connectionString;

            new DatabaseObjectLoader().LoadAll();
            if (installTables)
                new DatabaseObjectInstaller().Install();
        }
    }
}
