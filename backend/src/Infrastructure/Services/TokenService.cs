namespace backend.src.Infrastructure.Services;

public class TokenService : ITokenService
{
    private readonly SymmetricSecurityKey _key;
    private readonly IConfiguration _config;

    public TokenService(IConfiguration config)
    {
        string key = config.GetValue<string>("Token:Key")!;
        _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        _config = config;
    }

    public string CreateToken(UserDTO user)
    {
        var claims = new List<Claim>()
        {
            new(JwtRegisteredClaimNames.Email, user.Email!.ToString()),
            new(JwtRegisteredClaimNames.GivenName, user.FirstName.ToString()),
            new(JwtRegisteredClaimNames.FamilyName, user.Surname.ToString()),
            new(JwtRegisteredClaimNames.UniqueName, user.DisplayName.ToString())
        };

        var credentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);
        var tokenDescrp = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddDays(7),
            SigningCredentials = credentials,
            Issuer = _config.GetValue<string>("Token:Issuer")!
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescrp);
        return tokenHandler.WriteToken(token);
    }
}
