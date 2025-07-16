namespace backend.src.API.Models.Request;

public class GenreUpdateRequest
{
    [Required] public int Id { get; set; }
    [Required] public string Name { get; set; } = string.Empty;
    [Required] public string Description { get; set; } = string.Empty;
}
