
namespace backend.src.Domain.Interfaces;

public interface IGenericRepository<T> where T : BaseEntity
{
    DataContext GetDbContext { get; }

    void Add(T entity);
    Task CompleteAsync();
    Task<int> CountAsync(ISpecification<T> spec);
    void Delete(T entity);
    void Delete(int id);
    Task<IReadOnlyList<T>> GetAllAsync();
    Task<IReadOnlyList<T>> GetAllAsync(ISpecification<T> spec);
    Task<T?> GetAsync(int id);
    Task<T?> GetAsync(ISpecification<T> spec);
    Task<T?> GetAsyncNoTracking(int id);
    Task<T?> GetAsyncNoTracking(ISpecification<T> spec);
    void Update(T entity);
}