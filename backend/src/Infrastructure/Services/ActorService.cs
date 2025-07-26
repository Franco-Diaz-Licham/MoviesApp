namespace backend.src.Infrastructure.Services;

public class ActorService : IActorService
{
    private readonly IMemoryCache _cache;
    private readonly IPhotoService _photoService;
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;
    private const string CACHE_KEY = nameof(ActorResponse);

    public ActorService(IMemoryCache cache, IUnitOfWork uow, IMapper mapper, IPhotoService photoService)
    {
        _cache = cache;
        _uow = uow;
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
            var models = await _uow.GetRepository<ActorEntity>().GetAllAsync(specs);
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
        var model = await _uow.GetRepository<ActorEntity>().GetAsync(specs);
        var output = _mapper.Map<ActorDTO>(model);
        return output;
    }

    /// <summary>
    /// Method used to check whether entry exits as a record.
    /// </summary>
    public async Task<bool> GetAsyncCheck(int id)
    {
        var model = await _uow.GetRepository<ActorEntity>().GetAsyncNoTracking(id);
        if (model is not null) return true;
        return false;
    }

    /// <summary>
    /// Method which creates an actor. Handles saving photos to cloudinary.
    /// </summary>
    public async Task<ActorDTO> CreateAsync(ActorDTO dto)
    {
        // Validate photo exists
        if (dto.Photo is null) throw new Exception("Photo is empty");
        await using var transaction = await _uow.BeginTransactionAsync();

        try
        {
            // Save and ensure not to create photo again.
            var createdPhoto = await _photoService.CreateProfileImageAsync(dto.Photo);
            dto.PhotoId = createdPhoto.Id;
            dto.Photo = null;
            var model = _mapper.Map<ActorEntity>(dto);
            _uow.GetRepository<ActorEntity>().Add(model);
            await _uow.CompleteAsync();
            await transaction.CommitAsync();

            // Return
            var output = _mapper.Map<ActorDTO>(model);
            _cache.Remove(CACHE_KEY);
            return output;
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    /// <summary>
    /// Method which updates actor information. Handles a change in photo.
    /// </summary>
    public async Task<ActorDTO> UpdateAsync(ActorDTO dto)
    {
        await using var transaction = await _uow.BeginTransactionAsync();

        try
        {
            // Retrieve existing hydrated actor from DB
            var specs = new ActorDetailsSpecs(dto.Id);
            var actor = await _uow.GetRepository<ActorEntity>().GetAsync(specs);
            if (actor is null) throw new Exception("Actor not found");
            PhotoDTO? oldPhoto = null;

            // Replace photo if needed
            if (dto.Photo is not null)
            {
                var newPhoto = await _photoService.CreateProfileImageAsync(dto.Photo);
                oldPhoto = _mapper.Map<PhotoDTO>(actor.Photo);
                actor.PhotoId = newPhoto.Id;
            }

            // Update actor fields
            actor.Name = dto.Name;
            actor.Dob = dto.Dob;
            actor.Biography = dto.Biography;
            actor.UpdatedOn = DateTime.UtcNow;

            // save and commit transaction
            _uow.GetRepository<ActorEntity>().Update(actor);
            await _uow.CompleteAsync();
            await transaction.CommitAsync();

            // Delete old photo after transaction
            if (oldPhoto is not null) await _photoService.DeleteAsync(oldPhoto);
            _cache.Remove(CACHE_KEY);
            var output = await GetAsync(actor.Id);
            return output!;
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    /// <summary>
    /// Method which deletes an actor, deletes the photo in the Db and cloudinary.
    /// </summary>
    public async Task<bool> DeleteAsync(int id)
    {
        await using var transaction = await _uow.BeginTransactionAsync();

        try
        {
            // get actor
            var specs = new ActorDetailsSpecs(id);
            var model = await _uow.GetRepository<ActorEntity>().GetAsyncNoTracking(specs);
            if (model is null) return false;

            // delete actor with photo
            _uow.GetRepository<ActorEntity>().Delete(model);
            await _uow.CompleteAsync();
            await transaction.CommitAsync();

            // Delete photo 
            if (model.Photo is not null)
            {
                var photo = _mapper.Map<PhotoDTO>(model.Photo);
                await _photoService.DeleteAsync(photo);
            }
            _cache.Remove(CACHE_KEY);
            return true;
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}
