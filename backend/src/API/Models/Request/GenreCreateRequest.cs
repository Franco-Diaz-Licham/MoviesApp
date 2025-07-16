namespace backend.src.API.Models.Request;

public class GenreCreateRequest
{
    [Required] public string Name { get; set; } = string.Empty;
    [Required] public string Description { get; set; } = string.Empty;
}
