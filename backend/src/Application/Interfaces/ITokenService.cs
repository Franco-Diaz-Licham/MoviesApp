namespace backend.src.Application.Interfaces;

public interface ITokenService
{
    string CreateToken(UserDTO user);
}