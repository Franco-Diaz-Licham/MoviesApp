
namespace backend.src.Application.Interfaces;

public interface IGenreService
{
    Task<GenreDTO> CreateAsync(GenreDTO dto);
    Task<List<GenreDTO>> GetAllAsync();
}
