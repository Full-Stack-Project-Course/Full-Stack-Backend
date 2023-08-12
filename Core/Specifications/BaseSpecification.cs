using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class BaseSpecification<T> : ISpecification<T> where T : BaseEntity
    {
        public Expression<Func<T, bool>>? Criteria { get; }

        public BaseSpecification()
        {
        }
        public BaseSpecification(Expression<Func<T, bool>>? criteria)
        {
            Criteria = criteria;
        }

        public List<Expression<Func<T, object>>> IncludesList { get; } = new List<Expression<Func<T, object>>>();

        public Expression<Func<T, object>>? OrderBy { get; private set; }

        public Expression<Func<T, object>>? OrderByDescending { get; private set; }

        public int Take { get; private set; }

        public int Skip { get; private set; }

        public bool isPaginated { get; private set; }

        protected void SetPagination(int pageIndex, int pageSize)
        {
            Take = pageSize;
            Skip = pageSize * (pageIndex - 1);
            isPaginated = true;
        }

        protected void AddIncludes(Expression<Func<T, object>> include)
        {
            IncludesList.Add(include);
        }

        protected void SetOrder(Expression<Func<T, object>> order)
        {
            OrderBy = order;
        }

        protected void SetOrderByDescending(Expression<Func<T, object>> order)
        {
            OrderByDescending = order;
        }
    }
}
