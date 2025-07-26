namespace backendTests.API;

public class TheatreControllerTests
{
    private readonly TheatreController _sut;
    private readonly ITheatreService _mockTheatreService = A.Fake<ITheatreService>();
    private readonly IMapper _mockMapper = A.Fake<IMapper>();

    public TheatreControllerTests()
    {
        _sut = new TheatreController(_mockTheatreService, _mockMapper);
    }

    public static IEnumerable<object[]> GetAsyncTestCases => new List<object[]>
    {
        new object[] { 1, new TheatreDTO { Id = 1, Name = "Main Theatre" }, typeof(OkObjectResult), new ApiResponse(200, new TheatreResponse { Id = 1, Name = "Main Theatre" }) },
        new object[] { 999, null, typeof(NotFoundObjectResult), new ApiResponse(404) }
    };

    [Theory]
    [MemberData(nameof(GetAsyncTestCases))]
    public async Task GetAsync_ShouldReturnExpectedResult(int id, TheatreDTO? dto, Type expectedType, ApiResponse expectedResponse)
    {
        // Arrange
        A.CallTo(() => _mockTheatreService.GetAsync(id)).Returns(dto);
        if (dto is not null) A.CallTo(() => _mockMapper.Map<TheatreResponse>(dto))!.Returns(expectedResponse.Data as TheatreResponse);

        // Act
        var result = await _sut.GetAsync(id);

        // Assert
        result.Result.Should().BeOfType(expectedType);
        var actual = (result.Result as ObjectResult)!.Value;
        actual.Should().BeEquivalentTo(expectedResponse);
    }

    public static IEnumerable<object[]> GetAllAsyncTestCases => new List<object[]>
    {
        new object[] { new List<TheatreDTO>(), typeof(NotFoundObjectResult), new ApiResponse(404) },
        new object[]
        {
            new List<TheatreDTO> { new() { Id = 1, Name = "Cinema One" } },
            typeof(OkObjectResult),
            new ApiResponse(200, new List<TheatreResponse> { new() { Id = 1, Name = "Cinema One" } })
        }
    };

    [Theory]
    [MemberData(nameof(GetAllAsyncTestCases))]
    public async Task GetAllAsync_ShouldReturnExpectedResult(List<TheatreDTO> dtos, Type expectedType, ApiResponse expectedResponse)
    {
        // Arrange
        A.CallTo(() => _mockTheatreService.GetAllAsync()).Returns(dtos);
        if (dtos.Count > 0) A.CallTo(() => _mockMapper.Map<List<TheatreResponse>>(dtos))!.Returns(expectedResponse.Data as List<TheatreResponse>);

        // Act
        var result = await _sut.GetAllAsync();

        // Assert
        result.Result.Should().BeOfType(expectedType);
        var actual = (result.Result as ObjectResult)?.Value;
        actual.Should().BeEquivalentTo(expectedResponse);
    }

    [Fact]
    public async Task CreateAsync_ShouldReturnAccepted()
    {
        // Arrange
        var request = new TheatreCreateRequest { Name = "New Theatre" };
        var dto = new TheatreDTO { Id = 1, Name = "New Theatre" };
        var createdDto = new TheatreDTO { Id = 1, Name = "New Theatre" };
        var response = new TheatreResponse { Id = 1, Name = "New Theatre" };

        A.CallTo(() => _mockMapper.Map<TheatreDTO>(request)).Returns(dto);
        A.CallTo(() => _mockTheatreService.CreateAsync(dto)).Returns(createdDto);
        A.CallTo(() => _mockMapper.Map<TheatreResponse>(createdDto)).Returns(response);

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
        var request = new TheatreUpdateRequest { Id = 1, Name = "Updated Theatre" };
        var dto = new TheatreDTO { Id = 1, Name = "Updated Theatre" };
        var updatedDto = new TheatreDTO { Id = 1, Name = "Updated Theatre" };
        var response = new TheatreResponse { Id = 1, Name = "Updated Theatre" };

        A.CallTo(() => _mockMapper.Map<TheatreDTO>(request)).Returns(dto);
        A.CallTo(() => _mockTheatreService.UpdateAsync(dto)).Returns(updatedDto);
        A.CallTo(() => _mockMapper.Map<TheatreResponse>(updatedDto)).Returns(response);

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
        new object[] { 999, false, typeof(BadRequestObjectResult), new ApiResponse(400) }
    };

    [Theory]
    [MemberData(nameof(DeleteAsyncTestCases))]
    public async Task DeleteAsync_ShouldReturnExpectedResult(int id, bool deleted, Type expectedType, ApiResponse? expectedResponse)
    {
        // Arrange
        A.CallTo(() => _mockTheatreService.DeleteAsync(id)).Returns(deleted);

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
        A.CallTo(() => _mockMapper.Map<TheatreDTO>(A<TheatreCreateRequest>._)).Throws<Exception>();

        // Act
        var act = async () => await _sut.CreateAsync(A.Fake<TheatreCreateRequest>());

        // Assert
        await act.Should().ThrowAsync<Exception>();
    }
}
