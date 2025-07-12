namespace backend.src.API.Models.Request;

public class GenreRequest
{
    [Required] public string Name { get; set; } = string.Empty;
}
