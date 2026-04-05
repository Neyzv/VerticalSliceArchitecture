namespace VerticalSliceArchitecture.Infrastructure.InMemory.Entities;

public sealed class MovieEntity
{
    /// <summary>
    /// The id of the movie.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The title of the movie.
    /// </summary>
    public required string Title { get; set; }
    
    /// <summary>
    /// The descriptions of the movie.
    /// </summary>
    public required string Description { get; set; }
    
    /// <summary>
    /// The release date of the movie.
    /// </summary>
    public required DateOnly ReleaseDate { get; set; }
    
    /// <summary>
    /// The id of the author of the movie.
    /// </summary>
    public Guid AuthorId { get; set; }
    
    /// <summary>
    /// Dependency property to get the author instance of the movie.
    /// </summary>
    public AuthorEntity Author { get; set; } = null!;
}