using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using VerticalSliceArchitecture.Infrastructure.Extensions;
using VerticalSliceArchitecture.Infrastructure.Persistence.InMemory.Entities;
using VerticalSliceArchitecture.Infrastructure.Persistence.InMemory.Repositories.Specifications.Ordering;

namespace VerticalSliceArchitecture.Infrastructure.Persistence.InMemory.Repositories;

public sealed class MovieRepository(IDbContextFactory<InMemoryDbContext> dbContextFactory)
    : IMovieRepository
{
    public async IAsyncEnumerable<MovieEntity> GetAllMoviesOrderedByReleaseDateDescendingAsync(
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        await using var dbContext = await dbContextFactory.CreateDbContextAsync(cancellationToken);

        await foreach (var movie in dbContext.Movies
                           .GetQuery(new ByReleaseDateOrderedDescSpecification())
                           .AsAsyncEnumerable()
                           .WithCancellation(cancellationToken))
        {
            yield return movie;
        }
    }
}