

using Core.Entities;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class SpecificationEvaluator<T> where T : BaseEntity
    {
        public static IQueryable<T> GetQuery(IQueryable<T> InputQuery , ISpecification<T> spec)
        {
            IQueryable<T> query = InputQuery;
            if(spec.Criteria is not null)
            {
                query = query.Where(spec.Criteria);
            }

            if(spec.IncludesList.Count > 0)
            {
                query = spec.IncludesList.Aggregate(query,(current,include)=>current.Include(include));
            }

            if(spec.OrderBy is not null)
            {
                query = query.OrderBy(spec.OrderBy);
            }

            if (spec.OrderByDescending is not null)
            {
                query = query.OrderByDescending(spec.OrderByDescending);
            }

            if (spec.isPaginated)
            {
                query = query.Skip(spec.Skip).Take(spec.Take);
            }
            return query;
        }
    }
}
