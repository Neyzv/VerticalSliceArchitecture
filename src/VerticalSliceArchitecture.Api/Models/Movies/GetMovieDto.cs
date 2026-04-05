using Facet;
using Facet.Mapping;
using VerticalSliceArchitecture.Infrastructure.InMemory.Entities;

namespace VerticalSliceArchitecture.Api.Models.Movies;

/// <summary>
/// The DTO for the <see cref="MovieEntity"/>.
/// </summary>
[Facet(typeof(MovieEntity), Configuration = typeof(GetMovieDtoConfiguration), Include = [nameof(MovieEntity.Title), nameof(MovieEntity.Description)])]
public sealed partial record GetMovieDto
{
    public int YearOfRelease { get; set; }

    public string Author { get; set; } = null!;
}

/// <summary>
/// The configuration for the <see cref="GetMovieDto"/>.
/// </summary>
public sealed class GetMovieDtoConfiguration
    : IFacetMapConfiguration<MovieEntity, GetMovieDto>
{
    public static void Map(MovieEntity source, GetMovieDto target)
    {
        target.YearOfRelease = source.ReleaseDate.Year;
        target.Author = source.Author.Name;
    }
}