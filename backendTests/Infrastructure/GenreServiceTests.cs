namespace backendTests.Infrastructure;

public class GenreServiceTests
{
    private readonly IMemoryCache _mockCache = A.Fake<IMemoryCache>();
    private readonly IUnitOfWork _mockUow = A.Fake<IUnitOfWork>();
    private readonly IMapper _mockMapper = A.Fake<IMapper>();
    private readonly GenreService _sut;

    public GenreServiceTests()
    {
        _sut = new GenreService(_mockCache, _mockUow, _mockMapper);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnGenres_WhenCacheIsMissed()
    {
        // Arrange
        var repo = A.Fake<IGenericRepository<GenreEntity>>();
        var entities = new List<GenreEntity> { new() { Id = 1, Name = "Action" } };
        var mapped = new List<GenreDTO> { new() { Id = 1, Name = "Action" } };

        A.CallTo(() => _mockUow.GetRepository<GenreEntity>()).Returns(repo);
        A.CallTo(() => repo.GetAllAsync()).Returns(entities);
        A.CallTo(() => _mockMapper.Map<List<GenreDTO>>(entities)).Returns(mapped);

        var cacheEntry = A.Fake<ICacheEntry>();
        A.CallTo(() => _mockCache.CreateEntry(A<object>._)).Returns(cacheEntry);

        var memoryCache = new MemoryCache(new MemoryCacheOptions());
        var service = new GenreService(memoryCache, _mockUow, _mockMapper);

        // Act
        var result = await service.GetAllAsync();

        // Assert
        result.Should().BeEquivalentTo(mapped);
    }

    public static IEnumerable<object[]> GetAsyncTestCases => new List<object[]>
    {
        new object[] 
        { 
            1, 
            new GenreEntity { Id = 1, Name = "Comedy" }, 
            new GenreDTO { Id = 1, Name = "Comedy" } 
        },
        new object[] 
        { 
            99,
            null, 
            null 
        }
    };

    [Theory]
    [MemberData(nameof(GetAsyncTestCases))]
    public async Task GetAsync_ShouldReturnExpectedResult(int id, GenreEntity? entity, GenreDTO? dto)
    {
        // Arrange
        var repo = A.Fake<IGenericRepository<GenreEntity>>();
        A.CallTo(() => _mockUow.GetRepository<GenreEntity>()).Returns(repo);
        A.CallTo(() => repo.GetAsync(id)).Returns(entity);
        A.CallTo(() => _mockMapper.Map<GenreDTO>(entity))!.Returns(dto);

        // Act
        var result = await _sut.GetAsync(id);

        // Assert
        result.Should().BeEquivalentTo(dto);
    }


    [Fact]
    public async Task CreateAsync_ShouldAddGenre_AndReturnDto()
    {
        // Arrange
        var dto = new GenreDTO { Name = "Thriller" };
        var entity = new GenreEntity { Id = 2, Name = "Thriller" };
        var resultDto = new GenreDTO { Id = 2, Name = "Thriller" };
        var repo = A.Fake<IGenericRepository<GenreEntity>>();

        A.CallTo(() => _mockMapper.Map<GenreEntity>(dto)).Returns(entity);
        A.CallTo(() => _mockUow.GetRepository<GenreEntity>()).Returns(repo);
        A.CallTo(() => _mockMapper.Map<GenreDTO>(entity)).Returns(resultDto);

        // Act
        var result = await _sut.CreateAsync(dto);

        // Assert
        A.CallTo(() => repo.Add(entity)).MustHaveHappened();
        A.CallTo(() => _mockUow.CompleteAsync()).MustHaveHappened();
        A.CallTo(() => _mockCache.Remove("GenreResponse")).MustHaveHappened();
        result.Should().BeEquivalentTo(resultDto);
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateGenre_AndReturnDto()
    {
        // Arrange
        var dto = new GenreDTO { Id = 3, Name = "Action" };
        var entity = new GenreEntity { Id = 3, Name = "Action" };
        var resultDto = new GenreDTO { Id = 3, Name = "Action" };
        var repo = A.Fake<IGenericRepository<GenreEntity>>();

        A.CallTo(() => _mockMapper.Map<GenreEntity>(dto)).Returns(entity);
        A.CallTo(() => _mockUow.GetRepository<GenreEntity>()).Returns(repo);
        A.CallTo(() => _mockMapper.Map<GenreDTO>(entity)).Returns(resultDto);

        // Act
        var result = await _sut.UpdateAsync(dto);

        // Assert
        A.CallTo(() => repo.Update(entity)).MustHaveHappened();
        A.CallTo(() => _mockUow.CompleteAsync()).MustHaveHappened();
        A.CallTo(() => _mockCache.Remove("GenreResponse")).MustHaveHappened();
        result.Should().BeEquivalentTo(resultDto);
    }

    [Fact]
    public async Task DeleteAsync_ShouldDeleteGenre_WhenGenreExists()
    {
        // Arrange
        var id = 4;
        var repo = A.Fake<IGenericRepository<GenreEntity>>();

        A.CallTo(() => _mockUow.GetRepository<GenreEntity>()).Returns(repo);
        A.CallTo(() => repo.GetAsyncNoTracking(id)).Returns(new GenreEntity { Id = id });

        // Act
        var result = await _sut.DeleteAsync(id);

        // Assert
        result.Should().BeTrue();
        A.CallTo(() => repo.Delete(id)).MustHaveHappened();
        A.CallTo(() => _mockUow.CompleteAsync()).MustHaveHappened();
        A.CallTo(() => _mockCache.Remove("GenreResponse")).MustHaveHappened();
    }

    [Fact]
    public async Task DeleteAsync_ShouldReturnFalse_WhenGenreDoesNotExist()
    {
        // Arrange
        var id = 99;
        var repo = A.Fake<IGenericRepository<GenreEntity>>();

        A.CallTo(() => _mockUow.GetRepository<GenreEntity>()).Returns(repo);
        A.CallTo(() => repo.GetAsyncNoTracking(id)).Returns((GenreEntity?)null);

        // Act
        var result = await _sut.DeleteAsync(id);

        // Assert
        result.Should().BeFalse();
        A.CallTo(() => repo.Delete(A<int>._)).MustNotHaveHappened();
    }
}

