namespace backend.src.Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly DataContext _db;
    private Hashtable _repos { get; set; } = new();

    public UnitOfWork(DataContext db)
    {
        _db = db;
    }

    /// <summary>
    /// Method which commits changes to the database.
    /// </summary>
    public async Task<int> CompleteAsync()
    {
        return await _db.SaveChangesAsync();
    }

    /// <summary>
    /// Get transaction scope instance.
    /// </summary>
    public async Task<IDbContextTransaction> BeginTransactionAsync() => await _db.Database.BeginTransactionAsync();


    /// <summary>
    /// Method which disposes changes to the database.
    /// </summary>
    public void Dispose()
    {
        _db.Dispose();
    }

    /// <summary>
    /// Method which gets a repo and stores it internally in a hashtable.
    /// </summary>
    public IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity
    {
        var type = typeof(TEntity).Name;
        if (!_repos.ContainsKey(type))
        {
            var repositoryType = typeof(GenericRepository<>);
            var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _db);
            _repos.Add(type, repositoryInstance);
        }

        return (IGenericRepository<TEntity>)_repos[type]!;
    }
}
