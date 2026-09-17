
using onine.core.Entites;
using onine.core.Specifications;

using Microsoft.EntityFrameworkCore;


namespace onine.Repository
{
    public class SpecificationEvaluator <T> where T : BaseEntity
    {
        public static IQueryable <T> GetQuery(IQueryable<T> inputQuery , ISpecifications<T> spec)
        {
            var query = inputQuery;
            if (spec.Criteria is not null)
            {
                query = query.Where(spec.Criteria);

            }
            if(spec.OrderBy is not null)
            { 
                query = query.OrderBy(spec.OrderBy);
            }

            if (spec.OrderByDesc is not null)
            {
                query = query.OrderByDescending(spec.OrderByDesc);

            }
            if (spec.IsPaginationEnabled)
            {
                query =query.Skip(spec.Skip).Take(spec.Take);
            }


                query = spec.Includes.Aggregate(
                query, (CurrentQuery, IncludeExpression) => CurrentQuery.Include(IncludeExpression));
            return query;

        }


    }
}
