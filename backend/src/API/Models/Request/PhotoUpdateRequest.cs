namespace backend.src.API.Models.Request;

public class PhotoUpdateRequest
{
    [Required] public int Id { get; set; }
    [Required] public required IFormFile Image { get; set; }
}
