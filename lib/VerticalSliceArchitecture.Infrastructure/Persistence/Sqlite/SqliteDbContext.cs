using Microsoft.EntityFrameworkCore;
using VerticalSliceArchitecture.Infrastructure.Persistence.Seeding;
using VerticalSliceArchitecture.Infrastructure.Persistence.Sqlite.Entities;

namespace VerticalSliceArchitecture.Infrastructure.Persistence.Sqlite;

public sealed class SqliteDbContext(DbContextOptions<SqliteDbContext> options, IEnumerable<ISeeder<SqliteDbContext>> seeders)
    : DbContext
{
    public DbSet<VideoGameEntity> VideoGames { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SqliteDbContext).Assembly);
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseSeeding(Seed)
            .UseAsyncSeeding(SeedAsync);
    }

    private void Seed(DbContext context, bool migrationApplied)
    {
        if (context is not SqliteDbContext sqliteDbContext)
            return;
        
        foreach (var seeder in seeders.Where(x => x.ShouldBeApplied(sqliteDbContext)))
            seeder.Seed(sqliteDbContext);
    }

    private async Task SeedAsync(DbContext context, bool migrationApplied, CancellationToken token)
    {
        if (context is not SqliteDbContext sqliteDbContext)
            return;
        
        var tasks = new List<Task>();

        foreach (var seeder in seeders)
        {
            if (!await seeder.ShouldBeAppliedAsync(sqliteDbContext, token).ConfigureAwait(false))
                continue;

            tasks.Add(seeder.SeedAsync(sqliteDbContext, token));
        }

        await Task.WhenAll(tasks).ConfigureAwait(false);
    }
}