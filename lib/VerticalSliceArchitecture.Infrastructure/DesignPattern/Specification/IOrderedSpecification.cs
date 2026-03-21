using System.Linq.Expressions;

namespace VerticalSliceArchitecture.Infrastructure.DesignPattern.Specification;

public interface IOrderedSpecification<TEntity>
    : ISpecification<TEntity>
    where TEntity : class
{
    /// <summary>
    /// Expression to specify the property to order on.
    /// </summary>
    Expression<Func<TEntity, object>> OrderBy { get; }
}