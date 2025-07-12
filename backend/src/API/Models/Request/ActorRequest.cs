namespace backend.src.API.Models.Request;

public class ActorRequest
{
    [Required] public string Name { get; set; } = string.Empty;
    [Required] public DateOnly Dob { get; set; }
    public string? ImageUrl { get; set; }
    [Required] public string Biography { get; set; } = string.Empty;
}
