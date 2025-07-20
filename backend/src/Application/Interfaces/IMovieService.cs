
namespace backend.src.Application.Interfaces;

public interface IMovieService
{
    Task<MovieDTO> CreateAsync(MovieCreateDTO dto);
    Task<bool> DeleteAsync(int id);
    Task<List<MovieDTO>> GetAllAsync();
    Task<List<MovieDTO>> GetAllDetailsAsync();
    Task<MovieDTO?> GetAsync(int id);
    Task<bool> GetAsyncCheck(int id);
    Task<MovieDTO> UpdateAsync(MovieUpdateDTO dto);
}
