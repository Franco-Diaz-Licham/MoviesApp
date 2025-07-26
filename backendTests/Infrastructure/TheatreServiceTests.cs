namespace backendTests.Infrastructure;

public class TheatreServiceTests
{
    private readonly IMemoryCache _mockCache = A.Fake<IMemoryCache>();
    private readonly IUnitOfWork _mockUow = A.Fake<IUnitOfWork>();
    private readonly IMapper _mockMapper = A.Fake<IMapper>();
    private readonly TheatreService _sut;

    public TheatreServiceTests()
    {
        _sut = new TheatreService(_mockCache, _mockUow, _mockMapper);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnTheatres_WhenCacheIsMissed()
    {
        // Arrange
        var repo = A.Fake<IGenericRepository<TheatreEntity>>();
        var entities = new List<TheatreEntity> { new() { Id = 1, Name = "Theatre A" } };
        var mapped = new List<TheatreDTO> { new() { Id = 1, Name = "Theatre A" } };

        A.CallTo(() => _mockUow.GetRepository<TheatreEntity>()).Returns(repo);
        A.CallTo(() => repo.GetAllAsync()).Returns(entities);
        A.CallTo(() => _mockMapper.Map<List<TheatreDTO>>(entities)).Returns(mapped);

        var memoryCache = new MemoryCache(new MemoryCacheOptions());
        var service = new TheatreService(memoryCache, _mockUow, _mockMapper);

        // Act
        var result = await service.GetAllAsync();

        // Assert
        result.Should().BeEquivalentTo(mapped);
    }

    [Fact]
    public async Task GetAsync_ShouldReturnTheatre_WhenItExists()
    {
        // Arrange
        var id = 2;
        var entity = new TheatreEntity { Id = id, Name = "Theatre B" };
        var dto = new TheatreDTO { Id = id, Name = "Theatre B" };
        var repo = A.Fake<IGenericRepository<TheatreEntity>>();

        A.CallTo(() => _mockUow.GetRepository<TheatreEntity>()).Returns(repo);
        A.CallTo(() => repo.GetAsync(id)).Returns(entity);
        A.CallTo(() => _mockMapper.Map<TheatreDTO>(entity)).Returns(dto);

        // Act
        var result = await _sut.GetAsync(id);

        // Assert
        result.Should().BeEquivalentTo(dto);
    }

    public static IEnumerable<object[]> GetAsyncTestCases => new List<object[]>
    {
        new object[] { 2, new TheatreEntity { Id = 2, Name = "Theatre B" }, new TheatreDTO { Id = 2, Name = "Theatre B" } },
        new object[] { 99, null, null }
    };

    [Theory]
    [MemberData(nameof(GetAsyncTestCases))]
    public async Task GetAsync_ShouldReturnExpectedResult(int id, TheatreEntity? entity, TheatreDTO? dto)
    {
        // Arrange
        var repo = A.Fake<IGenericRepository<TheatreEntity>>();
        A.CallTo(() => _mockUow.GetRepository<TheatreEntity>()).Returns(repo);
        A.CallTo(() => repo.GetAsync(id)).Returns(entity);
        A.CallTo(() => _mockMapper.Map<TheatreDTO>(entity))!.Returns(dto);

        // Act
        var result = await _sut.GetAsync(id);

        // Assert
        result.Should().BeEquivalentTo(dto);
    }

    [Fact]
    public async Task CreateAsync_ShouldAddTheatre_AndReturnDto()
    {
        // Arrange
        var dto = new TheatreDTO { Name = "New Theatre" };
        var entity = new TheatreEntity { Id = 3, Name = "New Theatre" };
        var resultDto = new TheatreDTO { Id = 3, Name = "New Theatre" };
        var repo = A.Fake<IGenericRepository<TheatreEntity>>();

        A.CallTo(() => _mockMapper.Map<TheatreEntity>(dto)).Returns(entity);
        A.CallTo(() => _mockUow.GetRepository<TheatreEntity>()).Returns(repo);
        A.CallTo(() => _mockMapper.Map<TheatreDTO>(entity)).Returns(resultDto);

        // Act
        var result = await _sut.CreateAsync(dto);

        // Assert
        A.CallTo(() => repo.Add(entity)).MustHaveHappened();
        A.CallTo(() => _mockUow.CompleteAsync()).MustHaveHappened();
        A.CallTo(() => _mockCache.Remove("TheatreResponse")).MustHaveHappened();
        result.Should().BeEquivalentTo(resultDto);
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateTheatre_AndReturnDto()
    {
        // Arrange
        var dto = new TheatreDTO { Id = 4, Name = "Updated Theatre" };
        var entity = new TheatreEntity { Id = 4, Name = "Updated Theatre" };
        var resultDto = new TheatreDTO { Id = 4, Name = "Updated Theatre" };
        var repo = A.Fake<IGenericRepository<TheatreEntity>>();

        A.CallTo(() => _mockMapper.Map<TheatreEntity>(dto)).Returns(entity);
        A.CallTo(() => _mockUow.GetRepository<TheatreEntity>()).Returns(repo);
        A.CallTo(() => _mockMapper.Map<TheatreDTO>(entity)).Returns(resultDto);

        // Act
        var result = await _sut.UpdateAsync(dto);

        // Assert
        A.CallTo(() => repo.Update(entity)).MustHaveHappened();
        A.CallTo(() => _mockUow.CompleteAsync()).MustHaveHappened();
        A.CallTo(() => _mockCache.Remove("TheatreResponse")).MustHaveHappened();
        result.Should().BeEquivalentTo(resultDto);
    }

    [Fact]
    public async Task DeleteAsync_ShouldDeleteTheatre_WhenItExists()
    {
        // Arrange
        var id = 5;
        var repo = A.Fake<IGenericRepository<TheatreEntity>>();

        A.CallTo(() => _mockUow.GetRepository<TheatreEntity>()).Returns(repo);
        A.CallTo(() => repo.GetAsyncNoTracking(id)).Returns(new TheatreEntity { Id = id });

        // Act
        var result = await _sut.DeleteAsync(id);

        // Assert
        result.Should().BeTrue();
        A.CallTo(() => repo.Delete(id)).MustHaveHappened();
        A.CallTo(() => _mockUow.CompleteAsync()).MustHaveHappened();
        A.CallTo(() => _mockCache.Remove("TheatreResponse")).MustHaveHappened();
    }

    [Fact]
    public async Task DeleteAsync_ShouldReturnFalse_WhenTheatreDoesNotExist()
    {
        // Arrange
        var id = 999;
        var repo = A.Fake<IGenericRepository<TheatreEntity>>();

        A.CallTo(() => _mockUow.GetRepository<TheatreEntity>()).Returns(repo);
        A.CallTo(() => repo.GetAsyncNoTracking(id)).Returns((TheatreEntity?)null);

        // Act
        var result = await _sut.DeleteAsync(id);

        // Assert
        result.Should().BeFalse();
        A.CallTo(() => repo.Delete(id)).MustNotHaveHappened();
    }
}
