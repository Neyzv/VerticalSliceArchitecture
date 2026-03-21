namespace VerticalSliceArchitecture.Infrastructure.DesignPattern.Specification;

/// <summary>
/// Base class for all kinds of specifications.
/// </summary>
/// <typeparam name="TDbEntity">The type of the entity.</typeparam>
public interface ISpecification<TEntity>
    where TEntity : class
{
}