using Microsoft.EntityFrameworkCore;
using VerticalSliceArchitecture.Infrastructure.Persistence.Seeding;

namespace VerticalSliceArchitecture.Infrastructure.Persistence.InMemory;

public sealed class InMemoryDbContext(DbContextOptions<InMemoryDbContext> options, IEnumerable<ISeeder> seeders)
    : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(InMemoryDbContext).Assembly);
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseSeeding(Seed)
            .UseAsyncSeeding(SeedAsync);
    }

    private void Seed(DbContext context, bool migrationApplied)
    {
        foreach (var seeder in seeders.Where(x => x.ShouldBeApplied(context)))
            seeder.Seed(context);
    }

    private async Task SeedAsync(DbContext context, bool migrationApplied, CancellationToken token)
    {
        var tasks = new List<Task>();

        foreach (var seeder in seeders)
        {
            if (!await seeder.ShouldBeAppliedAsync(context, token).ConfigureAwait(false))
                continue;

            tasks.Add(seeder.SeedAsync(context, token));
        }

        await Task.WhenAll(tasks).ConfigureAwait(false);
    }
}