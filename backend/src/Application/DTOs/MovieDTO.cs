namespace backend.src.Application.DTOs;

public class MovieDTO
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public bool InTheatresFlag { get; set; }
    public bool UpComingFlag { get; set; }
    public string? ImageUrl { get; set; }
}
