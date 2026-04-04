using Microsoft.EntityFrameworkCore;
using VerticalSliceArchitecture.Infrastructure.InMemory.Entities;
using VerticalSliceArchitecture.Infrastructure.Persistence.Seeding;

namespace VerticalSliceArchitecture.Infrastructure.InMemory;

public sealed class InMemoryDbContext(DbContextOptions<InMemoryDbContext> options, IEnumerable<ISeeder<InMemoryDbContext>> seeders)
    : DbContext(options)
{
    public DbSet<MovieEntity> Movies { get; set; }
    
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
        if (context is not InMemoryDbContext inMemoryDbContext)
            return;
        
        foreach (var seeder in seeders.Where(x => x.ShouldBeApplied(inMemoryDbContext)))
            seeder.Seed(inMemoryDbContext);
    }

    private async Task SeedAsync(DbContext context, bool migrationApplied, CancellationToken token)
    {
        if (context is not InMemoryDbContext inMemoryDbContext)
            return;
        
        var tasks = new List<Task>();

        foreach (var seeder in seeders)
        {
            if (!await seeder.ShouldBeAppliedAsync(inMemoryDbContext, token).ConfigureAwait(false))
                continue;

            tasks.Add(seeder.SeedAsync(inMemoryDbContext, token));
        }

        await Task.WhenAll(tasks).ConfigureAwait(false);
    }
}