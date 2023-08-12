using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Data
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreContext _storeContext;
       // private readonly ISpecification<T> _specification;
        public GenericRepository(StoreContext storeContext)
        {

            _storeContext = storeContext;
          //  _specification = specification;

        }

        public async Task<int> CountAsync(ISpecification<T> spec)
        {
            return await QueryBuilder(spec).CountAsync();
        }

        public async Task<T?> GetOneByIDAsync(int id)
        {
            return await _storeContext.Set<T>().FindAsync(id) ;
        }

        public async Task<T?> GetOneEntityWithSpec( ISpecification<T> spec)
        {
            return await QueryBuilder(spec).FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
            return await _storeContext.Set<T>().ToListAsync();
        }

        public async Task<IReadOnlyList<T>> ListEntityWithSpec(ISpecification<T> spec)
        {
            return await QueryBuilder(spec).ToListAsync();
        }

        private IQueryable<T> QueryBuilder(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_storeContext.Set<T>(), spec);
        }
    }
}
