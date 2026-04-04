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

    /// <summary>
    /// Create a new movie entity in the database.
    /// </summary>
    /// <param name="entity">The entity that needs to be added.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous save operation. The task result contains the number of state entries written to the database.</returns>
    Task<int> CreateMovieAsync(MovieEntity entity, CancellationToken cancellationToken = default);
}