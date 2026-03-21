using System.Linq.Expressions;

namespace VerticalSliceArchitecture.Infrastructure.DesignPattern.Specification;

public interface IOrderedDescSpecification<TEntity>
    : ISpecification<TEntity>
    where TEntity : class
{
    /// <summary>
    /// Expression to specify the property to order descending on.
    /// </summary>
    Expression<Func<TEntity, object>> OrderByDescending { get; }
}