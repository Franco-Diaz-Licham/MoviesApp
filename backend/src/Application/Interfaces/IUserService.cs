namespace backend.src.Application.Interfaces
{
    public interface IUserService
    {
        Task<UserDTO?> FindByEmailAsync(string email);
        Task<UserDTO?> LoginAsync(UserLoginDTO dto);
        Task<UserDTO?> RegisterAsync(UserRegisterDTO dto);
        Task<UserDTO?> UpdateAsync(UserUpdateDTO dto);
    }
}