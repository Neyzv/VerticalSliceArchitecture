using System.Runtime.CompilerServices;
using DorApiExplorer;
using Facet.Extensions;
using MediaThor;
using VerticalSliceArchitecture.Api.Models.Movies;
using VerticalSliceArchitecture.Infrastructure.InMemory.Entities;
using VerticalSliceArchitecture.Infrastructure.InMemory.Repositories;
namespace VerticalSliceArchitecture.Api.Features.Movies;

public static class GetMoviesFeature
{
    public sealed record GetMoviesQuery
        : IStreamRequest<GetMovieDto>;

    public sealed class GetMoviesHandler(IMovieRepository movieRepository)
        : IStreamRequestHandler<GetMoviesQuery, GetMovieDto>
    {
        public async IAsyncEnumerable<GetMovieDto> HandleAsync(
            GetMoviesQuery request,
            [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            await foreach (var movie in movieRepository
                               .GetAllMoviesOrderedByReleaseDateDescendingAsync(cancellationToken))
            {
                yield return movie.ToFacet<MovieEntity, GetMovieDto>();
            }
        }
    }

    public sealed class GetMoviesEndpoint
        : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("/movies", async (IMediator mediator) => await mediator.Send(new GetMoviesQuery()))
                .WithName("GetMovies")
                .WithDescription("Get all movies ordered by release date.");
        }
    }
}