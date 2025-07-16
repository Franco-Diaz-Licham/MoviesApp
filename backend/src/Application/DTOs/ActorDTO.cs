namespace backend.src.Application.DTOs;

public class ActorDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime Dob { get; set; }
    public string Biography { get; set; } = string.Empty;
    public int PhotoId { get; set; }
    public PhotoDTO? Photo { get; set; }
    public List<MovieDTO> Movies { get; set; } = new();
}
