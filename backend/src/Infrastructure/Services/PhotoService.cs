namespace backend.src.Infrastructure.Services;

public class PhotoService : IPhotoService
{
    private readonly ICloudinaryService _cloudinaryService;
    private readonly IGenericRepository<PhotoEntity> _photoRepo;
    private readonly IMapper _mapper;
    private readonly ILogger<PhotoService> _logger;

    public PhotoService(
            IGenericRepository<PhotoEntity> PhotoRepo, 
            IMapper mapper, 
            ICloudinaryService cloudinaryService,
            ILogger<PhotoService> logger)
    {
        _photoRepo = PhotoRepo;
        _mapper = mapper;
        _cloudinaryService = cloudinaryService;
        _logger = logger;
    }

    public async Task<PhotoDTO?> GetAsync(int id)
    {
        var model = await _photoRepo.GetAsync(id);
        var output = _mapper.Map<PhotoDTO>(model);
        return output;
    }

    public async Task<PhotoDTO> CreateAsync(PhotoDTO dto)
    {
        // create image
        if (dto.Image is null) throw new Exception("Photo is empty");
        var img = await _cloudinaryService.UploadPhotoAsync(dto.Image);

        // map and save
        dto.PublicUrl = img.SecureUrl.ToString();
        dto.PublicId = img.PublicId;
        var model = _mapper.Map<PhotoEntity>(dto);

        // save and return
        _photoRepo.Add(model);
        await _photoRepo.CompleteAsync();
        return _mapper.Map<PhotoDTO>(model);
    }

    public async Task<bool> DeleteAsync(PhotoDTO dto)
    {
        var entity = await _photoRepo.GetAsyncNoTracking(dto.Id);
        if (entity is not null)
        {
            _photoRepo.Delete(dto.Id);
            await _photoRepo.CompleteAsync();
        }

        // Cloudinary deletion should be idempotent
        await _cloudinaryService.DeletePhotoAsync(dto.PublicId);
        return true;
    }
}
