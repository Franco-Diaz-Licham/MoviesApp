namespace backend.src.API.Models.Request;

public class PhotoCreateRequest
{
    [Required] public required IFormFile Image { get; set; }
}
