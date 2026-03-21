using System.Linq.Expressions;

namespace VerticalSliceArchitecture.Infrastructure.DesignPattern.Specification;

public interface ICriteriaSpecification<TEntity>
    : ISpecification<TEntity>
    where TEntity : class
{
    /// <summary>
    /// Expression to specify the criteria to retrieve an instance of the model.
    /// </summary>
    Expression<Func<TEntity, bool>> Criteria { get; }
}