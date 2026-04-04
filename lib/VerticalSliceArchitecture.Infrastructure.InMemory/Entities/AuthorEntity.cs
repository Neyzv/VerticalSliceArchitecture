namespace VerticalSliceArchitecture.Infrastructure.InMemory.Entities;

public sealed class AuthorEntity
{
    public Guid Id { get; set; }

    public required string Name { get; set; }
}