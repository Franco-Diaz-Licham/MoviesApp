namespace backend.src.API.Models.Response;

public class PhotoResponse
{
    public int Id { get; set; }
    public required string PublicUrl { get; set; }
    public required string PublicId { get; set; }
}
