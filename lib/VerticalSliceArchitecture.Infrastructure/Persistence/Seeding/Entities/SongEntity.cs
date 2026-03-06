namespace VerticalSliceArchitecture.Infrastructure.Persistence.Seeding.Entities;

public sealed class SongEntity
{
    public Guid Id { get; set; }
    
    public required string Title { get; set; }
    
    public TimeSpan Duration { get; set; }
}