namespace backend.src.Infrastructure.Services;

public class TheatreService : ITheatreService
{
    private readonly IMemoryCache _cache;
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;
    private const string CACHE_KEY = nameof(TheatreResponse);

    public TheatreService(IMemoryCache cache, IUnitOfWork uow, IMapper mapper)
    {
        _cache = cache;
        _uow = uow;
        _mapper = mapper;
    }

    /// <summary>
    /// Method whicch gets all theatres.
    /// </summary>
    public async Task<List<TheatreDTO>> GetAllAsync()
    {
        var output = await _cache.GetOrCreateAsync(CACHE_KEY, async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(6);
            var models = await _uow.GetRepository<TheatreEntity>().GetAllAsync();
            var output = _mapper.Map<List<TheatreDTO>>(models.ToList());
            return output;
        });

        return output ?? new();
    }

    /// <summary>
    /// Method which gets a genre.
    /// </summary>
    public async Task<TheatreDTO?> GetAsync(int id)
    {
        var model = await _uow.GetRepository<TheatreEntity>().GetAsync(id);
        var output = _mapper.Map<TheatreDTO>(model);
        return output;
    }

    /// <summary>
    /// Method whic creates a threatre.
    /// </summary>
    public async Task<TheatreDTO> CreateAsync(TheatreDTO dto)
    {
        var model = _mapper.Map<TheatreEntity>(dto);
        _uow.GetRepository<TheatreEntity>().Add(model);
        await _uow.CompleteAsync();
        var output = _mapper.Map<TheatreDTO>(model);
        _cache.Remove(CACHE_KEY);
        return output;
    }

    /// <summary>
    /// Method which updates a theatre.
    /// </summary>
    public async Task<TheatreDTO> UpdateAsync(TheatreDTO dto)
    {
        var model = _mapper.Map<TheatreEntity>(dto);
        _uow.GetRepository<TheatreEntity>().Update(model);
        await _uow.CompleteAsync();
        var output = _mapper.Map<TheatreDTO>(model);
        _cache.Remove(CACHE_KEY);
        return output;
    }

    /// <summary>
    /// Method which deletes a genre.
    /// </summary>
    public async Task<bool> DeleteAsync(int id)
    {
        var model = await _uow.GetRepository<TheatreEntity>().GetAsyncNoTracking(id);
        if (model is null) return false;
        _uow.GetRepository<TheatreEntity>().Delete(id);
        await _uow.CompleteAsync();
        _cache.Remove(CACHE_KEY);
        return true;
    }
}
