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
    public sealed record CreateMovieRequest(string Title, string Description, DateTime ReleaseDate)
        : IRequest<GetMovieDto>;
    
    public sealed class CreateMovieRequestHandler(IMovieRepository movieRepository)
        : IRequestHandler<CreateMovieRequest, GetMovieDto>
    {
        public async Task<GetMovieDto> HandleAsync(CreateMovieRequest request, CancellationToken cancellationToken)
        {
            var movieEntity = new MovieEntity
            {
                Title = request.Title,
                Description = request.Description,
                ReleaseDate = DateOnly.FromDateTime(request.ReleaseDate)
            };

            await movieRepository.CreateMovieAsync(movieEntity, cancellationToken);

            return movieEntity.ToFacet<MovieEntity, GetMovieDto>();
        }
    }

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