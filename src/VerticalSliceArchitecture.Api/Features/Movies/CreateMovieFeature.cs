using DorApiExplorer;
using Facet.Extensions;
using MediaThor;
using Microsoft.AspNetCore.Mvc;
using VerticalSliceArchitecture.Api.Models.Movies;
using VerticalSliceArchitecture.Infrastructure.InMemory.Entities;
using VerticalSliceArchitecture.Infrastructure.InMemory.Repositories;

namespace VerticalSliceArchitecture.Api.Features.Movies;

public static class CreateMovieFeature
{
    /// <summary>
    /// Request to create a movie.
    /// </summary>
    /// <param name="Title">The title of the movie.</param>
    /// <param name="Description">The description of the movie.</param>
    /// <param name="ReleaseDate">The release date of the movie.</param>
    public sealed record CreateMovieRequest(string Title, string Description, DateTime ReleaseDate)
        : IRequest<GetMovieDto>;
    
    /// <summary>
    /// The handler of the <see cref="CreateMovieRequest"/>.
    /// </summary>
    /// <param name="movieRepository">The movie repository.</param>
    public sealed class CreateMovieRequestHandler(IMovieRepository movieRepository)
        : IRequestHandler<CreateMovieRequest, GetMovieDto>
    {
        public async Task<GetMovieDto> HandleAsync(CreateMovieRequest request, CancellationToken cancellationToken)
        {
            var movieEntity = new MovieEntity
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                Description = request.Description,
                ReleaseDate = DateOnly.FromDateTime(request.ReleaseDate)
            };

            await movieRepository.CreateMovieAsync(movieEntity, cancellationToken);

            return movieEntity.ToFacet<MovieEntity, GetMovieDto>();
        }
    }

    /// <summary>
    /// The auto registered endpoint for the create movie feature.
    /// </summary>
    public sealed class CreateMovieEndpoint
        : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("/movies", async (IMediator mediator, [FromBody] CreateMovieRequest request) => await mediator.Send(request))
                .WithName("CreateMovie")
                .WithName("Used to create a movie.");
        }
    }
}