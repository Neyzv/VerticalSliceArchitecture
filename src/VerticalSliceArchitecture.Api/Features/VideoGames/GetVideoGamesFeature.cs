using System.Runtime.CompilerServices;
using DorApiExplorer;
using Facet.Extensions;
using MediaThor;
using VerticalSliceArchitecture.Api.Models.VideoGames;
using VerticalSliceArchitecture.Infrastructure.Sqlite.Entities;
using VerticalSliceArchitecture.Infrastructure.Sqlite.Repositories;

namespace VerticalSliceArchitecture.Api.Features.VideoGames;

public static class GetVideoGamesFeature
{
    public sealed record GetVideoGamesQuery
        : IStreamRequest<GetVideoGameDto>;
    
    public sealed class GetVideoGamesQueryHandler(IVideoGameRepository videoGameRepository)
        : IStreamRequestHandler<GetVideoGamesQuery, GetVideoGameDto>
    {
        public async IAsyncEnumerable<GetVideoGameDto> HandleAsync(GetVideoGamesQuery request,
            [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            await foreach (var videoGame in videoGameRepository
                               .GetAllVideoGamesOrderedByReleaseDateAscendingAsync(cancellationToken))
            {
                yield return videoGame.ToFacet<VideoGameEntity, GetVideoGameDto>();
            }
        }
    }
    
    public sealed class GetVideoGameEndpoint
        : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("/videogames", async (IMediator mediator) => await mediator.Send(new GetVideoGamesQuery()))
                .WithName("GetVideoGames")
                .WithDescription("Get all video games ordered by release date.");
        }
    }
}