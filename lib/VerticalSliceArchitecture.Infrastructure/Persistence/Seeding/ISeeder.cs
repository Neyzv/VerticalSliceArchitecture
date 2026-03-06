using Microsoft.EntityFrameworkCore;

namespace VerticalSliceArchitecture.Infrastructure.Persistence.Seeding;

public interface ISeeder<TDbContext>
    where TDbContext : DbContext
{
    /// <summary>
    /// Determinate if the seeder should be applied.
    /// </summary>
    /// <param name="context">The <see cref="DbContext"/> of the seeded database.</param>
    /// <returns><c>true</c> if it should be applied, otherwise <c>false</c>.</returns>
    bool ShouldBeApplied(TDbContext context);
    
    /// <summary>
    /// Add static data to the database.
    /// </summary>
    /// <param name="context">The <see cref="DbContext"/> of the seeded database.</param>
    void Seed(TDbContext context);
    
    /// <summary>
    /// Determinate if the seeder should be applied asynchronously.
    /// </summary>
    /// <param name="context">The <see cref="DbContext"/> of the seeded database.</param>
    /// <param name="token">The cancellation token</param>
    /// <returns><c>true</c> if it should be applied, otherwise <c>false</c>.</returns>
    Task<bool> ShouldBeAppliedAsync(TDbContext context, CancellationToken token);
    
    /// <summary>
    /// Add static data to the database asynchronously.
    /// </summary>
    /// <param name="context">The <see cref="DbContext"/> of the seeded database.</param>
    /// <param name="token">The cancellation token</param>
    /// <returns></returns>
    Task SeedAsync(TDbContext context, CancellationToken token);
}