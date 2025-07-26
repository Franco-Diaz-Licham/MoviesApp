namespace backendTests.API;

public class AccountControllerTests
{
    private readonly AccountController _sut;
    private readonly IUserService _mockUserService = A.Fake<IUserService>();
    private readonly IMapper _mockMapper = A.Fake<IMapper>();

    public AccountControllerTests()
    {
        _sut = new AccountController(_mockUserService, _mockMapper);
    }

    [Fact]
    public async Task GetCurrentUser_ShouldReturnUser_WhenEmailIsValid()
    {
        // Arrange
        var email = "user@example.com";
        var userDto = new UserDTO { Email = email };
        var response = new UserResponse { Email = email };

        var httpContext = new DefaultHttpContext();
        var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Email, email) });
        httpContext.User = new ClaimsPrincipal(identity);
        _sut.ControllerContext = new ControllerContext { HttpContext = httpContext };

        A.CallTo(() => _mockUserService.FindByEmailAsync(email)).Returns(userDto);
        A.CallTo(() => _mockMapper.Map<UserResponse>(userDto)).Returns(response);

        // Act
        var result = await _sut.GetCurrentUser();

        // Assert
        result.Result.Should().BeOfType<OkObjectResult>();
        var value = (result.Result as ObjectResult)?.Value;
        value.Should().BeEquivalentTo(new ApiResponse(200, response));
    }

    [Fact]
    public async Task GetCurrentUser_ShouldReturnNotFound_WhenEmailClaimMissing()
    {
        // Arrange
        var httpContext = new DefaultHttpContext();
        _sut.ControllerContext = new ControllerContext { HttpContext = httpContext };

        // Act
        var result = await _sut.GetCurrentUser();

        // Assert
        result.Result.Should().BeOfType<NotFoundObjectResult>();
    }

    [Fact]
    public async Task GetCurrentUser_ShouldReturnNotFound_WhenUserNotFound()
    {
        // Arrange
        var email = "missing@example.com";
        var httpContext = new DefaultHttpContext();
        var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Email, email) });
        httpContext.User = new ClaimsPrincipal(identity);
        _sut.ControllerContext = new ControllerContext { HttpContext = httpContext };

        A.CallTo(() => _mockUserService.FindByEmailAsync(email)).Returns((UserDTO?)null);

        // Act
        var result = await _sut.GetCurrentUser();

        // Assert
        result.Result.Should().BeOfType<NotFoundObjectResult>();
    }

    [Theory]
    [InlineData("exists@example.com", true)]
    [InlineData("missing@example.com", false)]
    public async Task CheckEmailExistsAsync_ShouldReturnExpectedResult(string email, bool exists)
    {
        // Arrange
        var dto = exists ? new UserDTO { Email = email } : null;

        A.CallTo(() => _mockUserService.FindByEmailAsync(email)).Returns(dto);

        // Act
        var result = await _sut.CheckEmailExistsAsync(email);

        // Assert
        if (exists)
        {
            result.Result.Should().BeOfType<OkObjectResult>();
            var value = (result.Result as ObjectResult)?.Value;
            value.Should().BeEquivalentTo(new ApiResponse(200, dto));
        }
        else
        {
            result.Result.Should().BeOfType<NotFoundObjectResult>();
        }
    }

    [Fact]
    public async Task Login_ShouldReturnOk_WhenCredentialsAreCorrect()
    {
        // Arrange
        var request = new UserLoginRequest { Email = "user@example.com", Password = "pass" };
        var loginDto = new UserLoginDTO { Email = "user@example.com" };
        var resultDto = new UserDTO { Email = "user@example.com" };

        A.CallTo(() => _mockMapper.Map<UserLoginDTO>(request)).Returns(loginDto);
        A.CallTo(() => _mockUserService.LoginAsync(loginDto)).Returns(resultDto);
        A.CallTo(() => _mockMapper.Map<UserResponse>(resultDto)).Returns(new UserResponse { Email = "user@example.com" });

        // Act
        var result = await _sut.Login(request);

        // Assert
        result.Result.Should().BeOfType<OkObjectResult>();
        var value = (result.Result as ObjectResult)?.Value;
        value.Should().BeEquivalentTo(new ApiResponse(200, new UserResponse { Email = "user@example.com" }));
    }

    [Fact]
    public async Task Login_ShouldReturnUnauthorized_WhenCredentialsAreInvalid()
    {
        // Arrange
        var request = new UserLoginRequest { Email = "invalid@example.com", Password = "wrong" };
        var loginDto = new UserLoginDTO { Email = "invalid@example.com" };

        A.CallTo(() => _mockMapper.Map<UserLoginDTO>(request)).Returns(loginDto);
        A.CallTo(() => _mockUserService.LoginAsync(loginDto)).Returns((UserDTO?)null);

        // Act
        var result = await _sut.Login(request);

        // Assert
        result.Result.Should().BeOfType<UnauthorizedObjectResult>();
        var error = (result.Result as UnauthorizedObjectResult)?.Value;
        error.Should().BeEquivalentTo(new ApiValidationErrorResponse(new[] { "Email or password are incorrect" }));
    }

    [Fact]
    public async Task Register_ShouldReturnOk_WhenUserCreated()
    {
        // Arrange
        var request = new UserRegisterRequest { Email = "new@example.com" };
        var registerDto = new UserRegisterDTO { Email = "new@example.com" };
        var userDto = new UserDTO { Email = "new@example.com" };
        var response = new UserResponse { Email = "new@example.com" };

        A.CallTo(() => _mockMapper.Map<UserRegisterDTO>(request)).Returns(registerDto);
        A.CallTo(() => _mockUserService.RegisterAsync(registerDto)).Returns(userDto);
        A.CallTo(() => _mockMapper.Map<UserResponse>(userDto)).Returns(response);

        // Act
        var result = await _sut.Register(request);

        // Assert
        result.Result.Should().BeOfType<OkObjectResult>();
        var value = (result.Result as ObjectResult)?.Value;
        value.Should().BeEquivalentTo(new ApiResponse(200, response));
    }

    [Fact]
    public async Task Register_ShouldReturnBadRequest_WhenUserCreationFails()
    {
        // Arrange
        var request = new UserRegisterRequest { Email = "fail@example.com" };
        var registerDto = new UserRegisterDTO { Email = "fail@example.com" };

        A.CallTo(() => _mockMapper.Map<UserRegisterDTO>(request)).Returns(registerDto);
        A.CallTo(() => _mockUserService.RegisterAsync(registerDto)).Returns((UserDTO?)null);

        // Act
        var result = await _sut.Register(request);

        // Assert
        result.Result.Should().BeOfType<BadRequestObjectResult>();
        var error = (result.Result as BadRequestObjectResult)?.Value;
        error.Should().BeEquivalentTo(new ApiValidationErrorResponse(new[] { "Unable to create user" }));
    }

    [Fact]
    public async Task UpdateAsync_ShouldReturnOk_WhenUserUpdated()
    {
        // Arrange
        var request = new UserUpdateRequest { DisplayName = "Updated Name" };
        var dto = new UserUpdateDTO { DisplayName = "Updated Name" };
        var updated = new UserDTO { DisplayName = "Updated Name" };
        var response = new UserResponse { DisplayName = "Updated Name" };

        A.CallTo(() => _mockMapper.Map<UserUpdateDTO>(request)).Returns(dto);
        A.CallTo(() => _mockUserService.UpdateAsync(dto)).Returns(updated);
        A.CallTo(() => _mockMapper.Map<UserResponse>(updated)).Returns(response);

        // Act
        var result = await _sut.UpdateAsync(request);

        // Assert
        result.Result.Should().BeOfType<OkObjectResult>();
        var value = (result.Result as ObjectResult)?.Value;
        value.Should().BeEquivalentTo(new ApiResponse(200, response));
    }

    [Fact]
    public async Task UpdateAsync_ShouldReturnNotFound_WhenUserDoesNotExist()
    {
        // Arrange
        var request = new UserUpdateRequest { DisplayName = "Ghost" };
        var dto = new UserUpdateDTO { DisplayName = "Ghost" };

        A.CallTo(() => _mockMapper.Map<UserUpdateDTO>(request)).Returns(dto);
        A.CallTo(() => _mockUserService.UpdateAsync(dto)).Returns((UserDTO?)null);

        // Act
        var result = await _sut.UpdateAsync(request);

        // Assert
        result.Result.Should().BeOfType<NotFoundObjectResult>();
        var value = (result.Result as ObjectResult)?.Value;
        value.Should().BeEquivalentTo(new ApiResponse(404));
    }
}
