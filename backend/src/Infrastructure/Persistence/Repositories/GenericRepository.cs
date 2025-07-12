namespace backend.src.Infrastructure.Persistence.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    private readonly DataContext _db;
    public GenericRepository(DataContext db)
    {
        _db = db;
    }

    /// <summary>
    /// Method which gets an entity based on an id with no specification.
    /// </summary>
    public async Task<T?> GetAsync(int id)
    {
        var output = await _db.Set<T>().FindAsync(id);
        return output;
    }

    /// <summary>
    /// Method which gets an entity based on specification.
    /// </summary>
    public async Task<T?> GetEntityWithSpecification(ISpecification<T> spec)
    {
        var query = ApplySpecification(spec);
        var output = await query.FirstOrDefaultAsync();
        return output;
    }

    /// <summary>
    /// Method which counts all records based on an speficiation result.
    /// </summary>
    public async Task<int> CountAsync(ISpecification<T> spec)
    {
        var query = ApplySpecification(spec);
        var output = await query.CountAsync();
        return output;
    }

    /// <summary>
    /// Methodh whics gets entities with no specifications.
    /// </summary>
    /// <returns></returns>
    public async Task<IReadOnlyList<T>> GetAllAsync()
    {
        var output = await _db.Set<T>().ToListAsync();
        return output;
    }

    /// <summary>
    /// Method which gets entities based on an speficiation.
    /// </summary>
    public async Task<IReadOnlyList<T>> GetAllAsync(ISpecification<T> spec)
    {
        var query = ApplySpecification(spec);
        var output = await query.ToListAsync();
        return output;
    }

    /// <summary>
    /// Adds a record to the database with tracking.
    /// </summary>
    public void Add(T entity)
    {
        _db.Set<T>().Add(entity);
    }

    /// <summary>
    /// Updates a record with tracking.
    /// </summary>
    public void Update(T entity)
    {
        _db.Set<T>().Attach(entity);
        _db.Entry(entity).State = EntityState.Modified;
    }

    /// <summary>
    /// Method which removes a record from the database.
    /// </summary>
    public void Delete(T entity)
    {
        _db.Set<T>().Remove(entity);
    }

    /// <summary>
    /// Method which commits actions to the database.
    /// </summary>
    public async Task CompleteAsync()
    {
        await _db.SaveChangesAsync();
    }

    /// <summary>
    /// Method which builds an IQueryable to execute in the GET methods in this repository.
    /// </summary>
    private IQueryable<T> ApplySpecification(ISpecification<T> spec)
    {
        var query = _db.Set<T>().AsQueryable();
        var output = SpecificationEvaluator<T>.GetQuery(query, spec);
        return output;
    }
}
