using VerticalSliceArchitecture.Domain.Enums;

namespace VerticalSliceArchitecture.Infrastructure.Sqlite.Entities;

public sealed class VideoGameEntity
{
    public Guid Id { get; set; }

    public required string Title { get; set; }
    
    public required VideoGameGenre Genre { get; set; }
    
    public required string Description { get; set; }
    
    public required DateOnly ReleaseDate { get; set; }
}