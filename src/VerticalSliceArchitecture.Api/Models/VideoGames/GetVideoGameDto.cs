using System.Text.RegularExpressions;
using Facet;
using Facet.Mapping;
using VerticalSliceArchitecture.Infrastructure.InMemory.Entities;
using VerticalSliceArchitecture.Infrastructure.Sqlite.Entities;

namespace VerticalSliceArchitecture.Api.Models.VideoGames;

[Facet(typeof(VideoGameEntity), Configuration = typeof(GetVideoGameDtoConfiguration), Include = [nameof(MovieEntity.Title), nameof(MovieEntity.Description)])]
public sealed partial record GetVideoGameDto
{
    public string Genre { get; set; } = null!;
}

public sealed partial class GetVideoGameDtoConfiguration
    : IFacetMapConfiguration<VideoGameEntity, GetVideoGameDto>
{
    private const string RegexReplacementPattern = "$1 $2";
    
    [GeneratedRegex("([a-z])([A-Z])")]
    private static partial Regex MatchPascalCaseUppercaseLettersRegex();
    
    public static void Map(VideoGameEntity source, GetVideoGameDto target)
    {
        target.Genre = MatchPascalCaseUppercaseLettersRegex().Replace(source.Genre.ToString(), RegexReplacementPattern);
    }
}