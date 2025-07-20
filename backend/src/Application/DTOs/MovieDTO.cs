namespace backend.src.Application.DTOs;

public class MovieDTO
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Plot { get; set; } = string.Empty;
    public bool InTheatresFlag { get; set; }
    public bool UpComingFlag { get; set; }
    public PhotoDTO? Photo { get; set; }
    public ICollection<GenreDTO> Genres { get; set; } = new List<GenreDTO>();
    public ICollection<ActorDTO> Actors { get; set; } = new List<ActorDTO>();
    public ICollection<TheatreDTO> Theatres { get; set; } = new List<TheatreDTO>();
}
