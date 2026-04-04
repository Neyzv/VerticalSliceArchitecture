using VerticalSliceArchitecture.Infrastructure.InMemory.Entities;

namespace VerticalSliceArchitecture.Infrastructure.InMemory.Repositories;

public interface IMovieRepository
{
    /// <summary>
    /// Get all movies ordered by most recent release date.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>An async enumeration of <see cref="MovieEntity"/>.</returns>
    IAsyncEnumerable<MovieEntity> GetAllMoviesOrderedByReleaseDateDescendingAsync(CancellationToken cancellationToken = default);
}