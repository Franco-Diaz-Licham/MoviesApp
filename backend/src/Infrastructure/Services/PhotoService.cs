namespace backend.src.Infrastructure.Services;

public class PhotoService : IPhotoService
{
    private readonly ICloudinaryService _cloudinaryService;
    private readonly IGenericRepository<PhotoEntity> _photoRepo;
    private readonly IMapper _mapper;

    public PhotoService(IGenericRepository<PhotoEntity> PhotoRepo, IMapper mapper, ICloudinaryService cloudinaryService)
    {
        _photoRepo = PhotoRepo;
        _mapper = mapper;
        _cloudinaryService = cloudinaryService;
    }

    public async Task<PhotoDTO?> GetAsync(int id)
    {
        var model = await _photoRepo.GetAsync(id);
        var output = _mapper.Map<PhotoDTO>(model);
        return output;
    }

    public async Task<PhotoDTO> CreateTransactionAsync(PhotoDTO dto)
    {
        // create image
        if (dto.Image is null) throw new("Photo is empty");
        var img = await _cloudinaryService.UploadPhotoAsync(dto.Image);

        // map and save
        dto.PublicUrl = img.SecureUrl.ToString();
        dto.PublicId = img.PublicId;
        var model = _mapper.Map<PhotoEntity>(dto);

        // save and return
        _photoRepo.Add(model);
        return _mapper.Map<PhotoDTO>(model);
    }

    public async Task<bool> DeleteTransactionAsync(int id)
    {
        var model = await _photoRepo.GetAsyncNoTracking(id);
        if (model is null) throw new Exception("Photo model not found");
        await _cloudinaryService.DeletePhotoAsync(model.PublicId);
        var toDelete = _mapper.Map<PhotoEntity>(model);
        _photoRepo.Delete(toDelete);
        return true;
    }
}
