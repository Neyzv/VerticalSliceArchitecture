using Microsoft.EntityFrameworkCore;

namespace VerticalSliceArchitecture.Infrastructure.Persistence.Seeding;

public interface ISeeder
{
    /// <summary>
    /// Determinate if the seeder should be applied.
    /// </summary>
    /// <param name="context">The <see cref="DbContext"/> of the seeded database.</param>
    /// <returns><c>true</c> if it should be applied, otherwise <c>false</c>.</returns>
    bool ShouldBeApplied(DbContext context);
    
    /// <summary>
    /// Add static data to the database.
    /// </summary>
    /// <param name="context">The <see cref="DbContext"/> of the seeded database.</param>
    void Seed(DbContext context);
    
    /// <summary>
    /// Determinate if the seeder should be applied asynchronously.
    /// </summary>
    /// <param name="context">The <see cref="DbContext"/> of the seeded database.</param>
    /// <param name="token">The cancellation token</param>
    /// <returns><c>true</c> if it should be applied, otherwise <c>false</c>.</returns>
    Task<bool> ShouldBeAppliedAsync(DbContext context, CancellationToken token);
    
    /// <summary>
    /// Add static data to the database asynchronously.
    /// </summary>
    /// <param name="context">The <see cref="DbContext"/> of the seeded database.</param>
    /// <param name="token">The cancellation token</param>
    /// <returns></returns>
    Task SeedAsync(DbContext context, CancellationToken token);
}