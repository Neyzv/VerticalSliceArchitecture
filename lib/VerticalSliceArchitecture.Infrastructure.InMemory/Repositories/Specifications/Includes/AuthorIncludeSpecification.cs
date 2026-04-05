using System.Linq.Expressions;
using VerticalSliceArchitecture.Infrastructure.DesignPattern.Specification;
using VerticalSliceArchitecture.Infrastructure.InMemory.Entities;

namespace VerticalSliceArchitecture.Infrastructure.InMemory.Repositories.Specifications.Includes;

public sealed class AuthorIncludeSpecification
    : IIncludeSpecification<MovieEntity>
{
    public Expression<Func<MovieEntity, object>> Include { get; } = movie => movie.Author;
}