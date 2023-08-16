using Core.Entities;
using Core.Specifications;

namespace Core.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<IReadOnlyList<T>> ListAllAsync();

        Task<int> CountAsync(ISpecification<T> spec);

        Task<T?> GetOneByIDAsync(int id);

        Task<IReadOnlyList<T>> ListEntityWithSpec(ISpecification<T> spec);

        Task<T?> GetOneEntityWithSpec(ISpecification<T> spec);

        Task Add(T entity);
        void Update(T entity);
        void Delete(T entity);

    }
}
