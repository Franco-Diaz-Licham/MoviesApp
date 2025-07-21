namespace backend.src.Application.DTOs;

public class UserDTO
{
    public string Id { get; set; } = default!;
    public string FirstName { get; set; } = default!;
    public string Surname { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string DisplayName { get; set; } = default!;
    public string Token { get; set; } = default!;
}
