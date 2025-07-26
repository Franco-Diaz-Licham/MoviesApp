namespace backendTests.Infrastructure;

public class ActorServiceTests
{
    private readonly IMemoryCache _mockCache = A.Fake<IMemoryCache>();
    private readonly IUnitOfWork _mockUow = A.Fake<IUnitOfWork>();
    private readonly IMapper _mockMApper = A.Fake<IMapper>();
    private readonly IPhotoService _mockPhotoService = A.Fake<IPhotoService>();
    private readonly ActorService _sut;

    public ActorServiceTests()
    {
        _sut = new ActorService(_mockCache, _mockUow, _mockMApper, _mockPhotoService);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnActors_WhenCacheIsEmpty()
    {
        // Arrange
        object dummy;
        var repo = A.Fake<IGenericRepository<ActorEntity>>();
        var dbData = new List<ActorEntity> { new() { Id = 1, Name = "DB Actor" } };
        var mappedData = new List<ActorDTO> { new() { Id = 1, Name = "Mapped Actor" } };

        A.CallTo(() => _mockCache.TryGetValue(A<object>._, out dummy)).Returns(false);
        A.CallTo(() => _mockUow.GetRepository<ActorEntity>()).Returns(repo);
        A.CallTo(() => repo.GetAllAsync(A<ISpecification<ActorEntity>>._)).Returns(dbData);
        A.CallTo(() => _mockMApper.Map<List<ActorDTO>>(dbData)).Returns(mappedData);
        A.CallTo(() => _mockCache.CreateEntry(A<object>._)).Returns(A.Fake<ICacheEntry>());

        // Act
        var result = await _sut.GetAllAsync();

        // Assert
        result.Should().BeEquivalentTo(mappedData);
    }

    public static IEnumerable<object[]> GetAsyncTestCases => new List<object[]>
    {
        new object[] 
        { 
            1, 
            new ActorEntity { Id = 1, Name = "Actor Name" }, 
            new ActorDTO { Id = 1, Name = "Mapped Actor Name" } 
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
    public async Task GetAsync_ShouldReturnExpectedResult(int id, ActorEntity? entity, ActorDTO? dto)
    {
        // Arrange 
        var repo = A.Fake<IGenericRepository<ActorEntity>>();
        A.CallTo(() => _mockUow.GetRepository<ActorEntity>()).Returns(repo);
        A.CallTo(() => repo.GetAsync(A<ISpecification<ActorEntity>>._)).Returns(entity);
        A.CallTo(() => _mockMApper.Map<ActorDTO>(entity))!.Returns(dto);

        // Act
        var result = await _sut.GetAsync(id);

        // Assert
        result.Should().BeEquivalentTo(dto);
    }


    public static IEnumerable<object[]> GetAsyncCheckTestCases => new List<object[]>
    {
        new object[] 
        { 
            10, 
            new ActorEntity { Id = 10, Name = "Actor"} , 
            true 
        },
        new object[] 
        { 
            99, 
            null, 
            false 
        }
    };

    [Theory]
    [MemberData(nameof(GetAsyncCheckTestCases))]
    public async Task GetAsyncCheck_ShouldReturnExpectedResult(int id, ActorEntity? entity, bool expected)
    {
        // Arrange
        var repo = A.Fake<IGenericRepository<ActorEntity>>();
        A.CallTo(() => _mockUow.GetRepository<ActorEntity>()).Returns(repo);
        A.CallTo(() => repo.GetAsyncNoTracking(id)).Returns(entity);

        // Act
        var result = await _sut.GetAsyncCheck(id);

        // Assert
        result.Should().Be(expected);
    }

    [Fact]
    public async Task CreateAsync_ShouldThrow_WhenPhotoIsNull()
    {
        // Arrange
        var dto = new ActorDTO { Name = "Test Actor", Photo = null };

        // Act
        var act = async () => await _sut.CreateAsync(dto);

        // Assert
        await act.Should().ThrowAsync<Exception>().WithMessage("Photo is empty");
    }

    [Fact]
    public async Task CreateAsync_ShouldSaveActor_WhenValidDtoProvided()
    {
        // Arrange
        var mockPhoto = A.Fake<IFormFile>();
        var photoDto = new PhotoDTO { Image = mockPhoto };
        var dto = new ActorDTO { Id = 1, Name = "Actor", Photo = photoDto };
        var photo = new PhotoDTO { Id = 99 };
        var entity = new ActorEntity { Id = 1, Name = "Actor", PhotoId = 99 };
        var resultDto = new ActorDTO { Id = 1, Name = "Actor" };
        var repo = A.Fake<IGenericRepository<ActorEntity>>();
        var transaction = A.Fake<IDbContextTransaction>();
        var dbTransaction = A.Fake<DbTransaction>();

        A.CallTo(() => _mockUow.GetRepository<ActorEntity>()).Returns(repo);
        A.CallTo(() => _mockUow.BeginTransactionAsync()).Returns(transaction);
        A.CallTo(() => _mockPhotoService.CreateProfileImageAsync(dto.Photo)).Returns(photo);
        A.CallTo(() => _mockMApper.Map<ActorEntity>(A<ActorDTO>._)).Returns(entity);
        A.CallTo(() => _mockMApper.Map<ActorDTO>(entity)).Returns(resultDto);

        // Act
        var result = await _sut.CreateAsync(dto);

        // Assert
        A.CallTo(() => repo.Add(entity)).MustHaveHappened();
        A.CallTo(() => transaction.CommitAsync(CancellationToken.None)).MustHaveHappened();
        A.CallTo(() => _mockCache.Remove("ActorResponse")).MustHaveHappened();
        result.Should().BeEquivalentTo(resultDto);
    }

    [Fact]
    public async Task CreateAsync_ShouldRollback_WhenExceptionOccurs()
    {
        // Arrange
        var mockPhoto = A.Fake<IFormFile>();
        var photoDto = new PhotoDTO { Image = mockPhoto };
        var dto = new ActorDTO { Name = "Actor", Photo = photoDto };

        var transaction = A.Fake<IDbContextTransaction>();
        var dbTransaction = A.Fake<DbTransaction>();

        A.CallTo(() => transaction.RollbackAsync(CancellationToken.None)).Returns(Task.CompletedTask);
        A.CallTo(() => _mockUow.BeginTransactionAsync()).Returns(transaction);
        A.CallTo(() => _mockPhotoService.CreateProfileImageAsync(dto.Photo)).Throws(new Exception("photo upload failed"));

        // Act
        var act = async () => await _sut.CreateAsync(dto);

        // Assert
        await act.Should().ThrowAsync<Exception>().WithMessage("photo upload failed");
        A.CallTo(() => transaction.RollbackAsync(CancellationToken.None)).MustHaveHappened();
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateActor_WhenValidDtoProvided()
    {
        // Arrange
        var dto = new ActorDTO { Id = 1, Name = "Updated Name", Dob = new DateTime(1990, 1, 1), Biography = "Updated bio" };
        var actor = new ActorEntity{ Id = 1, Name = "Old Name", Dob = new DateTime(1980, 1, 1), Biography = "Old bio", Photo = new PhotoEntity(), PhotoId = 5};
        var updatedDto = new ActorDTO { Id = 1, Name = "Updated Name" };
        var transaction = A.Fake<IDbContextTransaction>();
        var repo = A.Fake<IGenericRepository<ActorEntity>>();
        var fakeService = A.Fake<IActorService>();

        A.CallTo(() => _mockUow.GetRepository<ActorEntity>()).Returns(repo);
        A.CallTo(() => _mockUow.BeginTransactionAsync()).Returns(transaction);
        A.CallTo(() => repo.GetAsync(A<ISpecification<ActorEntity>>._)).Returns(actor);
        A.CallTo(() => _mockMApper.Map<ActorDTO>(actor)).Returns(updatedDto);
        A.CallTo(() => fakeService.GetAsync(actor.Id)).Returns(updatedDto);

        // Act
        var result = await _sut.UpdateAsync(dto);

        // Assert
        A.CallTo(() => repo.Update(actor)).MustHaveHappened();
        A.CallTo(() => transaction.CommitAsync(CancellationToken.None)).MustHaveHappened();
        A.CallTo(() => _mockCache.Remove("ActorResponse")).MustHaveHappened();
        result.Should().BeEquivalentTo(updatedDto);
    }

    [Fact]
    public async Task UpdateAsync_ShouldRollback_WhenExceptionOccurs()
    {
        // Arrange
        var dto = new ActorDTO { Id = 99 };
        var transaction = A.Fake<IDbContextTransaction>();
        var repo = A.Fake<IGenericRepository<ActorEntity>>();

        A.CallTo(() => transaction.RollbackAsync(CancellationToken.None)).Returns(Task.CompletedTask);
        A.CallTo(() => _mockUow.BeginTransactionAsync()).Returns(transaction);
        A.CallTo(() => _mockUow.GetRepository<ActorEntity>()).Returns(repo);
        A.CallTo(() => repo.GetAsync(A<ISpecification<ActorEntity>>._)).Returns((ActorEntity?)null);

        // Act
        var act = async () => await _sut.UpdateAsync(dto);

        // Assert
        await act.Should().ThrowAsync<Exception>().WithMessage("Actor not found");
        A.CallTo(() => transaction.RollbackAsync(CancellationToken.None)).MustHaveHappened();
    }

    [Fact]
    public async Task DeleteAsync_ShouldDeleteActorAndPhoto_WhenActorExists()
    {
        // Arrange
        var actorId = 1;
        var actor = new ActorEntity{ Id = actorId, Name = "Deletable Actor", Photo = new PhotoEntity { Id = 10 } };
        var photoDto = new PhotoDTO { Id = 10 };

        var repo = A.Fake<IGenericRepository<ActorEntity>>();
        var transaction = A.Fake<IDbContextTransaction>();

        A.CallTo(() => _mockUow.GetRepository<ActorEntity>()).Returns(repo);
        A.CallTo(() => _mockUow.BeginTransactionAsync()).Returns(transaction);
        A.CallTo(() => repo.GetAsyncNoTracking(A<ISpecification<ActorEntity>>._)).Returns(actor);
        A.CallTo(() => _mockMApper.Map<PhotoDTO>(actor.Photo)).Returns(photoDto);

        // Act
        var result = await _sut.DeleteAsync(actorId);

        // Assert
        result.Should().BeTrue();
        A.CallTo(() => repo.Delete(actor)).MustHaveHappened();
        A.CallTo(() => _mockUow.CompleteAsync()).MustHaveHappened();
        A.CallTo(() => transaction.CommitAsync(CancellationToken.None)).MustHaveHappened();
        A.CallTo(() => _mockPhotoService.DeleteAsync(photoDto)).MustHaveHappened();
        A.CallTo(() => _mockCache.Remove("ActorResponse")).MustHaveHappened();
    }

    [Fact]
    public async Task DeleteAsync_ShouldReturnFalse_WhenActorDoesNotExist()
    {
        // Arrange
        var actorId = 99;
        var repo = A.Fake<IGenericRepository<ActorEntity>>();
        var transaction = A.Fake<IDbContextTransaction>();

        A.CallTo(() => _mockUow.GetRepository<ActorEntity>()).Returns(repo);
        A.CallTo(() => _mockUow.BeginTransactionAsync()).Returns(transaction);
        A.CallTo(() => repo.GetAsyncNoTracking(A<ISpecification<ActorEntity>>._)).Returns((ActorEntity?)null);

        // Act
        var result = await _sut.DeleteAsync(actorId);

        // Assert
        result.Should().BeFalse();
        A.CallTo(() => repo.Delete(A<ActorEntity>._)).MustNotHaveHappened();
        A.CallTo(() => _mockPhotoService.DeleteAsync(A<PhotoDTO>._)).MustNotHaveHappened();
        A.CallTo(() => transaction.CommitAsync(CancellationToken.None)).MustNotHaveHappened();
    }

    [Fact]
    public async Task DeleteAsync_ShouldRollback_WhenExceptionThrown()
    {
        // Arrange
        var actorId = 1;
        var actor = new ActorEntity { Id = actorId, Name = "Error Actor" };

        var repo = A.Fake<IGenericRepository<ActorEntity>>();
        var transaction = A.Fake<IDbContextTransaction>();

        A.CallTo(() => _mockUow.GetRepository<ActorEntity>()).Returns(repo);
        A.CallTo(() => _mockUow.BeginTransactionAsync()).Returns(transaction);
        A.CallTo(() => repo.GetAsyncNoTracking(A<ISpecification<ActorEntity>>._)).Returns(actor);
        A.CallTo(() => _mockUow.CompleteAsync()).Throws(new Exception("Simulated failure"));
        A.CallTo(() => transaction.RollbackAsync(CancellationToken.None)).Returns(Task.CompletedTask);

        // Act
        var act = async () => await _sut.DeleteAsync(actorId);

        // Assert
        await act.Should().ThrowAsync<Exception>().WithMessage("Simulated failure");
        A.CallTo(() => transaction.RollbackAsync(CancellationToken.None)).MustHaveHappened();
    }
}
