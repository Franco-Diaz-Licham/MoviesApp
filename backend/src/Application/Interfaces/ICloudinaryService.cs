namespace backend.src.Application.Interfaces;

public interface ICloudinaryService
{
    Task<ImageUploadResult> UploadPhotoAsync(IFormFile file, Transformation transform);
    Task<DeletionResult> DeletePhotoAsync(string publicId);
    Task<ImageUploadResult> UploadPhotoAsync(byte[] fileContent, string fileName, Transformation transform);
}