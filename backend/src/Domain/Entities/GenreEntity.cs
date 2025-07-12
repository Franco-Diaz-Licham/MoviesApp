namespace backend.src.Domain.Entities;

public class GenreEntity : BaseEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime CreatedOn { get; set; }
    public DateTime? UpdatedOn { get; set; }
    public ICollection<MovieEntity> Movies { get; set; } = new List<MovieEntity>();
}

