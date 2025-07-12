namespace backend.src.Domain.Entities;

public class MovieTheatreEntity : BaseEntity
{
    public int MovieId { get; set; }
    public MovieEntity? Movie { get; set; }
    public int TheatreId { get; set; }
    public TheatreEntity? Theatre { get; set; }
}
