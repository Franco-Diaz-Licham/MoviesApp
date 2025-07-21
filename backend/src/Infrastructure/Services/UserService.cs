namespace backend.src.Infrastructure.Services;

public class UserService : IUserService
{
    private readonly UserManager<UserEntity> _userManager;
    private readonly SignInManager<UserEntity> _signInManager;
    private readonly ITokenService _tokenService;
    private readonly IMapper _mapper;

    public UserService(UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager, ITokenService tokenService, IMapper mapper)
    {
        _mapper = mapper;
        _tokenService = tokenService;
        _signInManager = signInManager;
        _userManager = userManager;
    }

    public async Task<UserDTO?> FindByEmailAsync(string email)
    {
        var result = await _userManager.Users.SingleOrDefaultAsync(x => x.Email == email);
        var output = _mapper.Map<UserDTO>(result);
        return output;
    }

    public async Task<UserDTO?> LoginAsync(UserLoginDTO dto)
    {
        // Find user and validate login.
        var user = await _userManager.FindByEmailAsync(dto.Email);
        if (user == null) return null;
        var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, false);
        if (!result.Succeeded) return null;

        // Compose DTO and return.
        var output = _mapper.Map<UserDTO>(user);
        output.Token = _tokenService.CreateToken(output);
        return output;
    }

    public async Task<UserDTO?> RegisterAsync(UserRegisterDTO dto)
    {
        // Validate creation request.
        var existingUser = await _userManager.Users.SingleOrDefaultAsync(x => x.Email == dto.Email);
        if (existingUser is not null) return null;
        var user = _mapper.Map<UserEntity>(dto);
        var result = await _userManager.CreateAsync(user, dto.Password);
        if (!result.Succeeded) return null;

        // Compose DTO and return.
        var output = _mapper.Map<UserDTO>(user);
        output.Token = _tokenService.CreateToken(output);
        return output;
    }

    public async Task<UserDTO?> UpdateAsync(UserUpdateDTO dto)
    {
        // Search user.
        var user = await _userManager.Users.SingleOrDefaultAsync(x => x.Email == dto.Email);
        if (user == null) return null;

        // Update details.
        user.DisplayName = dto.DisplayName;
        user.FirstName = dto.FirstName;
        user.Surname = dto.Surname;

        // Update and return.
        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded) return null;
        var output = _mapper.Map<UserDTO>(user);
        return output;
    }
}
