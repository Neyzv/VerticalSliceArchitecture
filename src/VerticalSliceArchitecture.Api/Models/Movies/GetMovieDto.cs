using Facet;
using Facet.Mapping;
using VerticalSliceArchitecture.Infrastructure.Persistence.InMemory.Entities;

namespace VerticalSliceArchitecture.Api.Models.Movies;

[Facet(typeof(MovieEntity), Include = [nameof(MovieEntity.Title), nameof(MovieEntity.Description)])]
public sealed partial record GetMovieDto
{
    public int YearOfRelease { get; set; }
}

public sealed class GetMovieDtoConfiguration
    : IFacetMapConfiguration<MovieEntity, GetMovieDto>
{
    public static void Map(MovieEntity source, GetMovieDto target)
    {
        target.YearOfRelease = source.ReleaseDate.Year;
    }
}