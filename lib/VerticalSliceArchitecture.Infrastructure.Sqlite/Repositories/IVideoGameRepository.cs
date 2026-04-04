using VerticalSliceArchitecture.Infrastructure.Sqlite.Entities;

namespace VerticalSliceArchitecture.Infrastructure.Sqlite.Repositories;

public interface IVideoGameRepository
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    IAsyncEnumerable<VideoGameEntity> GetAllVideoGamesOrderedByReleaseDateAscendingAsync(CancellationToken cancellationToken = default);
}