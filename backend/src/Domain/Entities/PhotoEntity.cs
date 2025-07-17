namespace backend.src.Domain.Entities;

public class PhotoEntity : BaseEntity
{
    public PhotoEntity(string publicId, string publicUrl, string fileName)
    {
        PublicId = publicId;
        PublicUrl = publicUrl;
        FileName = fileName;
    }

    public string PublicUrl { get; set; } = default!;
    public string PublicId { get; set; } = default!;
    public string FileName { get; set; } = default!;
}
