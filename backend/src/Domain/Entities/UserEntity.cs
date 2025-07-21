namespace backend.src.Domain.Entities;

public class UserEntity : IdentityUser
{
    public string FirstName { get; set; } = default!;
    public string Surname { get; set; } = default!;
    public string DisplayName { get; set; } = default!;
}
