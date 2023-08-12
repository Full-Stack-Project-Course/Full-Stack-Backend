using Core.Entities;
using System.Linq.Expressions;

namespace Core.Specifications
{
    public interface ISpecification<T> where T : BaseEntity
    {
        Expression<Func<T, bool>>? Criteria { get; }

        List<Expression<Func<T, object>>> IncludesList { get; }

        Expression<Func<T, object>>? OrderBy { get; }

        Expression<Func<T, object>>? OrderByDescending { get; }

        int Take { get; }

        int Skip { get; }
        bool isPaginated { get; }
    }
}
