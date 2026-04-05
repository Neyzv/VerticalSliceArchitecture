namespace VerticalSliceArchitecture.Infrastructure.InMemory.Entities;

public sealed class AuthorEntity
{
    /// <summary>
    /// The id of the author.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The name of the author.
    /// </summary>
    public required string Name { get; set; }
}