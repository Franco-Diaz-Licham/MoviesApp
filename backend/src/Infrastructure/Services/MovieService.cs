namespace backend.src.Infrastructure.Services;

public class MovieService : IMovieService
{
    private readonly IMemoryCache _cache;
    private readonly IGenericRepository<MovieEntity> _movieRepo;
    private readonly IMapper _mapper;
    private const string CACHE_KEY = nameof(MovieResponse);
    private const string CACHE_DETAILS_KET = CACHE_KEY + "Details";

    public MovieService(IMemoryCache cache, IGenericRepository<MovieEntity> movieRepo, IMapper mapper)
    {
        _cache = cache;
        _movieRepo = movieRepo;
        _mapper = mapper;
    }

    public async Task<List<MovieDTO>> GetAllAsync()
    {
        var output = await _cache.GetOrCreateAsync(CACHE_KEY, async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(6);
            var models = await _movieRepo.GetAllAsync();
            var output = _mapper.Map<List<MovieDTO>>(models.ToList());
            return output;
        });

        return output ?? new();
    }

    public async Task<List<MovieDTO>> GetAllDetailsAsync()
    {
        var output = await _cache.GetOrCreateAsync(CACHE_DETAILS_KET, async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(6);
            var specs = new MovieDetailsSpecs();
            var models = await _movieRepo.GetAllAsync(specs);
            var output = _mapper.Map<List<MovieDTO>>(models.ToList());
            return output;
        });

        return output ?? new();
    }

    public async Task<MovieDTO> CreateAsync(MovieDTO dto)
    {
        var model = _mapper.Map<MovieEntity>(dto);
        _movieRepo.Add(model);
        await _movieRepo.CompleteAsync();
        var output = _mapper.Map<MovieDTO>(model);
        _cache.Remove(CACHE_KEY);
        _cache.Remove(CACHE_DETAILS_KET);
        return output;
    }

    public async Task<MovieDTO> UpdateAsync(MovieDTO dto)
    {
        var model = _mapper.Map<MovieEntity>(dto);
        _movieRepo.Update(model);
        await _movieRepo.CompleteAsync();
        var output = _mapper.Map<MovieDTO>(model);
        _cache.Remove(CACHE_KEY);
        _cache.Remove(CACHE_DETAILS_KET);
        return output;
    }
}
