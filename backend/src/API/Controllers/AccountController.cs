namespace backend.src.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;
    public AccountController(IUserService userService, IMapper mapper)
    {
        _mapper = mapper;
        _userService = userService;
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<UserResponse>> GetCurrentUser()
    {
        var email = User.FindFirstValue(ClaimTypes.Email);
        if (string.IsNullOrEmpty(email)) return NotFound(new ApiResponse(404));
        var user = await _userService.FindByEmailAsync(email);
        if (user == null) return NotFound(new ApiResponse(404));
        var output = _mapper.Map<UserResponse>(user);
        return Ok(new ApiResponse(200, output));
    }

    [HttpGet("email")]
    public async Task<ActionResult<bool>> CheckEmailExistsAsync([FromQuery] string email)
    {
        var output = await _userService.FindByEmailAsync(email);
        if (output == null) return NotFound(new ApiResponse(404));
        return Ok(new ApiResponse(200, output));
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserResponse>> Login(UserLoginRequest request)
    {
        var user = _mapper.Map<UserLoginDTO>(request);
        var output = await _userService.LoginAsync(user);
        if (output == null) return Unauthorized(new ApiValidationErrorResponse(new[] {"Email or password are incorrect"}));
        return Ok(new ApiResponse(200, output));
    }

    [HttpPost("register")]
    public async Task<ActionResult<UserResponse>> Register(UserRegisterRequest request)
    {
        var dto = _mapper.Map<UserRegisterDTO>(request);
        var userDto = await _userService.RegisterAsync(dto);
        if (userDto is null) return BadRequest(new ApiValidationErrorResponse(new[] { "Unable to create user" }));
        var output = _mapper.Map<UserResponse>(userDto);
        return Ok(new ApiResponse(200, output));
    }

    [Authorize]
    [HttpPut]
    public async Task<ActionResult<UserResponse>> UpdateAsync(UserUpdateRequest request)
    {
        var user = _mapper.Map<UserUpdateDTO>(request);
        var updatedUser = await _userService.UpdateAsync(user);
        if (updatedUser == null) return NotFound(new ApiResponse(404));
        var output = _mapper.Map<UserResponse>(updatedUser);
        return Ok(new ApiResponse(200, output));
    }
}
