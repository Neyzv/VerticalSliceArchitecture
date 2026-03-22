using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace VerticalSliceArchitecture.Infrastructure.Persistence.Sqlite;

/// <summary>
/// Class that provides an instance of the <see cref="SqliteDbContext"/> to execute EF Core commands.
/// </summary>
public sealed class SqliteDbContextDesignTimeFactory
    : IDesignTimeDbContextFactory<SqliteDbContext>
{
    public SqliteDbContext CreateDbContext(string[] args)
    {
        return new SqliteDbContext(new DbContextOptionsBuilder<SqliteDbContext>().UseSqlite("Filename=:memory:").Options, []);
    }
}