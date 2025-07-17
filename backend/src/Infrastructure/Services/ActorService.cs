namespace backend.src.Infrastructure.Services;

public class ActorService : IActorService
{
    private readonly IMemoryCache _cache;
    private readonly IPhotoService _photoService;
    private readonly IGenericRepository<ActorEntity> _actorRepo;
    private readonly IMapper _mapper;
    private const string CACHE_KEY = nameof(ActorResponse);

    public ActorService(IMemoryCache cache, IGenericRepository<ActorEntity> actorRepo, IMapper mapper, IPhotoService photoService)
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

    /// <summary>
    /// Method which creates an actor. Handles saving photos to cloudinary.
    /// </summary>
    public async Task<ActorDTO> CreateAsync(ActorDTO dto)
    {
        // Validate photo exists
        if (dto.Photo is null) throw new Exception("Photo is empty");
        await using var transaction = await _actorRepo.GetDbContext.Database.BeginTransactionAsync();

        try
        {
            // Save and ensure not to create photo again.
            dto.PhotoId = (await _photoService.CreateAsync(dto.Photo)).Id;
            dto.Photo = null;
            var model = _mapper.Map<ActorEntity>(dto);
            _actorRepo.Add(model);
            await _actorRepo.CompleteAsync();
            await transaction.CommitAsync();

            // Return
            var output = _mapper.Map<ActorDTO>(model);
            _cache.Remove(CACHE_KEY);
            return output;
        }
        catch
        {
            if (transaction.GetDbTransaction().Connection != null) await transaction.RollbackAsync();
            throw;
        }
    }

    /// <summary>
    /// Method which updates actor information. Handles a change in photo.
    /// </summary>
    public async Task<ActorDTO> UpdateAsync(ActorDTO dto)
    {
        await using var transaction = await _actorRepo.GetDbContext.Database.BeginTransactionAsync();

        try
        {
            // Retrieve existing hydrated actor from DB
            var specs = new ActorDetailsSpecs(dto.Id);
            var existingActor = await _actorRepo.GetAsync(specs);
            if (existingActor is null) throw new Exception("Actor not found");
            PhotoDTO? oldPhoto = null;

            // Replace photo if needed
            if (dto.Photo is not null)
            {
                var newPhoto = await _photoService.CreateAsync(dto.Photo);
                oldPhoto = _mapper.Map<PhotoDTO>(existingActor.Photo);
                existingActor.PhotoId = newPhoto.Id;
            }

            // Update actor fields
            existingActor.Name = dto.Name;
            existingActor.Dob = dto.Dob;
            existingActor.Biography = dto.Biography;
            existingActor.UpdatedOn = DateTime.UtcNow;

            // save and commit transaction
            _actorRepo.Update(existingActor);
            await _actorRepo.CompleteAsync();
            await transaction.CommitAsync();

            // Delete old photo after transaction
            if (oldPhoto is not null) await _photoService.DeleteAsync(oldPhoto);
            _cache.Remove(CACHE_KEY);
            var output = await GetAsync(existingActor.Id);
            return output!;
        }
        catch
        {
            if (transaction.GetDbTransaction().Connection != null) await transaction.RollbackAsync();
            throw;
        }
    }

    /// <summary>
    /// Method which deletes an actor, deletes the photo in the Db and cloudinary.
    /// </summary>
    public async Task<bool> DeleteAsync(int id)
    {
        await using var transaction = await _actorRepo.GetDbContext.Database.BeginTransactionAsync();

        try
        {
            // get actor
            var specs = new ActorDetailsSpecs(id);
            var model = await _actorRepo.GetAsyncNoTracking(specs);
            if (model is null) return false;

            // delete actor with photo
            var photo = _mapper.Map<PhotoDTO>(model.Photo);
            _actorRepo.Delete(model);
            await _actorRepo.CompleteAsync();
            await transaction.CommitAsync();

            // delete 
            await _photoService.DeleteAsync(photo);
            _cache.Remove(CACHE_KEY);
            return true;
        }
        catch
        {
            if (transaction.GetDbTransaction().Connection != null) await transaction.RollbackAsync();
            throw;
        }
    }
}
