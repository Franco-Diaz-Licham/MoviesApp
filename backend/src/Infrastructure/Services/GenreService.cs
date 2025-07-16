namespace backend.src.Infrastructure.Services;

public class GenreService : IGenreService
{
    private readonly IMemoryCache _cache;
    private readonly IGenericRepository<GenreEntity> _genreRepo;
    private readonly IMapper _mapper;
    private const string CACHE_KEY = nameof(GenreResponse);

    public GenreService(IMemoryCache cache, IGenericRepository<GenreEntity> genreRepo, IMapper mapper)
    {
        _cache = cache;
        _genreRepo = genreRepo;
        _mapper = mapper;
    }

    public async Task<List<GenreDTO>> GetAllAsync()
    {
        var output = await _cache.GetOrCreateAsync(CACHE_KEY, async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(6);
            var models = await _genreRepo.GetAllAsync();
            var output = _mapper.Map<List<GenreDTO>>(models.ToList());
            return output;
        });

        return output ?? new();
    }

    public async Task<GenreDTO?> GetAsync(int id)
    {
        var model = await _genreRepo.GetAsync(id);
        var output = _mapper.Map<GenreDTO>(model);
        return output;
    }

    public async Task<GenreDTO> CreateAsync(GenreDTO dto)
    {
        var model = _mapper.Map<GenreEntity>(dto);
        _genreRepo.Add(model);
        await _genreRepo.CompleteAsync();
        var output = _mapper.Map<GenreDTO>(model);
        _cache.Remove(CACHE_KEY);
        return output;
    }

    public async Task<GenreDTO> UpdateAsync(GenreDTO dto)
    {
        var model = _mapper.Map<GenreEntity>(dto);
        _genreRepo.Update(model);
        await _genreRepo.CompleteAsync();
        var output = _mapper.Map<GenreDTO>(model);
        _cache.Remove(CACHE_KEY);
        return output;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var model = await _genreRepo.GetAsyncNoTracking(id);
        if (model is null) return false;
        var toDelete = _mapper.Map<GenreEntity>(model);
        _genreRepo.Delete(toDelete);
        await _genreRepo.CompleteAsync();
        _cache.Remove(CACHE_KEY);
        return true;
    }
}
