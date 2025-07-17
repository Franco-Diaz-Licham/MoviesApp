namespace backend.src.Infrastructure.Services;

public class CloudinaryService : ICloudinaryService
{
    private readonly Cloudinary _cloud;

    public CloudinaryService(IOptions<CloudinarySettings> config)
    {
        var acc = new Account(config.Value.CloudName, config.Value.ApiKey, config.Value.ApiSecret);
        _cloud = new Cloudinary(acc);
    }

    public async Task<ImageUploadResult> UploadPhotoAsync(IFormFile file)
    {
        var uploadResp = new ImageUploadResult();
        if (file.Length > 0)
        {
            await using var stream = file.OpenReadStream();
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(file.FileName, stream),
                Transformation = new Transformation().Height(500).Width(500).Crop("fill").Gravity("face")
            };

            uploadResp = await _cloud.UploadAsync(uploadParams);
        }

        return uploadResp;
    }

    public async Task<ImageUploadResult> UploadPhotoAsync(byte[] fileContent, string fileName)
    {
        var uploadResp = new ImageUploadResult();
        var uploadParams = new ImageUploadParams()
        {
            File = new FileDescription(fileName, new MemoryStream(fileContent)),
            Transformation = new Transformation().Height(500).Width(500).Crop("fill").Gravity("face")
        };

        uploadResp = await _cloud.UploadAsync(uploadParams);
        return uploadResp;
    }

    public async Task<DeletionResult> DeletePhotoAsync(string publicId)
    {
        var deleteParams = new DeletionParams(publicId);
        var result = await _cloud.DestroyAsync(deleteParams);
        return result;
    }
}

public class CloudinarySettings
{
    public string CloudName { get; set; } = string.Empty;
    public string ApiKey { get; set; } = string.Empty;
    public string ApiSecret { get; set; } = string.Empty;
}
