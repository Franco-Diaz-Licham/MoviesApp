namespace backend.src.API.Models.Request;

public class ActorCreateRequest
{
    [Required] public string Name { get; set; } = string.Empty;
    [Required] public DateTime Dob { get; set; }
    [Required] public string Biography { get; set; } = string.Empty;
    [Required] public required PhotoCreateRequest Photo { get; set; }
}
