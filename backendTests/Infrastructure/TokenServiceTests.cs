namespace backendTests.Infrastructure;

public class TokenServiceTests
{
    private readonly IConfiguration _mockConfig;
    private readonly TokenService _sut;

    public TokenServiceTests()
    {
        // Dummy secret and issuer
        var inMemorySettings = new Dictionary<string, string>
        {
            { "Token:Key", "super_secret_testing_key_123982392839823982938347wre734231234567890" },
            { "Token:Issuer", "TestIssuer" }
        };

        _mockConfig = new ConfigurationBuilder().AddInMemoryCollection(inMemorySettings).Build();
        _sut = new TokenService(_mockConfig);
    }

    [Fact]
    public void CreateToken_ShouldGenerateValidJwt_WhenUserIsValid()
    {
        // Arrange
        var user = new UserDTO{ Email = "user@example.com", FirstName = "John", Surname = "Doe", DisplayName = "jdoe" };

        // Act
        var token = _sut.CreateToken(user);

        // Assert 
        token.Should().NotBeNullOrWhiteSpace();

        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);

        jwtToken.Issuer.Should().Be("TestIssuer");

        var emailClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Email)?.Value;
        var nameClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.GivenName)?.Value;
        var surnameClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.FamilyName)?.Value;
        var displayNameClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.UniqueName)?.Value;

        emailClaim.Should().Be("user@example.com");
        nameClaim.Should().Be("John");
        surnameClaim.Should().Be("Doe");
        displayNameClaim.Should().Be("jdoe");
        jwtToken.ValidTo.Should().BeAfter(DateTime.UtcNow);
    }
}
