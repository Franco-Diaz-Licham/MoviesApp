
namespace backend.src.Application.Interfaces;

public interface IMovieService
{
    Task<MovieDTO> CreateAsync(MovieDTO dto);
    Task<List<MovieDTO>> GetAllAsync();
    Task<List<MovieDTO>> GetAllDetailsAsync();
    Task<MovieDTO> UpdateAsync(MovieDTO dto);
}
