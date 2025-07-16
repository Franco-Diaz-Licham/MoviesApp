namespace backend.src.API.Models.Request;

public class ActorUpdateRequest
{
    [Required] public int Id { get; set; }
    [Required, MinLength(3)] public string Name { get; set; } = default!;
    [Required] public DateTime Dob { get; set; }
    [Required, MinLength(3)] public string Biography { get; set; } = default!;
    public PhotoCreateRequest? Photo { get; set; }
}
