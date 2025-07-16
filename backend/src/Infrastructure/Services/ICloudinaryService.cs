
namespace backend.src.Infrastructure.Services
{
    public interface ICloudinaryService
    {
        Task<ImageUploadResult> SavePhotoAsync(IFormFile file);
        Task<DeletionResult> DeletePhotoAsync(string publicId);
    }
}