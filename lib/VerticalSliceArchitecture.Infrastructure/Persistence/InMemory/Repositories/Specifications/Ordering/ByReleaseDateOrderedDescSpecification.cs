using System.Linq.Expressions;
using VerticalSliceArchitecture.Infrastructure.DesignPattern.Specification;
using VerticalSliceArchitecture.Infrastructure.Persistence.InMemory.Entities;

namespace VerticalSliceArchitecture.Infrastructure.Persistence.InMemory.Repositories.Specifications.Ordering;

/// <summary>
/// Ordering descending specification by <see cref="MovieEntity.ReleaseDate"/>.
/// </summary>
public sealed class ByReleaseDateOrderedDescSpecification
    : IOrderedDescSpecification<MovieEntity>
{
    public Expression<Func<MovieEntity, object>> OrderByDescending { get; } = x => x.ReleaseDate;
}