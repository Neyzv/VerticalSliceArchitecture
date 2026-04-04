using Microsoft.EntityFrameworkCore;
using VerticalSliceArchitecture.Infrastructure.DesignPattern.Specification;

namespace VerticalSliceArchitecture.Infrastructure.Extensions;

public static class QueryableExtensions
{
    extension<TEntity>(IQueryable<TEntity> baseQuery)
        where TEntity : class
    {
        public IQueryable<TEntity> GetQuery(params IEnumerable<ISpecification<TEntity>> specifications)
        {
            var query = baseQuery;

            var orderBySpecifications = new List<ISpecification<TEntity>>();

            foreach (var specification in specifications)
            {
                switch (specification)
                {
                    case ICriteriaSpecification<TEntity> criteriaSpecification:
                        query = query.Where(criteriaSpecification.Criteria);
                        break;

                    case IOrderedSpecification<TEntity> or IOrderedDescSpecification<TEntity>:
                        orderBySpecifications.Add(specification);
                        break;

                    case IIncludeSpecification<TEntity> includeSpecification:
                        query = query.Include(includeSpecification.Include);
                        break;

                    default:
                        continue;
                }
            }

            if (orderBySpecifications.Count is 0)
                return query;

            var orderedQuery = orderBySpecifications[0] switch
            {
                IOrderedSpecification<TEntity> orderedSpecification => query.OrderBy(orderedSpecification.OrderBy),
                IOrderedDescSpecification<TEntity> orderedDescSpecification => query.OrderByDescending(orderedDescSpecification.OrderByDescending),
                _ => throw new InvalidOperationException()
            };

            for (var i = 1; i < orderBySpecifications.Count; i++)
            {
                orderedQuery = orderBySpecifications[i] switch
                {
                    IOrderedSpecification<TEntity> orderedSpecification => orderedQuery.ThenBy(orderedSpecification.OrderBy),
                    IOrderedDescSpecification<TEntity> orderedDescSpecification => orderedQuery.ThenByDescending(orderedDescSpecification.OrderByDescending),
                    _ => throw new InvalidOperationException()
                };
            }

            query = orderedQuery;

            return query;
        }
    }
}