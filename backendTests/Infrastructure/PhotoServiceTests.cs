namespace backendTests.Infrastructure;

public class PhotoServiceTests
{
    private readonly IUnitOfWork _mockUow = A.Fake<IUnitOfWork>();
    private readonly IMapper _mockMapper = A.Fake<IMapper>();
    private readonly ICloudinaryService _mockCloudinaryService = A.Fake<ICloudinaryService>();
    private readonly PhotoService _sut;

    public PhotoServiceTests()
    {
        _sut = new PhotoService(_mockUow, _mockMapper, _mockCloudinaryService);
    }

    public static IEnumerable<object[]> GetAsyncTestCases => new List<object[]>
    {
        new object[] { 1, new PhotoEntity { Id = 1, PublicId = "abc" }, new PhotoDTO { Id = 1, PublicId = "abc" } },
        new object[] { 999, null, null }
    };

    [Theory]
    [MemberData(nameof(GetAsyncTestCases))]
    public async Task GetAsync_ShouldReturnExpectedResult(int id, PhotoEntity? entity, PhotoDTO? dto)
    {
        // Arrange
        var repo = A.Fake<IGenericRepository<PhotoEntity>>();
        A.CallTo(() => _mockUow.GetRepository<PhotoEntity>()).Returns(repo);
        A.CallTo(() => repo.GetAsync(id)).Returns(entity);
        A.CallTo(() => _mockMapper.Map<PhotoDTO>(entity))!.Returns(dto);

        // Act
        var result = await _sut.GetAsync(id);

        // Assert
        result.Should().BeEquivalentTo(dto);
    }

    [Fact]
    public async Task CreateProfileImageAsync_ShouldUploadAndSavePhoto()
    {
        // Arrange
        var formFile = A.Fake<IFormFile>();
        var inputDto = new PhotoDTO { Image = formFile };
        var uploadResult = new ImageUploadResult
        {
            PublicId = "uploaded",
            SecureUrl = new Uri("https://myapp.com/test-image.jpg")
        };
        var entity = new PhotoEntity { Id = 10, PublicId = "uploaded" };
        var resultDto = new PhotoDTO { Id = 10, PublicId = "uploaded" };

        var repo = A.Fake<IGenericRepository<PhotoEntity>>();
        A.CallTo(() => _mockCloudinaryService.UploadPhotoAsync(formFile, A<Transformation>._)).Returns(uploadResult);
        A.CallTo(() => _mockMapper.Map<PhotoEntity>(A<PhotoDTO>._)).Returns(entity);
        A.CallTo(() => _mockMapper.Map<PhotoDTO>(entity)).Returns(resultDto);
        A.CallTo(() => _mockUow.GetRepository<PhotoEntity>()).Returns(repo);

        // Act
        var result = await _sut.CreateProfileImageAsync(inputDto);

        // Assert
        A.CallTo(() => repo.Add(entity)).MustHaveHappened();
        A.CallTo(() => _mockUow.CompleteAsync()).MustHaveHappened();
        result.Should().BeEquivalentTo(resultDto);
    }

    [Fact]
    public async Task CreateProfileImageAsync_ShouldThrow_WhenPhotoIsNull()
    {
        // Arrange
        var dto = new PhotoDTO { Image = null };

        // Act
        var act = async () => await _sut.CreateProfileImageAsync(dto);

        // Assert
        await act.Should().ThrowAsync<Exception>().WithMessage("Photo is empty");
    }

    [Fact]
    public async Task CreatePosterImageAsync_ShouldUploadAndSavePhoto()
    {
        // Arrange
        var formFile = A.Fake<IFormFile>();
        var inputDto = new PhotoDTO { Image = formFile };
        var uploadResult = new ImageUploadResult
        {
            PublicId = "poster",
            SecureUrl = new Uri("https://cdn.com/poster.jpg")
        };
        var entity = new PhotoEntity { Id = 20, PublicId = "poster" };
        var resultDto = new PhotoDTO { Id = 20, PublicId = "poster" };

        var repo = A.Fake<IGenericRepository<PhotoEntity>>();
        A.CallTo(() => _mockCloudinaryService.UploadPhotoAsync(formFile, A<Transformation>._)).Returns(uploadResult);
        A.CallTo(() => _mockMapper.Map<PhotoEntity>(A<PhotoDTO>._)).Returns(entity);
        A.CallTo(() => _mockMapper.Map<PhotoDTO>(entity)).Returns(resultDto);
        A.CallTo(() => _mockUow.GetRepository<PhotoEntity>()).Returns(repo);

        // Act
        var result = await _sut.CreatePosterImageAsync(inputDto);

        // Assert
        A.CallTo(() => repo.Add(entity)).MustHaveHappened();
        A.CallTo(() => _mockUow.CompleteAsync()).MustHaveHappened();
        result.Should().BeEquivalentTo(resultDto);
    }

    [Fact]
    public async Task DeleteAsync_ShouldDeleteFromDbAndCloud_WhenPhotoExists()
    {
        // Arrange
        var dto = new PhotoDTO { Id = 1, PublicId = "to_delete" };
        var entity = new PhotoEntity { Id = 1, PublicId = "to_delete" };
        var repo = A.Fake<IGenericRepository<PhotoEntity>>();

        A.CallTo(() => _mockUow.GetRepository<PhotoEntity>()).Returns(repo);
        A.CallTo(() => repo.GetAsyncNoTracking(dto.Id)).Returns(entity);

        // Act
        var result = await _sut.DeleteAsync(dto);

        // Assert
        result.Should().BeTrue();
        A.CallTo(() => repo.Delete(dto.Id)).MustHaveHappened();
        A.CallTo(() => _mockUow.CompleteAsync()).MustHaveHappened();
        A.CallTo(() => _mockCloudinaryService.DeletePhotoAsync(dto.PublicId)).MustHaveHappened();
    }

    [Fact]
    public async Task DeleteAsync_ShouldSkipDbDelete_WhenEntityIsNull()
    {
        // Arrange
        var dto = new PhotoDTO { Id = 999, PublicId = "nonexistent" };
        var repo = A.Fake<IGenericRepository<PhotoEntity>>();

        A.CallTo(() => _mockUow.GetRepository<PhotoEntity>()).Returns(repo);
        A.CallTo(() => repo.GetAsyncNoTracking(dto.Id)).Returns((PhotoEntity?)null);

        // Act
        var result = await _sut.DeleteAsync(dto);

        // Assert
        result.Should().BeTrue();
        A.CallTo(() => repo.Delete(A<int>._)).MustNotHaveHappened();
        A.CallTo(() => _mockCloudinaryService.DeletePhotoAsync(dto.PublicId)).MustHaveHappened();
    }
}

