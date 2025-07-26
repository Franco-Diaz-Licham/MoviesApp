namespace backendTests.Infrastructure;

public class MovieServiceTests
{
    private readonly IMemoryCache _mockCache = A.Fake<IMemoryCache>();
    private readonly IUnitOfWork _mockUow = A.Fake<IUnitOfWork>();
    private readonly IMapper _mockMapper = A.Fake<IMapper>();
    private readonly IPhotoService _mockPhotoService = A.Fake<IPhotoService>();
    private readonly MovieService _sut;

    public MovieServiceTests()
    {
        _sut = new MovieService(_mockCache, _mockUow, _mockMapper, _mockPhotoService);
    }

    public static IEnumerable<object[]> GetAsyncTestCases => new List<object[]>
    {
        new object[] { 1, new MovieEntity { Id = 1, Title = "Inception" }, new MovieDTO { Id = 1, Title = "Inception" } },
        new object[] { 99, null, null }
    };

    [Theory]
    [MemberData(nameof(GetAsyncTestCases))]
    public async Task GetAsync_ShouldReturnExpectedResult(int id, MovieEntity? entity, MovieDTO? dto)
    {
        // Arrange
        var repo = A.Fake<IGenericRepository<MovieEntity>>();
        A.CallTo(() => _mockUow.GetRepository<MovieEntity>()).Returns(repo);
        A.CallTo(() => repo.GetAsync(A<ISpecification<MovieEntity>>._)).Returns(entity);
        A.CallTo(() => _mockMapper.Map<MovieDTO>(entity))!.Returns(dto);

        // Act
        var result = await _sut.GetAsync(id);

        // Assert
        result.Should().BeEquivalentTo(dto);
    }

    public static IEnumerable<object[]> GetAsyncCheckTestCases => new List<object[]>
    {
        new object[] { 5, new MovieEntity { Id = 5 }, true },
        new object[] { 888, null, false }
    };

    [Theory]
    [MemberData(nameof(GetAsyncCheckTestCases))]
    public async Task GetAsyncCheck_ShouldReturnExpectedResult(int id, MovieEntity? entity, bool expected)
    {
        // Arrange
        var repo = A.Fake<IGenericRepository<MovieEntity>>();
        A.CallTo(() => _mockUow.GetRepository<MovieEntity>()).Returns(repo);
        A.CallTo(() => repo.GetAsyncNoTracking(id)).Returns(entity);

        // Act
        var result = await _sut.GetAsyncCheck(id);

        // Assert
        result.Should().Be(expected);
    }

    [Fact]
    public async Task DeleteAsync_ShouldDeleteMovieAndPhoto_WhenMovieExists()
    {
        // Arrange
        var id = 1;
        var movie = new MovieEntity{ Id = id, Title = "To Delete", Photo = new PhotoEntity { Id = 10 } };
        var photoDto = new PhotoDTO { Id = 10 };
        var repo = A.Fake<IGenericRepository<MovieEntity>>();
        var transaction = A.Fake<IDbContextTransaction>();

        A.CallTo(() => _mockUow.GetRepository<MovieEntity>()).Returns(repo);
        A.CallTo(() => _mockUow.BeginTransactionAsync()).Returns(transaction);
        A.CallTo(() => repo.GetAsyncNoTracking(A<ISpecification<MovieEntity>>._)).Returns(movie);
        A.CallTo(() => _mockMapper.Map<PhotoDTO>(movie.Photo)).Returns(photoDto);

        // Act
        var result = await _sut.DeleteAsync(id);

        // Assert
        result.Should().BeTrue();
        A.CallTo(() => repo.Delete(movie)).MustHaveHappened();
        A.CallTo(() => transaction.CommitAsync(CancellationToken.None)).MustHaveHappened();
        A.CallTo(() => _mockPhotoService.DeleteAsync(photoDto)).MustHaveHappened();
        A.CallTo(() => _mockCache.Remove("MovieResponse")).MustHaveHappened();
    }

    [Fact]
    public async Task DeleteAsync_ShouldReturnFalse_WhenMovieDoesNotExist()
    {
        // Arrange
        var id = 404;
        var repo = A.Fake<IGenericRepository<MovieEntity>>();
        var transaction = A.Fake<IDbContextTransaction>();

        A.CallTo(() => _mockUow.GetRepository<MovieEntity>()).Returns(repo);
        A.CallTo(() => _mockUow.BeginTransactionAsync()).Returns(transaction);
        A.CallTo(() => repo.GetAsyncNoTracking(A<ISpecification<MovieEntity>>._)).Returns((MovieEntity?)null);

        // Act
        var result = await _sut.DeleteAsync(id);

        // Assert
        result.Should().BeFalse();
        A.CallTo(() => repo.Delete(A<MovieEntity>._)).MustNotHaveHappened();
        A.CallTo(() => _mockPhotoService.DeleteAsync(A<PhotoDTO>._)).MustNotHaveHappened();
        A.CallTo(() => transaction.CommitAsync(CancellationToken.None)).MustNotHaveHappened();
    }

    [Fact]
    public async Task DeleteAsync_ShouldRollback_WhenExceptionOccurs()
    {
        // Arrange
        var id = 2;
        var movie = new MovieEntity { Id = id };
        var repo = A.Fake<IGenericRepository<MovieEntity>>();
        var transaction = A.Fake<IDbContextTransaction>();

        A.CallTo(() => _mockUow.GetRepository<MovieEntity>()).Returns(repo);
        A.CallTo(() => _mockUow.BeginTransactionAsync()).Returns(transaction);
        A.CallTo(() => repo.GetAsyncNoTracking(A<ISpecification<MovieEntity>>._)).Returns(movie);
        A.CallTo(() => _mockUow.CompleteAsync()).Throws(new Exception("Commit failed"));

        // Act
        var act = async () => await _sut.DeleteAsync(id);

        // Assert
        await act.Should().ThrowAsync<Exception>().WithMessage("Commit failed");
        A.CallTo(() => transaction.RollbackAsync(CancellationToken.None)).MustHaveHappened();
    }
}

