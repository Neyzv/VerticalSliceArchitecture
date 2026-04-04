using System.Linq.Expressions;
using VerticalSliceArchitecture.Infrastructure.DesignPattern.Specification;
using VerticalSliceArchitecture.Infrastructure.Sqlite.Entities;

namespace VerticalSliceArchitecture.Infrastructure.Sqlite.Repositories.Specifications.Ordering;

public sealed class ByReleaseDateOrderedSpecification
    : IOrderedSpecification<VideoGameEntity>
{
    public Expression<Func<VideoGameEntity, object>> OrderBy { get; } =  x => x.ReleaseDate;
}