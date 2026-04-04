namespace VerticalSliceArchitecture.Infrastructure.InMemory.Entities;

public sealed class MovieEntity
{
    public Guid Id { get; set; }

    public required string Title { get; set; }
    
    public required string Description { get; set; }
    
    public required DateOnly ReleaseDate { get; set; }
}