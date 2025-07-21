namespace backend.src.API.Models.Request;

public class UserUpdateRequest
{
    [Required] public string Email { get; set; } = default!;
    [Required] public string FirstName { get; set; } = default!;
    [Required] public string Surname { get; set; } = default!;
    [Required] public string DisplayName { get; set; } = default!;
}
