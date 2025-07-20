
namespace backend.src.Infrastructure.Persistence;

public interface IUnitOfWork
{
    Task<IDbContextTransaction> BeginTransactionAsync();
    Task<int> CompleteAsync();
    void Dispose();
    IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity;
}