using Core.Entities;

namespace Core.Interfaces;

public interface IGenericRepository<T> where T : BaseEntity
{
    Task<T> GetEntityByIdAsync(int id);
    Task<T> GetEntityWithSpecAsync(ISpecification<T> spec);
    Task<IReadOnlyList<T>> ListAllAsync();
    Task<IReadOnlyList<T>> ListAllWithSpecAsync(ISpecification<T> spec);
    Task<int> GetCountWithSpecAsync(ISpecification<T> spec);

    void Add(T entity);
    void AddRange(T[] entity);
    void Update(T entity);
    void Delete(T entity);
    Task<int> SaveChangesAsync();
}
