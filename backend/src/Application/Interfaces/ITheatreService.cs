
namespace backend.src.Application.Interfaces;

public interface ITheatreService
{
    Task<TheatreDTO> CreateAsync(TheatreDTO dto);
    Task<List<TheatreDTO>> GetAllAsync();
    Task<TheatreDTO> UpdateAsync(TheatreDTO dto);
}
