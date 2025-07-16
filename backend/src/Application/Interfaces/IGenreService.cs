
namespace backend.src.Application.Interfaces;

public interface IGenreService
{
    Task<GenreDTO> CreateAsync(GenreDTO dto);
    Task<bool> DeleteAsync(int id);
    Task<List<GenreDTO>> GetAllAsync();
    Task<GenreDTO?> GetAsync(int id);
    Task<GenreDTO> UpdateAsync(GenreDTO dto);
}
