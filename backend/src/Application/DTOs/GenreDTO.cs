namespace backend.src.Application.DTOs;

public class GenreDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime? UpdatedOn { get; set; }
}
