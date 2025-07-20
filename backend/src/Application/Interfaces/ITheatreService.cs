
namespace backend.src.Application.Interfaces;

public interface ITheatreService
{
    Task<TheatreDTO> CreateAsync(TheatreDTO dto);
    Task<bool> DeleteAsync(int id);
    Task<List<TheatreDTO>> GetAllAsync();
    Task<TheatreDTO?> GetAsync(int id);
    Task<TheatreDTO> UpdateAsync(TheatreDTO dto);
}
