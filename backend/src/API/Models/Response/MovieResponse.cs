namespace backend.src.API.Models.Response;

public class MovieResponse
{
    public string Title { get; set; } = string.Empty;
    public bool InTheatresFlag { get; set; }
    public bool UpComingFlag { get; set; }
    public string? ImageUrl { get; set; }
}
