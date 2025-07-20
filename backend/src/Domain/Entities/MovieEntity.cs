namespace backend.src.Domain.Entities;

public class MovieEntity : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string Plot { get; set; } = string.Empty;
    public bool InTheatresFlag { get; set; }
    public bool UpComingFlag { get; set; }
    public int PhotoId { get; set; }
    public PhotoEntity? Photo { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime? UpdatedOn { get; set; }

    // Collections
    public ICollection<GenreEntity> Genres { get; set; } = new List<GenreEntity>();
    public ICollection<ActorEntity> Actors { get; set; } = new List<ActorEntity>();
    public ICollection<TheatreEntity> Theatres { get; set; } = new List<TheatreEntity>();
}
