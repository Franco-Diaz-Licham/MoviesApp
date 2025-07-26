namespace backendTests.API;

public class MovieControllerTests
{
    private readonly MovieController _sut;
    private readonly IMovieService _mockMovieService = A.Fake<IMovieService>();
    private readonly IMapper _mockMapper = A.Fake<IMapper>();

    public MovieControllerTests()
    {
        _sut = new MovieController(_mockMovieService, _mockMapper);
    }

    public static IEnumerable<object[]> GetAsyncTestCases => new List<object[]>
    {
        new object[] { 1, new MovieDTO { Id = 1, Title = "Inception" }, typeof(OkObjectResult), new ApiResponse(200, new MovieResponse { Id = 1, Title = "Inception" }) },
        new object[] { 999, null, typeof(NotFoundObjectResult), new ApiResponse(404) }
    };

    [Theory]
    [MemberData(nameof(GetAsyncTestCases))]
    public async Task GetAsync_ShouldReturnExpectedResult(int id, MovieDTO? dto, Type expectedType, ApiResponse expectedResponse)
    {
        // Arrange
        A.CallTo(() => _mockMovieService.GetAsync(id)).Returns(dto);
        if (dto is not null) A.CallTo(() => _mockMapper.Map<MovieResponse>(dto))!.Returns(expectedResponse.Data as MovieResponse);

        // Act
        var result = await _sut.GetAsync(id);

        // Assert
        result.Result.Should().BeOfType(expectedType);
        var actual = (result.Result as ObjectResult)!.Value;
        actual.Should().BeEquivalentTo(expectedResponse);
    }

    public static IEnumerable<object[]> GetAllAsyncTestCases => new List<object[]>
    {
        new object[] { new List<MovieDTO>(), typeof(NotFoundObjectResult), new ApiResponse(404) },
        new object[] { new List<MovieDTO> { new() { Id = 1, Title = "Interstellar" } }, typeof(OkObjectResult), new ApiResponse(200, new List<MovieResponse> { new() { Id = 1, Title = "Interstellar" } })}
    };

    [Theory]
    [MemberData(nameof(GetAllAsyncTestCases))]
    public async Task GetAllDetailsAsync_ShouldReturnExpectedResult(List<MovieDTO> dtos, Type expectedType, ApiResponse expectedResponse)
    {
        // Arrange
        A.CallTo(() => _mockMovieService.GetAllDetailsAsync()).Returns(dtos);
        if (dtos.Count > 0) A.CallTo(() => _mockMapper.Map<List<MovieResponse>>(dtos))!.Returns(expectedResponse.Data as List<MovieResponse>);

        // Act
        var result = await _sut.GetAllDetailsAsync();

        // Assert
        result.Result.Should().BeOfType(expectedType);
        var actual = (result.Result as ObjectResult)?.Value;
        actual.Should().BeEquivalentTo(expectedResponse);
    }

    [Fact]
    public async Task CreateAsync_ShouldReturnAccepted()
    {
        // Arrange
        var request = new MovieCreateRequest { Title = "Tenet", Photo = new() { Image = A.Fake<IFormFile>() } };
        var dto = new MovieCreateDTO { Title = "Tenet" };
        var createdDto = new MovieDTO { Id = 1, Title = "Tenet" };
        var response = new MovieResponse { Id = 1, Title = "Tenet" };

        A.CallTo(() => _mockMapper.Map<MovieCreateDTO>(request)).Returns(dto);
        A.CallTo(() => _mockMovieService.CreateAsync(dto)).Returns(createdDto);
        A.CallTo(() => _mockMapper.Map<MovieResponse>(createdDto)).Returns(response);

        // Act
        var result = await _sut.CreateAsync(request);

        // Assert
        var accepted = result.Result as ObjectResult;
        accepted.Should().NotBeNull();
        accepted!.Value.Should().BeEquivalentTo(new ApiResponse(201, response));
    }

    [Fact]
    public async Task UpdateAsync_ShouldReturnAccepted()
    {
        // Arrange
        var request = new MovieUpdateRequest { Id = 1, Title = "Updated Title" };
        var dto = new MovieUpdateDTO { Id = 1, Title = "Updated Title" };
        var updatedDto = new MovieDTO { Id = 1, Title = "Updated Title" };
        var response = new MovieResponse { Id = 1, Title = "Updated Title" };

        A.CallTo(() => _mockMapper.Map<MovieUpdateDTO>(request)).Returns(dto);
        A.CallTo(() => _mockMovieService.UpdateAsync(dto)).Returns(updatedDto);
        A.CallTo(() => _mockMapper.Map<MovieResponse>(updatedDto)).Returns(response);

        // Act
        var result = await _sut.UpdateAsync(1, request);

        // Assert
        var accepted = result.Result as ObjectResult;
        accepted.Should().NotBeNull();
        accepted!.Value.Should().BeEquivalentTo(new ApiResponse(202, response));
    }

    public static IEnumerable<object[]> DeleteAsyncTestCases => new List<object[]>
    {
        new object[] { 1, true, typeof(NoContentResult), null },
        new object[] { 99, false, typeof(BadRequestObjectResult), new ApiResponse(400) }
    };

    [Theory]
    [MemberData(nameof(DeleteAsyncTestCases))]
    public async Task DeleteAsync_ShouldReturnExpectedResult(int id, bool deleted, Type expectedType, ApiResponse? expectedResponse)
    {
        // Arrange
        A.CallTo(() => _mockMovieService.DeleteAsync(id)).Returns(deleted);

        // Act
        var result = await _sut.DeleteAsync(id);

        // Assert
        result.Should().BeOfType(expectedType);

        if (expectedResponse is not null)
        {
            var objectResult = result as ObjectResult;
            objectResult!.Value.Should().BeEquivalentTo(expectedResponse);
        }
    }

    [Fact]
    public async Task CreateAsync_ShouldThrow_WhenMapperFails()
    {
        // Arrange
        A.CallTo(() => _mockMapper.Map<MovieCreateDTO>(A<MovieCreateRequest>._)).Throws<Exception>();

        // Act
        var act = async () => await _sut.CreateAsync(A.Fake<MovieCreateRequest>());

        // Assert
        await act.Should().ThrowAsync<Exception>();
    }
}
