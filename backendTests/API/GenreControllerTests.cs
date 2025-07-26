namespace backendTests.API;

public class GenreControllerTests
{
    private readonly GenreController _sut;
    private readonly IGenreService _mockGenreService = A.Fake<IGenreService>();
    private readonly IMapper _mockMapper = A.Fake<IMapper>();

    public GenreControllerTests()
    {
        _sut = new GenreController(_mockGenreService, _mockMapper);
    }

    public static IEnumerable<object[]> GetAsyncTestCases => new List<object[]>
    {
        new object[] 
        { 
            1, 
            new GenreDTO { Id = 1, Name = "Action" }, 
            typeof(OkObjectResult), 
            new ApiResponse(200, new GenreResponse { Id = 1, Name = "Action" }) 
        },
        new object[] 
        { 
            999, 
            null, 
            typeof(NotFoundObjectResult), new ApiResponse(404) 
        }
    };

    [Theory]
    [MemberData(nameof(GetAsyncTestCases))]
    public async Task GetAsync_ShouldReturnExpectedResult(int id, GenreDTO? dto, Type expectedType, ApiResponse expectedResponse)
    {
        // Arrange
        A.CallTo(() => _mockGenreService.GetAsync(id)).Returns(dto);
        if (dto is not null) A.CallTo(() => _mockMapper.Map<GenreResponse>(dto))!.Returns(expectedResponse.Data as GenreResponse);

        // Act
        var result = await _sut.GetAsync(id);

        // Assert
        result.Result.Should().BeOfType(expectedType);
        var actual = (result.Result as ObjectResult)!.Value;
        actual.Should().BeEquivalentTo(expectedResponse);
    }

    public static IEnumerable<object[]> GetAllAsyncTestCases => new List<object[]>
    {
        new object[] 
        { 
            new List<GenreDTO>(), 
            typeof(NotFoundObjectResult), 
            new ApiResponse(404) 
        },
        new object[] 
        { 
            new List<GenreDTO> { new() { Id = 1, Name = "Action" } }, 
            typeof(OkObjectResult), 
            new ApiResponse(200, new List<GenreResponse> { new() { Id = 1, Name = "Action" } }) }
    };

    [Theory]
    [MemberData(nameof(GetAllAsyncTestCases))]
    public async Task GetAllAsync_ShouldReturnExpectedResult(List<GenreDTO> dtos, Type expectedType, ApiResponse expectedResponse)
    {
        // Arrange
        A.CallTo(() => _mockGenreService.GetAllAsync()).Returns(dtos);
        if (dtos.Count > 0) A.CallTo(() => _mockMapper.Map<List<GenreResponse>>(dtos))!.Returns(expectedResponse.Data as List<GenreResponse>);

        // Act
        var result = await _sut.GetAllAsync();

        // Assert
        result.Result.Should().BeOfType(expectedType);
        var actual = (result.Result as ObjectResult)?.Value;
        actual.Should().BeEquivalentTo(expectedResponse);
    }

    [Fact]
    public async Task CreateAsync_ShouldReturnCreated()
    {
        // Arrange
        var request = new GenreCreateRequest { Name = "New Genre" };
        var dto = new GenreDTO { Id = 1, Name = "New Genre" };
        var created = new GenreDTO { Id = 1, Name = "New Genre" };
        var response = new GenreResponse { Id = 1, Name = "New Genre" };

        A.CallTo(() => _mockMapper.Map<GenreDTO>(request)).Returns(dto);
        A.CallTo(() => _mockGenreService.CreateAsync(dto)).Returns(created);
        A.CallTo(() => _mockMapper.Map<GenreResponse>(created)).Returns(response);

        // Act
        var result = await _sut.CreateAsync(request);

        // Assert
        var createdResult = result.Result as ObjectResult;
        createdResult.Should().NotBeNull();
        createdResult!.Value.Should().BeOfType<ApiResponse>();
        createdResult!.Value.Should().BeEquivalentTo(new ApiResponse(201, response));
    }

    [Fact]
    public async Task CreateAsync_ShouldThrow_WhenMappingFails()
    {
        // Arrange
        A.CallTo(() => _mockMapper.Map<GenreDTO>(A<GenreCreateRequest>._)).Throws<Exception>();

        // Act
        Func<Task> act = async () => await _sut.CreateAsync(A.Fake<GenreCreateRequest>());

        // Assert
        await act.Should().ThrowAsync<Exception>();
    }

    [Fact]
    public async Task UpdateAsync_ShouldReturnAccepted()
    {
        // Arrange
        var request = new GenreUpdateRequest { Id = 1, Name = "Updated Genre" };
        var dto = new GenreDTO { Id = 1, Name = "Updated Genre" };
        var updated = new GenreDTO { Id = 1, Name = "Updated Genre" };
        var response = new GenreResponse { Id = 1, Name = "Updated Genre" };

        A.CallTo(() => _mockMapper.Map<GenreDTO>(request)).Returns(dto);
        A.CallTo(() => _mockGenreService.UpdateAsync(dto)).Returns(updated);
        A.CallTo(() => _mockMapper.Map<GenreResponse>(updated)).Returns(response);

        // Act
        var result = await _sut.UpdateAsync(1, request);

        // Assert
        var accepted = result.Result as ObjectResult;
        accepted.Should().NotBeNull();
        accepted!.Value.Should().BeEquivalentTo(new ApiResponse(202, response));
    }

    public static IEnumerable<object[]> DeleteAsyncTestCases => new List<object[]>
    {
        new object[] 
        { 
            1, 
            true,
            typeof(NoContentResult), 
            null 
        },
        new object[] 
        { 
            99, 
            false, 
            typeof(BadRequestObjectResult), 
            new ApiResponse(400) 
        }
    };

    [Theory]
    [MemberData(nameof(DeleteAsyncTestCases))]
    public async Task DeleteAsync_ShouldReturnExpectedResult(int id, bool deleted, Type expectedType, ApiResponse? expectedResponse)
    {
        // Arrange
        A.CallTo(() => _mockGenreService.DeleteAsync(id)).Returns(deleted);

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
}
