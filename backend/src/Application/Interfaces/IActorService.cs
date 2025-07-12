

namespace backend.src.Application.Interfaces;

public interface IActorService
{
    Task<ActorDTO> CreateAsync(ActorDTO dto);
    Task<List<ActorDTO>> GetAllAsync();
    Task<ActorDTO> UpdateAsync(ActorDTO dto);
}
