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
    public async Task<T?> GetAsync(int id) => await _db.Set<T>().FindAsync(id);

    /// <summary>
    /// Method which gets an entity based on specification.
    /// </summary>
    public async Task<T?> GetAsync(ISpecification<T> spec)
    {
        var query = ApplySpecification(spec);
        var output = await query.FirstOrDefaultAsync();
        return output;
    }

    /// <summary>
    /// Method which gets an entity with no tracking.
    /// </summary>
    public async Task<T?> GetAsyncNoTracking(int id) => await _db.Set<T>().AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);

    /// <summary>
    /// Method which gets an entity with no tracking.
    /// </summary>
    public async Task<T?> GetAsyncNoTracking(ISpecification<T> spec)
    {
        var query = ApplySpecification(spec).AsNoTracking();
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
    public async Task<IReadOnlyList<T>> GetAllAsync() => await _db.Set<T>().ToListAsync();

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
    public void Add(T entity) => _db.Set<T>().Add(entity);

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
    public void Delete(T entity) => _db.Set<T>().Remove(entity);

    /// <summary>
    /// Method which deletes by id
    /// </summary>
    public void Delete(int id) => _db.Set<T>().Where(e => e.Id == id).ExecuteDelete();

    /// <summary>
    /// Method which handles collection comparisons and detects what has changed.
    /// </summary>
    public async Task UpdateCollectionAsync(ICollection<T> current, List<int> incomingIds)
    {
        var currentIds = current.Select(x => x.Id).ToList();

        // Remove items
        var toRemove = current.Where(x => !incomingIds.Contains(x.Id)).ToList();
        foreach (var r in toRemove) current.Remove(r);

        // Add new items
        var toAddIds = incomingIds.Except(currentIds).ToList();
        if (toAddIds.Any())
        {
            var toAdd = await Query().Where(x => toAddIds.Contains(x.Id)).ToListAsync();
            foreach (var a in toAdd) current.Add(a);
        }
    }

    /// <summary>
    /// Exposes a query for easy specific searching.
    /// </summary>
    /// <returns></returns>
    public IQueryable<T> Query() => _db.Set<T>().AsQueryable();

    /// <summary>
    /// Method which builds an IQueryable to execute in the GET methods in this repository.
    /// </summary>
    private IQueryable<T> ApplySpecification(ISpecification<T> spec)
    {
        var query = _db.Set<T>().AsQueryable();
        query = SpecificationEvaluator<T>.GetQuery(query, spec);
        return query;
    }
}
