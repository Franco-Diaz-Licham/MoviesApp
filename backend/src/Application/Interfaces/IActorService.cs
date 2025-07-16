

namespace backend.src.Application.Interfaces;

public interface IActorService
{
    Task<ActorDTO> CreateAsync(ActorDTO dto);
    Task<bool> DeleteAsync(int id);
    Task<List<ActorDTO>> GetAllAsync();
    Task<ActorDTO?> GetAsync(int id);
    Task<bool> GetAsyncCheck(int id);
    Task<ActorDTO> UpdateAsync(ActorDTO dto);
}
