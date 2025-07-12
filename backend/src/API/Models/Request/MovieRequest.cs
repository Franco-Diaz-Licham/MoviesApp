namespace backend.src.API.Models.Request;

public class MovieRequest
{
    [Required] public string Title { get; set; } = string.Empty;
    [Required] public bool InTheatresFlag { get; set; }
    [Required] public bool UpComingFlag { get; set; }
    public string? ImageUrl { get; set; }
    public List<int>? Genres { get; set; }
    public List<int>? Actors { get; set; }
    public List<int>? Theatres { get; set; }
}
