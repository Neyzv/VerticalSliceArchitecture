using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using VerticalSliceArchitecture.Infrastructure.Extensions;
using VerticalSliceArchitecture.Infrastructure.Sqlite.Entities;
using VerticalSliceArchitecture.Infrastructure.Sqlite.Repositories.Specifications.Ordering;

namespace VerticalSliceArchitecture.Infrastructure.Sqlite.Repositories;

public sealed class VideoGameRepository(IDbContextFactory<SqliteDbContext> dbContextFactory)
    : IVideoGameRepository
{
    public async IAsyncEnumerable<VideoGameEntity> GetAllVideoGamesOrderedByReleaseDateAscendingAsync([EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        await using var dbContext = await dbContextFactory.CreateDbContextAsync(cancellationToken);

        await foreach (var videoGame in dbContext.VideoGames
                           .GetQuery(new ByReleaseDateOrderedSpecification())
                           .AsAsyncEnumerable()
                           .WithCancellation(cancellationToken))
        {
            yield return videoGame;
        }
    }
}