namespace backend.src.Domain.Entities;

public class MovieGenreEntity
{
    public int MovieId { get; set; }
    public MovieEntity? Movie { get; set; }
    public int GenreId { get; set; }
    public GenreEntity? Genre { get; set; }
}
