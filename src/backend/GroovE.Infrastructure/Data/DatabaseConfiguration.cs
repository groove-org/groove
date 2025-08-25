namespace GroovE.Infrastructure.Data;

internal class DatabaseConfiguration
{
    public string ConnectionString { get; set; } = null!;
    public DatabaseProvider Provider { get; set; }
}

internal enum DatabaseProvider
{
    SQLite,
    PostgreSQL
}