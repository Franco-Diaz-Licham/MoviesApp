namespace backend.src.Application.DTOs;

public class UserRegisterDTO
{
    public string FirstName { get; set; } = default!;
    public string Surname { get; set; } = default!;
    public string DisplayName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
}
