
namespace backend.src.Infrastructure.Services;

public interface IPhotoService
{
    Task<PhotoDTO> CreateAsync(PhotoDTO dto);
    Task<bool> DeleteAsync(PhotoDTO dto);
    Task<PhotoDTO?> GetAsync(int id);
}