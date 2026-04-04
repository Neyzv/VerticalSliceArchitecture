using DorApiExplorer;
using MediaThor;
using Microsoft.AspNetCore.Mvc;
using VerticalSliceArchitecture.Api.Models.Movies;

namespace VerticalSliceArchitecture.Api.Features.Movies;

public static class CreateMovieFeature
{
    public sealed record CreateMovieRequest(string Title, string Description, DateTime ReleaseDate)
        : IRequest<GetMovieDto>;
    
    public sealed class CreateMovieRequestHandler
        : IRequestHandler<CreateMovieRequest, GetMovieDto>
    {
        public Task<GetMovieDto> HandleAsync(CreateMovieRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
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