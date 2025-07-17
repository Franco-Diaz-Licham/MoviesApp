namespace backend.src.Infrastructure.Services;

public class ActorService : IActorService
{
    private readonly IMemoryCache _cache;
    private readonly IPhotoService _photoService;
    private readonly IGenericRepository<ActorEntity> _actorRepo;
    private readonly IMapper _mapper;
    private const string CACHE_KEY = nameof(ActorResponse);

    public ActorService(
            IMemoryCache cache,
            IGenericRepository<ActorEntity> actorRepo,
            IMapper mapper,
            IPhotoService photoService
)
    {
        _cache = cache;
        _actorRepo = actorRepo;
        _mapper = mapper;
        _photoService = photoService;
    }

    /// <summary>
    /// Method which gets all actors.
    /// </summary>
    public async Task<List<ActorDTO>> GetAllAsync()
    {
        var output = await _cache.GetOrCreateAsync(CACHE_KEY, async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(6);
            var specs = new ActorDetailsSpecs();
            var models = await _actorRepo.GetAllAsync(specs);
            var output = _mapper.Map<List<ActorDTO>>(models.ToList());
            return output;
        });

        return output ?? new();
    }

    /// <summary>
    /// Method which gets single actor.
    /// </summary>
    public async Task<ActorDTO?> GetAsync(int id)
    {
        var specs = new ActorDetailsSpecs(id);
        var model = await _actorRepo.GetAsync(specs);
        var output = _mapper.Map<ActorDTO>(model);
        return output;
    }

    /// <summary>
    /// Method used to check whether entry exits as a record.
    /// </summary>
    public async Task<Boolean> GetAsyncCheck(int id)
    {
        var model = await _actorRepo.GetAsyncNoTracking(id);
        if (model != null) return true;
        return false;
    }

    public async Task<ActorDTO> CreateAsync(ActorDTO dto)
    {
        // Validate photo exists
        if (dto.Photo is null) throw new Exception("Photo is empty");
        await using var transaction = await _actorRepo.GetDbContext.Database.BeginTransactionAsync();

        try
        {
            // Save
            dto.Photo = await _photoService.CreateTransactionAsync(dto.Photo);
            var model = _mapper.Map<ActorEntity>(dto);
            _actorRepo.Add(model);
            await _actorRepo.CompleteAsync();

            // Commit transaction
            await transaction.CommitAsync();

            // Return
            var output = _mapper.Map<ActorDTO>(model);
            _cache.Remove(CACHE_KEY);
            return output;
        }
        catch (Exception error)
        {
            if (transaction.GetDbTransaction().Connection != null) await transaction.RollbackAsync();
            throw new Exception(error.Message);
        }
    }

    public async Task<ActorDTO> UpdateAsync(ActorDTO dto)
    {
        await using var transaction = await _actorRepo.GetDbContext.Database.BeginTransactionAsync();

        try
        {
            // Retrieve existing actor from DB
            var existingActor = await _actorRepo.GetAsync(dto.Id);
            if (existingActor is null) throw new Exception("Actor not found");

            // Replace photo if needed
            if (dto.Photo is not null)
            {
                await _photoService.DeleteTransactionAsync(existingActor.PhotoId);
                var newPhoto = await _photoService.CreateTransactionAsync(dto.Photo);
                existingActor.PhotoId = newPhoto.Id;
            }

            // Update simple fields
            existingActor.Name = dto.Name;
            existingActor.Dob = dto.Dob;
            existingActor.Biography = dto.Biography;
            existingActor.UpdatedOn = DateTime.UtcNow;

            // Save
            _actorRepo.Update(existingActor);
            await _actorRepo.CompleteAsync();
            await transaction.CommitAsync();

            _cache.Remove(CACHE_KEY);
            var output = await GetAsync(existingActor.Id);
            return output!;
        }
        catch
        {
            // only rollback only is transction is not yet committed.
            if (transaction.GetDbTransaction().Connection != null) await transaction.RollbackAsync();
            throw new Exception("Could not save actor.");
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        await using var transaction = await _actorRepo.GetDbContext.Database.BeginTransactionAsync();

        try
        {
            // get actor
            var model = await _actorRepo.GetAsyncNoTracking(id);
            if (model is null) return false;

            // delete photo and acto
            await _photoService.DeleteTransactionAsync(model.PhotoId);
            var toDelete = _mapper.Map<ActorEntity>(model);
            _actorRepo.Delete(toDelete);

            // save return
            await _actorRepo.CompleteAsync();
            await transaction.CommitAsync();
            _cache.Remove(CACHE_KEY);
            return true;
        }
        catch
        {
            if (transaction.GetDbTransaction().Connection != null) await transaction.RollbackAsync();
            throw new Exception("Coould not save actor.");
        }
    }
}
