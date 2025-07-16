
namespace backend.src.Infrastructure.Services;

public interface IPhotoService
{
    Task<PhotoDTO> CreateTransactionAsync(PhotoDTO dto);
    Task<bool> DeleteTransactionAsync(int id);
    Task<PhotoDTO?> GetAsync(int id);
}