using System.Linq.Expressions;

namespace VerticalSliceArchitecture.Infrastructure.DesignPattern.Specification;

public interface IIncludeSpecification<TEntity>
    : ISpecification<TEntity>
    where TEntity : class
{
    /// <summary>
    /// Expression to specify the dependency property that needs to be loaded when retrieving result.
    /// </summary>
    Expression<Func<TEntity, object>> Include { get; }
}