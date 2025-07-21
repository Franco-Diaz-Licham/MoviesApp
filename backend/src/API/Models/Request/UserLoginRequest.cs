namespace backend.src.API.Models.Request;

public class UserLoginRequest
{
    [Required] public string Email { get; set; } = default!;
    [Required] public string Password { get; set; } = default!;
}
