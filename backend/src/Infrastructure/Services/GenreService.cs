namespace backend.src.Infrastructure.Services;

public class GenreService : IGenreService
{
    private readonly IMemoryCache _cache;
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;
    private const string CACHE_KEY = nameof(GenreResponse);

    public GenreService(IMemoryCache cache, IUnitOfWork uow, IMapper mapper)
    {
        _cache = cache;
        _uow = uow;
        _mapper = mapper;
    }

    /// <summary>
    /// Method which gets all genres.
    /// </summary>
    public async Task<List<GenreDTO>> GetAllAsync()
    {
        var output = await _cache.GetOrCreateAsync(CACHE_KEY, async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(6);
            var models = await _uow.GetRepository<GenreEntity>().GetAllAsync();
            var output = _mapper.Map<List<GenreDTO>>(models.ToList());
            return output;
        });

        return output ?? new();
    }

    /// <summary>
    /// Method which gets a genre.
    /// </summary>
    public async Task<GenreDTO?> GetAsync(int id)
    {
        var model = await _uow.GetRepository<GenreEntity>().GetAsync(id);
        var output = _mapper.Map<GenreDTO>(model);
        return output;
    }

    /// <summary>
    /// Method creates a genre.
    /// </summary>
    public async Task<GenreDTO> CreateAsync(GenreDTO dto)
    {
        var model = _mapper.Map<GenreEntity>(dto);
        _uow.GetRepository<GenreEntity>().Add(model);
        await _uow.CompleteAsync();
        var output = _mapper.Map<GenreDTO>(model);
        _cache.Remove(CACHE_KEY);
        return output;
    }

    /// <summary>
    /// Method which updates a genre.
    /// </summary>
    public async Task<GenreDTO> UpdateAsync(GenreDTO dto)
    {
        var model = _mapper.Map<GenreEntity>(dto);
        _uow.GetRepository<GenreEntity>().Update(model);
        await _uow.CompleteAsync();
        var output = _mapper.Map<GenreDTO>(model);
        _cache.Remove(CACHE_KEY);
        return output;
    }

    /// <summary>
    /// Method which deletes a genre.
    /// </summary>
    public async Task<bool> DeleteAsync(int id)
    {
        var model = await _uow.GetRepository<GenreEntity>().GetAsyncNoTracking(id);
        if (model is null) return false;
        _uow.GetRepository<GenreEntity>().Delete(id);
        await _uow.CompleteAsync();
        _cache.Remove(CACHE_KEY);
        return true;
    }
}
