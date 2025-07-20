


namespace backend.src.Domain.Interfaces;

public interface IGenericRepository<T> where T : BaseEntity
{
    void Add(T entity);
    Task<int> CountAsync(ISpecification<T> spec);
    void Delete(T entity);
    void Delete(int id);
    Task<IReadOnlyList<T>> GetAllAsync();
    Task<IReadOnlyList<T>> GetAllAsync(ISpecification<T> spec);
    Task<T?> GetAsync(int id);
    Task<T?> GetAsync(ISpecification<T> spec);
    Task<T?> GetAsyncNoTracking(int id);
    Task<T?> GetAsyncNoTracking(ISpecification<T> spec);
    IQueryable<T> Query();
    void Update(T entity);
    Task UpdateCollectionAsync(ICollection<T> current, List<int> incomingIds);
}