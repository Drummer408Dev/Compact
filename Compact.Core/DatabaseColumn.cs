namespace Compact.Core
{
    internal class DatabaseColumn
    {
        internal string Name { get; set; }
        internal string DataType { get; set; }
        internal bool PrimaryKey { get; set; }
        internal bool Nullable { get; set; }
    }
}
