using MediaThor;
using VerticalSliceArchitecture.Api.Models.Movies;

namespace VerticalSliceArchitecture.Api.Features.Movies;

public static class GetMoviesFeature
{
    public sealed record GetMoviesQuery()
        : IStreamRequest<GetMovieDto>;

    public sealed class GetMoviesHandler
        : IStreamRequestHandler<GetMoviesQuery, GetMovieDto>
    {
        public IAsyncEnumerable<GetMovieDto> HandleAsync(GetMoviesQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}