namespace backend.src.API.Models.Request;

public class MovieCreateRequest
{
    [Required] public string Title { get; set; } = default!;
    [Required] public string Plot { get; set; } = default!;
    [Required] public bool InTheatresFlag { get; set; }
    [Required] public bool UpComingFlag { get; set; }
    public PhotoCreateRequest? Photo { get; set; }
    public List<int>? Genres { get; set; }
    public List<int>? Actors { get; set; }
    public List<int>? Theatres { get; set; }
}
