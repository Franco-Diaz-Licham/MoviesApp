namespace backend.src.API.Models.Response;

public class MovieResponse
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Plot { get; set; } = string.Empty;
    public bool InTheatresFlag { get; set; }
    public bool UpComingFlag { get; set; }
    public PhotoResponse? Photo { get; set; }
    public List<GenreResponse>? Genres { get; set; }
    public List<ActorResponse>? Actors { get; set; }
    public List<TheatreResponse>? Theatres { get; set; }
}
