namespace backend.src.Infrastructure.Services;

public class MovieService : IMovieService
{
    private readonly IMemoryCache _cache;
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;
    private readonly IPhotoService _photoService;
    private const string CACHE_KEY = nameof(MovieResponse);

    public MovieService(IMemoryCache cache, IUnitOfWork uow, IMapper mapper, IPhotoService photoService)
    {
        _cache = cache;
        _uow = uow;
        _mapper = mapper;
        _photoService = photoService;
    }

    /// <summary>
    /// Method which gets movies with limited model hydration.
    /// </summary>
    public async Task<List<MovieDTO>> GetAllAsync()
    {
        var output = await _cache.GetOrCreateAsync(CACHE_KEY, async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(6);
            var specs = new MovieSpecs();
            var models = await _uow.GetRepository<MovieEntity>().GetAllAsync(specs);
            var output = _mapper.Map<List<MovieDTO>>(models.ToList());
            return output;
        });

        return output ?? new();
    }

    /// <summary>
    /// Method which gets movies with ful model hydration.
    /// </summary>
    public async Task<List<MovieDTO>> GetAllDetailsAsync()
    {
        var output = await _cache.GetOrCreateAsync(CACHE_KEY, async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(6);
            var specs = new MovieDetailsSpecs();
            var models = await _uow.GetRepository<MovieEntity>().GetAllAsync(specs);
            var output = _mapper.Map<List<MovieDTO>>(models.ToList());
            return output;
        });

        return output ?? new();
    }

    /// <summary>
    /// Method which gets single movie.
    /// </summary>
    public async Task<MovieDTO?> GetAsync(int id)
    {
        var specs = new MovieDetailsSpecs(id);
        var model = await _uow.GetRepository<MovieEntity>().GetAsync(specs);
        var output = _mapper.Map<MovieDTO>(model);
        return output;
    }

    /// <summary>
    /// Method used to check whether entry exits as a record.
    /// </summary>
    public async Task<bool> GetAsyncCheck(int id)
    {
        var model = await _uow.GetRepository<MovieEntity>().GetAsyncNoTracking(id);
        if (model != null) return true;
        return false;
    }

    /// <summary>
    /// Method which creates a movie.
    /// </summary>
    public async Task<MovieDTO> CreateAsync(MovieCreateDTO dto)
    {
        var model = _mapper.Map<MovieEntity>(dto);
        await using var transaction = await _uow.BeginTransactionAsync();

        try
        {
            // Load and set related models.
            if (dto.Genres?.Any() == true) model.Genres = await _uow.GetRepository<GenreEntity>().Query().Where(g => dto.Genres.Contains(g.Id)).ToListAsync();
            if (dto.Actors?.Any() == true) model.Actors = await _uow.GetRepository<ActorEntity>().Query().Where(g => dto.Actors.Contains(g.Id)).ToListAsync();
            if (dto.Theatres?.Any() == true) model.Theatres = await _uow.GetRepository<TheatreEntity>().Query().Where(g => dto.Theatres.Contains(g.Id)).ToListAsync();
            if (dto.Photo is not null)
            {
                var photo = _mapper.Map<PhotoDTO>(dto.Photo);
                model.PhotoId = (await _photoService.CreatePosterImageAsync(photo)).Id;
                model.Photo = null;
            }

            // Save and return
            _uow.GetRepository<MovieEntity>().Add(model);
            await _uow.CompleteAsync();
            await transaction.CommitAsync();
            var output = _mapper.Map<MovieDTO>(model);
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
    /// Method whic updates a movie.
    /// </summary>
    public async Task<MovieDTO> UpdateAsync(MovieUpdateDTO dto)
    {
        await using var transaction = await _uow.BeginTransactionAsync();

        try
        {
            var specs = new MovieDetailsSpecs(dto.Id);
            var movie = await _uow.GetRepository<MovieEntity>().GetAsync(specs);
            if (movie == null) throw new Exception("Not found");

            // Basic fields
            movie.Title = dto.Title;
            movie.InTheatresFlag = dto.InTheatresFlag;
            movie.UpComingFlag = dto.UpComingFlag;

            // Replace collections
            if (dto.Genres?.Any() == true) await _uow.GetRepository<GenreEntity>().UpdateCollectionAsync(movie.Genres, dto.Genres);
            if (dto.Actors?.Any() == true) await _uow.GetRepository<ActorEntity>().UpdateCollectionAsync(movie.Actors, dto.Actors);
            if (dto.Theatres?.Any() == true) await _uow.GetRepository<TheatreEntity>().UpdateCollectionAsync(movie.Theatres, dto.Theatres);
            if (dto.Photo is not null)
            {
                var newPhoto = await _photoService.CreatePosterImageAsync(dto.Photo);
                movie.PhotoId = newPhoto.Id;
            }

            _uow.GetRepository<MovieEntity>().Update(movie);
            await _uow.CompleteAsync();
            await transaction.CommitAsync();

            // Delete old photo after transaction
            if (dto.Photo is not null)
            {
                var oldPhoto = _mapper.Map<PhotoDTO>(movie.Photo);
                await _photoService.DeleteAsync(oldPhoto);
            }
            var output = _mapper.Map<MovieDTO>(movie);
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
    /// Method which deletes an actor, deletes the photo in the Db and cloudinary.
    /// </summary>
    public async Task<bool> DeleteAsync(int id)
    {
        await using var transaction = await _uow.BeginTransactionAsync();

        try
        {
            // get Movie
            var specs = new MovieDetailsSpecs(id);
            var model = await _uow.GetRepository<MovieEntity>().GetAsyncNoTracking(specs);
            if (model is null) return false;

            // delete Movie with photo
            _uow.GetRepository<MovieEntity>().Delete(model);
            await _uow.CompleteAsync();
            await transaction.CommitAsync();

            // Delete Photo
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
            if (transaction.GetDbTransaction().Connection != null) await transaction.RollbackAsync();
            throw;
        }
    }
}
