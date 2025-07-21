namespace backend.src.API.Models.Request;

public class UserRegisterRequest
{
    [Required] public string FirstName { get; set; } = default!;
    [Required] public string Surname { get; set; } = default!;
    [Required] public string Email { get; set; } = default!;
    [Required] public string DisplayName { get; set; } = default!;
    [Required] public string Password { get; set; } = default!;
}
