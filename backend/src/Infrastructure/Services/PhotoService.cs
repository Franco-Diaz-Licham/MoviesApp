namespace backend.src.Infrastructure.Services;

public class PhotoService : IPhotoService
{
    private readonly ICloudinaryService _cloudinaryService;
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public PhotoService(IUnitOfWork uow, IMapper mapper, ICloudinaryService cloudinaryService)
    {
        _uow = uow;
        _mapper = mapper;
        _cloudinaryService = cloudinaryService;
    }

    /// <summary>
    /// Method which fetches a photo from the database.
    /// </summary>
    public async Task<PhotoDTO?> GetAsync(int id)
    {
        var model = await _uow.GetRepository<PhotoEntity>().GetAsync(id);
        var output = _mapper.Map<PhotoDTO>(model);
        return output;
    }

    /// <summary>
    /// Method which creates a photo and uploads to cloudinary.
    /// </summary>
    public async Task<PhotoDTO> CreateProfileImageAsync(PhotoDTO dto)
    {
        // create image
        if (dto.Image is null) throw new Exception("Photo is empty");
        var transform = new Transformation().Height(500).Width(500).Crop("fill").Gravity("face");
        var img = await _cloudinaryService.UploadPhotoAsync(dto.Image, transform);

        // map and save
        dto.PublicUrl = img.SecureUrl.ToString();
        dto.PublicId = img.PublicId;
        var model = _mapper.Map<PhotoEntity>(dto);

        // save and return
        _uow.GetRepository<PhotoEntity>().Add(model);
        await _uow.CompleteAsync();
        return _mapper.Map<PhotoDTO>(model);
    }

    public async Task<PhotoDTO> CreatePosterImageAsync(PhotoDTO dto)
    {
        // create image
        if (dto.Image is null) throw new Exception("Photo is empty");
        var img = await _cloudinaryService.UploadPhotoAsync(dto.Image, new());

        // map and save
        dto.PublicUrl = img.SecureUrl.ToString();
        dto.PublicId = img.PublicId;
        var model = _mapper.Map<PhotoEntity>(dto);

        // save and return
        _uow.GetRepository<PhotoEntity>().Add(model);
        await _uow.CompleteAsync();
        return _mapper.Map<PhotoDTO>(model);
    }

    /// <summary>
    /// Method which deletes a photo and deletes it from cloudinary.
    /// </summary>
    public async Task<bool> DeleteAsync(PhotoDTO dto)
    {
        var entity = await _uow.GetRepository<PhotoEntity>().GetAsyncNoTracking(dto.Id);
        if (entity is not null)
        {
            _uow.GetRepository<PhotoEntity>().Delete(dto.Id);
            await _uow.CompleteAsync();
        }
        await _cloudinaryService.DeletePhotoAsync(dto.PublicId);
        return true;
    }
}
