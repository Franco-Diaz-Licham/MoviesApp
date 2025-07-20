namespace backend.src.Application.DTOs;

/// <summary>
/// DTO with flatten lists.
/// </summary>
public class MovieCreateDTO
{
    public string Title { get; set; } = default!;
    public string Plot { get; set; } = default!;
    public bool InTheatresFlag { get; set; }
    public bool UpComingFlag { get; set; }
    public PhotoDTO? Photo { get; set; }
    public List<int>? Genres { get; set; }
    public List<int>? Actors { get; set; }
    public List<int>? Theatres { get; set; }
}
