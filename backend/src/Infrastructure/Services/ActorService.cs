namespace backend.src.Infrastructure.Services;

public class ActorService : IActorService
{
    private readonly IMemoryCache _cache;
    private readonly IGenericRepository<ActorEntity> _actorRepo;
    private readonly IMapper _mapper;
    private const string CACHE_KEY = nameof(ActorResponse);

    public ActorService(IMemoryCache cache, IGenericRepository<ActorEntity> actorRepo, IMapper mapper)
    {
        _cache = cache;
        _actorRepo = actorRepo;
        _mapper = mapper;
    }

    public async Task<List<ActorDTO>> GetAllAsync()
    {
        var output = await _cache.GetOrCreateAsync(CACHE_KEY, async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(6);
            var models = await _actorRepo.GetAllAsync();
            var output = _mapper.Map<List<ActorDTO>>(models.ToList());
            return output;
        });

        return output ?? new();
    }

    public async Task<ActorDTO> CreateAsync(ActorDTO dto)
    {
        var model = _mapper.Map<ActorEntity>(dto);
        _actorRepo.Add(model);
        await _actorRepo.CompleteAsync();
        var output = _mapper.Map<ActorDTO>(model);
        _cache.Remove(CACHE_KEY);
        return output;
    }

    public async Task<ActorDTO> UpdateAsync(ActorDTO dto)
    {
        var model = _mapper.Map<ActorEntity>(dto);
        _actorRepo.Update(model);
        await _actorRepo.CompleteAsync();
        var output = _mapper.Map<ActorDTO>(model);
        _cache.Remove(CACHE_KEY);
        return output;
    }
}
