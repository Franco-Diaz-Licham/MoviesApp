namespace backend.src.API.Models.Response;

public class MovieDetailsResponse
{
    public string Title { get; set; } = string.Empty;
    public bool InTheatresFlag { get; set; }
    public bool UpComingFlag { get; set; }
    public string? ImageUrl { get; set; }
    public List<GenreEntity>? Genres { get; set; }
    public List<ActorEntity>? Actors { get; set; }
    public List<TheatreEntity>? Theatres { get; set; }
}
