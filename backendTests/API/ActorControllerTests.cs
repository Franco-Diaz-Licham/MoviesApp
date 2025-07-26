namespace backendTests.API;

public class ActorControllerTests
{
    private readonly ActorController _sut;
    private readonly IActorService _mockActorService = A.Fake<IActorService>();
    private readonly IMapper _mockMapper = A.Fake<IMapper>();

    public ActorControllerTests()
    {
        _sut = new ActorController(_mockActorService, _mockMapper);
    }

    public static IEnumerable<object[]> GetAsyncTestCases => new List<object[]>
    {
        new object[] { 10, new ActorDTO { Id = 10, Name = "Test Actor" }, typeof(OkObjectResult), new ApiResponse(200, new ActorResponse { Id = 10, Name = "Test Actor" }) },
        new object[] { -1, null, typeof(NotFoundObjectResult), new ApiResponse(404) }
    };

    [Theory]
    [MemberData(nameof(GetAsyncTestCases))]
    public async Task GetAsync_ShouldReturnExpectedResult(int id, ActorDTO? dto, Type expectedType, ApiResponse expectedResponse)
    {
        // Arrange
        A.CallTo(() => _mockActorService.GetAsync(id)).Returns(dto);
        if (dto is not null) A.CallTo(() => _mockMapper.Map<ActorResponse>(dto))!.Returns(expectedResponse.Data as ActorResponse);

        // Act
        var result = await _sut.GetAsync(id);

        // Assert
        result.Result.Should().BeOfType(expectedType);
        var actual = result.Result as ObjectResult;
        actual!.Value.Should().BeEquivalentTo(expectedResponse);
    }

    public static IEnumerable<object[]> GetAllAsyncTestCases => new List<object[]>
    {
        new object[] { new List<ActorDTO>(), typeof(NotFoundObjectResult), new ApiResponse(404) },
        new object[] { new List<ActorDTO>{ new (){ Id = 1, Name = "Actor One" }}, typeof(OkObjectResult), new ApiResponse(200, new List<ActorResponse>{ new() { Id = 1, Name = "Actor One" } })},
        new object[] { new List<ActorDTO>{ new (){ Id = 1, Name = "Actor One" },  new () { Id = 2, Name = "Actor Two" } }, typeof(OkObjectResult), new ApiResponse(200, new List<ActorResponse>{ new() { Id = 1, Name = "Actor One" }, new() { Id = 2, Name = "Actor Two" } }) }
    };

    [Theory]
    [MemberData(nameof(GetAllAsyncTestCases))]
    public async Task GetAllAsync_ShouldReturnExpectedResult(List<ActorDTO> dtos, Type expectedType, ApiResponse expectedResponse)
    {
        // Arrange
        A.CallTo(() => _mockActorService.GetAllAsync()).Returns(dtos);
        if (dtos.Count != 0) A.CallTo(() => _mockMapper.Map<List<ActorResponse>>(dtos))!.Returns(expectedResponse.Data as List<ActorResponse>);

        // Act
        var result = await _sut.GetAllAsync();

        // Assert
        result.Result.Should().BeOfType(expectedType);
        var actual = (result.Result as ObjectResult)!.Value;
        actual.Should().BeEquivalentTo(expectedResponse);
    }

    [Fact]
    public async Task UpdateAsync_ShouldReturnAccepted_WhenActorExists()
    {
        // Arrange
        var id = 1;
        var request = new ActorUpdateRequest { Id = id, Name = "Updated Actor" };
        var dto = new ActorDTO { Id = id, Name = "Updated Actor" };
        var updatedDto = new ActorDTO { Id = id, Name = "Updated Actor" };
        var response = new ActorResponse { Id = id, Name = "Updated Actor" };
        var expectedResponse = new ApiResponse(202, response);

        A.CallTo(() => _mockActorService.GetAsyncCheck(id)).Returns(true);
        A.CallTo(() => _mockMapper.Map<ActorDTO>(request)).Returns(dto);
        A.CallTo(() => _mockActorService.UpdateAsync(dto)).Returns(updatedDto);
        A.CallTo(() => _mockMapper.Map<ActorResponse>(updatedDto)).Returns(response);

        // Act
        var result = await _sut.UpdateAsync(id, request);

        // Assert
        result.Result.Should().BeOfType<AcceptedResult>();
        var objectResult = result.Result as ObjectResult;
        objectResult!.Value.Should().BeEquivalentTo(expectedResponse);
    }

    [Fact]
    public async Task UpdateAsync_ShouldReturnNotFound_WhenActorDoesNotExist()
    {
        // Arrange
        var id = 2;
        var request = new ActorUpdateRequest();
        var expectedResponse = new ApiResponse(404);

        A.CallTo(() => _mockActorService.GetAsyncCheck(id)).Returns(false);

        // Act
        var result = await _sut.UpdateAsync(id, request);

        // Assert
        result.Result.Should().BeOfType<NotFoundObjectResult>();
        var objectResult = result.Result as ObjectResult;
        objectResult!.Value.Should().BeEquivalentTo(expectedResponse);
    }


    [Fact]
    public async Task CreateAsync_ReturnsAccepted()
    {
        // Arrange
        var request = new ActorCreateRequest { Name = "New Actor", Dob = DateTime.UtcNow, Biography = "Bio", Photo = new() { Image = A.Fake<IFormFile>() } };
        var dto = new ActorDTO { Id = 1, Name = request.Name };
        var createdDto = new ActorDTO { Id = 1, Name = request.Name };
        var response = new ActorResponse { Id = 1, Name = request.Name };

        A.CallTo(() => _mockMapper.Map<ActorDTO>(request)).Returns(dto);
        A.CallTo(() => _mockActorService.CreateAsync(dto)).Returns(createdDto);
        A.CallTo(() => _mockMapper.Map<ActorResponse>(createdDto)).Returns(response);

        // Act
        var result = await _sut.CreateAsync(request);

        // Assert
        var accepted = result.Result as ObjectResult;
        accepted.Should().NotBeNull();
        accepted!.Value.Should().BeOfType<ApiResponse>();
        accepted!.Value.Should().BeEquivalentTo(new ApiResponse(201, response));
    }

    [Fact]
    public async Task CreateAsync_ReturnsInternalServerError()
    {
        // Arrange
        A.CallTo(() => _mockMapper.Map<ActorDTO>(A<ActorCreateRequest>._)).Throws<Exception>();

        // Act
        Func<Task> act = async () => await _sut.CreateAsync(A.Fake<ActorCreateRequest>());

        // Assert
        await act.Should().ThrowAsync<Exception>();
    }

    public static IEnumerable<object[]> DeleteAsyncTestCases => new List<object[]>
    {
        new object[] { 1, true, typeof(NoContentResult), null },
        new object[] { 99, false, typeof(BadRequestObjectResult), new ApiResponse(400) }
    };

    [Theory]
    [MemberData(nameof(DeleteAsyncTestCases))]
    public async Task DeleteAsync_ShouldReturnExpectedResult(int id, bool deleteSucceeded, Type expectedType, ApiResponse? expectedResponse)
    {
        // Arrange
        A.CallTo(() => _mockActorService.DeleteAsync(id)).Returns(deleteSucceeded);

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
