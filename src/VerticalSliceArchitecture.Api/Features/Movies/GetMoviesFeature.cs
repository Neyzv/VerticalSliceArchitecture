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
    /// <summary>
    /// The query to get all movies
    /// </summary>
    public sealed record GetMoviesQuery
        : IStreamRequest<GetMovieDto>;

    /// <summary>
    /// The handler to get all movies.
    /// </summary>
    /// <param name="movieRepository">The movie repository.</param>
    public sealed class GetMoviesHandler(IMovieRepository movieRepository)
        : IStreamRequestHandler<GetMoviesQuery, GetMovieDto>
    {
        public async IAsyncEnumerable<GetMovieDto> HandleAsync(
            GetMoviesQuery request,
            [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            await foreach (var movie in movieRepository
                               .GetAllMoviesOrderedByReleaseDateDescendingAsync(true, cancellationToken))
            {
                yield return movie.ToFacet<MovieEntity, GetMovieDto>();
            }
        }
    }

    /// <summary>
    /// The auto registered endpoint for get movies feature.
    /// </summary>
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