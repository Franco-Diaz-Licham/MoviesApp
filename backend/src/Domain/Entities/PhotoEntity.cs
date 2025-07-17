namespace backend.src.Domain.Entities;

public class PhotoEntity : BaseEntity
{
    public PhotoEntity() { }
    public PhotoEntity(string publicId, string publicUrl)
    {
        PublicId = publicId;
        PublicUrl = publicUrl;
    }

    public string PublicUrl { get; set; } = default!;
    public string PublicId { get; set; } = default!;
}
