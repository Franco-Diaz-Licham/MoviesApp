
namespace backend.src.Infrastructure.Services;

public interface IPhotoService
{
    Task<PhotoDTO> CreatePosterImageAsync(PhotoDTO dto);
    Task<PhotoDTO> CreateProfileImageAsync(PhotoDTO dto);
    Task<bool> DeleteAsync(PhotoDTO dto);
    Task<PhotoDTO?> GetAsync(int id);
}