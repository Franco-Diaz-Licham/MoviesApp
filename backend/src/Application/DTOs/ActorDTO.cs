namespace backend.src.Application.DTOs;

public class ActorDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateOnly Dob { get; set; }
    public string? ImageUrl { get; set; }
    public string Biography { get; set; } = string.Empty;
}
