namespace backend.src.Infrastructure.Services;

public class TheatreService : ITheatreService
{
    private readonly IMemoryCache _cache;
    private readonly IGenericRepository<TheatreEntity> _theatreRepo;
    private readonly IMapper _mapper;
    private const string CACHE_KEY = nameof(TheatreResponse);

    public TheatreService(IMemoryCache cache, IGenericRepository<TheatreEntity> theatreRepo, IMapper mapper)
    {
        _cache = cache;
        _theatreRepo = theatreRepo;
        _mapper = mapper;
    }

    public async Task<List<TheatreDTO>> GetAllAsync()
    {
        var output = await _cache.GetOrCreateAsync(CACHE_KEY, async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(6);
            var models = await _theatreRepo.GetAllAsync();
            var output = _mapper.Map<List<TheatreDTO>>(models.ToList());
            return output;
        });

        return output ?? new();
    }

    public async Task<TheatreDTO> CreateAsync(TheatreDTO dto)
    {
        var model = _mapper.Map<TheatreEntity>(dto);
        _theatreRepo.Add(model);
        await _theatreRepo.CompleteAsync();
        var output = _mapper.Map<TheatreDTO>(model);
        _cache.Remove(CACHE_KEY);
        return output;
    }

    public async Task<TheatreDTO> UpdateAsync(TheatreDTO dto)
    {
        var model = _mapper.Map<TheatreEntity>(dto);
        _theatreRepo.Update(model);
        await _theatreRepo.CompleteAsync();
        var output = _mapper.Map<TheatreDTO>(model);
        _cache.Remove(CACHE_KEY);
        return output;
    }
}
